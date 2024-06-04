using UnityEngine;
using System.Collections;

public class FoodStuff : MonoBehaviour
{
	[HideInInspector]
	public AnimalAICore Parent;
	public float Health;

	[Header("Food")]
	public float MaxFoodBar;
	public float CurrentFoodBar;
	public float StartSearchFood;
	[Tooltip("Food Level When Animal Stamina Regen Very Slow")]
	public float StaminaSlowRegen;

	public float HungrySpeed;
	public float EatSpeed;

	public bool SearchFood = false;

	[Header("Water")]
	public float MaxWaterBar;
	public float CurrentWaterBar;
	public float StartSearchWater;
	[Tooltip("Water Level When Animal Using Stamina Very Fast")]
	public float StaminaRunOutHurry;

	public float ThirstySpeed;
	public float DrinkSpeed;

	public bool SearchWater = false;



	public void Eat()
	{
		CurrentFoodBar += Time.fixedDeltaTime * EatSpeed;
	}

	public void Hungry()
	{
		if (CurrentFoodBar > MaxFoodBar)
			SearchFood = false;
		if (CurrentFoodBar < StartSearchFood)
			SearchFood = true;

		CurrentFoodBar -= Time.fixedDeltaTime * HungrySpeed;
	}

	public void CheckHungry()
	{
		if (CurrentFoodBar > MaxFoodBar) {
			Parent.TaskManager.DoneTask (Behavior.SearchFood);
		}
		if (CurrentFoodBar < StartSearchFood) {
			Parent.TaskManager.AddTask (Behavior.SearchFood);
		}
	}

	public void Drink()
	{
		CurrentWaterBar += Time.fixedDeltaTime * DrinkSpeed;
	}

	public void Thirsty()
	{
		if (CurrentWaterBar > MaxWaterBar)
			SearchWater = false;
		if (CurrentWaterBar < StartSearchWater)
			SearchWater = true;

		CurrentWaterBar -= Time.fixedDeltaTime * ThirstySpeed;
	}

	public void CheckThirsty()
	{
		if (CurrentWaterBar > MaxWaterBar) {
			Parent.TaskManager.DoneTask (Behavior.SearchWater);
		}
		if (CurrentWaterBar < StartSearchWater) {
			Parent.TaskManager.AddTask (Behavior.SearchWater);
		}
	}


}
