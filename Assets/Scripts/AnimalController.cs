using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnimalController : Singleton<AnimalController> 
{

	public Slider TimeLine;
	public float DayLenght;
	[HideInInspector]
	public float DayCycleStep;
	public float CurrentTime;
	public Transform Sunlight;


	void Start()
	{
		DayCycleStep = DayLenght / 24f;
	}

	public void FixedUpdate()
	{
		CurrentTime += Time.fixedDeltaTime/DayCycleStep;

		if (CurrentTime > 1440) 
		{
			CurrentTime = 0;
		}
		Sunlight.eulerAngles = new Vector3(Mathf.Lerp (0, 360,  CurrentTime/1440),-30,0);
	}
}
