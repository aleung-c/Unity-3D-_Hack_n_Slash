using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Fire : Skill, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	private Vector3		_init_pos;
	public GameObject	prefab_effect;

	public void Reset()
	{
		id = 0;
		skill_name = "Fire";
		is_available = true;
		req_lv = 12;
		cost = 25;
		damage = 15;
		increase_per_lv = 8;
		cast_time = 1.0F;
		skill_range = 6.0F;
		passive = false;
		active = true;
		tooltip_text = "More science ! Burn them from afar !\n" 
			+ "Damage: " + damage.ToString() + " + " + increase_per_lv.ToString() + " per level" ;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		_init_pos = transform.position;
		GameManager.instance.skill_being_dragged = this.gameObject;
	}

	public void OnDrag(PointerEventData data) {
		if (level >= 1)
			transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		ui_skill_panel ui_skill_panel = transform.parent.transform.parent.transform.parent.GetComponent <ui_skill_panel> ();
		if (level >= 1) {
			if (RectTransformUtility.RectangleContainsScreenPoint
		    (ui_skill_panel.skill_slot1.GetComponent<RectTransform> (), eventData.position, null)) {
				Debug.Log ("dropped skill in slot1");
				transform.parent.transform.parent.transform.parent.GetComponent<ui_skill_panel> ().
				skill_slot1.GetComponent<skill_slot> ().assigned_skill = this.gameObject;
			} else if (RectTransformUtility.RectangleContainsScreenPoint
			(ui_skill_panel.skill_slot2.GetComponent<RectTransform> (), eventData.position, null)) {
				Debug.Log ("dropped skill in slot2");
				transform.parent.transform.parent.transform.parent.GetComponent<ui_skill_panel> ().
				skill_slot2.GetComponent<skill_slot> ().assigned_skill = this.gameObject;

			} else if (RectTransformUtility.RectangleContainsScreenPoint
		         (ui_skill_panel.skill_slot3.GetComponent<RectTransform> (), eventData.position, null)) {
				Debug.Log ("dropped skill in slot3");
				transform.parent.transform.parent.transform.parent.GetComponent<ui_skill_panel> ().
				skill_slot3.GetComponent<skill_slot> ().assigned_skill = this.gameObject;
			
			} else if (RectTransformUtility.RectangleContainsScreenPoint
		         (ui_skill_panel.skill_slot4.GetComponent<RectTransform> (), eventData.position, null)) {
				Debug.Log ("dropped skill in slot4");
				transform.parent.transform.parent.transform.parent.GetComponent<ui_skill_panel> ().
				skill_slot4.GetComponent<skill_slot> ().assigned_skill = this.gameObject;
			}
			transform.position = _init_pos;
		}
	}

	public void on_hover() {
		GameObject tool_tip_access = transform.root.gameObject.
			transform.Find ("skill_panel").GetComponent<ui_skill_panel> ().tool_tip_text_obj;
		//Debug.Log (tool_tip_access.name);
		tool_tip_access.GetComponent<Text> ().text = tooltip_text;
		//Debug.Log ("hover skill");
	}

	public void click_plus() {
		if (GameManager.instance.player.GetComponent<player> ().skill_points > 0 && level < level_max &&
		    GameManager.instance.player.GetComponent<player> ().level >= req_lv) {
			level += 1;
			if (level != 1) {
				damage += increase_per_lv;
				tooltip_text = "More science ! Burn them from afar !\n" 
					+ "Damage: " + damage.ToString() + " + " + increase_per_lv.ToString() + " per level" ;
			}
			GameManager.instance.player.GetComponent<player> ().skill_points -= 1;
		}
	}

	override public void use() {
		GameObject player = GameManager.instance.player;
		GameObject target = player.GetComponent<player> ().cur_target;
		/*if (player.GetComponent<game_character> ().cur_MP >= cost) {

			player.GetComponent<player> ().can_move = false;
			player.GetComponent<player> ().active_aim = true;
			player.GetComponent<Animator> ().SetTrigger ("cast_buff");
			player.GetComponent<NavMeshAgent> ().destination = player.transform.position;
		}*/

		if (target && Vector3.Distance(player.transform.position, target.transform.position) <= skill_range && player.GetComponent<game_character> ().cur_MP >= cost) {
			Debug.Log ("using Fire !");
			player.GetComponent<player> ().can_move = false;
			player.transform.LookAt(target.transform.position);
			player.GetComponent<Animator> ().SetTrigger("cast_buff");
			player.GetComponent<player> ().start_cast = Time.time;
			player.GetComponent<player> ().cast_time = cast_time;
			player.GetComponent<player> ().is_using_skill = true;
			Instantiate(prefab_effect,target.transform.position, Quaternion.LookRotation(player.transform.forward, Vector3.up));
			target.GetComponent<game_character> ().cur_HP -= damage;
			player.GetComponent<player> ().cur_MP -= cost;
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
