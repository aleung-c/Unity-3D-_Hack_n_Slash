using UnityEngine;
using System.Collections;

public class player_sylvain : MonoBehaviour {

	private Collider actual_collider;
	public GameObject current_weapon;
	public GameObject stat_panel_weapon;
	public GameObject inventory_panel;
	private Collider flag_weapon_hover;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
		{
			inventory_panel.SetActive(!inventory_panel.activeSelf);
			weapon_manager.instance.refresh_inventory_panel();
		}
		if (Input.GetKey(KeyCode.W))
			GetComponent<NavMeshAgent>().Move(Vector3.forward / 3);
		if (Input.GetKey(KeyCode.S))
			GetComponent<NavMeshAgent>().Move(-Vector3.forward /3);
		if (Input.GetKey(KeyCode.A))
			GetComponent<NavMeshAgent>().Move(Vector3.left / 3);
		if (Input.GetKey(KeyCode.D))
			GetComponent<NavMeshAgent>().Move(Vector3.right /3);

		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			if (hit.collider.tag == "weapon" && (flag_weapon_hover == null || flag_weapon_hover != hit.collider))
			{
				stat_panel_weapon.SetActive(true);
				flag_weapon_hover = hit.collider;
				hit.collider.GetComponent<weapon>().display_stat_weapon(stat_panel_weapon);
			}
			else if (hit.collider.tag != "weapon")
			{
				flag_weapon_hover = null;
				stat_panel_weapon.SetActive(false);
			}
			if (hit.collider == actual_collider && Input.GetMouseButtonDown(0))	
			{
				weapon_manager.instance.add_to_inventory(hit.collider.gameObject);
				/*weapon_manager.instance.list_weapon.Remove(hit.collider.gameObject);
				weapon_manager.instance.list_inventory_player.Add(hit.collider.gameObject);
				hit.collider.GetComponent<MeshRenderer>().enabled = false;
				hit.collider.enabled = false;*/
			}
		}
	}
	void OnTriggerEnter(Collider other) {
		actual_collider = other;
	}
	void OnTriggerExit(Collider other) {
		actual_collider = null;
	}

}
