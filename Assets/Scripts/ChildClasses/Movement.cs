using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	[HideInInspector]
	public AnimalAICore Parent;
	public Transform TargetsRoot;
	[HideInInspector]
	public Transform PointerReference;
	public Transform Target;

	[Header("Swimming")]
	public bool canSwim = false;
	public Transform MeshRoot;
	[HideInInspector]
	public float _maxDeep;
	[HideInInspector]
	public float _angle;
	[HideInInspector]
	public float _modifier;

	public Transform FirstRayPos;
	public Transform SecondRayPos;

	public float SwimmingSpeed = 1f;
	public float RotateSpeed = 1f;

	public bool inWater = false;

	public bool canSwimUnderSurface = false;
	public float swimYOffset = 3f;
	public float swimAngleOnSurface;
	public float swimAngleUnderSurface;
	[HideInInspector]
	public float swimAngleUnderSurfaceTemp;
	[HideInInspector]
	public bool _exitFromWater = false;

	public float changeDeepRate = 4f;
	[HideInInspector]
	public float _changeDeepTemp;
	//[HideInInspector]
	public float _randomDeep = 5;
	//[HideInInspector]
	public float _targetAngle = 18;


	[Header("GroupBehavior")]
	public AnimalAICore Leader;
	public float AreaAroundLeader;
	public float LeaderInfluenceRange = 50;
	public float CheckDistanceRate = 1.0f;
	private float TempCDR; //*TempCheckDistaceRate*
	public Vector3 Offset;
	public Vector3 RandomPos;
	public float MinRange;
	public float MaxRange;
	public float Range;
	[Space(10)]
	public float CurrentSpeed;
	[Header("Walking")]
	public float WalkSpeed = 3.5f;
	public float CreepUpSpeed = 2.0f;
	public float SlowRunSpeed = 5.0f;
	public float RunSpeed = 7.0f;
	public float StoppingDist = 2;

	[Header("Noise")]
	public Noise Noise;
	public float NoiseCheckRate = 0.5f;
	[HideInInspector]
	public float NoiseCheckTemp = 0;
	public float CurrentNoise;
	public float IdleNoise;
	public float WalkNoise;
	public float CreepUpNoise;
	public float SlowRunNoise;
	public float RunNoise;

	[Header("Stamina")]
	public float MaxStamina = 100;
	public float CurrentStamina = 100;
	public float StartRunAgain = 40;

	public float currentRegenStamina;

	public float DefaultRegenStamina = 5;
	public float SlowRegenStamina = 5;

	public float currentStaminaRunOut;

	public float StaminaRunOutDefault = 5;
	public float StaminaRunOutHurry = 11;

	[Header("RandomPoint")]
	public float MinRecalculateTime = 3;
	public float MaxRecalculateTime = 7;
	public float CurrentTimeToRecalculate = 5;
	public float TempRandomPointTime;

	void Start()
	{
		PointerReference = new GameObject ().transform;
		PointerReference.name = Parent.AnimalName + " Target";
		if (!TargetsRoot)
			return;
		PointerReference.SetParent (TargetsRoot);
	}

	public Transform CalculateRandomPoint()
	{
		if (Time.time > TempRandomPointTime + CurrentTimeToRecalculate) 
		{
			CurrentTimeToRecalculate = Random.Range (MinRecalculateTime, MaxRecalculateTime);
			if (Leader) 
			{
				if (Leader == Parent) 
				{
					Range = Random.Range (MinRange, MaxRange);
					var _CurrentRandPoint = Random.insideUnitSphere * Range;
					Vector3 _TempPoint = transform.position + _CurrentRandPoint;
					UnityEngine.AI.NavMeshHit hit;
					if (UnityEngine.AI.NavMesh.SamplePosition(_TempPoint, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas)) 
					{
						PointerReference.position = hit.position;
					}
				} else {
					Offset = Random.insideUnitSphere * AreaAroundLeader;
					PointerReference.position = Leader.transform.position + Offset + RandomPos;
					Range = Random.Range (MinRange, MaxRange);
					Vector3 randomPoint = Random.insideUnitSphere * Range;
					UnityEngine.AI.NavMeshHit hit;
					if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas)) 
					{
						RandomPos = hit.position;
					}
				
				}

			} else {
				Range = Random.Range (MinRange, MaxRange);
				var _CurrentRandPoint = Random.insideUnitSphere * Range;
				Vector3 _TempPoint = transform.position + _CurrentRandPoint;
				UnityEngine.AI.NavMeshHit hit;
				if (UnityEngine.AI.NavMesh.SamplePosition(_TempPoint, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas)) 
				{
					PointerReference.position = hit.position;
				}

			}
			TempRandomPointTime = Time.time;
		}
		return PointerReference;
	}

	private void Update()
	{
		if (canSwim)
		{
			Swimming ();
		}
		if (Time.time > NoiseCheckRate + NoiseCheckTemp) 
		{
			Noise.NoiseLevel = EmitNoise ();
			NoiseCheckTemp = Time.time;
		}

		if (!Leader)
			return;
		if (Time.time > CheckDistanceRate + TempCDR) 
		{
			if (Vector3.Distance (transform.position, Leader.transform.position) >LeaderInfluenceRange)
				Leader = null;
			TempCDR = Time.time;
		}

	
	}
	[HideInInspector]
	public bool canRun = false;

	public float Run()
	{
		float _speed = WalkSpeed;
		if (CurrentStamina > 0)
		{
			if (canRun)
			{
				CurrentStamina -= currentStaminaRunOut * Time.fixedDeltaTime;
			}
		}
		else
		{
			canRun = false;
		}

		if (CurrentStamina > 0)
		{
			if (canRun)
			{
				CurrentStamina -= currentStaminaRunOut * Time.fixedDeltaTime;
			}
		}
		else
		{
			canRun = false;
		}

		if (canRun)
		{
			_speed = RunSpeed;
		}
		else
		{
			_speed = SlowRunSpeed;
		}
		//WATER
		if (Parent.FoodStuff.CurrentWaterBar < Parent.FoodStuff.StaminaRunOutHurry)
		{
			currentStaminaRunOut = StaminaRunOutHurry;
		}
		else
		{
			currentStaminaRunOut = StaminaRunOutDefault;
		}
		//FOOD
		if (Parent.FoodStuff.CurrentFoodBar < Parent.FoodStuff.StaminaSlowRegen)
		{
			currentRegenStamina = SlowRegenStamina;
		}
		else
		{
			currentRegenStamina = DefaultRegenStamina;
		}

		if (!canRun && CurrentStamina > StartRunAgain)
		{
			canRun = true;
		}
		return _speed;
	}

	public float EmitNoise()
	{
		if (CurrentSpeed == 0f) {
			return IdleNoise;
		}
		if (CurrentSpeed == WalkSpeed) {
			return WalkNoise;
		}
		if (CurrentSpeed == CreepUpSpeed) {
			return CreepUpNoise;
		}
		if (CurrentSpeed == SlowRunSpeed) {
			return SlowRunNoise;
		}
		if (CurrentSpeed == RunSpeed) {
			return RunNoise;
		}
		return -1f;
	}
	#region Swimming
	public void Swimming()
	{
		CheckDeep ();
		if (NavMeshExtend.GetNavMeshAreaIndex (Parent.agent) == 8) 
		{
			if (_maxDeep > swimYOffset) 
			{
				inWater = true;
			} 
			else 
			{
				if(inWater)
					_exitFromWater = true;
				inWater = false;
			}
		} else {
			if (_maxDeep < swimYOffset) 
				inWater = false;
		}

		if (inWater) 
		{
			if (canSwimUnderSurface) 
			{
				if (_maxDeep > swimYOffset) 
				{
					if (Time.time > changeDeepRate + _changeDeepTemp) 
					{
						float tempDeep = Random.Range (_maxDeep,swimYOffset);

						if (tempDeep > _randomDeep) 
						{
							swimAngleUnderSurfaceTemp = swimAngleUnderSurface;
						} else {
							swimAngleUnderSurfaceTemp = -swimAngleUnderSurface;
						}
						_randomDeep = tempDeep;
						_changeDeepTemp = Time.time;
					}

					float step = SwimmingSpeed * Time.deltaTime;
					SmoothRotating (RotateSpeed, swimAngleUnderSurfaceTemp);
					if (MeshRoot.localPosition.y - MeshRoot.localScale.y/2 - 0.5f < -_maxDeep)
						MeshRoot.localPosition = Vector3.down * (_maxDeep - MeshRoot.localScale.y / 2f - 0.5f);
					else
						MeshRoot.localPosition = Vector3.MoveTowards (MeshRoot.localPosition,Vector3.down * (_randomDeep - MeshRoot.localScale.y / 2f - 0.5f),step);
				} else {
					MeshRoot.localEulerAngles = Vector3.right * _angle * _modifier;
				}


			}
			else 
			{

				MeshRoot.localPosition = Vector3.down * (Mathf.Clamp (_maxDeep, -swimYOffset, swimYOffset) - MeshRoot.localScale.y / 2f - 0.5f);
				if (_maxDeep > swimYOffset) 
				{
					SmoothRotating (RotateSpeed, swimAngleOnSurface);
				}
			}
		} 
		else 
		{
			MeshRoot.localPosition = Vector3.down * (_maxDeep - MeshRoot.localScale.y / 2f - 0.5f);
			if (_exitFromWater) 
			{
				SmoothRotating (RotateSpeed, _angle * _modifier);
			} 
			else
			{
				MeshRoot.localEulerAngles = Vector3.right * _angle * _modifier;
			}
		}
	}
	[HideInInspector]
	public float time;
	public void SmoothRotating(float speed,float newAngle)
	{
		if ((int)newAngle != (int)_targetAngle) 
		{
			time = 0;
			_targetAngle = newAngle;
		}
		if (_exitFromWater) 
		{
			if (time > 0.3f)
				_exitFromWater = false;
		}
		if (time < 1) 
		{
			time += speed * Time.deltaTime;
		} 
		MeshRoot.localEulerAngles = new Vector3( Mathf.LerpAngle (MeshRoot.localEulerAngles.x,_targetAngle,time),0,0);
	}

	public void CheckDeep()
	{
		RaycastHit hit0;
		RaycastHit hit1;
		if (Physics.Raycast (FirstRayPos.position, -transform.up, out hit0, 20,1))
		{
			Debug.DrawLine (FirstRayPos.position,hit0.point,Color.red);
		}
		if (Physics.Raycast (SecondRayPos.position, -transform.up, out hit1, 20,1))
		{
			Debug.DrawLine (SecondRayPos.position,hit1.point,Color.blue);
		}

		var heading = hit0.point - hit1.point;
		var distance = heading.magnitude;
		var direction = heading / distance;
		_angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));
		if (hit0.distance > hit1.distance) 
		{
			_modifier = 1.0f;
		} else {
			_modifier = -1.0f;
		}

		_maxDeep =  (hit0.distance + hit1.distance)/2;

	}

	#endregion
}
