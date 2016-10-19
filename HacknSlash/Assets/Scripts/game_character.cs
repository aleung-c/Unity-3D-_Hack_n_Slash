using UnityEngine;
using System.Collections;

public class game_character : MonoBehaviour {
	public bool			is_dead = false;

	// char stats //
	public int			level = 1;
	public int			cur_HP = 100;
	public int			Max_HP = 100; // = CON * 5;
	public int			cur_MP = 100;
	public int			Max_MP = 100;

	public int			FOR = 10;
	public int			AGI = 10;
	public int			CON = 20;

	public float		attack_delay = 1.0F;
	public float		range = 2.0F;
	
	// calculated stats;
	public int			MinDamage; // = FOR / 2 + weapon bonus;
	public int			MaxDamage; // = FOR + 4 + weapon bonus;
	public int			Hit_rate;


	// methods
	public void 	init_dynamic_stats_values_player () {
		Max_HP = CON * 5 + GameManager.instance.hp_bonus;
		//cur_HP = Max_HP;
		MinDamage = FOR / 2 + GameManager.instance.str_bonus + GameManager.instance.weapon_atk_bonus;
		MaxDamage = MinDamage + 4;
		Hit_rate = 75 + AGI;
		if (GameManager.instance.player.GetComponent<player> ().current_weapon) {
			range = GameManager.instance.player.GetComponent<player> ().current_weapon.GetComponent<weapon> ().range;

		}
	}

	public void 	init_dynamic_stats_values_enemy () {
		Max_HP = CON * 5;
		MinDamage = FOR / 2;
		MaxDamage = MinDamage + 4;
		Hit_rate = 75 + AGI;

	}

	// Use this for initialization
	void Start () {
		if (tag == "enemy") {
			init_dynamic_stats_values_enemy ();
		} else
			init_dynamic_stats_values_player ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
