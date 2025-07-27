using System.Linq;
using UnityEngine;

public class ChangingSkins : MonoBehaviour
{
    public static ChangingSkins instance;

    [Header("Female Skins")] 
    public SkinnedMeshRenderer[] F_Top_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Bot_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Shoes_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_TopAccesorry_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] F_Preset_prefabSkinnedMeshRenderers;
    public GameObject[] F_HeadAccesorry_prefab;
    public GameObject[] F_Head_prefab;

    [Header("Male Skins")]
    public SkinnedMeshRenderer[] M_Top_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Bot_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Shoes_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_TopAccesorry_prefabSkinnedMeshRenderers;
    public SkinnedMeshRenderer[] M_Preset_prefabSkinnedMeshRenderers;
    public GameObject[] M_HeadAccesorry_prefab;
    public GameObject[] M_Head_prefab;

    private void Awake()
    {
        instance = this;
    }

    public virtual void ChangingSkin(Transform slot, _CharID ID, _skinCategories skinCategories)
    {
        switch (skinCategories)
        {
            case _skinCategories.head:
                ChangingHair(slot, ID);
                break;
            case _skinCategories.head_accesories:
                ChangingHeadAccesories(slot, ID);
                break;
        }
    }
    public virtual void ChangingSkin(Transform rootBone, SkinnedMeshRenderer currSkinnedMeshRenderers, _CharID ID, _skinCategories skinCategories)
    {
        switch(skinCategories)
        {
            
            case _skinCategories.top:
                ChangingTop(rootBone, currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.bottom:
                ChangingBottom(rootBone, currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.shoes:
                ChangingShoes(rootBone, currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.top_accesories:
                ChangingTopAccesories(rootBone, currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.preset:
                ChangingOutfitPreset();
                break;

        }
    }

    public virtual void ChangingTop(Transform rootBone, SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_Top_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Top_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Top_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Top_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }

        }


    }

    public virtual void ChangingBottom(Transform rootBone, SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_Bot_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Bot_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Bot_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Bot_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
            }

        }
    }

    public virtual void ChangingShoes(Transform rootBone, SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_Shoes_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Shoes_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Shoes_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Shoes_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }

        }
    }

    public virtual void ChangingTopAccesories(Transform rootBone, SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_TopAccesorry_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_TopAccesorry_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_TopAccesorry_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_TopAccesorry_prefabSkinnedMeshRenderers), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);

            }

        }
    }

    public virtual void ChangingHeadAccesories(Transform slot, _CharID ID)
    {
        GameObject accesories;
        if (CheckIfHasSkin(slot))
        {
            accesories = slot.GetChild(0).gameObject;
            if (ID == _CharID.female)
            {
                Instantiate(Changing(accesories, F_HeadAccesorry_prefab), slot);
            }
            else
            {
                Instantiate(Changing(accesories, M_HeadAccesorry_prefab), slot);
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                accesories = F_HeadAccesorry_prefab[0];
                Instantiate(accesories, slot);
            }
            else
            {
                accesories = M_HeadAccesorry_prefab[0];
                Instantiate(accesories, slot);
            }
        }
    }

    

    public virtual void ChangingHair(Transform slot, _CharID ID)
    {
        GameObject hair;
        if(CheckIfHasSkin(slot))
        {
            hair = slot.GetChild(0).gameObject;
            if(ID == _CharID.female)
            {
                Instantiate(Changing(hair, F_HeadAccesorry_prefab), slot);
            }
            else
            {
                Instantiate(Changing(hair, M_HeadAccesorry_prefab), slot);
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                hair = F_Head_prefab[Random.Range(0, F_Head_prefab.Length-1)];
                Instantiate(hair, slot);
            }
            else
            {
                hair = M_Head_prefab[Random.Range(0, M_Head_prefab.Length-1)];
                Instantiate(hair, slot);
            }
        }
    }

    public virtual void ChangingOutfitPreset()
    {

    }

    public virtual bool CheckIfHasSkin(Transform slot)
    {
        if (slot.childCount > 0)
        {
            return true;
        }

        return false;
    }

    public virtual int CheckWhatIndex(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list)
    {

        for (int i = 0; i < list.Length; i++)
        {
            if (currSkin == list[i])
            {
                return i;
            }
        }
        return -1;
    }

    public virtual int CheckWhatIndex(GameObject currSkin, GameObject[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (currSkin == list[i])
            {
                return i;
            }
        }
        return -1;
    }

    public virtual SkinnedMeshRenderer ChangingRandom(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list)
    {
        int idx;
        idx = Random.Range(0, list.Length);

        return list[idx];
    }

    public virtual SkinnedMeshRenderer Changing(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list)
    {
        int idx;
        idx = CheckWhatIndex(currSkin, list);

        idx++;
        idx = idx % list.Length;

        return list[idx];
    }

    public virtual GameObject Changing(GameObject obj, GameObject[] list)
    {
        int idx;
        idx = CheckWhatIndex(obj, list);
        Destroy(obj);

        idx++;
        idx = idx % list.Length;

        return list[idx];
    }
}
