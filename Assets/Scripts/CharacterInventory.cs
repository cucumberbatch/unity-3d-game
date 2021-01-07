using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{

	public GameObject firstGun;
	public GameObject secondGun;

	//public Transform rightHand;
	//public Transform leftHand;

	private bool isTwoHanded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
        	isTwoHanded = !isTwoHanded;
			secondGun.SetActive(isTwoHanded);
			firstGun.SetActive(!isTwoHanded);
        }

    }
}
