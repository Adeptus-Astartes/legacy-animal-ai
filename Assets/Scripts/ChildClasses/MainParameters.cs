using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainParameters : MonoBehaviour 
{
	[Header("TeamBehavior")]
	public List<Transform> Team;
	public Transform Leader;

	[Header("TamedBehavior")]
	public Taming Taming;

	[Header("Sleep")]
	public float SleepFrom;
	public float SleepTo;
	public bool ReturnToHome = false;


}
