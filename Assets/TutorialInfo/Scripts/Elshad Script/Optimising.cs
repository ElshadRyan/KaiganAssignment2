using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class Optimising : MonoBehaviour
{

    public List<SkinnedMeshRenderer> partsToCombine;
    public Transform rootBone;

    //seting up the list to be used for combining
    public void Setup(List<SkinnedMeshRenderer> list)
    {
        partsToCombine.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            partsToCombine.Add(list[i]);
        }
    }

    //combining the mesh inside the list
    public GameObject Combine()
    {
        //required refrence to build the mesh
        //because the mesh is animated and has a lot of materials
        List<Material> materials = new List<Material>();
        List<Transform> bones = new List<Transform>();
        List<BoneWeight> boneWeights = new List<BoneWeight>();
        List<Matrix4x4> bindposes = new List<Matrix4x4>();

        int vertexOffset = 0;
        int boneOffset = 0;

        Mesh combinedMesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector4> tangents = new List<Vector4>();
        List<Vector2> uvs = new List<Vector2>();
        List<int[]> submeshTriangles = new List<int[]>();

        //looping through all of the skin mesh renderer inside the list
        foreach (var smr in partsToCombine)
        {
            Mesh mesh = smr.sharedMesh;
            if (mesh == null) continue;

            int subMeshCount = mesh.subMeshCount;

            //assigning the required variable inside the current skin mesh renderer list
            Vector3[] meshVerts = mesh.vertices;
            Vector3[] meshNormals = mesh.normals;
            Vector4[] meshTangents = mesh.tangents;
            Vector2[] meshUVs = mesh.uv;
            BoneWeight[] meshBoneWeights = mesh.boneWeights;

            for (int i = 0; i < meshVerts.Length; i++)
            {
                //adding the assign variable into the list that we created before
                vertices.Add(meshVerts[i]);
                normals.Add(meshNormals[i]);
                tangents.Add(meshTangents[i]);
                uvs.Add(meshUVs[i]);

                //assigning the bone weight and its offsets
                BoneWeight bw = meshBoneWeights[i];
                bw.boneIndex0 += boneOffset;
                bw.boneIndex1 += boneOffset;
                bw.boneIndex2 += boneOffset;
                bw.boneIndex3 += boneOffset;
                boneWeights.Add(bw);
            }

            for (int s = 0; s < subMeshCount; s++)
            {
                //assigning the submesh with its material into the list so the material doesnt get mixed up later
                int[] triangles = mesh.GetTriangles(s);
                for (int t = 0; t < triangles.Length; t++)
                {
                    triangles[t] += vertexOffset;
                }
                submeshTriangles.Add(triangles);
                materials.Add(smr.sharedMaterials[s]);
            }

            //assigning the bones
            bones.AddRange(smr.bones);
            bindposes.AddRange(mesh.bindposes);

            vertexOffset = vertices.Count;
            boneOffset += smr.bones.Length;
        }

        //combine all mesh and its property into 1 mesh
        combinedMesh.SetVertices(vertices);
        combinedMesh.SetNormals(normals);
        combinedMesh.SetTangents(tangents);
        combinedMesh.SetUVs(0, uvs);
        combinedMesh.boneWeights = boneWeights.ToArray();
        combinedMesh.bindposes = bindposes.ToArray();
        combinedMesh.subMeshCount = submeshTriangles.Count;
        for (int i = 0; i < submeshTriangles.Count; i++)
        {
            combinedMesh.SetTriangles(submeshTriangles[i], i);
        }

        //putting the combined mesh inside a game object to be used
        GameObject result = new GameObject("CombinedSkinnedMesh");
        SkinnedMeshRenderer smrFinal = result.AddComponent<SkinnedMeshRenderer>();
        smrFinal.sharedMesh = combinedMesh;
        smrFinal.bones = bones.ToArray();
        smrFinal.materials = materials.ToArray();
        smrFinal.rootBone = rootBone;

        return result;
    }

}
