using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AmbientBehavior
{
	public Behavior Behavior;
	public int MinChance;
	public int MaxChance;
	public float MinLenghtTime;
	public float MaxLenghtTime;

}

public class AmbientBehaviorBase : MonoBehaviour 
{
	[HideInInspector]
	public AnimalAICore Parent;

	public List<AmbientBehavior> Behaviors;

	[HideInInspector]
	public AmbientBehavior Current;
	[HideInInspector]
	public float Lenght;
	[HideInInspector]
	public float Temp;

	public AmbientBehavior RandomAmbientBehavior()
	{
		int dice = Random.Range (0, 100);
		AmbientBehavior temp = Behaviors[0];
		foreach(AmbientBehavior behavior in Behaviors)
		{
			if (dice > behavior.MinChance && dice < behavior.MaxChance) 
			{
				temp = behavior;
				break;
			}
		}

		return temp;
	}

	public void Update()
	{
		if (Time.time > Temp + Lenght) 
		{
			Current = RandomAmbientBehavior ();
			ChangeAmbientBehavior (Current.Behavior);
			Lenght = Random.Range (Current.MinLenghtTime,Current.MaxLenghtTime);
			Temp = Time.time;

		}
	}
		
	public void ChangeAmbientBehavior(Behavior _newBehaviour)
	{
		switch (_newBehaviour) 
		{
		case Behavior.Idle:
			Parent.TaskManager.AddTask (Behavior.Idle);
			Parent.TaskManager.DoneTask (Behavior.Walk);
			break;
		case Behavior.Walk:
			Parent.TaskManager.AddTask (Behavior.Walk);
			Parent.TaskManager.DoneTask (Behavior.Idle);
			break;

		}
	}
}
