﻿using UnityEngine;
using System.Collections;

public class TrueSwitch2 : MonoBehaviour 
{
	public bool exit = false;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log ("PASTEQUE");

			exit = true;
		}
	}
}