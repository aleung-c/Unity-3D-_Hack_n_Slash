using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StudentToughness : Skill, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	private Vector3		_init_pos;
	public GameObject	prefab_effect;

	public void Reset()
	{
		id = 1;
		skill_name = "Student Toughness";
		is_available = true;
		req_lv = 6;
		cost = 4;
		damage = 10; // en HP
		increase_per_lv = 10;
		cast_time = 0.0F; // pas besoin, passive
		skill_range = 2.5F; // pas besoin, target = self.
		passive = true;
		active = false;
		tooltip_text = "The P.E classes paid in the end ! You get tougher and tougher !\n" 
			+ "Hp bonus: " + damage.ToString() + " + " + increase_per_lv.ToString() + " per level" ;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		//nothing, passive skill
	}

	public void OnDrag(PointerEventData data) {
		//nothing, passive skill
	}

	public void OnEndDrag(PointerEventData eventData) {
		//nothing, passive skill
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
				tooltip_text = "The P.E classes paid in the end ! You get tougher and tougher !\n" 
					+ "Hp bonus: " + damage.ToString() + " + " + increase_per_lv.ToString() + " per level" ;
			}
			GameManager.instance.hp_bonus += increase_per_lv;
			GameManager.instance.player.GetComponent<player> ().skill_points -= 1;
		}
	}

	override public void use() {
		// nothing to do, passive skill
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
