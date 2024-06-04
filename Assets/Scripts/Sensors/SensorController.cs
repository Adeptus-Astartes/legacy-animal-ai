using UnityEngine;
using System.Collections;

public class SensorController : MonoBehaviour 
{
	[Header("Eye")]
	public EyeSensor Eye;
	public float EyeSizeAtNight;
	public float EyeSizeAtDay;
	public bool EyeDisableWhenSleep = false;

	[Header("Hear")]
	public HearSensor Hear;
	public float HearSensivityAtDay = 8;
	public float HearSenivityAtNight = 23;
	public float HearSizeAtNight = 55;
	public float HearSizeAtDay = 35;
	public bool HearDisableWhenSleep = false;

	public void DayComes()
	{
		Hear.ChangeSize (HearSizeAtDay);
	}

	public void NightComes()
	{
		Hear.ChangeSize (HearSizeAtNight);
	}

}
