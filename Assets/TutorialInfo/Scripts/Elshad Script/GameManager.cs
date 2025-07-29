using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("All data")]
    //setup for all the data required by a lot of code
    public List<GameObject> NPCGameObject;
    public _skinCategories skinCategories;
    public NPC currNPC;
    public _changeOrder order;
    public bool isPaused = false;

    private void Awake()
    {
        instance = this;
    }


}
