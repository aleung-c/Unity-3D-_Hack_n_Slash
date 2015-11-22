using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inv_slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public int				slot_id;
	private Vector3			_init_pos;
	public	GameObject		associated_weapon;
	
	public void OnBeginDrag(PointerEventData eventData) {
		_init_pos = transform.position;
		GameManager.instance.skill_being_dragged = this.gameObject;
	}
	
	public void OnDrag(PointerEventData data) {
			transform.position = Input.mousePosition;
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		if (!RectTransformUtility.RectangleContainsScreenPoint
		  (transform.parent.GetComponent<RectTransform> (), eventData.position, null)) {
			if (associated_weapon) {
				GameObject drop_w = (GameObject) GameObject.Instantiate(associated_weapon, GameManager.instance.player.transform.position, Quaternion.identity);
				// make the same
				drop_w.SetActive(true);
				drop_w.gameObject.GetComponent<weapon> ().set_weapon(associated_weapon.GetComponent<weapon>());
				// ------ //
				weapon_manager.instance.list_inventory_player.Remove(weapon_manager.instance.list_inventory_player[slot_id]);
				GetComponent<Image> ().sprite = null;
				Color ncolor = Color.white;
				ncolor.a = 0.0F;
				GetComponent<Image> ().color = ncolor;
				GameManager.instance.player.GetComponent<Animator> ().SetBool ("weapon", false);
				GameManager.instance.weapon_atk_bonus = 0;
				GameManager.instance.weapon_atk_speed = 1.0F;
				weapon_manager.instance.refresh_inventory_panel();
				GameManager.instance.player.GetComponent<player> ().current_weapon = null;
				this.associated_weapon = null;

			}
		}
			transform.position = _init_pos;
	}

	public void On_click_item() {
		if (associated_weapon) {
			GameManager.instance.player.GetComponent <player> ().current_weapon = associated_weapon;
			GameManager.instance.player.GetComponent<Animator> ().SetBool ("weapon", true);

			GameManager.instance.weapon_atk_bonus = associated_weapon.GetComponent<weapon> ().attack;
			GameManager.instance.weapon_atk_speed = associated_weapon.GetComponent<weapon> ().speed_attack;
		}
	}

	// Use this for initialization
	void Start () {
		associated_weapon = weapon_manager.instance.list_inventory_player [slot_id];
	}
	
	// Update is called once per frame
	void Update () {
		associated_weapon = weapon_manager.instance.list_inventory_player [slot_id];
	}
}
