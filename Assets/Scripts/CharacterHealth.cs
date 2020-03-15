using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int characterHealth = 40;

    private void ApplyDamage(int amountOfDamage)
    {
        characterHealth -= amountOfDamage;
        print(gameObject.name + ": " + characterHealth);
        if (characterHealth <= 0)
            BroadcastMessage("Die",SendMessageOptions.DontRequireReceiver);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) //номер пули в слоях
        {
            if (gameObject.CompareTag(collision.gameObject.GetComponent<Bullet>().shootedPerson.tag))
            {
                return;
            }
            ApplyDamage(collision.gameObject.GetComponent<Bullet>().amountOfDamage);
        }
    }

    
}
