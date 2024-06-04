using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Animal : MonoBehaviour 
{
	public string Name = "Gazelle";
	public int Level = 1;
	public Animal Leader;
	/*public void Update()
	{
		RayCast ();
	}

	public void RayCast()
	{
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			Ray ray = new Ray ();
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 3.0f)) 
			{
				if (hit.collider.GetComponent<Animal>()) 
				{
					Animal animal = hit.collider.GetComponent<Animal> ();
					if (animal.Name == Name)
					{
						if (!Leader) 
						{
							if (Level >= animal.Level) 
							{
								List<Animal> members = new List<Animal> (){ this, animal };
								AnimalSquads.Instance.CreateSquad (this, members);
							} else {
								List<Animal> members = new List<Animal> (){ animal, this };
								AnimalSquads.Instance.CreateSquad (animal, members);
							}
						} else {
							if (Leader.Level >= animal.Level) 
							{
								print (animal);
								if (AnimalSquads.Instance.GetSquadByLeader (animal) != null) {
									List<Animal> animalMembers = AnimalSquads.Instance.GetSquadByLeader (animal).SquadMembers;
									AnimalSquads.Instance.DeleteSquad (animal);
									AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (Leader), animalMembers);
								} else {
									AnimalSquads.Instance.AddMemberInSquad (AnimalSquads.Instance.GetSquadIdByLeader (Leader), new List<Animal>(){animal});
								}

							} else {
							}
						}
					}
				}
			}
		}
	}*/


		
}
