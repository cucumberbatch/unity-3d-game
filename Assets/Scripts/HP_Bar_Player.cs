using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar_Player : MonoBehaviour
{
    public int HP = 100;

    public Text HP_Bar;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Damage");
        {
            HP = HP - 25;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1)
        {
            gameObject.SetActive(false);
        }

        HP_Bar.text = "Здоровье " + HP;
    }
}
