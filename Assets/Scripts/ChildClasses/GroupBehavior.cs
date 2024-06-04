using UnityEngine;
using System.Collections;
public class GroupBehavior : Movement 
{
	public Transform Leader;


	/*public bool Enabled = false;
	public NavMeshAgent agent;
	public Transform Leader;
	public Vector3 Target;
	public Vector3 Offset;
	public Vector3 RandomPos;
	public float Range;

	public bool walk = false;
	public float Rate;
	public float RateMin = 0.5f;
	public float RateMax = 2;
	public float Temp;

	public bool attack = false;
	public Transform enemy;
	public bool InWater = false;
	public bool Jumping = false;

	void Update()
	{
		if (agent.GetNavMeshAreaIndex () == 8) {
			InWater = true;
		} else {
			InWater = false;
		}
		if (agent.isOnOffMeshLink)
			Jumping = true;
		else
			Jumping = false;

		if (walk) 
		{
			Target = Leader.position + Offset + RandomPos;
			agent.SetDestination (Target);
			if (Time.time > Rate + Temp) 
			{
				Rate = Random.Range (RateMin, RateMax);
				Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * Range/2;
				NavMeshHit hit;
				if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas)) 
				{
					RandomPos = hit.position;
				}
				Temp = Time.time;
			}
		}
		if (attack) {
			agent.SetDestination (enemy.position);
		}
	}

	public void Attack(Transform target)
	{
		walk = false;
		attack = true;
		enemy = target;
	}*/
}
