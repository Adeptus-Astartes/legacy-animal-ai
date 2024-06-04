using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class NoiseInfo
{
	[HideInInspector]
	public HearSensor sensor;
	
	public Noise Object;
	public float Distance;
	public float OriginalNoise;
	public float Noise;

	public float CalculateNoiseLevel(Vector3 pos,float hearRadius, AnimationCurve curve)
	{
		Distance = Vector3.Distance (Object.transform.position, pos);
		OriginalNoise = Object.NoiseLevel;
		var normalizedNoise = (1 - (Distance / hearRadius));
		Noise = curve.Evaluate(normalizedNoise) * OriginalNoise;
		return Noise;
	}
}

public class HearSensor : MonoBehaviour 
{
	public Memory Memory;

	public List<NoiseInfo> Noises;
	public float TotalRadius;
	public AnimationCurve NoiseCurve = AnimationCurve.Linear(0f,0,1f,1f);
	public float MinNoiseLevelDetect;
	public float CheckRate = 0.5f;
	private float CheckRateTemp = 0;

	void Start () 
	{
		TotalRadius = transform.localScale.x;
	}

	public void ChangeSize(float newSize)
	{
		transform.localScale = Vector3.one * newSize;
		TotalRadius = transform.localScale.x;
	}

	void Update () 
	{
		if (Time.time > CheckRate + CheckRateTemp) 
		{
			foreach(NoiseInfo Info in Noises)
			{
				if (Info.CalculateNoiseLevel (transform.position, TotalRadius / 2f, NoiseCurve) > MinNoiseLevelDetect) 
				{
					AddNoiseOwner (Info.Object.Owner);
				}
			}

			Noises = Noises.OrderByDescending (x => x.Noise).ToList ();

			CheckRateTemp = Time.time;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Noise")
			return;
		Noise noise = other.GetComponent<Noise> ();
		if (!ContainsNoiseElement (noise)) 
		{
			NoiseInfo _noiseInfo = new NoiseInfo ();
			_noiseInfo.Object = noise;
			Noises.Add (_noiseInfo);
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if (other.tag != "Noise")
			return;
		
		for (int i = Noises.Count - 1; i >= 0; i--) 
		{
			if (Noises [i].Object.transform == other.transform) 
			{
				RemoveNoiseOwner (Noises[i].Object.Owner);
				Noises.Remove (Noises[i]);
			}
		}
	}

	public bool ContainsNoiseElement(Noise _noise)
	{
		bool contains = false;
		foreach (NoiseInfo info in Noises) 
		{
			if (info.Object == _noise) 
			{
				contains = true;
			}
		}
		return contains;
	}

	public void AddNoiseOwner(Transform owner)
	{
		if (Memory.EnemyTags.Contains (owner.tag)) 
		{
			Memory.AddEnemy (owner);
		}
		if (Memory.PlayerTags.Contains (owner.tag)) 
		{
			Memory.AddPlayer (owner);
		}
		if (Memory.FriendlySpecies.Contains (owner.tag)) 
		{
			Memory.DetectFriendlySpecies (owner.GetComponent<AnimalAICore>());
		}
	}
	public void RemoveNoiseOwner(Transform owner)
	{
		if (Memory.EnemyTags.Contains (owner.tag)) 
		{
			Memory.RemoveEnemy (owner);
		}
		if (Memory.PlayerTags.Contains (owner.tag)) 
		{
			Memory.RemovePlayer (owner);
		}
	}
}
