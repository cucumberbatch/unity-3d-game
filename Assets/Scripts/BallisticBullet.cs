using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticBullet : MonoBehaviour
{
	public Transform originTransform;
	
	public float     speed;
	public float     mass;
	public float     gravityFactor;
	public float 	 rayDuration;

	private Vector3 _previousPosition;
	

	Vector3 GenerateRandomBrightColorVector() 
	{
		return new Vector3(
			Random.Range(0.0f, 1.0f), 
			Random.Range(0.0f, 1.0f), 
			Random.Range(0.0f, 1.0f)
		   ).normalized;
	}

	void Start()
	{
		_previousPosition = originTransform.position;
	}

	void Update()
	{
		// Set-up for drawing a colored path of bullet movement
		Vector3 colorVector 	= GenerateRandomBrightColorVector();
		Color 	generatedColor	= new Color(
				colorVector.x, 
				colorVector.y, 
				colorVector.z
		);
		
		Vector3 incidentBeamDirection	= transform.position - _previousPosition;
		Vector3 reflectedBeamDirection;	 	

		Debug.DrawRay(_previousPosition, incidentBeamDirection, generatedColor, rayDuration);

		
		// Raycasting for ricochete path calculations
		RaycastHit hit;

		if (Physics.Raycast(_previousPosition, incidentBeamDirection, out hit, incidentBeamDirection.magnitude))
		{
			incidentBeamDirection	= hit.point - _previousPosition;
			reflectedBeamDirection	= Vector3.Reflect(incidentBeamDirection, hit.normal);
			incidentBeamDirection	= reflectedBeamDirection;
			transform.position	= hit.point + incidentBeamDirection;
			_previousPosition	= hit.point;
		}
		else
		{
			_previousPosition   = transform.position;
		}
		
		// Rewrite position values of a bullet
		transform.position += incidentBeamDirection.normalized * speed * Time.deltaTime + (Vector3.down * mass * gravityFactor * Time.deltaTime * Time.deltaTime);
	}
}
