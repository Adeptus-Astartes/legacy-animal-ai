using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public List<string> AnimalTags = new List<string>(){"Animal","Gazelle"};
	public int PlayerLevel;
	public string itemName;
	public float tamingProgressPerItem;

	void Update()
	{
		RayCast ();
	}

	void RayCast()
	{
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			Ray ray = new Ray ();
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 3.0f)) 
			{
				foreach(string tag in AnimalTags)
				{
					if (hit.collider.tag == tag) 
					{
						hit.collider.GetComponent<AnimalAICore> ().Memory.Taming.AddTaming (transform, tamingProgressPerItem);
						break;
					}
				}
			}
		}
	}

}
