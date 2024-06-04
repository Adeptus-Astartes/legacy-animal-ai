using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyTag
{
	public string Tag;
}

public class Memory : MonoBehaviour
{
	[HideInInspector]
	public AnimalAICore Parent;

	public List<string> FoodTags;
	public List<Transform> Foods;

	public List<string> WaterTags;
	public List<Transform> WaterPoints;

	public List<string> EnemyTags;
	public List<Transform> Enemies;

	public List<string> HomeTags;
	public List<Transform> Homes;

	public List<string> PlayerTags;
	public List<Transform> Players;

	public Taming Taming;

	public List<string> FriendlySpecies;

	#region Food
	public void AddFood( Transform _food )
	{
		if (!Foods.Contains (_food))
		{
			Foods.Add (_food);
		}
	}
	public void RemoveFood( Transform _food )
	{
		if (Foods.Contains (_food))
		{
			Foods.Remove (_food);
		}
	}
	public Transform NearestFood()
	{
		float distance = float.PositiveInfinity;
		Transform mNearestFood = null;
		foreach(Transform mfood in Foods)
		{
			if (Vector3.Distance (transform.position, mfood.position) < distance) 
			{
				distance = Vector3.Distance (transform.position, mfood.position);
				mNearestFood = mfood;
			}
		}
		return mNearestFood;
	}

	public void AddWater( Transform _food )
	{
		if (!WaterPoints.Contains (_food))
		{
			WaterPoints.Add (_food);
		}
	}
	public void RemoveWater( Transform _food )
	{
		if (WaterPoints.Contains (_food))
		{
			WaterPoints.Remove (_food);
		}
	}

	public Transform NearestWater()
	{
		float distance = float.PositiveInfinity;
		Transform mNearestWater = null;
		foreach(Transform mwater in WaterPoints)
		{
			if (Vector3.Distance (transform.position, mwater.position) < distance) 
			{
				distance = Vector3.Distance (transform.position, mwater.position);
				mNearestWater = mwater;
			}
		}
		return mNearestWater;
	}

	#endregion

	#region Enemy
	public void AddEnemy( Transform _enemy ) 
	{
		if (!Enemies.Contains (_enemy)) 
		{
			Enemies.Add (_enemy);
		}
	}
	public void RemoveEnemy( Transform _enemy)
	{
		if (Enemies.Contains (_enemy)) 
		{
			Enemies.Remove (_enemy);
		}
	}
	#endregion

	public void AddHome(Transform _home)
	{
		if (!Homes.Contains (_home)) {
			Homes.Add (_home);
		}
	}

	public void RemoveHome(Transform _home)
	{
		if (Homes.Contains (_home)) {
			Homes.Remove (_home);
		}
	}

	public Transform FindNearestHome()
	{
		float distance = float.PositiveInfinity;
		Transform mNearestHome = null;
		foreach(Transform mhome in Homes)
		{
			if (Vector3.Distance (transform.position, mhome.position) < distance) 
			{
				distance = Vector3.Distance (transform.position, mhome.position);
				mNearestHome = mhome;
			}
		}
		return mNearestHome;
	} 

	public void AddPlayer(Transform player)
	{
		Taming.CheckPlayer (player.GetComponent<Player>());
		if (!Players.Contains (player)) 
		{
			Players.Add (player);
		}
	}
	public void RemovePlayer(Transform player)
	{
		if (Players.Contains (player)) {
			Players.Remove (player);
		}
	}

	public void DetectFriendlySpecies(AnimalAICore newAnimal)
	{
		Parent.Contact (newAnimal);
	}
}
