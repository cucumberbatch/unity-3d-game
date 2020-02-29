using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void damage(int amount)
    {
        health -= amount;
        print(health);
        if (health < 1)
            BroadcastMessage("Die",SendMessageOptions.DontRequireReceiver);
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8) //номер пули в слоях
        print("Collision detected");
        damage(200);
    }
}
