using System;
using UnityEngine;

public class EnemySphere : MonoBehaviour
{
    public Transform predator;
    public GameObject hidingSpot;
    public float speed = 1;
    
    [Header("State Materials")] 
    public Material onSteadyMaterial;
    public Material onAttackMaterial;

    private bool _isAttack;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector3 impulse = 0.0001f * (transform.position - hidingSpot.GetComponent<HidingSpot>().ChooseCover(transform.position, predator.position).position);
        
        _rigidbody.AddForce(impulse, ForceMode.Impulse);
    }
}
