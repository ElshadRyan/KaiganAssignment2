using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonToChangeSkins : MonoBehaviour
{
    GameManager GM;
    Player player;
    Optimising OP;
    public GameObject nextPrevGameObj;

    private void Start()
    {
        GM = GameManager.instance;
        player = Player.playerInstance;
        OP = GM.currNPC.gameObject.GetComponent<Optimising>();
    }

    //This function send info about what outfit categories that you choose so you can choose the outfit based on the chosen category
    //This will open the outfit cycle UI where you can choose the outfit by cycling through all option;
    public void ChangingSkinCategories(int skinCategories)
    {
        nextPrevGameObj.SetActive(true);
        GM.skinCategories = (_skinCategories)skinCategories;
    }

    //This region is for cycling through all of the option for the outfit
    //Assign this function inside the button
    #region outfit Cycle
    public void NextButton()
    {
        GM.order = _changeOrder.next;
        GM.currNPC.ChangingSelectedParts();
    }
    public void PrevButton()
    {
        GM.order = _changeOrder.prev;
        GM.currNPC.ChangingSelectedParts();
    }
    //this function only purpose is for turning off the cycling option when you finished choosing the outfit
    public void DoneButton(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion


    //this function is called when you finished customizing your NPC's outfit
    //the game will continue/resumed, and will merge all of the mesh that is active on that NPC
    public void Resume()
    {
        GM.isPaused = false;
        player.LerpBackToOriginal();

        //combining the body's and outfit's meshes into 1 list of mesh
        #region combining
        for (int i = 0; i < GM.currNPC.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            if (GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                for (int j = 0; j < GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).childCount; j++)
                {
                    if (GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).gameObject.activeSelf)
                    {
                        GM.currNPC.renderers.Add(GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetComponent<SkinnedMeshRenderer>());
                    }

                    if (GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).childCount > 0)
                    {
                        for (int k = 0; k < GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).childCount; k++)
                        {
                            GM.currNPC.renderers.Add(GM.currNPC.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetChild(k).GetComponent<SkinnedMeshRenderer>());
                        }
                    }
                }
            }
        }

        for (int i = 0; i < GM.currNPC.transform.GetChild(0).GetChild(1).childCount; i++)
        {
            if (GM.currNPC.transform.GetChild(0).GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                GM.currNPC.renderers.Add(GM.currNPC.transform.GetChild(0).GetChild(1).GetChild(i).GetComponent<SkinnedMeshRenderer>());
            }
        }
        #endregion

        //combining the previous list of mesh into 1
        OP.Setup(GM.currNPC.renderers);
        GM.currNPC.O_combinedGameObject = OP.Combine();
        GM.currNPC.C_combinedGameObject = Instantiate(GM.currNPC.O_combinedGameObject, GM.currNPC.transform);
        GM.currNPC.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GM.currNPC.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        Destroy(GM.currNPC.O_combinedGameObject);

    }

}
