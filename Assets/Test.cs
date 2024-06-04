using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour 
{

	void Update () 
	{
		RaycastHit hit1;
		if (Physics.Raycast (transform.position, -transform.up, out hit1, 20,1))
		{
			Debug.DrawLine (transform.position,hit1.point,Color.red);
		}
	}
}
