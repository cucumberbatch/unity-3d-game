using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticBullet : MonoBehaviour
{
	public BallisticGun	gun;
	public Transform 	originTransform;
	
	public float     speed;
	public float     mass;
	public float     gravityFactor;
	public float 	 rayDuration;

	private Vector3 	_previousPosition;
	private RaycastHit 	_hit;
	private Vector3		_incidentBeamDirection;


	public void SetGunReference(BallisticGun gun)
	{
		this.gun = gun;
	}

	Color GenerateRandomBrightColorVector() 
	{
		Vector3 colorVector = new Vector3(
				Random.Range(0.0f, 1.0f), 
				Random.Range(0.0f, 1.0f), 
				Random.Range(0.0f, 1.0f)
			).normalized;
		
		return new Color(colorVector.x, colorVector.y, colorVector.z);
	}

	void Start()
	{
		_previousPosition = gun.bulletEmitterTransform.position;
	}

	void Update()
	{
		// Find out the movement direction
		_incidentBeamDirection	= transform.position - _previousPosition;
		
		// Raycasting for ricochete path calculations
		if (Physics.Raycast(_previousPosition, _incidentBeamDirection, out _hit, _incidentBeamDirection.magnitude))
		{
			_incidentBeamDirection	= Vector3.Reflect(_hit.point - _previousPosition, _hit.normal);
			_previousPosition	= _hit.point;
			transform.position	= _previousPosition + _incidentBeamDirection;
		}
		else
		{
			_previousPosition   = transform.position;
		}
		
		// Rewrite position values of a bullet
		transform.position += _incidentBeamDirection.normalized * speed * Time.deltaTime + (Vector3.down * mass * gravityFactor * Time.deltaTime * Time.deltaTime);

		// Set-up for drawing a colored path of bullet movement
		Color 	generatedColor	= GenerateRandomBrightColorVector();

		// Draw bullet trace (uncomment for debug)
		// Debug.DrawRay(_previousPosition, _incidentBeamDirection, generatedColor, rayDuration);

	}
}
