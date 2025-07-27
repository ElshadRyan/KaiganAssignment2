using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BodyToEnabled
{
    public List<bool> partsToEnabled = new List<bool>();
}
public class ChangingSkins : MonoBehaviour
{
    public static ChangingSkins instance;

    [Header("Female Body Parts To Enabled")]
    public List<BodyToEnabled> F_topBody;
    public List<BodyToEnabled> F_bottomBody;

    [Header("Male Body Parts To Enabled")]
    public List<BodyToEnabled> M_topBody;
    public List<BodyToEnabled> M_bottomBody;

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
    public virtual void ChangingSkin(Transform rootBone, ref SkinnedMeshRenderer currSkinnedMeshRenderers, _CharID ID, _skinCategories skinCategories, GameObject[] topBody, GameObject[] bottomBody)
    {
        switch(skinCategories)
        {
            
            case _skinCategories.top:
                ChangingTop(rootBone, ref currSkinnedMeshRenderers, ID, false, topBody);
                break;
            case _skinCategories.bottom:
                ChangingBottom(rootBone, ref currSkinnedMeshRenderers, ID, false, bottomBody);
                break;
            case _skinCategories.shoes:
                ChangingShoes(rootBone, ref currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.top_accesories:
                ChangingTopAccesories(rootBone, ref currSkinnedMeshRenderers, ID, false);
                break;
            case _skinCategories.preset:
                ChangingOutfitPreset();
                break;

        }
    }

    
    
    public virtual void ChangingTop(Transform rootBone, ref SkinnedMeshRenderer currSkin, _CharID ID, bool random, GameObject[] topBody)
    {
        SkinnedMeshRenderer tempSkin;
        SkinnedMeshRenderer original;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                
                tempSkin = Instantiate(Changing(currSkin, F_Top_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;
                original = F_Top_prefabSkinnedMeshRenderers[indexNum];

                Destroy(currSkin.gameObject);
                currSkin = original;
                //EnablingBodyParts(topBody, CheckWhatIndex(currSkin, F_Top_prefabSkinnedMeshRenderers));

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Top_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;
                original = F_Top_prefabSkinnedMeshRenderers[indexNum];

                Destroy(currSkin.gameObject);
                currSkin = original;
                //EnablingBodyParts(topBody, CheckWhatIndex(currSkin, M_Top_prefabSkinnedMeshRenderers));


            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Top_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;
                //EnablingBodyParts(topBody, CheckWhatIndex(currSkin, F_Top_prefabSkinnedMeshRenderers));

                Destroy(currSkin.gameObject);
                currSkin = M_Top_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, M_Top_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;
                //EnablingBodyParts(topBody, CheckWhatIndex(currSkin, M_Top_prefabSkinnedMeshRenderers));

                Destroy(currSkin.gameObject);
                currSkin = M_Top_prefabSkinnedMeshRenderers[indexNum];

            }

        }


    }

    public virtual void ChangingBottom(Transform rootBone, ref SkinnedMeshRenderer currSkin, _CharID ID, bool random, GameObject[] bottomBody)
    {
        SkinnedMeshRenderer tempSkin;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_Bot_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = F_Bot_prefabSkinnedMeshRenderers[indexNum];
                //EnablingBodyParts(bottomBody, CheckWhatIndex(currSkin, F_Bot_prefabSkinnedMeshRenderers));


            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Bot_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_Bot_prefabSkinnedMeshRenderers[indexNum];
                //EnablingBodyParts(bottomBody, CheckWhatIndex(currSkin, M_Bot_prefabSkinnedMeshRenderers));

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Bot_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_Bot_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, M_Bot_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_Bot_prefabSkinnedMeshRenderers[indexNum];

            }

        }
    }

    public virtual void ChangingShoes(Transform rootBone, ref SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_Shoes_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = F_Shoes_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_Shoes_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_Shoes_prefabSkinnedMeshRenderers[indexNum];

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_Shoes_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = F_Shoes_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, M_Shoes_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_Shoes_prefabSkinnedMeshRenderers[indexNum];

            }

        }
    }

    public virtual void ChangingTopAccesories(Transform rootBone, ref SkinnedMeshRenderer currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer tempSkin;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(Changing(currSkin, F_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = F_TopAccesorry_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(Changing(currSkin, M_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_TopAccesorry_prefabSkinnedMeshRenderers[indexNum];

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, F_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = F_TopAccesorry_prefabSkinnedMeshRenderers[indexNum];

            }
            else
            {
                tempSkin = Instantiate(ChangingRandom(currSkin, M_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum), currSkin.transform.parent);
                tempSkin.bones = currSkin.bones;
                tempSkin.rootBone = rootBone;

                Destroy(currSkin.gameObject);
                currSkin = M_TopAccesorry_prefabSkinnedMeshRenderers[indexNum];

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
                hair = F_Head_prefab[UnityEngine.Random.Range(0, F_Head_prefab.Length-1)];
                Instantiate(hair, slot);
            }
            else
            {
                hair = M_Head_prefab[UnityEngine.Random.Range(0, M_Head_prefab.Length-1)];
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

    public virtual SkinnedMeshRenderer ChangingRandom(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list, out int num)
    {
        
        int idx;
        idx = UnityEngine.Random.Range(0, list.Length);

        num = idx;

        return list[idx];
    }

    public virtual SkinnedMeshRenderer Changing(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list, out int num)
    {
        int idx;
        idx = CheckWhatIndex(currSkin, list);

        idx++;
        idx = idx % list.Length;

        num = idx;

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

    public virtual void EnablingBodyParts(GameObject[] bodyParts, int idx)
    {

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].gameObject.SetActive(F_topBody[idx].partsToEnabled[i]);
        }
    }
}
