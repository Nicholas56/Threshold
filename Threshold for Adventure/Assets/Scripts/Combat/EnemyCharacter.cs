using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyCharacter : MonoBehaviour, IPointerClickHandler
{
    public string enemyName;
    public bool isTraining;
    Animator anim;
    NavMeshAgent agent;
    Enemy enemy;
    int health;

    public void OnPointerClick(PointerEventData eventData)
    {
        EnemyManager.current.SetCurrentEnemy(this);
    }

    private void Awake()
    {
        enemy = EnemyManager.current.GetEnemy(enemyName);
        health = enemy.hitPoints;
        if (!isTraining)
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = enemy.movementSpeed;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (!isTraining)
        {
            health += amount;
            if (health < 1) { StartCoroutine("Death"); }
        }
    }
    public void BeginCombat() { if (isTraining) { return; } StartCoroutine("Combat"); }

    IEnumerator Combat()
    {
        yield return new WaitForSeconds(enemy.attackSpeed);
        if (Vector3.Distance(transform.position, PlayerCombat.current.transform.position) > 1)
        {
            //move to player
            agent.SetDestination(PlayerCombat.current.transform.position);
        }
        else
        {
            //Enemy attacks
            EnemyManager.current.DealDamageToPlayer(enemy);
        }

        if (Vector3.Distance(transform.position, PlayerCombat.current.transform.position) < 15)
        {
            StartCoroutine("Combat");
        }
    }

    IEnumerator Death()
    {
        //death animation
        yield return new WaitForSeconds(2);
        GameEvents.current.LogEntry("You defeated <color=#c23616>" + enemy.name + "</color>.");
        SaveData.current.profile.exp+=enemy.expReward;
        gameObject.SetActive(false);
    }
}
