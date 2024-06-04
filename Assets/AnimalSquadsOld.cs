using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SquadOld
{
	public Animal Leader;
	public List<Animal> SquadMembers = new List<Animal>();
}

public class AnimalSquadsOld : Singleton<AnimalSquads>
{
	public List<SquadOld> AllSquads;

	public void CreateSquad(Animal leader, Animal member)
	{
		
	}

	public void UpdateSquad(Animal leader, Animal member)
	{
		if (!ContainsSquad (leader)) 
		{
			SquadOld squad = new SquadOld ();
			squad.Leader = leader;
			squad.SquadMembers.Add (member);
			AllSquads.Add (squad);
		}
		else 
		{
			Debug.Log ("AlreadyRegistered");
		}
	}

	public void CreateSquad(Animal leader, List<Animal> members)
	{
		if (!ContainsSquad (leader)) 
		{
			SquadOld squad = new SquadOld ();
			squad.Leader = leader;
			squad.SquadMembers.AddRange (members);
			foreach (Animal member in squad.SquadMembers)
				member.Leader = leader;
			AllSquads.Add (squad);
		}
	}
	public void UpdateSquad(int id, Animal leader, List<Animal> members)
	{

	}

	public void AddMemberInSquad(int squadId,List<Animal> newMembers)
	{
		if (squadId == -1)
			return;
		SquadOld squad = AllSquads [squadId];
		foreach(Animal newMember in newMembers)
		{
			if (!squad.SquadMembers.Contains (newMember))
			{
				newMember.Leader = squad.Leader;
				squad.SquadMembers.Add (newMember);
			}
		}
		AllSquads [squadId] = squad;
	}

	public void RemoveMember(Animal _animal)
	{
		if (!_animal.Leader)
			return;
		SquadOld squad = GetSquadByLeader (_animal.Leader);
		if (squad.Leader == _animal) 
		{
			squad.SquadMembers.Remove (_animal);
			int maxLevel = -1;
			int id = -1;
			for(int i = 0; i<squad.SquadMembers.Count;i++) {
				if (squad.SquadMembers[i].Level > maxLevel) 
				{
					maxLevel = squad.SquadMembers [i].Level;
					id = i;
				}
			}
			squad.Leader = squad.SquadMembers [id];
		}
		else
		{
			squad.SquadMembers.Remove (_animal);
		}
	}

	public SquadOld GetSquadByLeader(Animal _leader)
	{
		int id = -1;
		for(int i = 0; i < AllSquads.Count; i++)
		{
			if (AllSquads[i].Leader == _leader) 
			{
				id = i;
			}
		}
		if (id == -1)
			return null;
		else
			return AllSquads[id];
	}

	public int GetSquadIdByLeader(Animal _leader)
	{
		int id = -1;
		for(int i = 0; i < AllSquads.Count; i++)
		{
			if (AllSquads[i].Leader == _leader) 
			{
				id = i;
			}
		}
		return id;
	}

	public bool ContainsSquad(Animal _leader)
	{
		bool contains = false;
		foreach (SquadOld squad in AllSquads) 
		{
			if (squad.Leader == _leader) 
			{
				contains = true;
			}
		}
		return contains;
	}

	public void DeleteSquad(int id)
	{

	}

	public void DeleteSquad(Animal _leader)
	{
		int id = -1;
		for(int i = 0; i < AllSquads.Count; i++)
		{
			if (AllSquads[i].Leader == _leader) 
			{
				id = i;
			}
		}
		if(id != -1)
		AllSquads.RemoveAt (id);
	}


	/*

	public void UpdateSquad(Animal leader, List<Animal> members)
	{
		if (!ContainsSquad (leader)) 
		{
			Squad squad = new Squad ();
			squad.Leader = leader;
			squad.SquadMembers.AddRange (members);
			AllSquads.Add (squad);
		}
		else 
		{
			Squad squad = AllSquads [GetSquadIdByLeader (leader)];
			squad.Leader = leader;
			squad.SquadMembers = members;
			AllSquads.Insert (GetSquadIdByLeader (leader),squad);
		}
	}

	public void UpdateSquadAt(int id, Animal leader, List<Animal> members)
	{

		Squad squad = new Squad ();
		squad.Leader = leader;
		squad.SquadMembers.AddRange (members);
		AllSquads.Insert (id,squad);
	}
	*/



}
