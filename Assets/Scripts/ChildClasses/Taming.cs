using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerTaming
{
	public Transform Player;
	public float TamingProgress;
}
public class Taming : MonoBehaviour 
{
	[HideInInspector]
	public AnimalAICore Parent;

	public int MinPlayerLevel = 10;
	public List<string> FoodTag;
	public bool Tamed = false;
	public Transform Owner = null;

	public float MinRange;
	public float MaxRange;
	public float ChangeRate = 3.0f;
	[HideInInspector]
	public float rateTemp = 0;

	public float MaxLoveBar = 100;
	public float LoveBar = 100;
	public float LoveBarDecreasingSpeed;
	public float MaxDistToPlayer = 30;
	public float CurrentDistanceToPlayer = 0;
	public float CheckRate = 0.5f;
	private float CheckRateTemp = 0;

	public List<PlayerTaming> Players;

	void Update()
	{
		if (!Owner)
			return;
		if (Time.time > CheckRateTemp + CheckRate) 
		{
			CurrentDistanceToPlayer = Vector3.Distance (transform.position,Owner.position);
			if (CurrentDistanceToPlayer > MaxDistToPlayer) {
				LoveBar -= LoveBarDecreasingSpeed * Time.deltaTime;
				if (LoveBar < 0)
					SetFree ();
			} else {
				LoveBar = MaxLoveBar;
			}
			CheckRateTemp = Time.time;
		}
	}


	public void CheckPlayer(Player player)
	{
		if (FoodTag.Contains (player.itemName)) 
		{
			if (player.PlayerLevel >= MinPlayerLevel)
			{
				Parent.TaskManager.AddTask (Behavior.Taming);
			}
			else 
			{
				print ("EnableDefenceBehavior");
			}
		} 
		else 
		{
			print ("Unacceptable");
		}
		print (player.itemName + player.PlayerLevel);
	}
		
	public Transform GetPlayerPos(Transform target)
	{
		//target.position = Owner.position;
		Vector3 random = Vector3.zero;
		if (Time.time > ChangeRate + rateTemp) 
		{
			random = Random.insideUnitSphere * Random.Range (MinRange,MaxRange);
			target.position = Owner.position + random;
			rateTemp = Time.time;
		}
		return target;
	}

	public void AddTaming(Transform _player,float _progress)
	{
		if (ContainsPlayer (_player)) {
			foreach (PlayerTaming _ptaming in Players) {
				if (_ptaming.Player == _player) {
					_ptaming.TamingProgress += _progress;

					if (_ptaming.TamingProgress > 100) {
						Tamed = true;
						Owner = _player;
					}
				}
			}
		} else {
			PlayerTaming newPlayer = new PlayerTaming ();
			newPlayer.Player = _player;
			newPlayer.TamingProgress = _progress;
			Players.Add (newPlayer);
		}


	}
	public bool ContainsPlayer(Transform _player)
	{
		bool contains = false;
		foreach (PlayerTaming info in Players) 
		{
			if (info.Player == _player) 
			{
				contains = true;
			}
		}
		return contains;
	}


	public void GetDamageFromOwner(float damage)
	{
		LoveBar -= damage;
		if (LoveBar < 0)
			SetFree ();
	}

	public void SetFree()
	{
		Tamed = false;
		Owner = null; 
		Players.Clear ();
	}
}
