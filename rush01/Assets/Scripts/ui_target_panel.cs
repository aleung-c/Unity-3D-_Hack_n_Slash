using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_target_panel : MonoBehaviour {
	private	GameObject _player;
	public GameObject	target_panel;

	public void update_target_panel() {
		if (_player.GetComponent<player> ().cur_target) { // si player a une cible, update target panel
			target_panel.SetActive (true);
			Image cur_hp_image = transform.Find ("target_panel_to_hide").transform.Find ("target_hp_fond").
				transform.Find ("target_hp_cur").GetComponent<Image> ();
			game_character target = _player.GetComponent<player> ().cur_target.GetComponent<game_character> ();
			cur_hp_image.fillAmount = (float)((float)target.cur_HP / (float)target.Max_HP);
			transform.transform.Find ("target_panel_to_hide").transform.Find ("target_name").GetComponent<Text> ().text 
				= _player.GetComponent<player>().cur_target.GetComponent <enemy> ().target_name;
			transform.transform.Find ("target_panel_to_hide").transform.Find ("target_lv_val").GetComponent<Text> ().text
				= _player.GetComponent<player>().cur_target.GetComponent <enemy> ().level.ToString();
		} else {
			target_panel.SetActive (false);
		}
			
	}

	IEnumerator ui_checks() {
		for (;;) {
			update_target_panel();
			yield return new WaitForSeconds(0.2F);
		}
	}

	// Use this for initialization
	void Start () {
		_player = GameManager.instance.player;
		StartCoroutine (ui_checks());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
