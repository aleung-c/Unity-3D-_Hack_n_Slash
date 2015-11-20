using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {
	public	int						nb_spawn = 3;
	public	List<GameObject>		prefab_list = new List<GameObject> ();
	private	int						_cur_enemy_spawn = 0;
	public	float					cycle_delay = 10;


	public void generate_enemy() {
		int prefab_rand = Random.Range (0, prefab_list.Count);

		Vector3 spawn_pos = transform.position;
		spawn_pos.x += Random.Range (0.0F, 5.0F);
		spawn_pos.y += Random.Range (0.0F, 5.0F);

		GameObject new_enemy = (GameObject)Instantiate (prefab_list [prefab_rand], spawn_pos, Quaternion.identity);
	}

	IEnumerator spawn_routine () {
		for (;;) {
			float rand_time = Random.Range(3.0F, 8.0F);
			if (_cur_enemy_spawn < nb_spawn) {
				generate_enemy();
				_cur_enemy_spawn += 1;
			}
			if (_cur_enemy_spawn == nb_spawn) {
				rand_time = cycle_delay;
				_cur_enemy_spawn = 0;
			}
			yield return new WaitForSeconds(rand_time);
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (spawn_routine ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
