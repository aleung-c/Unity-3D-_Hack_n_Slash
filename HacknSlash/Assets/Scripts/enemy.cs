using UnityEngine;
using System.Collections;

public class enemy : game_character {
	public float			vision_range = 12.0F;
	private GameObject		_player;
	public bool				prepare_to_ground = false;
	public bool				go_to_delete = false;
	public float			ground_delay = 2.0F;
	public int 				given_exp = 1;
	public bool				exp_given = false;
	private Vector3			_init_pos;
	public	string			target_name;

	// for boss 
	public GameObject	prefab_effect;

	public void 	goto_player_pos() {
		GetComponent<NavMeshAgent> ().destination = _player.transform.position;
		GetComponent<Animator> ().SetBool ("is_running", true);
	}

	// COROUTINES
	IEnumerator		check_surroundings() {
		for (;;) {
			float reaction_time = Random.Range(0.2F, 1.0F);
			if (!is_dead && Vector3.Distance(_player.transform.position, transform.position) <= vision_range) {
				goto_player_pos();
			}
			if (!is_dead &&  Vector3.Distance(_player.transform.position, transform.position) <= range) {
				GetComponent<Animator> ().SetBool ("is_running", false);
				GetComponent<NavMeshAgent> ().destination = transform.position;
			}
			yield return new WaitForSeconds(reaction_time);
		}
	}

	public int		calculate_enemy_attack() {
		int touch = Random.Range (0, 100);
		if (touch < Hit_rate) {
			// ennemy touched.
			int damage = Random.Range (MinDamage, MaxDamage);
			return (damage);
		} else {
			Debug.Log("enemy missed player"); //
			return (0); // ennemy missed;
		}
	}


	public void		enemy_attack() {

		transform.LookAt (_player.transform.position);
		GetComponent<Animator> ().SetTrigger ("attack");
		if (this.name == "Boss") {
			int rand_skill = Random.Range(0, 5); // boss a une chance sur 5 de lancer le skill
			if (rand_skill == 1) {
				Instantiate(prefab_effect,GameManager.instance.player.transform.position, Quaternion.identity);
				_player.GetComponent<game_character> ().cur_HP -= 15; // boss skill hp.
			}
		}
		_player.GetComponent<game_character> ().cur_HP -= calculate_enemy_attack(); // a changer pour le calcul;
	}

	IEnumerator		attack_routine() {
		for (;;) {
			if (_player.GetComponent<game_character>().is_dead == false && !is_dead && 
			    Vector3.Distance(transform.position, _player.transform.position) <= range) {
				enemy_attack();
			}
			yield return new WaitForSeconds(attack_delay);
		}
	}

	IEnumerator		grounding() {
		for (;;) {
			if (prepare_to_ground == true) {
				ground_delay = 0.3F;
				Vector3 new_pos = transform.position;
				new_pos.y -= 0.01F;
				transform.position = new_pos;
				if (transform.position.y <= (_init_pos.y - 8.0F)) {
					go_to_delete = true;
					GameObject.Destroy(this.gameObject);
				}
			}
			yield return new WaitForSeconds(ground_delay);
			prepare_to_ground = true;
		}
	}

	public void		check_states () {
		if (cur_HP <= 0) { // ennemy dies.
			is_dead = true;
			GetComponent<Animator> ().SetBool("down", true);
			GetComponent<NavMeshAgent> ().enabled = false;
			gameObject.layer = 11;
			if (!exp_given) {
				int rand = Random.Range(0, 8);
				if (rand >= 4) {
					GameObject.Instantiate(weapon_manager.instance.prefab_weapon, transform.position, Quaternion.identity);
				}
				GameManager.instance.player.GetComponent<player> ().current_exp += given_exp; // give exp to player;
				exp_given = true;
			}
			StartCoroutine(grounding());
		}
	}

	public void		init_enemy_val() {
		level = GameManager.instance.player.GetComponent<game_character> ().level;
		CON += level;
		FOR += level;
		init_dynamic_stats_values_enemy();
		cur_HP = Max_HP;
	}

	// Use this for initialization
	void			Start () {
		_player = GameManager.instance.player;
		_init_pos = transform.position;
		init_enemy_val ();
		StartCoroutine (check_surroundings ());
		StartCoroutine (attack_routine ());
	}

	// Update is called once per frame
	void			Update () {
		check_states ();
	}
}
