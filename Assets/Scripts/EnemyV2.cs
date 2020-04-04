using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyV2 : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject player;
    private Transform _agentTransform;
    private Transform _target;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        _agentTransform = GetComponent<Transform>();
        _target = player.GetComponent<Transform>();  //преследуем игрока
    }

    // Update is called once per frame
    void Update()
    {    if (_agent.velocity == Vector3.zero)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        
        if (_agent.velocity != Vector3.zero)
        {
            if (Mathf.Abs((_agentTransform.position - _target.position).magnitude) > _agent.stoppingDistance * 2.0f)
            {
                _agent.speed = 6;
                gameObject.GetComponent<Animator>().SetTrigger("Run");
            }
            else
            {
                _agent.speed = 3;
                gameObject.GetComponent<Animator>().SetTrigger("Walk");
            }
        }
            
            
        _agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "XR")
        {   
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            
        }
    }
}