using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Squad
{
	public AnimalAICore Leader;
	public List<AnimalAICore> SquadMembers = new List<AnimalAICore>();
}

public class AnimalSquads : Singleton<AnimalSquads>
{
	public List<Squad> AllSquads;

	public void CreateSquad(AnimalAICore leader, AnimalAICore member)
	{
		
	}

	public void UpdateSquad(AnimalAICore leader, AnimalAICore member)
	{
		if (!ContainsSquad (leader)) 
		{
			Squad squad = new Squad ();
			squad.Leader = leader;
			squad.SquadMembers.Add (member);
			AllSquads.Add (squad);
		}
		else 
		{
			Debug.Log ("AlreadyRegistered");
		}
	}

	public void CreateSquad(AnimalAICore leader, List<AnimalAICore> members)
	{
		if (!ContainsSquad (leader)) 
		{
			Squad squad = new Squad ();
			squad.Leader = leader;
			squad.SquadMembers.AddRange (members);
			foreach (AnimalAICore member in squad.SquadMembers)
				member.Movement.Leader = leader;
			AllSquads.Add (squad);
		}
	}
	public void UpdateSquad(int id, AnimalAICore leader, List<AnimalAICore> members)
	{

	}

	public void AddMemberInSquad(int squadId,List<AnimalAICore> newMembers)
	{
		if (squadId == -1)
			return;
		Squad squad = AllSquads [squadId];
		foreach(AnimalAICore newMember in newMembers)
		{
			if (!squad.SquadMembers.Contains (newMember))
			{
				newMember.Movement.Leader = squad.Leader;
				squad.SquadMembers.Add (newMember);
			}
		}
		AllSquads [squadId] = squad;
	}

	public void RemoveMember(AnimalAICore _animal)
	{
		if (!_animal.Movement.Leader)
			return;
		Squad squad = GetSquadByLeader (_animal.Movement.Leader);
		if (squad.Leader == _animal) 
		{
			squad.SquadMembers.Remove (_animal);
			int maxLevel = -1;
			int id = -1;
			for(int i = 0; i<squad.SquadMembers.Count;i++) {
				if (squad.SquadMembers[i].AnimalLevel > maxLevel) 
				{
					maxLevel = squad.SquadMembers [i].AnimalLevel;
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

	public Squad GetSquadByLeader(AnimalAICore _leader)
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

	public int GetSquadIdByLeader(AnimalAICore _leader)
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

	public bool ContainsSquad(AnimalAICore _leader)
	{
		bool contains = false;
		foreach (Squad squad in AllSquads) 
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

	public void DeleteSquad(AnimalAICore _leader)
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
