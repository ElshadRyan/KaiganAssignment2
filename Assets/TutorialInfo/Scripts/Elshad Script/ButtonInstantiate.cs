using TMPro;
using UnityEngine;

public class ButtonInstantiate : MonoBehaviour
{
    GameManager GM;

    public GameObject parent;
    public int quantityToInstantiate;
    public TMP_InputField inputNum;

    private void Start()
    {
        GM = GameManager.instance;
        inputNum.contentType = TMP_InputField.ContentType.IntegerNumber;
    }

    //this is for spawning NPC based on the number that you input
    public void NPCInstantiating()
    {
        int.TryParse(inputNum.text, out quantityToInstantiate);
        if( quantityToInstantiate > 0 )
        {
            for (int i = 0; i < quantityToInstantiate; i++)
            {
                Vector3 randomingPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
                GameObject randomingGender = GM.NPCGameObject[Random.Range(0, GM.NPCGameObject.Count - 1)];
                Instantiate(randomingGender, randomingPosition, Quaternion.identity, parent.transform);
            }
        }
    }
}
