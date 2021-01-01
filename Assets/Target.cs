using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	private bool _isTriggered = false;

	private void OnTriggerEnter(Collider other)
	{
		_isTriggered = true;
	}

	public bool GetTargetStatus()
	{
		return _isTriggered;
	}

	public void ResetTarget()
	{
		_isTriggered = false;
	}
}
