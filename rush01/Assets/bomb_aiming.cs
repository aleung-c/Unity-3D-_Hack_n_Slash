using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class bomb_aiming : MonoBehaviour {
	public bool		active_aiming = false;

	public GameObject Bomb_skill;
	public List <GameObject> enemy_list = new List<GameObject> ();
	
	void OnTriggerEnter(Collider col) {
		Debug.Log ("bomb catch " + col.gameObject.name);
		if (col.gameObject.tag == "enemy") {
			enemy_list.Add (col.gameObject);
		}
	}
	
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "enemy") {
			enemy_list.Remove (col.gameObject);
		}
	}
	
	void damage_enemys() {
		foreach (GameObject enemy in enemy_list) {
			enemy.GetComponent<game_character> ().cur_HP -=
				Bomb_skill.GetComponent<Skill> ().damage;
		}
		enemy_list.Clear();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (active_aiming == true) {
			RaycastHit hit;
			Ray aim_pos = Camera.main.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (aim_pos, out hit);
			Vector3 hit_pos = hit.point;
			hit_pos.y = 0.0F;
			if (Vector3.Distance(hit_pos, GameManager.instance.player.transform.position) <= Bomb_skill.GetComponent<Skill> ().skill_range) {
				transform.position = hit_pos;
			}
			if (Input.GetMouseButtonDown (0)) {
				GameManager.instance.player.GetComponent<Animator> ().SetTrigger("throw");
				transform.Find ("Explosion").gameObject.SetActive (true);
				active_aiming = false;
				GameManager.instance.player.GetComponent<player> ().active_aim = false;
				GameManager.instance.player.GetComponent<player> ().can_move = true;
				transform.Find ("target_zone").gameObject.SetActive(false);
				transform.Find("Explosion").GetComponent<ParticleSystem> ().Play();
				damage_enemys();
			}
		}
	}
}