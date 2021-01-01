using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestTargetLevels : MonoBehaviour
{
	private Target[] _targets;
	private float _penetrationPercentage;



	private void Start()
	{
		_targets = new Target[transform.childCount];
		
		for (int index = 0; index < transform.childCount; index++)
		{
			_targets[index] = transform.GetChild(index).GetComponent<Target>();
		}
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetTestResults();
		}
	}

	private void IncreasePenetrationPercentage()
	{
		_penetrationPercentage += 1f / _targets.Length;
	}

	private static void TargetNotPenetratedMessage(Target target)
	{ 
		Debug.Log(target.gameObject.name + " was not penetrated!");
	}

	private void TargetPenetrationLevelMessage()
	{
		Debug.Log("The penetration level is " + _penetrationPercentage * 100f + "%");
	}
	
	private void CollectTargetsData()
	{
		_penetrationPercentage = 0f;
		foreach (Target target in _targets)
		{
			if (target.GetTargetStatus())
			{
				IncreasePenetrationPercentage();
			}
			else
			{
				TargetNotPenetratedMessage(target);
			}
			
			target.ResetTarget();
		}
	}
	
	private void ShowPenetrationData()
	{
		TargetPenetrationLevelMessage();
	}

	public void GetTestResults()
	{
		CollectTargetsData();
		ShowPenetrationData();
	}

}
