using UnityEngine;

public class M_NPC_Parent : MonoBehaviour
{
    [Header("Female Skins")] 
    public SkinnedMeshRenderer[] F_Top_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Bot_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Shoes_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_TopAccesorry_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Preset_prefabSkinnedMeshRenderers;
    public GameObject[] F_HeadAccesorry_prefabSkinnedMeshRenderers;
    public GameObject[] F_Head_prefabSkinnedMeshRenderers;

    [Header("Male Skins")]
    public SkinnedMeshRenderer[] M_Top_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Bot_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Shoes_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_TopAccesorry_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Preset_prefabSkinnedMeshRenderers;
    public GameObject[] M_HeadAccesorry_prefabSkinnedMeshRenderers;
    public GameObject[] M_Head_prefabSkinnedMeshRenderers;


    public virtual void ChangingSkin(Transform slot, _CharID ID)
    {

    }
    public virtual void ChangingSkin(Transform rootBone, SkinnedMeshRenderer currSkinnedMeshRenderers, _CharID ID)
    {

    }

    public virtual void ChangingTop()
    {

    }

    public virtual void ChangingBottom()
    {

    }

    public virtual void ChangingShoes()
    {

    }

    public virtual void ChangingHeadAccesories()
    {

    }

    public virtual void ChangingTopAccesories()
    {

    }

    public virtual void ChangingHead()
    {

    }

    public virtual void ChangingOutfitPreset()
    {

    }
}
