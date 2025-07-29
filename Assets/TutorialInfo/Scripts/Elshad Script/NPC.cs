using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;



public class NPC : MonoBehaviour
{
    private ChangingSkins CS;
    private GameManager GM;
    private Optimising OP;

    public GameObject O_combinedGameObject; 
    public GameObject C_combinedGameObject; 
    public List<SkinnedMeshRenderer> renderers;

    [Header("Character Body Parts")]
    public GameObject[] topBody;
    public GameObject[] bottomBody;

    [Header("Character Settup")]
    public int idx;
    public Transform rootBone;
    public Transform headSlot;
    public Transform accesoriesHeadSlot;
    public _CharID ID;
    public NavMeshAgent character;


    [Header("Original Parts Data")]
    public SkinnedMeshRenderer O_topSkinMeshRenderer;
    public SkinnedMeshRenderer O_bottomSkinMeshRenderer;
    public SkinnedMeshRenderer O_shoesSkinMeshRenderer;
    public SkinnedMeshRenderer O_topAccesoriesSkinMeshRenderer;
    public SkinnedMeshRenderer O_presetSkinMeshRenderer;
    public GameObject O_hair;
    public GameObject O_headAccesories;

    [Header("Copy Parts Data")]
    public SkinnedMeshRenderer C_topSkinMeshRenderer;
    public SkinnedMeshRenderer C_bottomSkinMeshRenderer;
    public SkinnedMeshRenderer C_shoesSkinMeshRenderer;
    public SkinnedMeshRenderer C_topAccesoriesSkinMeshRenderer;
    public SkinnedMeshRenderer C_presetSkinMeshRenderer;
    public GameObject C_hair;
    public GameObject C_headAccesories;


    private void Start()
    {
        CS = ChangingSkins.instance;
        GM = GameManager.instance;
        OP = GetComponent<Optimising>();
        StartSetup();
    }

    //destroying all outfit that is disabled
    public void AssigningRefrence()
    {
        for (int i = 1; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            GameObject currGameObject = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).gameObject;
            if(!currGameObject.activeSelf)
            {
                Destroy(currGameObject);
            }
        }
    }    

    //starting setup when NPC is instantiated
    public void StartSetup()
    {
        AssigningRefrence();

        //randomizing the outfit
        CS.ChangingHair(ref O_hair, ref C_hair, headSlot, ID);
        CS.ChangingTop(rootBone, ref O_topSkinMeshRenderer, ref C_topSkinMeshRenderer, ID, true, topBody);
        CS.ChangingBottom(rootBone, ref O_bottomSkinMeshRenderer, ref C_bottomSkinMeshRenderer, ID, true, bottomBody);
        CS.ChangingShoes(rootBone, ref O_shoesSkinMeshRenderer, ref C_shoesSkinMeshRenderer, ID, true);
        CS.ChangingTopAccesories(rootBone, ref O_topAccesoriesSkinMeshRenderer, ref C_topAccesoriesSkinMeshRenderer, ID, true);
        CS.ChangingHeadAccesories(ref O_headAccesories, ref C_headAccesories, accesoriesHeadSlot, ID);

        //mesh combine the outfit into 1 list
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                for(int j = 0; j < transform.GetChild(0).GetChild(0).GetChild(i).childCount; j++)
                {
                    if(transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).gameObject.activeSelf)
                    {
                        renderers.Add(transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetComponent<SkinnedMeshRenderer>());
                    }

                    if(transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).childCount > 0)
                    {
                        for(int k = 0; k < transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).childCount; k++)
                        {
                            renderers.Add(transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetChild(k).GetComponent<SkinnedMeshRenderer>());
                        }
                    }
                }
            }
        }
        //mesh combine the body parts into previous list
        for (int i = 0; i < transform.GetChild(0).GetChild(1).childCount; i++)
        {
            if(transform.GetChild(0).GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                renderers.Add(transform.GetChild(0).GetChild(1).GetChild(i).GetComponent<SkinnedMeshRenderer>());
            }
        }

        //combining the mesh
        OP.Setup(renderers);
        O_combinedGameObject = OP.Combine();
        C_combinedGameObject = Instantiate(O_combinedGameObject, gameObject.transform);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        Destroy(O_combinedGameObject);

    }

    //changing the skins based on the category that you choose
    public void ChangingSelectedParts()
    {
        if(GM.skinCategories == _skinCategories.head || GM.skinCategories == _skinCategories.head_accesories)
        {
            switch(GM.skinCategories)
            {
                case _skinCategories.head:
                    CS.ChangingSkin(ref O_hair, ref C_hair, headSlot, ID, GM.skinCategories);
                    break;
                case _skinCategories.head_accesories:
                    CS.ChangingSkin(ref O_headAccesories, ref C_headAccesories, accesoriesHeadSlot, ID, GM.skinCategories);
                    break;
            }
        }
        else
        {
            switch (GM.skinCategories)
            {
                case _skinCategories.top:
                    CS.ChangingSkin(rootBone, ref O_topSkinMeshRenderer, ref C_topSkinMeshRenderer, ID, GM.skinCategories, topBody, bottomBody);
                    break;
                case _skinCategories.bottom:
                    CS.ChangingSkin(rootBone, ref O_bottomSkinMeshRenderer, ref C_bottomSkinMeshRenderer, ID, GM.skinCategories, topBody, bottomBody);
                    break;
                case _skinCategories.shoes:
                    CS.ChangingSkin(rootBone, ref O_shoesSkinMeshRenderer, ref C_shoesSkinMeshRenderer, ID, GM.skinCategories, topBody, bottomBody);
                    break;
                case _skinCategories.top_accesories:
                    CS.ChangingSkin(rootBone, ref O_topAccesoriesSkinMeshRenderer, ref C_topAccesoriesSkinMeshRenderer, ID, GM.skinCategories, topBody, bottomBody);
                    break;
                case _skinCategories.preset:
                    CS.ChangingSkin(rootBone, ref O_presetSkinMeshRenderer, ref C_presetSkinMeshRenderer, ID, GM.skinCategories, topBody, bottomBody);
                    break;
            }
        }


    }

    //simple AI move
    public void RandomMove()
    {
        Vector3 destination = new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
        character.destination = transform.position + destination;
    }

}
