using UnityEngine;

/* This is an example script using A* pathfinding to chase a
 * target game object*/

public class Chase : MonoBehaviour
{

	// Target of the chase
	// (initialise via the Inspector Panel)
	public GameObject target = null;

	private GameObject purp = null;

	// Chaser's speed
	// (initialise via the Inspector Panel)
	public float baseSpeed;

	public float speed;
	private float maxSpeed;

	// Chasing game object must have a AStarPathfinder component - 
	// this is a reference to that component, which will get initialised
	// in the Start() method
	private AStarPathfinder pathfinder = null;

	private EnemyRange er;

	// Use this for initialization
	void Start()
	{
		//Get the reference to object's AStarPathfinder component
		pathfinder = gameObject.GetComponent<AStarPathfinder>();
		maxSpeed = speed = baseSpeed;
		er = gameObject.GetComponentInChildren<EnemyRange>();
	}

	// Freezes enemy when activated
	void HitByRedRay(GameObject light)
	{
		er.hitByLight = true;
		purp = light;
		if (speed > 0)
		{
			speed -= (maxSpeed / 100);
			baseSpeed = speed;
		}
		if (pathfinder != null)
		{
			pathfinder.GoTowards(target, speed);
		}
	}

	// Slows enemy when activated
	void HitByYellowRay(GameObject light)
	{
		er.hitByLight = true;
		purp = light;
		speed = baseSpeed / 8;
		if (pathfinder != null)
		{
			pathfinder.GoTowards(target, speed);
		}
	}

	// Repels enemy when activated
	void HitByUVRay(GameObject light)
	{
		er.hitByLight = true;
		speed = -baseSpeed / 4;
		purp = light;
		if (pathfinder != null)
		{
			pathfinder.GoTowards(purp, speed);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (er.inRange && pathfinder == null)
		{
			pathfinder = gameObject.GetComponent<AStarPathfinder>();
		}
		if (pathfinder != null && purp == null)
		{
			//Travel towards the target object at certain speed.
			pathfinder.GoTowards(target, speed);
		}
		if (purp == null)
		{
			if (baseSpeed < maxSpeed)
			{
				baseSpeed += (maxSpeed / 25);
			}
			speed = baseSpeed;
		}
	}
}