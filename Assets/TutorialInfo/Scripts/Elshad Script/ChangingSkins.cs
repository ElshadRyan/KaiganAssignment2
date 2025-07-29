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
    private GameManager GM;

    [Header("Female Body Parts To Enabled")]
    public List<BodyToEnabled> F_topBody;
    public List<BodyToEnabled> F_bottomBody;
    public List<BodyToEnabled> F_presetTopBody;
    public List<BodyToEnabled> F_presetBottomBody;
    public List<BodyToEnabled> F_hair;

    [Header("Male Body Parts To Enabled")]
    public List<BodyToEnabled> M_topBody;
    public List<BodyToEnabled> M_bottomBody;
    public List<BodyToEnabled> M_presetTopBody;
    public List<BodyToEnabled> M_presetBottomBody;
    public List<BodyToEnabled> M_hair;

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

    private void Start()
    {
        GM = GameManager.instance;
    }

    // handles changing skins that use prefab gameObject
    //it required original game object(prefab), copy game object(instantiate), head slot (to put the skins), gender, skin category to change
    public virtual void ChangingSkin(ref GameObject O_currObj, ref GameObject C_currOBJ, Transform slot, _CharID ID, _skinCategories skinCategories)
    {
        switch (skinCategories)
        {
            case _skinCategories.head:
                ChangingHair(ref O_currObj, ref C_currOBJ, slot, ID);
                break;
            case _skinCategories.head_accesories:
                ChangingHeadAccesories(ref O_currObj, ref C_currOBJ, slot, ID);
                break;
        }
    }
    // handles changing skins that use skinnedMeshRenderer
    //it required root bone, original skin(prefab), copy skin(instantiate), Gender, skin category to change, list of NPC top body(example: hands, torso), and list of NPC bottom body(example: hips, legs)
    public virtual void ChangingSkin(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, _skinCategories skinCategories, GameObject[] topBody, GameObject[] bottomBody)
    {
        switch(skinCategories)
        {
            case _skinCategories.top:
                ChangingTop(rootBone, ref O_currSkin, ref C_currSkin, ID, false, topBody);
                break;
            case _skinCategories.bottom:
                ChangingBottom(rootBone, ref O_currSkin, ref C_currSkin, ID, false, bottomBody);
                break;
            case _skinCategories.shoes:
                ChangingShoes(rootBone, ref O_currSkin, ref C_currSkin, ID, false);
                break;
            case _skinCategories.top_accesories:
                ChangingTopAccesories(rootBone, ref O_currSkin, ref C_currSkin, ID, false);
                break;
            case _skinCategories.preset:
                ChangingOutfitPreset(rootBone, ref O_currSkin, ref C_currSkin, ID, false, topBody, bottomBody);
                break;
        }
    }

    
    //changing the top clothes
    public virtual void ChangingTop(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, bool random, GameObject[] topBody)
    {
        SkinnedMeshRenderer originalSkinToChange;
        SkinnedMeshRenderer copySkinToInstantiate;
        int indexNum;
        int otherIndexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = Changing(O_currSkin, F_Top_prefabSkinnedMeshRenderers, out indexNum);
                if (!C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer.gameObject.activeSelf)
                {
                    EnablingBodyParts(F_topBody, topBody, indexNum);
                }
                else
                {
                    C_currSkin.transform.parent.gameObject.SetActive(true);
                    SkinnedMeshRenderer tempSkin = C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer;
                    tempSkin.gameObject.SetActive(false);
                    tempSkin.transform.parent.gameObject.SetActive(false);

                    otherIndexNum = CheckWhatIndex(GM.currNPC.O_bottomSkinMeshRenderer, F_Bot_prefabSkinnedMeshRenderers);
                    EnablingBodyParts(F_bottomBody, GM.currNPC.bottomBody, otherIndexNum);
                    EnablingBodyParts(F_topBody, topBody, indexNum);

                }
            }
            else
            {
                originalSkinToChange = Changing(O_currSkin, M_Top_prefabSkinnedMeshRenderers, out indexNum);
                if (C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer.gameObject.activeSelf)
                {
                    EnablingBodyParts(M_topBody, topBody, indexNum);
                }
                else
                {
                    C_currSkin.transform.parent.gameObject.SetActive(true);
                    SkinnedMeshRenderer tempSkin = C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer;
                    tempSkin.gameObject.SetActive(false);
                    tempSkin.transform.parent.gameObject.SetActive(false);

                    otherIndexNum = CheckWhatIndex(GM.currNPC.O_bottomSkinMeshRenderer, M_Bot_prefabSkinnedMeshRenderers);
                    EnablingBodyParts(M_bottomBody, GM.currNPC.bottomBody, otherIndexNum);
                    EnablingBodyParts(M_topBody, topBody, indexNum);

                }
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = ChangingRandom(F_Top_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(F_topBody, topBody, indexNum);
            }
            else
            {
                originalSkinToChange = ChangingRandom(M_Top_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(M_topBody, topBody, indexNum);
            }
        }

        copySkinToInstantiate = Instantiate(originalSkinToChange, C_currSkin.transform.parent);
        copySkinToInstantiate.bones = C_currSkin.bones;
        copySkinToInstantiate.rootBone = rootBone;

        if (C_currSkin != null)
        {
            Destroy(C_currSkin.gameObject);
        }

        O_currSkin = originalSkinToChange;
        C_currSkin = copySkinToInstantiate;

    }

    //changing the bottom clothes
    public virtual void ChangingBottom(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, bool random, GameObject[] bottomBody)
    {
        SkinnedMeshRenderer originalSkinToChange;
        SkinnedMeshRenderer copySkinToInstantiate;
        int indexNum;
        int otherIndexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = Changing(O_currSkin, F_Bot_prefabSkinnedMeshRenderers, out indexNum);
                if (!C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer.gameObject.activeSelf)
                {
                    EnablingBodyParts(F_bottomBody, bottomBody, indexNum);
                }
                else
                {
                    C_currSkin.transform.parent.gameObject.SetActive(true);
                    SkinnedMeshRenderer tempSkin = C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer;
                    tempSkin.gameObject.SetActive(false);
                    tempSkin.transform.parent.gameObject.SetActive(false);

                    otherIndexNum = CheckWhatIndex(GM.currNPC.O_topSkinMeshRenderer, F_Top_prefabSkinnedMeshRenderers);
                    EnablingBodyParts(F_topBody, GM.currNPC.topBody, otherIndexNum);
                    EnablingBodyParts(F_bottomBody, bottomBody, indexNum);
                }
            }
            else
            {
                originalSkinToChange = Changing(O_currSkin, M_Bot_prefabSkinnedMeshRenderers, out indexNum);
                if (!C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer.gameObject.activeSelf)
                {
                    EnablingBodyParts(M_bottomBody, bottomBody, indexNum);
                }
                else
                {
                    C_currSkin.transform.parent.gameObject.SetActive(true);
                    SkinnedMeshRenderer tempSkin = C_currSkin.GetComponentInParent<NPC>().C_presetSkinMeshRenderer;
                    tempSkin.gameObject.SetActive(false);
                    tempSkin.transform.parent.gameObject.SetActive(false);

                    otherIndexNum = CheckWhatIndex(GM.currNPC.O_topSkinMeshRenderer, M_Top_prefabSkinnedMeshRenderers);
                    EnablingBodyParts(M_topBody, GM.currNPC.topBody, otherIndexNum);
                    EnablingBodyParts(M_bottomBody, bottomBody, indexNum);
                }
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = ChangingRandom(F_Bot_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(F_bottomBody, bottomBody, indexNum);

            }
            else
            {
                originalSkinToChange = ChangingRandom(M_Bot_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(M_bottomBody, bottomBody, indexNum);

            }
        }

        copySkinToInstantiate = Instantiate(originalSkinToChange, C_currSkin.transform.parent);
        copySkinToInstantiate.bones = C_currSkin.bones;
        copySkinToInstantiate.rootBone = rootBone;


        if (C_currSkin != null)
        {
            Destroy(C_currSkin.gameObject);
        }

        O_currSkin = originalSkinToChange;
        C_currSkin = copySkinToInstantiate;
    }

    //changing the shoes
    public virtual void ChangingShoes(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer originalSkinToChange;
        SkinnedMeshRenderer copySkinToInstantiate;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = Changing(O_currSkin, F_Shoes_prefabSkinnedMeshRenderers, out indexNum);
            }
            else
            {
                originalSkinToChange = Changing(O_currSkin, M_Shoes_prefabSkinnedMeshRenderers, out indexNum);
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = ChangingRandom(F_Shoes_prefabSkinnedMeshRenderers, out indexNum);
            }
            else
            {
                originalSkinToChange = ChangingRandom(M_Shoes_prefabSkinnedMeshRenderers, out indexNum);
            }
        }

        copySkinToInstantiate = Instantiate(originalSkinToChange, C_currSkin.transform.parent);
        copySkinToInstantiate.bones = C_currSkin.bones;
        copySkinToInstantiate.rootBone = rootBone;

        if (C_currSkin != null)
        {
            Destroy(C_currSkin.gameObject);
        }

        O_currSkin = originalSkinToChange;
        C_currSkin = copySkinToInstantiate;
    }

    //changing the top accesories(example: shawl)
    public virtual void ChangingTopAccesories(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, bool random)
    {
        SkinnedMeshRenderer originalSkinToChange;
        SkinnedMeshRenderer copySkinToInstantiate;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = Changing(O_currSkin, F_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum);
            }
            else
            {
                originalSkinToChange = Changing(O_currSkin, M_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum);
            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = ChangingRandom(F_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum);
            }
            else
            {
                originalSkinToChange = ChangingRandom(M_TopAccesorry_prefabSkinnedMeshRenderers, out indexNum);
            }
        }

        copySkinToInstantiate = Instantiate(originalSkinToChange, C_currSkin.transform.parent);
        copySkinToInstantiate.bones = C_currSkin.bones;
        copySkinToInstantiate.rootBone = rootBone;

        if (C_currSkin != null)
        {
            Destroy(C_currSkin.gameObject);
        }

        O_currSkin = originalSkinToChange;
        C_currSkin = copySkinToInstantiate;
    }

    //changing the head accesories(example: helmet, glasses)
    public virtual void ChangingHeadAccesories(ref GameObject O_currObj, ref GameObject C_currOBJ, Transform slot, _CharID ID)
    {
        GameObject accesories;
        GameObject tempObj;
        GameObject hairSlot = slot.GetComponentInParent<NPC>().headSlot.gameObject;
        int indexNum;
        if (CheckIfHasSkin(slot))
        {
            tempObj = slot.transform.GetChild(0).gameObject;
            if (ID == _CharID.female)
            {
                accesories = Changing(O_currObj, F_HeadAccesorry_prefab, out indexNum);
                O_currObj = accesories;
                C_currOBJ = Instantiate(O_currObj, slot);
            }
            else
            {
                accesories = Changing(O_currObj, M_HeadAccesorry_prefab, out indexNum);
                O_currObj = accesories;
                C_currOBJ = Instantiate(O_currObj, slot);
            }
            Destroy(tempObj);
        }
        else
        {
            if (ID == _CharID.female)
            {
                indexNum = UnityEngine.Random.Range(0, F_HeadAccesorry_prefab.Length - 1);

                accesories = F_HeadAccesorry_prefab[indexNum];
                O_currObj = accesories;
                C_currOBJ = Instantiate(accesories, slot);
            }
            else
            {
                indexNum = UnityEngine.Random.Range(0, F_HeadAccesorry_prefab.Length - 1);
                accesories = M_HeadAccesorry_prefab[indexNum];
                O_currObj = accesories;
                C_currOBJ = Instantiate(accesories, slot);
            }
        }

        if (hairSlot.transform.childCount > 0)
        {
            if(ID == _CharID.female)
            {
                hairSlot.SetActive(F_hair[indexNum].partsToEnabled[0]);
            }
            else
            {
                hairSlot.SetActive(M_hair[indexNum].partsToEnabled[0]);
            }
        }

    }


    //changing the hair
    public virtual void ChangingHair(ref GameObject O_currObj, ref GameObject C_currOBJ, Transform slot, _CharID ID)
    {
        GameObject hair;
        GameObject tempObj;
        int indexNum;
        if (CheckIfHasSkin(slot))
        {
            tempObj = slot.transform.GetChild(0).gameObject;
            if (ID == _CharID.female)
            {
                hair = Changing(O_currObj, F_Head_prefab, out indexNum);
                O_currObj = hair;
                C_currOBJ = Instantiate(O_currObj, slot);
            }
            else
            {
                hair = Changing(O_currObj, M_Head_prefab, out indexNum);
                O_currObj = hair;
                C_currOBJ = Instantiate(O_currObj, slot);
            }
            Destroy(tempObj);
        }
        else
        {
            if (ID == _CharID.female)
            {
                hair = F_Head_prefab[UnityEngine.Random.Range(0, F_Head_prefab.Length-1)];
                O_currObj = hair;
                C_currOBJ = Instantiate(hair, slot);
            }
            else
            {
                hair = M_Head_prefab[UnityEngine.Random.Range(0, M_Head_prefab.Length-1)];
                O_currObj = hair;
                C_currOBJ = Instantiate(hair, slot);
            }
        }
    }

    //changing the preset outfit that has been made
    //if player change the preset, player cannot see all other categories of outfit
    //this is also true for vice versa
    public virtual void ChangingOutfitPreset(Transform rootBone, ref SkinnedMeshRenderer O_currSkin, ref SkinnedMeshRenderer C_currSkin, _CharID ID, bool random, GameObject[] topBody, GameObject[] bottomBody)
    {
        SkinnedMeshRenderer originalSkinToChange;
        SkinnedMeshRenderer copySkinToInstantiate;
        int indexNum;
        if (!random)
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = Changing(O_currSkin, F_Preset_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(F_presetTopBody, topBody, indexNum);
                EnablingBodyParts(F_presetBottomBody, bottomBody, indexNum);

            }
            else
            {
                originalSkinToChange = Changing(O_currSkin, M_Preset_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(M_presetTopBody, topBody, indexNum);
                EnablingBodyParts(M_presetBottomBody, bottomBody, indexNum);

            }
        }
        else
        {
            if (ID == _CharID.female)
            {
                originalSkinToChange = ChangingRandom(F_Preset_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(F_presetTopBody, topBody, indexNum);
                EnablingBodyParts(F_presetBottomBody, bottomBody, indexNum);

            }
            else
            {
                originalSkinToChange = ChangingRandom(M_Preset_prefabSkinnedMeshRenderers, out indexNum);
                EnablingBodyParts(M_presetTopBody, topBody, indexNum);
                EnablingBodyParts(M_presetBottomBody, bottomBody, indexNum);

            }
        }

        copySkinToInstantiate = Instantiate(originalSkinToChange, C_currSkin.transform.parent);
        copySkinToInstantiate.bones = C_currSkin.bones;
        copySkinToInstantiate.rootBone = rootBone;
        copySkinToInstantiate.gameObject.SetActive(true);

        if(copySkinToInstantiate.transform.childCount > 0)
        {
            SkinnedMeshRenderer child = copySkinToInstantiate.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            child.bones = C_currSkin.bones;
            child.rootBone = rootBone;
        }



        C_currSkin.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
        C_currSkin.transform.parent.gameObject.SetActive(true);

        if (C_currSkin != null)
        {
            Destroy(C_currSkin.gameObject);
        }

        O_currSkin = originalSkinToChange;
        C_currSkin = copySkinToInstantiate;
    }

    //cheking if has skin inside the head slot
    public virtual bool CheckIfHasSkin(Transform slot)
    {
        if (slot.childCount > 0)
        {
            return true;
        }

        return false;
    }

    //checking what skin index are we at right now for skinnedMeshRenderers
    //example: top, bottom, shoes
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

    //checking what skin index are we at right now for game objects
    //example: hair, head accesories
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

    //changing the skin that use SkinnedMeshRenderer at random
    public virtual SkinnedMeshRenderer ChangingRandom(SkinnedMeshRenderer[] list, out int num)
    {
        
        int idx;
        idx = UnityEngine.Random.Range(0, list.Length);

        num = idx;

        return list[idx];
    }

    //changing the skin that use SkinnedMeshRenderer by cycling through the index
    public virtual SkinnedMeshRenderer Changing(SkinnedMeshRenderer currSkin, SkinnedMeshRenderer[] list, out int num)
    {
        int idx;
        idx = CheckWhatIndex(currSkin, list);

        if(GM.order == _changeOrder.next)
        {
            idx++;
            idx = idx % list.Length;
        }
        else
        {
            idx--;
            if(idx < 0)
            {
                idx = list.Length - 1;
            }
        }

        num = idx;

        return list[idx];
    }

    //changing the skin that use GameObject by cycling through the index
    public virtual GameObject Changing(GameObject obj, GameObject[] list, out int num)
    {
        int idx;
        idx = CheckWhatIndex(obj, list);

        if (GM.order == _changeOrder.next)
        {
            idx++;
            idx = idx % list.Length;
        }
        else
        {
            idx--;
            if (idx < 0)
            {
                idx = list.Length - 1;
            }
        }

        num = idx;

        return list[idx];
    }

    //enabling body parts based on the skin that the player use (example: enabling torso if needed, legs if needed)
    //if the body parts is not needed, then the code will made the part disabled
    public virtual void EnablingBodyParts(List<BodyToEnabled> list, GameObject[] bodyParts, int idx)
    {

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].gameObject.SetActive(list[idx].partsToEnabled[i]);
        }
    }
}
