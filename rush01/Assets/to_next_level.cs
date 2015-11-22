using UnityEngine;
using System.Collections;

public class to_next_level : MonoBehaviour {

	public int 			door_id;
	public GameObject	next_door;
	public GameObject	sunlight;

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "player") {
			Quaternion rot_sun = sunlight.transform.rotation; 
			rot_sun.x -= 0.2F;
			sunlight.transform.rotation = rot_sun;

			col.gameObject.GetComponent<NavMeshAgent> ().enabled = false;
			col.gameObject.transform.position = next_door.transform.position;
			col.gameObject.GetComponent<NavMeshAgent> ().enabled = true;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
