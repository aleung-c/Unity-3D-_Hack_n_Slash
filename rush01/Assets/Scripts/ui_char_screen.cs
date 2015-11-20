using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_char_screen : MonoBehaviour {
	public GameObject panel_access;

	public void update_char_screen () {
		//display stats.
		panel_access.transform.Find ("level_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().level.ToString();
		panel_access.transform.Find ("FOR_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().FOR.ToString();
		panel_access.transform.Find ("AGI_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().AGI.ToString();
		panel_access.transform.Find ("CON_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().CON.ToString();
		//display dynamic stats.
		panel_access.transform.Find ("HP_cur").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().cur_HP.ToString();
		panel_access.transform.Find ("HP_max").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().Max_HP.ToString();
		panel_access.transform.Find ("MP_cur").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().cur_MP.ToString();
		panel_access.transform.Find ("MP_max").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().Max_MP.ToString();
		panel_access.transform.Find ("hit_rate_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().Hit_rate.ToString() + " %";
		panel_access.transform.Find ("attack_speed_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().attack_delay.ToString("F2") + " sec";
		panel_access.transform.Find ("attack_range_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().range.ToString("F2");
		panel_access.transform.Find ("min_damage_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().MinDamage.ToString();
		panel_access.transform.Find ("max_damage_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().MaxDamage.ToString();
		panel_access.transform.Find ("exp_cur").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().current_exp.ToString();
		panel_access.transform.Find ("exp_next").GetComponent<Text> ().text = GameManager.instance.cur_required_exp.ToString ();
		panel_access.transform.Find ("statpoints_val").GetComponent<Text> ().text = GameManager.instance.player.GetComponent<player> ().stat_points.ToString();

	}

	public void on_click_for_plus() {
		// fix temporaire.
		// GameManager.instance.player.GetComponent<NavMeshAgent> ().destination = GameManager.instance.player.transform.position; 
		if (GameManager.instance.player.GetComponent<player> ().stat_points > 0) {
			GameManager.instance.player.GetComponent<game_character> ().FOR += 1;
			GameManager.instance.player.GetComponent<player> ().stat_points -= 1;
		}
	}

	public void on_click_agi_plus() {
		// fix temporaire.
		// GameManager.instance.player.GetComponent<NavMeshAgent> ().destination = GameManager.instance.player.transform.position; 
		if (GameManager.instance.player.GetComponent<player> ().stat_points > 0) {
			GameManager.instance.player.GetComponent<game_character> ().AGI += 1;
			GameManager.instance.player.GetComponent<player> ().stat_points -= 1;
		}
	}
	public void on_click_con_plus() {
		// fix temporaire.
		// GameManager.instance.player.GetComponent<NavMeshAgent> ().destination = GameManager.instance.player.transform.position; 
		if (GameManager.instance.player.GetComponent<player> ().stat_points > 0) {
			GameManager.instance.player.GetComponent<game_character> ().CON += 1;
			GameManager.instance.player.GetComponent<player> ().stat_points -= 1;
		}
	}

	public void on_click_close_char_screen() {
		panel_access.gameObject.SetActive(false);
		GameManager.instance.player.GetComponent<player>().can_move = true;
	}

	IEnumerator ui_checks() {
		for (;;) {
			update_char_screen();
			yield return new WaitForSeconds(0.2F);
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (ui_checks ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			if (panel_access.activeSelf == false) { 
				panel_access.gameObject.SetActive(true);
				GameManager.instance.player.GetComponent<player>().can_move = false;
			}
			else {
				panel_access.gameObject.SetActive(false);
				GameManager.instance.player.GetComponent<player>().can_move = true;
		}
	}
	}
}
