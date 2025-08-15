using UnityEngine;
using System.Collections;

/* This is an example script using A* pathfinding to chase a
 * target game object*/

public class Chase : MonoBehaviour {

	// Target of the chase
	// (initialise via the Inspector Panel)
	public GameObject target = null;

	private GameObject purp = null;

	// Chaser's speed
	// (initialise via the Inspector Panel)
	public float baseSpeed;

	private float speed;

	// Chasing game object must have a AStarPathfinder component - 
	// this is a reference to that component, which will get initialised
	// in the Start() method
	private AStarPathfinder pathfinder = null;

	private EnemyRange er;

	// Use this for initialization
	void Start () {
		//Get the reference to object's AStarPathfinder component
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		speed = baseSpeed;
		er = GetComponentInChildren<EnemyRange>();
	}

	// Freezes enemy when activated
	void HitByRedRay(GameObject light) {
		purp = light;
		speed = 0;
		if (pathfinder != null)
		{
			pathfinder.GoTowards(target, speed);
		}
	}

	// Slows enemy when activated
	void HitByYellowRay(GameObject light) {
		purp = light;
		speed = baseSpeed/4;
		if (pathfinder != null)
        {
			pathfinder.GoTowards(target, speed);
		}
	}

	// Repels enemy when activated
	void HitByUVRay(GameObject light) {
		speed = -baseSpeed;
		purp = light;
		if (pathfinder != null)
		{
			pathfinder.GoTowards(purp, speed);
		}
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (er.inRange && pathfinder == null)
		{
			pathfinder = transform.GetComponent<AStarPathfinder>();
		}
		if (pathfinder != null && purp == null) {
			//Travel towards the target object at certain speed.
			pathfinder.GoTowards(target, speed);
		}
		if (purp == null) {
			speed = baseSpeed;
		}
	}
}
