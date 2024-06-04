using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BehaviorTask
{
	public Behavior Behaviour;
	public int Priority;
}
public class TaskManager : MonoBehaviour
{
	public List<BehaviorTask> AllTasks;
	public List<BehaviorTask> ActiveTasks;
	public Behavior CurrentBehaviour;

	public void Start()
	{
		SortByPriority ();
	}

	public void SortByPriority()
	{
		ActiveTasks = ActiveTasks.OrderByDescending (x => x.Priority).ToList ();
		if (ActiveTasks.Count > 0)
			CurrentBehaviour = ActiveTasks [0].Behaviour;
	}

	public void AddTask( Behavior _behaviour )
	{
		foreach(BehaviorTask Task in AllTasks)
		{
			if (Task.Behaviour == _behaviour) 
			{
				if (!ActiveTasks.Contains (Task)) {
					ActiveTasks.Add (Task);

				}
			}
		}
		SortByPriority ();
	}

	public void DoneTask( Behavior _behaviour )
	{
		foreach(BehaviorTask Task in AllTasks)
		{
			if (Task.Behaviour == _behaviour) 
			{
				if (ActiveTasks.Contains (Task)) 
				{
					ActiveTasks.Remove (Task);

				}
			}
		}
		SortByPriority ();
	}
}
