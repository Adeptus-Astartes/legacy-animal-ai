/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public enum Behavior { Sleep, Idle, Walk, SearchFood, SearchWater, Battle, Defence, Null };

#region Behavior
[System.Serializable]
public class MovementSettings
{
    public float WalkSpeed = 5;
    public float CreepUpSpeed = 3;
    public float CurrentRunSpeed;
    public float DefaultRunSpeed = 8;
    public float SlowRunSpeed = 6;

    public float StoppingDist = 2;

    [Space(10)]
    public float changeDirRateMin;
    public float changeDirRateMax;
    public float currentDirRate;
    [HideInInspector]
    public float temp1;

    [Space(10)]
    public Vector2 targetScatter;

    [Space(10)]
    public float MaxStamina = 100;
    public float CurrentStamina = 100;
    public float StartRunAgain = 40;

    public float currentRegenStamina;

    public float DefaultRegenStamina = 5;
    public float SlowRegenStamina = 5;

    public float currentStaminaRunOut;

    public float StaminaRunOutDefault = 5;
    public float StaminaRunOutHurry = 11;

    [Space(10)]
    public Vector2 RunOutRandomDir = new Vector2(-5f, -5f);
    public float rate = 0.5f;
    [HideInInspector]
    public float temp;

    public Transform chaser;
}

[System.Serializable]
public class AnimatorParams
{
    public Animator Animator;
    [Header("Bools")]
    public List<string> Bools = new List<string>()
    {
        "isIdle", //0
		"isWalk", //1
		"isCreepUp", //2
		"isSlowRun", //3
		"isRun", //4
		"isRotating", //5
		"isEat", //6
		"isDrink", //7
		"isSleep", //8
		"isWarning",//9
		"isDead" //10
	};
    public string Idle = "isIdle";
    public string Walk = "isWalk";
    public string CreepUp = "isCreepUp";
    public string Run = "isRun";

    public string Rotating = "isRotating";
    public string Eat = "isEat";
    public string Drink = "isDrink";
    public string Sleep = "isSleep";
    public string Dead = "isDead";

    [Header("Floats")]
    public string TurnX = "TurnDirX";
    public string TurnZ = "TurnDirZ";

    public string HitX = "HitDirX";
    public string HitZ = "HitDirZ";

    [Header("Triggers")]
    public string Attack = "PlayAttack";
    public string Defence = "PlayDefence";
    public string Hit = "PlayHit";
    public string Jump = "PlayJump";
    [HideInInspector]
    public int tempId = 0;

    public void TurnBoolOn(string value)
    {
        for (int i = 0; i < Bools.Count; i++)
        {
            if (Bools[i] == value)
            {
                tempId = i;
            }
        }
        if (!Animator.GetBool(Bools[tempId]))
        {

            for (int i = 0; i < Bools.Count; i++)
            {
                if (i == tempId)
                    Animator.SetBool(Bools[i], true);
                else
                    Animator.SetBool(Bools[i], false);
            }
        }

    }

}


[System.Serializable]
public class MindSettings
{
    public float Health = 100;
    public float MinDamage = 5;
    public float MaxDamage = 9;
    public float AttackRate = 0.5f;
    public string DeadTag = "";
    [HideInInspector]
    public float attackRateTemp;
    [Space(10)]
    public bool isPredator = false;
    public bool freezeMetabolism = false;
    public bool NeedSleep = false;
    public bool Sleeping = false;

    public float MinSleepOffset = 4;
    public float MaxSleepOffset = 7;
    public float CurrentSleepOffset;
    [HideInInspector]
    public float SleepOffsetTemp;

    public float WakeUpOffset = 4;
    //public float MaxWakeUpOffset = 7;
    public float CurrentWakeUpOffset;
    [HideInInspector]
    public float WakeUpOffsetTemp;

    public bool returnToHome = false;
    [Header("Food")]
    public float MaxFoodBar = 3000;
    public float CurrentFoodBar = 3000;
    public float MinFoodBar = 1200;
    public float HungrySpeed;
    public float EatSpeed = 10;
    [HideInInspector]
    public float EatingTemp;

    [Space(10)]
    public float startSearchingFood;
    public bool needSearchFood = false;
    public float SlowRunWithoutFood;//Level Of Food Bar, when animal cant run

    [Header("Water")]
    public float MaxWaterBar = 3000;
    public float CurrentWaterBar = 3000;
    public float MinWaterBar = 1200;
    public float ThirstySpeed;
    public float DrinkSpeed;
    [Space(10)]
    public float startSearchingWater;
    public bool needSearchWater = false;
    public float StaminaRunOut;//Level Of Water Bar, when stamina run out hurry

    [Header("DayNightCycle")]
    public float whenSleep;
    public float whenWakeUp;

    [Header("Walk/Idle")]
    public float MinWalkTime;
    public float MaxWalkTime;
    public bool WalkCalculated = false;
    public float CurrentWalkTime;
    public float TempCurrentWalkTime;

    public float MinIdleTime;
    public float MaxnIdleTime;
    public bool IdleCalculated = false;
    public float CurrentIdleTime;
    public float TempCurrentIdleTime;

    [Header("Smells")]
    public string smellTag;
    public float smellRate;
    public GameObject smellPrefab;
    public float minSmellRadius;
    public float maxSmellRadius;
    [HideInInspector]
    public float tempSmellRate;
    public float minSmellDestroyTime;
    public float maxSmellDestroyTime;



    private AnimalAICore m_animal;
    [HideInInspector]
    public AnimalAICore animal
    {
        get
        {
            return m_animal;
        }
        set
        {
            m_animal = value;
        }
    }
    [Header("Prey")]
    public float StartRunOut = 10;
    public float EndRunOut = 20;
    public bool canDefence = false;
    public float warningDistance = 30.0f;
    public float attackDistance = 10.0f;
    public float damageDistance = 3.0f;
    public float eatDistance = 1.5f;

    [Header("Predator")]
    public float WarningStopTimeMin = 2;
    public float WarningStopTimeMax = 5;
    public float CurrentWarningStopTime;
    [HideInInspector]
    public float WarningStopTemp;

    public float CreepUpDist = 20;
    public float ChaseStartDist = 5;
    public float ChaseEndDist = 40;
}

[System.Serializable]
public class AnimalUnique
{
    public bool CalculateUnique = false;
    [Header("Movement")]
    public float MinStopDist = 1.5f;
    public float MaxStopDist = 3.0f;

    public float MinRNDchangeDirRateMin;
    public float MaxRNDchangeDirRateMin;

    public float MinRNDchangeDirRateMax;
    public float MaxRNDchangeDirRateMax;

    public float MinRndMaxStamina;
    public float MaxRndMaxStamina;

    public float MinDefaultRegenStamina;
    public float MaxDefaultRegenStamina;

    public float MinSlowRegenStamina;
    public float MaxSlowRegenStamina;

    public float MinStaminaRunOutDefault;
    public float MaxStaminaRunOutDefault;

    public float MinStaminaRunOutHurry;
    public float MaxStaminaRunOutHurry;

    [Header("MindSettings")]

    //Food
    [Space(10)]
    public float MinHungrySpeed;
    public float MaxHungrySpeed;

    public float MinFoodBar;
    public float MaxFoodBar;

    public float MinSearchFood;
    public float MaxSearchFood;

    public float MinWaterBar;
    public float MaxWaterBar;

    public float MinSearchWater;
    public float MaxSearchWater;

    public float MinThirstySpeed;
    public float MaxThirstySpeed;

    public float MinDrinkSpeed;
    public float MaxDrinkSpeed;

    public float MinSleepTime;
    public float MaxSleepTime;

    public float MinWakeUp;
    public float MaxWakeUp;

    public float MinStartRunOut;
    public float MaxStartRunOut;

    public float MinEndRunOut;
    public float MaxEndRunOut;

    public float MinAttackDist;
    public float MaxAttackDist;

    public float MinCreepUpDist;
    public float MaxCreepUpDist;

    public float MinChaseStartDist;
    public float MaxChaseStartDist;

    public void Calculate(AnimalAICore animal)
    {
        if (CalculateUnique)
        {
            //SetStopingDist
            animal.Movement.StoppingDist = Random.Range(MinStopDist, MaxStopDist);
            //SetRandom ChangeDirection
            animal.Movement.changeDirRateMin = Random.Range(MinRNDchangeDirRateMin, MaxRNDchangeDirRateMin);
            animal.Movement.changeDirRateMax = Random.Range(MinRNDchangeDirRateMax, MaxRNDchangeDirRateMax);
            //SetMaxStamina
            animal.Movement.MaxStamina = Random.Range(MinRndMaxStamina, MaxRndMaxStamina);
            animal.Movement.CurrentStamina = animal.Movement.MaxStamina;
            //SetStaminaRegenParams
            animal.Movement.DefaultRegenStamina = Random.Range(MinDefaultRegenStamina, MaxDefaultRegenStamina);
            animal.Movement.SlowRegenStamina = Random.Range(MinSlowRegenStamina, MaxSlowRegenStamina);
            //SetStaminaRunOut
            animal.Movement.StaminaRunOutDefault = Random.Range(MinStaminaRunOutDefault, MaxStaminaRunOutDefault);
            animal.Movement.StaminaRunOutHurry = Random.Range(MinStaminaRunOutHurry, MaxStaminaRunOutHurry);

            //Set "Food" Params
            animal.Mind.MaxFoodBar = Random.Range(MinFoodBar, MaxFoodBar);
            animal.Mind.HungrySpeed = Random.Range(MinHungrySpeed, MaxHungrySpeed);
            animal.Mind.startSearchingFood = Random.Range(MinSearchFood, MaxSearchFood);
            //Set "Water" Params
            animal.Mind.MaxWaterBar = Random.Range(MinWaterBar, MaxWaterBar);
            animal.Mind.ThirstySpeed = Random.Range(MinThirstySpeed, MaxThirstySpeed);
            animal.Mind.startSearchingWater = Random.Range(MinSearchWater, MaxSearchWater);
            animal.Mind.DrinkSpeed = Random.Range(MinDrinkSpeed, MaxDrinkSpeed);
            //SetDayNightCycle
            animal.Mind.whenSleep = Random.Range(MinSleepTime, MaxSleepTime);
            animal.Mind.whenWakeUp = Random.Range(MinWakeUp, MaxWakeUp);
            //HuntParams
            animal.Mind.StartRunOut = Random.Range(MinStartRunOut, MaxStartRunOut);
            animal.Mind.EndRunOut = Random.Range(MinEndRunOut, MaxEndRunOut);
            animal.Mind.attackDistance = Random.Range(MinAttackDist, MaxAttackDist);
            animal.Mind.CreepUpDist = Random.Range(MinCreepUpDist, MaxCreepUpDist);
            animal.Mind.ChaseStartDist = Random.Range(MinChaseStartDist, MaxChaseStartDist);
        }
    }
}

[System.Serializable]
public class Sensors
{
    public Hear hearing;
    public float normalRadius = 15;
    public float extendRadius = 40;

    public bool canLookTroughtWall = true;
    public MeshCollider eyes;

    [Header("Optional")]
    public SmellDetector smellDetector;
}

[System.Serializable]
public class Memory
{
    public List<string> foodTags;
    public List<Transform> foodAround;
    public Transform nearestFood;
    public bool targetLocked = false;
    [Space(10)]
    public List<string> enemyTags;
    public List<Transform> enemiesAround;
    public Transform nearestEnemy;
    [Space(10)]
    public string waterTag;
    public List<Transform> waterPoints;
    public Transform nearestWaterPoint;
    [Space(10)]
    public string homeTag = "Home";
    public Transform home;
    [Space(10)]
    public List<string> smellTags;
    public List<SmellPoint> smellPoints;
}

#endregion
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalAICore : MonoBehaviour
{
    public string AnimalName = "EnterName";

    public Vector3 Target;

    public MovementSettings Movement;
    public AnimatorParams AnimatorParams;
    public MindSettings Mind;
    public AnimalUnique Unique;
    public Sensors Sensors;
    public Memory Memory;
    private NavMeshAgent m_agent;

    public void Awake()
    {
        Mind.animal = this;
        Unique.Calculate(this);
    }

    public void FixedUpdate()
    {
        Life();
    }

    [HideInInspector]
    public bool SleepOffset = false;
    [HideInInspector]
    public bool WakeOffset = false;

    public void Life()
    {

        if (!Mind.freezeMetabolism)
        {
            Hungry();
            Thirsty();
        }

        if (Movement.CurrentStamina < Movement.MaxStamina)
            Movement.CurrentStamina += Movement.currentRegenStamina * Time.fixedDeltaTime;

        if (!Mind.isPredator)
        {
            if (Movement.chaser == null)
            {
                if (AnimalController.Instance.CurrentTime > Mind.whenSleep && AnimalController.Instance.CurrentTime < Mind.whenWakeUp)
                {
                    if (!TaskManager.ContainsTask(Behavior.Sleep))
                        TaskManager.AddTask(Behavior.Sleep);
                }
                else
                {
                    if (TaskManager.ContainsTask(Behavior.Sleep))
                    {
                        if (!WakeOffset)
                        {
                            Mind.WakeUpOffsetTemp = Time.time;
                            WakeOffset = true;
                            AnimatorParams.Animator.SetBool(AnimatorParams.Sleep, false);
                        }

                        if (Time.time > Mind.WakeUpOffsetTemp + Mind.WakeUpOffset)
                        {
                            WakeUp();
                        }
                    }

                }
            }
        }
        else
        {
            if (!Memory.targetLocked)
            {
                if (AnimalController.Instance.CurrentTime > Mind.whenSleep && AnimalController.Instance.CurrentTime < Mind.whenWakeUp)
                {
                    if (!SleepOffset)
                    {
                        Mind.SleepOffsetTemp = Time.time;
                        Mind.CurrentSleepOffset = Random.Range(Mind.MinSleepOffset, Mind.MaxSleepOffset);
                        SleepOffset = true;
                    }
                    if (Time.time > Mind.SleepOffsetTemp + Mind.CurrentSleepOffset)
                    {
                        TaskManager.AddTask(Behavior.Sleep);
                    }

                }
                else
                {
                    if (TaskManager.ContainsTask(Behavior.Sleep))
                    {
                        if (!WakeOffset)
                        {
                            Mind.WakeUpOffsetTemp = Time.time;
                            //Mind.CurrentSleepOffset = Random.Range (Mind.MinSleepOffset,Mind.MaxSleepOffset);
                            WakeOffset = true;
                            AnimatorParams.Animator.SetBool(AnimatorParams.Sleep, false);
                        }
                        if (Time.time > Mind.WakeUpOffsetTemp + Mind.WakeUpOffset)
                        {
                            WakeUp();
                        }
                    }
                }
            }
        }

        if (!Mind.isPredator)
        {
            if (Movement.chaser != null)
            {
                PrepareForBattle(Movement.chaser);
            }
            /*else 
			{
				if(TaskManager.ContainsTask(Behavior.Battle))
					TaskManager.CompleteTask (Behavior.Battle);
			}
        }

        if (Mind.needSearchWater && !Memory.targetLocked)
        {
            TaskManager.AddTask(Behavior.SearchWater);
        }
        if (Mind.needSearchFood)
        {
            TaskManager.AddTask(Behavior.SearchFood);
        }

    }

    public void ProceedTasks()
    {
        switch (TaskManager.activeTask[0].name)
        {
            case Behavior.Sleep:
                Sleep();
                break;
            case Behavior.Idle:
                Idle();
                break;
            case Behavior.Walk:
                Walk();
                break;
            case Behavior.SearchFood:
                SearchinFood();
                break;
            case Behavior.SearchWater:
                SearchinWater();
                break;
            case Behavior.Battle:
                Battle();
                break;
        }
    }

    #region MOVEMENT

    public void Idle()
    {
        AnimatorParams.TurnBoolOn("isIdle");

        if (!Mind.WalkCalculated)
        {

            m_agent.Stop();
            Mind.CurrentWalkTime = Random.Range(Mind.MinWalkTime, Mind.MaxWalkTime);
            Mind.WalkCalculated = true;
            Mind.IdleCalculated = false;
        }

        if (Time.time > Mind.TempCurrentWalkTime + Mind.CurrentWalkTime)
        {
            m_agent.Resume();
            TaskManager.AddTask(Behavior.Walk);
            TaskManager.CompleteTask(Behavior.Idle);
            Mind.TempCurrentIdleTime = Time.time;
        }



    }

    public void Walk()
    {

        Move(Movement.WalkSpeed);
        AnimatorParams.TurnBoolOn("isWalk");

        if (!Mind.IdleCalculated)
        {
            Mind.CurrentIdleTime = Random.Range(Mind.MinIdleTime, Mind.MaxnIdleTime);
            Mind.IdleCalculated = true;
            Mind.WalkCalculated = false;

        }

        if (Time.time > Mind.TempCurrentIdleTime + Mind.CurrentIdleTime)
        {
            TaskManager.AddTask(Behavior.Idle);
            TaskManager.CompleteTask(Behavior.Walk);
            Mind.TempCurrentIdleTime = Time.time;
        }


        if (Vector3.Distance(transform.position, Target) < Movement.StoppingDist)
        {
            ChangeDirection();
        }
        if (Time.time > Movement.currentDirRate + Movement.temp1)
        {
            ChangeDirection();
            Movement.currentDirRate = Random.Range(Movement.changeDirRateMin, Movement.changeDirRateMax);
            Movement.temp1 = Time.time;
        }

        if (Time.time > Mind.smellRate + Mind.tempSmellRate)
        {
            GameObject tempSmellPoint = Instantiate(Mind.smellPrefab, transform.position, Quaternion.identity) as GameObject;
            tempSmellPoint.transform.localScale = Vector3.one * Random.Range(Mind.minSmellRadius, Mind.maxSmellRadius);
            tempSmellPoint.tag = Mind.smellTag;
            tempSmellPoint.AddComponent<SmellPoint>().parent = this;
            Destroy(tempSmellPoint, Random.Range(Mind.minSmellDestroyTime, Mind.maxSmellDestroyTime));
            Mind.tempSmellRate = Time.time;
        }
    }

    bool canRun = false;
    public void Run()
    {

        if (Movement.CurrentStamina > 0)
        {
            if (canRun)
            {
                Movement.CurrentStamina -= Movement.currentStaminaRunOut * Time.fixedDeltaTime;
            }
        }
        else
        {
            canRun = false;
        }

        if (canRun)
        {
            AnimatorParams.TurnBoolOn("isRun");
            Movement.CurrentRunSpeed = Movement.DefaultRunSpeed;
        }
        else
        {
            AnimatorParams.TurnBoolOn("isSlowRun");
            Movement.CurrentRunSpeed = Movement.SlowRunSpeed;
        }
        //WATER
        if (Mind.CurrentWaterBar < Mind.MinWaterBar)
        {
            Movement.currentStaminaRunOut = Movement.StaminaRunOutHurry;
        }
        else
        {
            Movement.currentStaminaRunOut = Movement.StaminaRunOutDefault;
        }
        //FOOD
        if (Mind.CurrentFoodBar < Mind.MinFoodBar)
        {
            Movement.currentRegenStamina = Movement.SlowRegenStamina;
        }
        else
        {
            Movement.currentRegenStamina = Movement.DefaultRegenStamina;
        }

        if (!canRun && Movement.CurrentStamina > Movement.StartRunAgain)
        {
            canRun = true;
        }

        Move(Movement.CurrentRunSpeed);
    }

    public void ChangeDirection()
    {
        var currentRandPoint = new Vector2(Random.Range(-Movement.targetScatter.x, Movement.targetScatter.x), Random.Range(-Movement.targetScatter.y, Movement.targetScatter.y));
        Target = new Vector3(transform.position.x + currentRandPoint.x, transform.position.y, transform.position.z + currentRandPoint.y);
        BuildPathTo(Target);
    }

    private NavMeshPath path;
    public void BuildPathTo(Vector3 _target)
    {
        path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, _target, NavMesh.AllAreas, path);

        if (path.corners.Length == 0 || path.status == NavMeshPathStatus.PathPartial)
        {
            //Debug.Log ("REBUILD");
            _target = new Vector3(transform.position.x + Random.Range(-Movement.targetScatter.x, Movement.targetScatter.x), transform.position.y, transform.position.z + Random.Range(-Movement.targetScatter.y, Movement.targetScatter.y));
            NavMesh.CalculatePath(transform.position, _target, NavMesh.AllAreas, path);
        }
        m_agent.SetPath(path);
    }

    public void Move(float speed)
    {
        m_agent.SetDestination(Target);
        m_agent.speed = speed;
        TurnAnimations();
    }

    public void Move(float speed, Vector3 pos)
    {
        m_agent.SetDestination(pos);
        m_agent.speed = speed;
        TurnAnimations();
    }

    public void Move(float speed, NavMeshPath path)
    {
        m_agent.speed = speed;
        m_agent.SetPath(path);
        TurnAnimations();
    }

    [HideInInspector]
    public string lastAngle;
    public void TurnAnimations()
    {
        if (m_agent.path.corners.Length > 1)
        {
            var heading = m_agent.path.corners[1] - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));

            if (angle.ToString("0") == lastAngle)
            {
                AnimatorParams.Animator.SetBool(AnimatorParams.Rotating, false);
            }
            else
            {

                if (direction.x > 0.3f || direction.x < -0.3f || direction.z > 0.3f || direction.z < -0.3f)
                {
                    AnimatorParams.Animator.SetBool(AnimatorParams.Rotating, true);
                    AnimatorParams.Animator.SetFloat(AnimatorParams.TurnX, direction.x);
                    AnimatorParams.Animator.SetFloat(AnimatorParams.TurnZ, direction.z);
                }
                lastAngle = angle.ToString("0");
            }

        }

    }

    #endregion

    public void Hungry()
    {
        Mind.CurrentFoodBar -= Mind.HungrySpeed * Time.fixedDeltaTime;
        if (Mind.CurrentFoodBar < Mind.startSearchingFood)
        {
            Mind.needSearchFood = true;
            if (!Mind.NeedSleep)
            {
                if (!TaskManager.ContainsTask(Behavior.SearchFood))
                {
                    TaskManager.AddTask(Behavior.SearchFood);
                }
            }
        }
        if (Mind.CurrentFoodBar > Mind.MaxFoodBar)
        {
            if (!TaskManager.ContainsTask(Behavior.Walk))
            {
                TaskManager.AddTask(Behavior.Walk);
            }
            AnimatorParams.TurnBoolOn("isWalk");
            Memory.targetLocked = false;
            Mind.needSearchFood = false;
            TaskManager.CompleteTask(Behavior.SearchFood);
            Memory.foodAround.Remove(Memory.nearestFood);
            Memory.nearestFood = null;

        }
    }

    public void Thirsty()
    {
        Mind.CurrentWaterBar -= Mind.ThirstySpeed * Time.fixedDeltaTime;
        if (Mind.CurrentWaterBar < Mind.startSearchingWater)
        {
            if (!Mind.needSearchFood)
                Mind.needSearchWater = true;
            if (!Mind.NeedSleep && !Memory.targetLocked)
            {
                if (!TaskManager.ContainsTask(Behavior.SearchWater))
                {
                    TaskManager.AddTask(Behavior.SearchWater);
                }
            }
        }
        if (Mind.CurrentWaterBar >= Mind.MaxWaterBar)
        {
            if (!TaskManager.ContainsTask(Behavior.Walk))
            {
                TaskManager.AddTask(Behavior.Walk);
            }
            AnimatorParams.TurnBoolOn("isWalk");
            Mind.needSearchWater = false;
            TaskManager.CompleteTask(Behavior.SearchWater);
        }
    }

    public void SearchinFood()
    {
        if (Memory.foodAround.Count > 0)
        {

            float nearestDist = float.PositiveInfinity;
            if (!Memory.targetLocked)
            {
                foreach (Transform food in Memory.foodAround)
                {
                    if (food)
                    {
                        if (Vector3.Distance(transform.position, food.position) < nearestDist)
                        {
                            nearestDist = Vector3.Distance(transform.position, food.position);
                            Memory.nearestFood = food;
                        }
                    }
                }

                Memory.targetLocked = true;
            }
            if (!Mind.isPredator)
            {

                if (Vector3.Distance(transform.position, Memory.nearestFood.position) < Movement.StoppingDist)
                {
                    Food food = Memory.nearestFood.GetComponent<Food>();
                    food.OnEat(this);
                }
                else
                {
                    Move(Movement.WalkSpeed, Memory.nearestFood.position);
                    AnimatorParams.TurnBoolOn("isWalk");
                }
            }
            else
            {
                Chase();
            }
        }
        else
        {
            Walk();
        }

    }
    public void EatFood(Food food)
    {
        AnimatorParams.TurnBoolOn("isEat");

        Mind.CurrentFoodBar += Mind.EatSpeed * Time.fixedDeltaTime;
        if ((Mind.CurrentFoodBar + 50) > Mind.MaxFoodBar)
        {
            //food.gameObject.tag = Mind.DieTag;
        }
        food.capacity -= Mind.EatSpeed * Time.fixedDeltaTime;
        if (food.capacity < 0)
        {
            AnimalController.Instance.RemoveGreenFood(food.transform);
            AnimalController.Instance.RemoveMeatFood(food.transform);
            FoodPlacer.Instance.AddFood(food.type);
            Destroy(food.gameObject);
            Memory.targetLocked = false;
            if (food.GetComponent<AnimalAICore>())
            {
                food.GetComponent<AnimalAICore>().DestroyBody();
            }
        }
        else
        {

        }
    }

    public void SearchinWater()
    {
        if (Memory.waterPoints.Count > 0)
        {
            float nearestDist = float.PositiveInfinity;
            foreach (Transform waterPoint in Memory.waterPoints)
            {
                if (Vector3.Distance(transform.position, waterPoint.position) < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, waterPoint.position);
                    Memory.nearestWaterPoint = waterPoint;
                }
            }

            if (Memory.nearestWaterPoint)
            {
                Move(Movement.WalkSpeed, Memory.nearestWaterPoint.position);
                if (Vector3.Distance(transform.position, Memory.nearestWaterPoint.position) < Movement.StoppingDist)
                {
                    AnimatorParams.TurnBoolOn("isDrink");

                    Mind.CurrentWaterBar += Mind.DrinkSpeed * Time.fixedDeltaTime;
                    if (Mind.CurrentWaterBar > Mind.MaxWaterBar)
                    {
                        TaskManager.CompleteTask(Behavior.SearchWater);
                    }
                }
                else
                {
                    if (!TaskManager.ContainsTask(Behavior.Walk))
                    {
                        TaskManager.AddTask(Behavior.Walk);
                    }
                    AnimatorParams.TurnBoolOn("isWalk");
                }
            }
        }
        else
        {
            Walk();
        }
    }

    public void Sleep()
    {

        if (!Mind.Sleeping)
        {
            if (Mind.returnToHome && Memory.home != null)
            {
                ReturningToHome();
            }
            else
            {
                AnimatorParams.TurnBoolOn("isSleep");
                Mind.NeedSleep = true;
                Mind.freezeMetabolism = true;
                Sensors.eyes.gameObject.SetActive(false);
                Sensors.hearing.ChangeRadius(Sensors.extendRadius);
                Mind.Sleeping = true;
                m_agent.Stop();
            }
        }
    }

    public void ReturningToHome()
    {
        if (Target != Memory.home.position)
        {
            Target = Memory.home.position;
        }
        if (Vector3.Distance(transform.position, Target) < Movement.StoppingDist)
        {
            AnimatorParams.TurnBoolOn("isSleep");
            Mind.NeedSleep = true;
            Mind.freezeMetabolism = true;
            Sensors.eyes.gameObject.SetActive(false);
            Sensors.hearing.ChangeRadius(Sensors.extendRadius);
            Mind.Sleeping = true;
            m_agent.Stop();
        }
        else
        {
            Move(Movement.WalkSpeed);
            AnimatorParams.TurnBoolOn("isWalk");
        }
    }

    public void WakeUp()
    {
        Mind.NeedSleep = false;
        Mind.freezeMetabolism = false;
        Mind.Sleeping = false;
        SleepOffset = false;
        WakeOffset = false;
        Sensors.eyes.gameObject.SetActive(true);
        Sensors.hearing.ChangeRadius(Sensors.normalRadius);
        m_agent.Resume();

        TaskManager.CompleteTask(Behavior.Sleep);
    }

    #region Battle

    public void PrepareForBattle(Transform _chaser)
    {
        Movement.chaser = _chaser;
        WakeUp();
        if (!TaskManager.ContainsTask(Behavior.Battle))
        {
            TaskManager.AddTask(Behavior.Battle);
        }
    }

    public void Chase()
    {
        if (!Memory.nearestFood)
            return;

        float Distance = Vector3.Distance(transform.position, Memory.nearestFood.position);

        if (Memory.nearestFood)
        {
            if (Target != Memory.nearestFood.position)
            {
                Target = Memory.nearestFood.position;
            }
            Move(Movement.WalkSpeed);
            AnimatorParams.TurnBoolOn("isWalk");
            if (Distance < Mind.damageDistance)
            {
                if (Time.time > Mind.AttackRate + Mind.attackRateTemp)
                {
                    AnimatorParams.Animator.SetTrigger(AnimatorParams.Attack);
                    Memory.nearestFood.GetComponent<AnimalAICore>().GetDamage(Random.Range(Mind.MinDamage, Mind.MaxDamage), this);

                    Mind.attackRateTemp = Time.time;
                }

            }
        }

        if (Distance > Mind.CreepUpDist)
        {
            Move(Movement.CreepUpSpeed);
            AnimatorParams.TurnBoolOn("isCreepUp");
        }
        else if (Distance < Mind.ChaseStartDist)
        {
            if (Distance < Mind.eatDistance && !Memory.nearestFood.GetComponent<AnimalAICore>().enabled)
            {
                m_agent.speed = 0;
                Food food = Memory.nearestFood.GetComponent<Food>();
                food.OnEat(this);
            }
            else
            {
                Memory.nearestFood.SendMessage("PrepareForBattle", transform);
                Run();
            }

        }
        else if (Distance > Mind.ChaseEndDist)
        {
            //Find new Target
            Memory.targetLocked = false;
            if (TaskManager.ContainsTask(Behavior.Battle))
            {
                TaskManager.CompleteTask(Behavior.Battle);
            }
        }

    }

    public void Battle()
    {
        if (!Movement.chaser)
            return;
        float DistanceToEnemy = Vector3.Distance(transform.position, Movement.chaser.position);
        if (!Mind.canDefence)
        {
            if (DistanceToEnemy < Mind.EndRunOut)
            {
                if (Time.time > Movement.rate + Movement.temp)
                {
                    var heading = Movement.chaser.position - transform.position + new Vector3(Random.Range(-Movement.RunOutRandomDir.x, Movement.RunOutRandomDir.x), 0, Random.Range(-Movement.RunOutRandomDir.y, Movement.RunOutRandomDir.y));
                    var distance = heading.magnitude;
                    var direction = heading / distance;

                    Target = transform.position - direction * 5f;

                    Movement.temp = Time.time;
                }

                Run();
                AnimatorParams.TurnBoolOn("isRun");
            }
            else
            {
                TaskManager.CompleteTask(Behavior.Battle);
                Movement.chaser = null;
                if (!TaskManager.ContainsTask(Behavior.Walk))
                {
                    TaskManager.AddTask(Behavior.Walk);
                }
                AnimatorParams.TurnBoolOn("isWalk");
            }
        }
        else
        {
            if (DistanceToEnemy < Mind.warningDistance)
            {
                if (DistanceToEnemy < Mind.attackDistance)
                {
                    if (DistanceToEnemy < Mind.damageDistance)
                    {
                        if (Time.time > Mind.AttackRate + Mind.attackRateTemp)
                        {
                            //PlayAttack Animation
                            AnimatorParams.Animator.SetTrigger(AnimatorParams.Attack);

                            //Send Damage
                            if (Movement.chaser.GetComponent<AnimalAICore>())
                                Movement.chaser.GetComponent<AnimalAICore>().GetDamage(Random.Range(Mind.MinDamage, Mind.MaxDamage), this);
                            //if (Movement.chaser.GetComponent<PlayerStats>())
                            //    Movement.chaser.GetComponent<PlayerStats>().GetDamage(Random.Range(Mind.MinDamage, Mind.MaxDamage), this);
                            //Obsolete
                            //Movement.chaser.SendMessage ("GetDamage",Random.Range(Mind.MinDamage,Mind.MaxDamage));

                            Mind.attackRateTemp = Time.deltaTime;
                        }
                    }
                    else
                    {
                        Run();
                        AnimatorParams.TurnBoolOn("isRun");
                        if (Target != Movement.chaser.position)
                            Target = Movement.chaser.position;
                        //m_agent.SetDestination (Movement.chaser.position);

                    }
                }
                else
                {
                    Movement.chaser.SendMessage("GetWarningFrom", this, SendMessageOptions.DontRequireReceiver);
                    AnimatorParams.TurnBoolOn("isWarning");
                    Move(0);
                }
            }
            else
            {
                Movement.chaser.SendMessage("ExitWarning", this, SendMessageOptions.DontRequireReceiver);
                TaskManager.CompleteTask(Behavior.Battle);
                Movement.chaser = null;
            }
        }
    }

    #endregion
    [HideInInspector]
    public bool WarningCalculated = false;
    //[HideInInspector]
    //public bool WarningRunOut = false;
    #region EVENTS
    public void GetWarningFrom(AnimalAICore animal)
    {
        if (TaskManager.ContainsTask(Behavior.Sleep))
        {

            Memory.targetLocked = true;
            WakeUp();
        }

        if (!WarningCalculated)
        {
            Mind.CurrentWarningStopTime = Random.Range(Mind.WarningStopTimeMin, Mind.WarningStopTimeMax);
            Mind.WarningStopTemp = Time.time;
            WarningCalculated = true;
        }
        if (Time.time > Mind.WarningStopTemp + Mind.CurrentWarningStopTime)
        {
            //int dice = Random.Range (0,2);
            //if (dice == 0) {
            Movement.chaser = animal.transform;
            if (!TaskManager.ContainsTask(Behavior.Battle))
            {
                TaskManager.CompleteTask(Behavior.Battle);
            }
            //} else {
            //Movement.chaser = animal.transform;
            //if (TaskManager.ContainsTask (Behavior.Battle)) {
            //	TaskManager.CompleteTask (Behavior.Battle);
            //}
            //}
        }
        else
        {
            AnimatorParams.TurnBoolOn("isWarning");
            Move(0);
        }

    }

    public void ExitWarning(AnimalAICore animal)
    {
        Memory.targetLocked = false;
    }

    public void GetDamage(float damage, AnimalAICore attacker)
    {
        var heading = attacker.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; //normalized

        AnimatorParams.Animator.SetFloat(AnimatorParams.HitX, direction.x);
        AnimatorParams.Animator.SetFloat(AnimatorParams.HitZ, direction.z);
        Mind.Health -= damage;
        if (Mind.Health < 0)
        {
            gameObject.tag = Mind.DeadTag;
            Dead();
        }
    }

    public void FoundHome(Home newHome)
    {
        if (Memory.home == null && newHome.Owner == null)
        {
            Memory.home = newHome.transform;
            Mind.returnToHome = true;
            newHome.Owner = transform;
        }
    }

    public void FoundFood(Transform foodTransform)
    {
        if (!Memory.foodAround.Contains(foodTransform))
        {
            Memory.foodAround.Add(foodTransform);
        }
    }

    public void FoundWater(Transform waterTransform)
    {
        if (!Memory.waterPoints.Contains(waterTransform))
        {
            Memory.waterPoints.Add(waterTransform);
        }
    }

    public void FoundSmell(SmellPoint smellPoint)
    {
        if (!Memory.smellPoints.Contains(smellPoint))
        {
            Memory.smellPoints.Add(smellPoint);
            if (Memory.foodTags.Contains(smellPoint.tag))
                Memory.foodAround.Add(smellPoint.parent.gameObject.transform);
            if (Memory.enemyTags.Contains(smellPoint.tag))
                Memory.enemiesAround.Add(smellPoint.parent.gameObject.transform);
            /*if (Mind.isPredator)
			{
				Memory.foodAround.Add (smellPoint.parent.gameObject.transform);
			}
        }
    }

    public void DetectEnemy(Transform enemy)
    {
        if (!Memory.enemiesAround.Contains(enemy))
        {
            Memory.enemiesAround.Add(enemy);

        }
        PrepareForBattle(enemy);
    }

    #endregion

    private void Dead()
    {
        AnimatorParams.TurnBoolOn("isDead");
        this.enabled = false;
        this.m_agent.enabled = false;

    }
    private void DestroyBody()
    {
        if (Memory.home != null)
            Memory.home.GetComponent<Home>().Owner = null;
        if (Mind.isPredator)
            AnimalController.Instance.RemovePredator(this);
        else
            AnimalController.Instance.RemoveGreen(this);

    }

}
*/