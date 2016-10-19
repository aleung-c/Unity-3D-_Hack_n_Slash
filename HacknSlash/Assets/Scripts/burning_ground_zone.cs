using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class burning_ground_zone : MonoBehaviour {

	public GameObject BurningAura_skill;
	public List <GameObject> enemy_list = new List<GameObject> ();

	void OnTriggerEnter(Collider col) {
		Debug.Log ("catch " + col.gameObject.name);
		if (col.gameObject.tag == "enemy") {
			enemy_list.Add (col.gameObject);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "enemy") {
			enemy_list.Remove (col.gameObject);
		}
	}

	IEnumerator damage_enemys() {
		for (;;) {
			if (GameManager.instance.player.GetComponent<player> ().using_burning_aura == true ) {
				foreach (GameObject enemy in enemy_list) {
					enemy.GetComponent<game_character> ().cur_HP -=
						BurningAura_skill.GetComponent<Skill> ().damage;
				}
				GameManager.instance.player.GetComponent<game_character> ().cur_MP -= BurningAura_skill.GetComponent<Skill> ().cost;
				if (GameManager.instance.player.GetComponent<game_character> ().cur_MP <= 0) {
					GameManager.instance.player.GetComponent<player> ().using_burning_aura = false;
				}
			}
			yield return new WaitForSeconds(1.0F);
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (damage_enemys ());
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.player.GetComponent<player> ().using_burning_aura == true) {
			foreach (GameObject enemy in enemy_list) {
				if (!enemy) {
					enemy_list.Remove(enemy);
				}
			}
		}
	}
}
