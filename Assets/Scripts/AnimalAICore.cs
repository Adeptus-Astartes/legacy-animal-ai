using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NodeCanvas.BehaviourTrees;

public enum Behavior { Idle, Walk, Howling, Defecation, Peeing, SearchFood, SearchWater, Battle, Sleep, Taming};
public enum FeedingType { Grass, Meat, All};
public enum BattleBehaviorEnum { Chicken, Defence, Agressive};


public class AnimalAICore : MonoBehaviour 
{
	public string AnimalName = "EnterAnimalName";
	public int AnimalLevel = 1;
	public bool CanBeInSquad = false;
	public int MaxSquadSize = 5;
	public int CurreintSquadSize;

	public FeedingType FeedType;
	public BattleBehaviorEnum BattleBehavior;

	public AmbientBehaviorBase Ambient;
	public Movement Movement;
	public FoodStuff FoodStuff;
	public Memory Memory;
	public Sleep Sleep;

	[HideInInspector]
	public TaskManager TaskManager;
	[HideInInspector]
	public BehaviourTreeOwner owner;
	public UnityEngine.AI.NavMeshAgent agent;

	public float UpdateInterval = 0.1f;
	private float Temp;

	public void Awake()
	{
		Ambient.Parent = this;
		Movement.Parent = this;
		FoodStuff.Parent = this;
		Memory.Parent = this;
		Memory.Taming.Parent = this;
		Sleep.Parent = this;
		TaskManager = this.GetComponent<TaskManager> ();
		owner = this.GetComponent<BehaviourTreeOwner> ();
	}

	private void Update()
	{
		if (Time.time > Temp + UpdateInterval)
		{
			FoodStuff.Hungry ();
			FoodStuff.CheckHungry ();

			FoodStuff.Thirsty ();
			FoodStuff.CheckThirsty ();

			Sleep.CheckSleep ();
			Sleep.TranquilizerEolation ();

			Temp = Time.time;
		}
	}

	public void MoveTo(Transform destination, float speed)
	{
		agent.speed = speed;
		agent.SetDestination (destination.position);
		Movement.CurrentSpeed = speed;
	}



	public void SendGraphEvent(string eventName)
	{
		owner.SendEvent (eventName);
	}

	public void RateEnemy()
	{

	}

	public void LookOn(Transform target)
	{
		Vector3 targetPos = target.position;
		targetPos.y = transform.position.y;
		transform.LookAt (targetPos);
	}

	public void PlayerDetected(Player player)
	{

	}

	public void Contact(AnimalAICore animal)
	{
		if (animal.AnimalName == AnimalName)
		{
			//Creating Squad
			if (!Movement.Leader)
			{
				if (AnimalLevel >= animal.AnimalLevel) {
					List<AnimalAICore> members = new List<AnimalAICore> (){ this, animal };
					AnimalSquads.Instance.CreateSquad (this, members);
				} else {
					List<AnimalAICore> members = new List<AnimalAICore> (){ animal, this };
					AnimalSquads.Instance.CreateSquad (animal, members);
				}
			} else {
				if (Movement.Leader.AnimalLevel >= animal.AnimalLevel) {
					//Check if animal have squad
					if (AnimalSquads.Instance.GetSquadByLeader (animal) != null) 
					{
						List<AnimalAICore> animalMembers = AnimalSquads.Instance.GetSquadByLeader (animal).SquadMembers;
						if (animalMembers.Count + AnimalSquads.Instance.GetSquadByLeader (Movement.Leader).SquadMembers.Count <= Movement.Leader.MaxSquadSize) 
						{
							AnimalSquads.Instance.DeleteSquad (animal);
							AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (Movement.Leader), animalMembers);
						}
					} else {
						//if (AnimalSquads.Instance.GetSquadByLeader (Movement.Leader).SquadMembers.Count + 1 <= Movement.Leader.MaxSquadSize)
							AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (Movement.Leader), new List<AnimalAICore>(){animal});
					}
				} else {

				}
				/*if (Movement.Leader.AnimalLevel >= animal.AnimalLevel) {
					if (AnimalSquads.Instance.GetSquadByLeader (animal) != null) {
						List<AnimalAICore> animalMembers = AnimalSquads.Instance.GetSquadByLeader (animal).SquadMembers;
						AnimalSquads.Instance.DeleteSquad (animal);
						AnimalSquads.Instance.CreateSquad (Movement.Leader, animalMembers);
					} else {
						AnimalSquads.Instance.CreateSquad (Movement.Leader, new List<AnimalAICore> (){ animal });//AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (Movement.Leader), new List<AnimalAICore>(){animal});
					}

				} else {
					if (AnimalSquads.Instance.GetSquadByLeader (Movement.Leader) != null) {
						List<AnimalAICore> animalMembers = AnimalSquads.Instance.GetSquadByLeader (Movement.Leader).SquadMembers;
						AnimalSquads.Instance.DeleteSquad (Movement.Leader);
						AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (animal), animalMembers);
						//AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (animal), animalMembers);
					} else {
						AnimalSquads.Instance.CreateSquad (animal, new List<AnimalAICore> (){ Movement.Leader });//AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (animal), new List<AnimalAICore>(){Movement.Leader});
					}
				}*/
			}

		}
	}
		
}
