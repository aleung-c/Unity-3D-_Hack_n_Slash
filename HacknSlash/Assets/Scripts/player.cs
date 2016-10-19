using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class player : game_character {
	public GameObject		main_camera;
	public GameObject		cur_target = null;
	public GameObject		current_weapon = null;
	public GameObject		hand_object;

	public GameObject 		Bomb;

	public GameObject		active_skill1;
	public GameObject		active_skill2;
	public GameObject		active_skill3;
	public GameObject		active_skill4;

	// Game status //
	public bool				weapon_equipped = false;
	public bool				can_move = true;
	public	bool			is_attacking = false;
	public bool				is_using_skill = false;
	public float			start_cast;
	public float			cast_time;
	public bool				active_aim = false;

	public bool				using_burning_aura = false;
	public bool				str_buffed = false;
	public float			start_buff;
	public float			buff_duration;

	public	LayerMask		clickables;
	
	// Game stats.
	public int				mp_regen_per_sec = 4;
	public int				current_exp = 0;
	public int				stat_points = 5;
	public int				skill_points = 1;

	// items 

	private Collider		flag_weapon_hover;
	private Collider		actual_collider;

	// gere les touches appuyees.
	public void		key_handler() {
		if (Input.GetMouseButton (0)) { // click gauche
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, clickables)) {
				Debug.Log("clicked on " + hit.collider.name);
				GetComponent<Animator> ().SetBool ("is_running", true);
				if (hit.collider.tag == "enemy") {
					cur_target = hit.collider.gameObject;
					if (is_attacking == false && Vector3.Distance (transform.position, cur_target.transform.position) < range) {
						is_attacking = true;
						GetComponent<NavMeshAgent> ().destination = transform.position;
					}
					if (Vector3.Distance (transform.position, cur_target.transform.position) > range) {
						GetComponent<NavMeshAgent> ().destination = hit.point;
					}
				} else if (hit.collider.tag == "weapon" && Vector3.Distance (transform.position, hit.collider.transform.position) <= 2.5F) {
					flag_weapon_hover = null;
					GameManager.instance.stat_panel_weapon.SetActive(false);
					weapon_manager.instance.add_to_inventory(hit.collider.gameObject);
				} else
					GetComponent<NavMeshAgent> ().destination = hit.point;

			}
		}
		// skill keys
		if (Input.GetKeyDown (KeyCode.Alpha1) && active_skill1) {
			Debug.Log ("use " + active_skill1.GetComponent<Skill> ().name);
			active_skill1.GetComponent<Skill> ().use();
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && active_skill2) {
			Debug.Log ("use " + active_skill2.GetComponent<Skill> ().name);
			active_skill2.GetComponent<Skill> ().use();
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && active_skill3) {
			Debug.Log ("use " + active_skill3.GetComponent<Skill> ().name);
			active_skill3.GetComponent<Skill> ().use();
		} else if (Input.GetKeyDown (KeyCode.Alpha4) && active_skill4) {
			Debug.Log ("use " + active_skill4.GetComponent<Skill> ().name);
			active_skill4.GetComponent<Skill> ().use();
		}


	}


	// place la camera fps.
	public void		set_camera () {
		Vector3 new_pos;
		new_pos = main_camera.transform.position;
		new_pos.x = transform.position.x;
		new_pos.y = transform.position.y + 9.0F;
		new_pos.z = transform.position.z - 8.0F;
		main_camera.transform.position = new_pos;
		main_camera.transform.LookAt (transform.position);
	}

	public int player_attack() {
		int touch = Random.Range (0, 100);
		if (touch < Hit_rate) {
			// ennemy touched.
			int damage = Random.Range (MinDamage, MaxDamage);
			return (damage);
		} else {
			Debug.Log("player missed enemy"); //
			return (0); // ennemy missed;
		}
	}

	public void		attack_target () {
		if (cur_target.GetComponent<game_character> ().is_dead == false 
		    && cur_target.GetComponent<game_character> ().cur_HP > 0) {
			transform.LookAt(cur_target.transform.position);
			if (weapon_equipped) {
				GetComponent<Animator> ().SetTrigger ("attack_sword");
			}
			else
				GetComponent<Animator> ().SetTrigger ("attack");
			play_attack_sound();
			cur_target.GetComponent<game_character> ().cur_HP -= player_attack(); // a changer pour le calcul;
		}
		if (cur_target.GetComponent<game_character> ().is_dead == true) {
			cur_target = null;
		}
	}

	public void 	change_stats_val () {
		init_dynamic_stats_values_player ();
	}

	// COROUTINES 
	// change player stats every 0.8 sec. check changement darme, buffs, etc ...
	IEnumerator		calculate_stats () {
		for (;;) {
			change_stats_val();
			yield return new WaitForSeconds(0.8F);
		}
	}

	public void play_heal_sound() {
		GameObject sound = transform.Find ("sound_list").gameObject;
		sound.GetComponent<AudioSource> ().clip = sound.GetComponent<sound_list> ().special1;
		if (sound.GetComponent<AudioSource> ().isPlaying == false) {
			sound.GetComponent<AudioSource> ().Play ();
		}
	}

	public void play_attack_sound() {
		GameObject sound = transform.Find ("sound_list").gameObject;
		if (!current_weapon) {
			sound.GetComponent<AudioSource> ().clip = sound.GetComponent<sound_list> ().attack1;
			if (sound.GetComponent<AudioSource> ().isPlaying == false) {
				sound.GetComponent<AudioSource> ().Play();
			}
		} else {
			sound.GetComponent<AudioSource> ().clip = sound.GetComponent<sound_list> ().attack2;
			if (sound.GetComponent<AudioSource> ().isPlaying == false) {
				sound.GetComponent<AudioSource> ().PlayDelayed(attack_delay / 2.0F);
			}
		}
	}

	// pour le delai d'attaque lorsque player relache le click.
	IEnumerator		mouse_up() {
		for (;;) {
			if (Input.GetMouseButtonUp (0)) {
				yield return new WaitForSeconds(attack_delay);
				is_attacking = false;
			}
			yield return null;
		}
	}

	public void check_mouse_over_weapon() {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			if (hit.collider.tag == "weapon" && (flag_weapon_hover == null || flag_weapon_hover != hit.collider))
			{
				GameManager.instance.stat_panel_weapon.SetActive(true);
				flag_weapon_hover = hit.collider;
				hit.collider.GetComponent<weapon>().display_stat_weapon(GameManager.instance.stat_panel_weapon);
			}
			else if (hit.collider.tag != "weapon")
			{
				flag_weapon_hover = null;
				GameManager.instance.stat_panel_weapon.SetActive(false);
			}
			if (hit.collider == actual_collider && Input.GetMouseButtonDown(0))	
			{
				weapon_manager.instance.add_to_inventory(hit.collider.gameObject);
				/*weapon_manager.instance.list_weapon.Remove(hit.collider.gameObject);
				weapon_manager.instance.list_inventory_player.Add(hit.collider.gameObject);
				hit.collider.GetComponent<MeshRenderer>().enabled = false;
				hit.collider.enabled = false;*/
			}
		}
	}

	IEnumerator 	mouse_over_weapons() {
		for (;;) {
			check_mouse_over_weapon();
			yield return new WaitForSeconds(0.5F);
			}
		}

	// check cur_target distance et attack si a portee et en mode attaque.
	IEnumerator		attack_routine() {
		for (;;) {
			if (cur_target && is_attacking == true && 
			    Vector3.Distance(transform.position, cur_target.transform.position) <= range) {
				attack_target();
				GetComponent<NavMeshAgent>().destination = transform.position;
			}
			yield return new WaitForSeconds(attack_delay);
		}
	}

	IEnumerator mp_regen () {
		for (;;) {
			if (str_buffed == true) {
				float time = Time.time;
				if (time - start_buff >= 10.0F) {
					GameManager.instance.str_bonus = 0;
					transform.Find ("buffed").gameObject.transform.Find ("Flame Enchant").gameObject.SetActive(false);
					str_buffed = false;
				}
			}

			if (GetComponent<game_character> ().cur_MP < GetComponent<game_character> ().Max_MP) {
				GetComponent<game_character> ().cur_MP += mp_regen_per_sec;
				if (GetComponent<game_character> ().cur_MP > GetComponent<game_character> ().Max_MP)
					GetComponent<game_character> ().cur_MP += GetComponent<game_character> ().Max_MP;
			}
			yield return new WaitForSeconds(1.0F);
		}
	}

	// checks events majeurs (mort)
	public void check_states () {
		if (cur_HP <= 0) {
			if (is_dead == false) {
				GetComponent<Animator> ().SetTrigger("is_dead");
				GetComponent<NavMeshAgent>().destination = transform.position;
			}
			is_dead = true;
		}
		// stop run animation si arrive a sa destination.
		if (GetComponent<NavMeshAgent> ().remainingDistance <= 0.1F) {
			GetComponent<Animator>().SetBool("is_running", false);
		}
	}

	public void block_player_during_skill () {
		if (is_using_skill) {
			can_move = false;
			is_attacking = false;
			GetComponent<NavMeshAgent>().destination = transform.position;
			float time = Time.time;
			if (time - start_cast >= cast_time) { // end skill
				can_move = true;
				is_using_skill = false;
			}
		}
	}

	// Use this for initialization
	void		Start () {
		StartCoroutine (attack_routine ());
		StartCoroutine (mp_regen ());
		StartCoroutine (mouse_up ());
		StartCoroutine (mouse_over_weapons ());
		StartCoroutine (calculate_stats());
	}

	void OnTriggerEnter(Collider other) {
		actual_collider = other;
	}
	void OnTriggerExit(Collider other) {
		actual_collider = null;
	}

	
	// Update is called once per frame
	void		Update () {
		block_player_during_skill();
		if (is_dead == false && can_move == true) {
			key_handler ();
			set_camera ();
		}
		check_states ();

		//weapon equip check;
		if (current_weapon != null) {
			hand_object.GetComponent<MeshFilter> ().mesh = current_weapon.GetComponent<MeshFilter> ().mesh;
			hand_object.GetComponent<MeshRenderer> ().material = current_weapon.GetComponent<MeshRenderer> ().material;
			attack_delay = current_weapon.GetComponent<weapon> ().speed_attack;
			weapon_equipped = true;
		} else {
			hand_object.GetComponent<MeshFilter> ().mesh = null;
			hand_object.GetComponent<MeshRenderer> ().material = null;
			attack_delay = 1.0F;
			weapon_equipped = false;
		}

		//skills checks;
		if (using_burning_aura == true) {
			transform.Find("aura_zone").transform.Find("Burning Ground").gameObject.SetActive(true);
		}
		else
			transform.Find("aura_zone").transform.Find("Burning Ground").gameObject.SetActive(false);
		if (active_aim == true) {
			Bomb.GetComponent<bomb_aiming> ().active_aiming = true;
			Bomb.transform.Find("target_zone").gameObject.SetActive(true);
		}

		// Cheat keys.
		if (Input.GetKeyDown(KeyCode.Y)) { // level up;
			current_exp += GameManager.instance.cur_required_exp;
		}
		if (Input.GetKeyDown(KeyCode.U)) { // create weapon;
			GameObject.Instantiate(weapon_manager.instance.GetComponent<weapon_manager> ().prefab_weapon, transform.position, Quaternion.identity);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("Scene00");
		}
	}
}
