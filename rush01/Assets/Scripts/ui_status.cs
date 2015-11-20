using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_status : MonoBehaviour {
	private	GameObject _player;

	public void update_status_panel() {
		Image cur_hp_image = transform.Find ("hp_fond").transform.Find ("hp_cur").GetComponent<Image> ();
		cur_hp_image.fillAmount = (float) ((float)_player.GetComponent<game_character> ().cur_HP / (float)_player.GetComponent<game_character> ().Max_HP);
		Image cur_mp_image = transform.Find ("mp_fond").transform.Find ("mp_cur").GetComponent<Image> ();
		cur_mp_image.fillAmount = (float) ((float)_player.GetComponent<game_character> ().cur_MP / (float)_player.GetComponent<game_character> ().Max_MP);
		//Debug.Log (cur_hp_image.fillAmount);
	}

	IEnumerator ui_checks() {
		for (;;) {
			update_status_panel();
			yield return new WaitForSeconds(0.1F);
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
