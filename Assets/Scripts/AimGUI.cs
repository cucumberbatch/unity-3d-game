using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGUI : MonoBehaviour
{ 
	public Texture2D aim; 
	public float x, y;
	
	void OnGUI () {
         GUI.DrawTexture (
	         new Rect (
		         Screen.width / 2 - x / 2, 
		         Screen.height / 2 - y / 2, 
		         x,
		         y),
	         aim);
	}
}

