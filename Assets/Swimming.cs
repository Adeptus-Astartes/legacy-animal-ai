using UnityEngine;
using System.Collections;

public class Swimming : MonoBehaviour 
{
	public Transform target;
	public UnityEngine.AI.NavMeshAgent agent;
	public Transform Mesh;
	public float Deep;
	public float Angle;
	public float Modifier;
	public Transform firstRayPos;
	public Transform secondRayPos;
	public bool inWater = false;
	public bool translate = false;
	public bool canSwimUnderSurface = false;
	public float swimAngle;
	public float randomswimAngle;
	public float swimYOffset = -1f;
	public float FloatSpeed = 1f;
	public float RotateSpeed = 1f;
	public float changeDeepRate = 0.5f;
	public float changeDeepTemp;
	public float randomDeep;
	public float TargetAngle;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		agent.SetDestination (target.position);
	
		if (NavMeshExtend.GetNavMeshAreaIndex (agent) == 8) 
		{
			if (Deep > swimYOffset) 
			{
				inWater = true;
			} else {
				if(inWater)
					translate = true;
				
				inWater = false;

			}
		} else {
			if (Deep < swimYOffset) {
				inWater = false;
			}
		}

		if (inWater) 
		{
			if (canSwimUnderSurface) 
			{
				if (Deep > swimYOffset) 
				{


					if (Time.time > changeDeepRate + changeDeepTemp) 
					{
						float tempDeep = Random.Range (Deep,swimYOffset);

						if (tempDeep > randomDeep) {
							randomswimAngle = 20;
						} else {
							randomswimAngle = -20;
						}
						randomDeep = tempDeep;
						changeDeepTemp = Time.time;
					}

					float step = FloatSpeed * Time.deltaTime;
					ChangeAngle (RotateSpeed, randomswimAngle);
					if (Mesh.localPosition.y - Mesh.localScale.y/2 -1 < -Deep)
						Mesh.localPosition = Vector3.down * (Deep - Mesh.localScale.y / 2f - 1);
					else
						Mesh.localPosition = Vector3.MoveTowards (Mesh.localPosition,Vector3.down * (randomDeep - Mesh.localScale.y / 2f - 1),step);
				} else {
					Mesh.localEulerAngles = Vector3.right * Angle * Modifier;
				}
				//Mesh.localPosition = Vector3.down * (Deep - Mesh.localScale.y / 2f - 1);

			}
			else 
			{
				
				Mesh.localPosition = Vector3.down * (Mathf.Clamp (Deep, -swimYOffset, swimYOffset) - Mesh.localScale.y / 2f - 1);
				if (Deep > swimYOffset) 
				{
					ChangeAngle (RotateSpeed, swimAngle);
				} else {
					//translate = true;
					//ChangeAngle (RotateSpeed, Angle * Modifier);
					//Mesh.localEulerAngles = Vector3.right * Angle * Modifier;
				}
			}
		} 
		else 
		{
			Mesh.localPosition = Vector3.down * (Deep - Mesh.localScale.y / 2f - 1);
			if (translate) 
			{
				ChangeAngle (RotateSpeed, Angle * Modifier);
			} 
			else
			{
				Debug.Log (123);
				Mesh.localEulerAngles = Vector3.right * Angle * Modifier;
			}
		}
		//float ste2p = RotateSpeed * Time.deltaTime;
		//Mesh.localEulerAngles = Vector3.RotateTowards (Mesh.localEulerAngles,Vector3.right * TargetAngle * Modifier,ste2p,0f);
		CheckDeep ();

	}
	[Range(0,1)]
	public float time;
	public void ChangeAngle(float speed,float newAngle)
	{
		if ((int)newAngle != (int)TargetAngle) 
		{
			time = 0;
			TargetAngle = newAngle;
		}
		if (translate) 
		{
			if (time > 0.2f)
				translate = false;
		}
		if (time < 1) 
		{
			time += speed * Time.deltaTime;
		} 
		Mesh.localEulerAngles = new Vector3( Mathf.LerpAngle (Mesh.localEulerAngles.x,TargetAngle,time),0,0);
	}
	public void Rotating()
	{
		
	}

	public void CheckDeep()
	{
		RaycastHit hit0;
		RaycastHit hit1;
		if (Physics.Raycast (firstRayPos.position, -transform.up, out hit0, 20))
		{
			Debug.DrawLine (firstRayPos.position,hit0.point,Color.red);
		}
		if (Physics.Raycast (secondRayPos.position, -transform.up, out hit1, 20))
		{
			Debug.DrawLine (secondRayPos.position,hit1.point,Color.blue);
		}

		var heading = hit0.point - hit1.point;
		var distance = heading.magnitude;
		var direction = heading / distance;
		Angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));
		if (hit0.distance > hit1.distance) 
		{
			Modifier = 1.0f;
		} else {
			Modifier = -1.0f;
		}

		Deep =  (hit0.distance + hit1.distance)/2;

	}

}
