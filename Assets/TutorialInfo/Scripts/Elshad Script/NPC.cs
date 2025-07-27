using System;
using System.Collections.Generic;
using UnityEngine;



public class NPC : MonoBehaviour
{
    private ChangingSkins CS;

    public GameObject[] topBody;
    public GameObject[] bottomBody;

    public int idx;
    public Transform rootBone;
    public Transform headSlot;

    public SkinnedMeshRenderer topSkinMeshRenderer;
    public SkinnedMeshRenderer bottomSkinMeshRenderer;
    public SkinnedMeshRenderer shoesSkinMeshRenderer;

    public _CharID ID;

    private void Start()
    {
        CS = ChangingSkins.instance;
        StartSetup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssigningRefrence();

            CS.ChangingTop(rootBone, ref topSkinMeshRenderer, ID, false, topBody);

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            AssigningRefrence();

            CS.ChangingBottom(rootBone, ref bottomSkinMeshRenderer, ID, false, bottomBody);

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AssigningRefrence();

            CS.ChangingShoes(rootBone, ref shoesSkinMeshRenderer, ID, false);

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    public void AssigningRefrence()
    {
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            GameObject currGameObject = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
            if(!currGameObject.activeSelf)
            {
                Destroy(currGameObject);
            }
        }
    }    

    public void StartSetup()
    {
        rootBone = transform.GetChild(0).GetChild(2).GetChild(0);
        headSlot = transform.GetChild(0).GetChild(1).GetChild(12);

        AssigningRefrence();


        CS.ChangingHair(headSlot, ID);
        CS.ChangingTop(rootBone, ref topSkinMeshRenderer, ID, true, topBody);
        CS.ChangingBottom(rootBone, ref bottomSkinMeshRenderer, ID, true, bottomBody);
        CS.ChangingShoes(rootBone, ref shoesSkinMeshRenderer, ID, true);

    }

    public void ChangingSelectedParts()
    {

    }

}
