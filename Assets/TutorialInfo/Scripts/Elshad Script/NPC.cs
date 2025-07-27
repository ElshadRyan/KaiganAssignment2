using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private ChangingSkins CS;

    public int idx;
    public Transform rootBone;
    public Transform headSlot;
    public List<SkinnedMeshRenderer> allSkinData;
    public _CharID ID;

    private void Start()
    {
        CS = ChangingSkins.instance;
        StartSetup();
    }
    
    public void StartSetup()
    {
        SkinnedMeshRenderer tempSkin;
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            GameObject currGameObject = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
            if(currGameObject.activeSelf)
            {
                tempSkin = currGameObject.GetComponent<SkinnedMeshRenderer>();
                tempSkin.bones = currGameObject.GetComponent<SkinnedMeshRenderer>().bones;
                tempSkin.rootBone = rootBone;
                allSkinData.Add(tempSkin);
                tempSkin.enabled = false;

                continue;
            }

            Destroy(currGameObject);
        }
        
        CS.ChangingHair(headSlot, ID);
        CS.ChangingTop(rootBone, allSkinData[0], ID, true);
        CS.ChangingBottom(rootBone, allSkinData[2], ID, true);
        CS.ChangingShoes(rootBone, allSkinData[3], ID, true);

    }

}
