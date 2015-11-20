using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject	player;

	public int				base_required_exp = 10;
	public int				exp_multiplier_by_level = 2;
	public int				cur_required_exp = 10;
	public int				hp_bonus = 0;
	public int				str_bonus = 0;
	public int				weapon_atk_bonus = 0; // in world distance.
	public float			weapon_atk_speed = 0; // in sec

	public GameObject inventory_panel;
	public GameObject stat_panel_weapon;

	// drag and drop skill;
	public GameObject	skill_being_dragged;

	// singleton GameManager
	static GameManager _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public GameManager instance
	{
		get {
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
				if (_instance == null)
				{
					GameObject go = new GameObject("_gamemanager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

	// ------- my methods ------ //

	public void check_level_up () {
		if (player.GetComponent<player> ().current_exp >= cur_required_exp) { // level up
			Debug.Log ("level up !");
			player.GetComponent<player> ().stat_points += 5;
			player.GetComponent<player> ().skill_points += 1;
			player.GetComponent<player> ().level += 1;
			cur_required_exp *= exp_multiplier_by_level;
		}
	}

	IEnumerator check_events() {
		for (;;) {
			check_level_up();
			yield return new WaitForSeconds(1.0F);
		}
	}

	// Use this for initialization
	void Start () {
		cur_required_exp = base_required_exp;
		StartCoroutine (check_events ());
	}
	
	// Update is called once per frame
	void Update () {

			if (Input.GetKeyDown(KeyCode.I))
			{
			if (inventory_panel.activeSelf == false) {
			    inventory_panel.SetActive(true);
				weapon_manager.instance.refresh_inventory_panel();
				player.GetComponent<player>().can_move = false;
				}
			else {
				inventory_panel.SetActive(false);
				weapon_manager.instance.refresh_inventory_panel();
				player.GetComponent<player>().can_move = true;
			}
		}
	
	}
}
