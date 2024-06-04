using UnityEngine;
using System.Collections;

public class Sleep : MonoBehaviour 
{
	[HideInInspector]
	public AnimalAICore Parent;

	public SensorController SensorController;

	public float WhenRegularSleep;
	public float RegularSleepLenght;
	public bool sleeping;
	public int TranquilzerTolerance = 100;
	public float TranqulizerInBlood = 0;
	public float TranqulizerEolationSpeed = 2;
	private float sleepingTemp;

	public void CheckSleep()
	{
		if (!sleeping) 
		{
			if (AnimalController.Instance.CurrentTime > WhenRegularSleep && AnimalController.Instance.CurrentTime < WhenRegularSleep + RegularSleepLenght) 
			{
//				int time;
//				int lenght;
//				int clamped;
//				int rest;
//				clamped = (int)wakeUpTemp / 1440;
//				rest = (int)wakeUpTemp - (clamped * 1440);
				SensorController.NightComes();
				Parent.TaskManager.AddTask (Behavior.Sleep);
				sleeping = true;
			}
		} 
		else 
		{
			if (AnimalController.Instance.CurrentTime > WhenRegularSleep + RegularSleepLenght) 
			{
				SensorController.DayComes();
				Parent.TaskManager.DoneTask (Behavior.Sleep);
				sleeping = false;
			}

		}
	}

	public void GetShotFromTranqulizer(int tranqulizerPower)
	{
		TranqulizerInBlood += tranqulizerPower;
		if (TranqulizerInBlood > TranquilzerTolerance)
		{
			float delta = TranqulizerInBlood - TranquilzerTolerance;

			Parent.TaskManager.AddTask (Behavior.Sleep);
		}
	}

	public void TranquilizerEolation()
	{
		if (TranqulizerInBlood <= 0)
			TranqulizerInBlood = 0;
		else
			TranqulizerInBlood -= TranqulizerEolationSpeed * Time.fixedDeltaTime;
	}
}
