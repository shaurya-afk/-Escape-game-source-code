using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public Animator anim;
    PlayerMovement targetMove;
    public GameObject player;
    public float health = 100f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim= gameObject.GetComponent<Animator>();
        targetMove = player.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        agent.destination = target.position;

        if (Vector3.Distance(transform.position,target.position) <= 10)
        {
            AudioManager.PlayAudio("roar");
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (targetMove.lightOn == true)
            {
                health -= 5f;
            }
        }
    }
}
