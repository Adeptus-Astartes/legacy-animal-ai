using UnityEngine;
using System.Collections;

public class EyeSensor : MonoBehaviour 
{
	public Memory Memory;
	public bool Rentgen = false;
	public void ChangeSensorSize( Vector3 _newSize )
	{
		transform.localScale = _newSize;
	}

	private void OnTriggerEnter( Collider _other )
	{
		if (!Rentgen)
		{
			if (Memory.FoodTags.Contains (_other.tag)) {
				Memory.AddFood (_other.transform);
			}
			if (Memory.WaterTags.Contains (_other.tag)) {
				Memory.AddWater (_other.transform);
			}
			if (Memory.EnemyTags.Contains (_other.tag)) {
				Memory.AddEnemy (_other.transform);
			}
			if (Memory.FriendlySpecies.Contains (_other.tag)) 
			{
				Memory.DetectFriendlySpecies (_other.GetComponent<AnimalAICore>());
			}
		} else {
			var heading = _other.transform.position - transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;

			RaycastHit hit;
			if (Physics.Raycast (transform.position, direction, out hit)) 
			{
				if (hit.collider == _other) 
				{
					if (Memory.FoodTags.Contains (_other.tag)) {
						Memory.AddFood (_other.transform);
					}
					if (Memory.WaterTags.Contains (_other.tag)) {
						Memory.AddWater (_other.transform);
					}
					if (Memory.EnemyTags.Contains (_other.tag)) {
						Memory.AddEnemy (_other.transform);
					}
					if (Memory.FriendlySpecies.Contains (_other.tag)) 
					{
						Memory.DetectFriendlySpecies (_other.GetComponent<AnimalAICore>());
					}
				}
				else 
				{
					//Wall
				}
			}
		}
	}
}
