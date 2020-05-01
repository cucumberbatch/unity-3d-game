using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeHead : MonoBehaviour
{
    public GameObject parent;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "XR")
            return;
        _animator.enabled = false;
        parent.GetComponent<Enemy>().enabled = false;
    }
}
