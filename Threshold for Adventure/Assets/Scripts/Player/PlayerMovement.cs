using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    bool isInteractive = false;
    bool isCombat = false;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isInteractive && agent.remainingDistance < 1.1f)
        {
            isInteractive = false;
            agent.SetDestination(transform.position);
            GameEvents.current.CurrentAction();
        }
        if (isCombat)
        {
            if (PlayerCombat.current.IsInRange(agent.remainingDistance))
            {
                agent.SetDestination(transform.position);
                PlayerCombat.current.ChooseAttackAnimation();
                PlayerCombat.current.StartCombat();

                isCombat = false;
                anim.SetTrigger("Combat");
                //Need to get UI clicks to not register. Allow button presses during combat.
            }
        }
        //if (InteractionScript.clicked) { agent.destination = transform.position; return; }
        //Navigation for arrow keys
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horInput, 0f, verInput);
            Vector3 moveDestination = transform.position + movement;
            agent.SetDestination(moveDestination);
        }
        //Navigation for mouse click
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    FindEvent(hit.transform);
                    if (hit.transform.tag == "Interactive") { isInteractive = true; }
                    if (hit.transform.tag == "Combat") { isCombat = true; } else { isCombat = false; PlayerCombat.current.EndCombat(); }
                    agent.destination = hit.point;
                }
            }
        }
        if (Vector3.Dot(transform.forward, agent.velocity) > 0.5f) { anim.SetBool("moving", true); }else { anim.SetBool("moving", false); }
        anim.SetFloat("turn", Vector3.Dot(transform.right, agent.velocity));
    }

    void FindEvent(Transform transform) { if (transform.GetComponent<NPCDialogue>()) { transform.GetComponent<NPCDialogue>().SetEvent(); } }

    void StorePosition()
    {
        SaveData.current.profile.lastPos = transform.position;
    }
    private void Start()
    {
        GameEvents.current.onSave += StorePosition;
    }
    private void OnDestroy()
    {
        GameEvents.current.onSave -= StorePosition;
    }
}
