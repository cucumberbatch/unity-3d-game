using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyV2 : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _agentTransform;
    private Transform _target;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _agentTransform = GetComponent<Transform>();
        _target = _player.GetComponent<Transform>();  //преследуем игрока
    }

    void Update()
    {    
        if (_agent.velocity == Vector3.zero)
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
            
            
        // _agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("XR"))
        {   
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            
        }
    }
}