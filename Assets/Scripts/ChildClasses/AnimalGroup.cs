using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalGroup : MonoBehaviour 
{
	public Transform leader;
	public List<GroupBehavior> Group;

	[Space(10)]
	public Transform target;
	public List<Transform> enemyTarget;

	public Behavior LeaderBehavior;

	/*void Start()
	{
		foreach (GroupBehavior tr in Group) 
		{
			tr.Offset = tr.transform.position - transform.position;
		}
	}

	public void AttackTarget()
	{
		foreach (GroupBehavior tr in Group)
		{
			tr.Attack (target);
		}
	}
	public void AttackGroup()
	{
		foreach (GroupBehavior tr in Group)
		{
			tr.Attack (enemyTarget[Random.Range(0,enemyTarget.Count)]);
		}
	}

	public void ShareLeaderBehavior()
	{

	}*/
}
