using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimation : MonoBehaviour
{
    GameManager GM;
    public Animator charAnimator;
    public NPC npc;

    private bool isWalking = true;
    private bool onlyOnce = false;
    private bool isWaiting = false;

    private NavMeshAgent agent;

    private void Start()
    {
        GM = GameManager.instance;
        charAnimator = GetComponentInChildren<Animator>();
        npc = GetComponent<NPC>();
        agent = npc.character;

        npc.RandomMove();
        Moving();
    }

    //making the NPC move with an interval
    private void Update()
    {
        if(!GM.isPaused)
        {
            if (isWalking)
            {
                if (!onlyOnce)
                {
                    npc.RandomMove();
                    onlyOnce = true;
                }

                Moving();

                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude < 0.01f && !isWaiting)
                {
                    Idle();
                    StartCoroutine(IdleWait(3));
                }
            }
        }
    }

    //making the moving animation run
    private void Moving()
    {
        charAnimator.SetBool("walk", true);
    }

    //making the idle animation run
    private void Idle()
    {
        charAnimator.SetBool("walk", false);
    }

    //the interval for the AI
    private IEnumerator IdleWait(int delay)
    {
        isWaiting = true;
        isWalking = false;
        onlyOnce = true;

        yield return new WaitForSeconds(delay);

        isWalking = true;
        onlyOnce = false;
        isWaiting = false;
    }
}
