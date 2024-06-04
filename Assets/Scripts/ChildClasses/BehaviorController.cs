using UnityEngine;
using System.Collections;

public abstract class BehaviorController : MonoBehaviour
{
	public abstract void Idle ();
	public abstract void Walk ();
	public abstract void SearchFood();
	public abstract void SearchWater();
}
