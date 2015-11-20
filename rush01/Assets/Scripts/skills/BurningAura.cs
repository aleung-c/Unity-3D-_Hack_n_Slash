using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BurningAura : Skill, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	private Vector3		_init_pos;
	public GameObject	prefab_effect;

	public void Reset()
	{
		id = 1;
		skill_name = "Burning Aura";
		is_available = true;
		req_lv = 6;
		cost = 4;
		damage = 1; // en pourcentage
		increase_per_lv = 2;
		cast_time = 0.0F; // toggle skill
		skill_range = 2.5F; // pas besoin, target = self.
		passive = false;
		active = true;
		tooltip_text = "You remember those days of hardcore studiying, and inspire fear around you !\n" 
			+ "Damage: " + damage.ToString() + " per second around the character + " + increase_per_lv.ToString() + " per level" ;
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
		if (GameManager.instance.player.GetComponent<player> ().skill_points > 0 && level < level_max  &&
		    GameManager.instance.player.GetComponent<player> ().level >= req_lv) {
			level += 1;
			if (level != 1) {
				damage += increase_per_lv;
				tooltip_text = "You remember those days of hardcore studiying, and inspire fear around you !\n" 
					+ "Damage: " + damage.ToString() + " per second around the character + " + increase_per_lv.ToString() + " per level" ;
			}
			GameManager.instance.player.GetComponent<player> ().skill_points -= 1;
		}
	}

	override public void use() {

		GameObject player = GameManager.instance.player;
		if (player.GetComponent<game_character> ().cur_MP >= cost) {
			Debug.Log ("using Burning Aura !");
			player.GetComponent<Animator> ().SetTrigger("cast_buff");
			if (player.GetComponent<player> ().using_burning_aura == false) {
				player.GetComponent<player> ().using_burning_aura = true;
			} else {
				player.GetComponent<player> ().using_burning_aura = false;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
