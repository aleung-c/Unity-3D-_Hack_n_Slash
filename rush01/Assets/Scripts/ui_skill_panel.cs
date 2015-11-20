using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_skill_panel : MonoBehaviour {
	public GameObject panel_access;

	public GameObject skill_slot1;
	public GameObject skill_slot2;
	public GameObject skill_slot3;
	public GameObject skill_slot4;
	public GameObject tool_tip_text_obj;

	public void update_skill_screen () {
		//display skill datas.
		panel_access.transform.Find ("skills_panel").transform.Find ("wild_blade").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("wild_blade").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("wild_blade").GetComponent<Skill> ().req_lv.ToString () + ")";
		panel_access.transform.Find ("available_skillpoints").GetComponent<Text> ().text 
			= "Available skill points: " + GameManager.instance.player.GetComponent<player> ().skill_points.ToString();

		panel_access.transform.Find ("skills_panel").transform.Find ("first_aid").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("first_aid").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("first_aid").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("skills_panel").transform.Find ("burning_aura").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("burning_aura").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("burning_aura").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("skills_panel").transform.Find ("student_toughness").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("student_toughness").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("student_toughness").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("skills_panel").transform.Find ("bomb").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("bomb").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("bomb").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("skills_panel").transform.Find ("fire").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("fire").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("fire").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("skills_panel").transform.Find ("gamers_rage").
			transform.Find ("skill_name").transform.Find ("skill_lv").
				GetComponent<Text> ().text 
				= "lv " +
				panel_access.transform.Find ("skills_panel").
				transform.Find ("gamers_rage").GetComponent<Skill> ().level.ToString () + " (req lv" + panel_access.transform.Find ("skills_panel").
				transform.Find ("gamers_rage").GetComponent<Skill> ().req_lv.ToString () + ")";

		panel_access.transform.Find ("available_skillpoints").GetComponent<Text> ().text 
			= "Available skill points: " + GameManager.instance.player.GetComponent<player> ().skill_points.ToString(); 
	}

	IEnumerator ui_checks() {
		for (;;) {
			update_skill_screen();
			yield return new WaitForSeconds(0.2F);
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (ui_checks ());
	}

	public void onclick_close_skill_panel() {
		panel_access.gameObject.SetActive(false);
		GameManager.instance.player.GetComponent<player>().can_move = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
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
