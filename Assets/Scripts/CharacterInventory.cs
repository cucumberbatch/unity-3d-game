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


    void Update()
    {
	/*
		Color 	redColor		= new Color(1, 0, 0);
		Vector3 originPosition		= transform.position;
		Vector3 incidentBeamDirection	= transform.rotation * new Vector3(0, 0, 1);
		Vector3 reflectedBeamDirection	= Vector3.zero;

		RaycastHit hit;

		while (Physics.Raycast(originPosition, incidentBeamDirection, out hit))
		{
			incidentBeamDirection	= hit.point - originPosition;
			reflectedBeamDirection	= Vector3.Reflect(incidentBeamDirection, hit.normal);

			Debug.DrawRay(originPosition, incidentBeamDirection, redColor);

			incidentBeamDirection	= reflectedBeamDirection;
			originPosition		= hit.point;
		}

		if (reflectedBeamDirection != Vector3.zero)
		{
			Debug.DrawRay(originPosition, reflectedBeamDirection, redColor);
		}

	*/

 	if (Input.GetKeyDown(KeyCode.T)) {
		isTwoHanded = !isTwoHanded;
		secondGun.SetActive(isTwoHanded);
		firstGun.SetActive(!isTwoHanded);
        }
	
    }
}
