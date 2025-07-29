using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager GM;

    public static Player playerInstance;

    [Header("Lerp Settings")]
    public float lerpDuration = 1f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isLerping = false;

    [Header("Player Settup")]
    public GameObject changeSkinGameObj;
    public LayerMask layerMask;
    public int moveSpeed;
    public Transform currTransform;

    private void Awake()
    {
        playerInstance = this;
    }

    private void Start()
    {
        GM = GameManager.instance;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    void Update()
    {
        //this is for choosing what NPC that you want to change the outfit of
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            { 

                changeSkinGameObj.SetActive(true);
                GameObject clickedObject = hit.collider.gameObject;
                GM.currNPC = clickedObject.GetComponent<NPC>();
                LerpToTarget(GM.currNPC.transform.GetChild(1));
                GM.isPaused = true;

                GM.currNPC.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GM.currNPC.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                GM.currNPC.renderers.Clear();
                Destroy(GM.currNPC.C_combinedGameObject);

            }
        }

        if(!GM.isPaused)
        {
            Moving();
        }
    }

    //handle basic movement for the player
    public void Moving()
    {
        Vector2 vectorDir = new Vector2();
        Vector3 dir = new Vector3();
        if(Input.GetKey(KeyCode.W))
        {
            vectorDir += new Vector2(0, 1);
        }
        if(Input.GetKey(KeyCode.A))
        {
            vectorDir += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vectorDir += new Vector2(0, -1);
        }
        if ( Input.GetKey(KeyCode.D))
        {
            vectorDir += new Vector2(1, 0);
        }

        dir = new Vector3(vectorDir.x, 0, vectorDir.y).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

    }

    //lerping the camera so it will be easier for player to see when they choosing the outfit
    public void LerpToTarget(Transform target)
    {
        if (!isLerping)
            StartCoroutine(LerpCamera(target.position, target.rotation));
    }

    //lerping the camera to the original position
    public void LerpBackToOriginal()
    {
        if (!isLerping)
            StartCoroutine(LerpCamera(originalPosition, originalRotation));
    }

    //function to lerp the camera
    private IEnumerator LerpCamera(Vector3 targetPos, Quaternion targetRot)
    {
        isLerping = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float time = 0f;

        while (time < lerpDuration)
        {
            float t = time / lerpDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;

        isLerping = false;
    }
}
