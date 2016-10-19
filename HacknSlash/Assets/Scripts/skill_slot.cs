using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class skill_slot : MonoBehaviour {

	public int				slot_number;
	public GameObject		assigned_skill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (assigned_skill) {
			transform.Find("Image").gameObject.GetComponent<Image> ().sprite = assigned_skill.GetComponent<Image> ().sprite;
			if (slot_number == 1) {
				GameManager.instance.player.GetComponent<player> ().active_skill1 = assigned_skill;
			} else if (slot_number == 2) {
				GameManager.instance.player.GetComponent<player> ().active_skill2 = assigned_skill;
			} else if (slot_number == 3) {
				GameManager.instance.player.GetComponent<player> ().active_skill3 = assigned_skill;
			} else if (slot_number == 4) {
				GameManager.instance.player.GetComponent<player> ().active_skill4 = assigned_skill;
			}
		}
	}
}