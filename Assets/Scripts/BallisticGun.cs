using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticGun : MonoBehaviour
{
	public Camera 		playerCamera;
	public GameObject 	bullet;
	public Transform 	bulletEmitterTransform;

	private RaycastHit 	_hit;
	private Ray		_ray;

	void Update()
	{
        	_ray = playerCamera.ScreenPointToRay(Input.mousePosition);

		// Instantly emmit bullets on left mouse click	
		if (Input.GetMouseButton(0))
		{
			bullet 	= Instantiate(bullet, _ray.origin + _ray.direction * 2, Quaternion.FromToRotation(-Vector3.forward, _hit.normal));

			bullet.GetComponent<BallisticBullet>().SetGunReference(this);
		}
		
		// Draw ray in look direction (uncomment for debug)
		// Debug.DrawRay(_ray.origin, _ray.direction * 5, Color.red);
	}
}
