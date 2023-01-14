using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        if (Vector3.Distance(transform.position,target.position) <= 10)
        {
            AudioManager.PlayAudio("roar");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.PlayAudio("growl");
            Destroy(gameObject);    
        }
    }
}
