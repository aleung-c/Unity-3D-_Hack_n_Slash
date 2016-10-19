using UnityEngine;
using System.Collections;
//using !
using System.Collections.Generic;
using UnityEngine.UI;

public class weapon : MonoBehaviour {
	
	public int attack;
	public float range;
	public float speed_attack;
	public int random;
	public List<Material> list_material_weapon = new List<Material>();
	public List<Mesh> list_mesh_weapon = new List<Mesh>();
	public List<Sprite> list_sprite_weapon = new List<Sprite>();

	// Use this for initialization
	void Start () {

		random = Random.Range(0, list_material_weapon.Count);
		GetComponent<MeshRenderer>().material = list_material_weapon.ToArray()[random];
		GetComponent<MeshFilter>().mesh = list_mesh_weapon.ToArray()[random];
		attack = GameManager.instance.player.GetComponent<player> ().level + Random.Range(0, 10);
		range = 2.0F + Random.Range(0.0F, 1.0F);
		speed_attack = 1.8F + Random.Range(0.0F, 1.0F);// ==> attack_delay
	}

	public void set_weapon(weapon old_weap) {
		GetComponent<MeshRenderer> ().material = old_weap.GetComponent<MeshRenderer> ().material;
		GetComponent<MeshFilter> ().mesh = old_weap.GetComponent<MeshFilter> ().mesh;
		attack = old_weap.attack;
		range = old_weap.range;
		speed_attack = old_weap.speed_attack;
	}

	public void display_stat_weapon(GameObject panel_stat_weapon)
	{
		panel_stat_weapon.GetComponentsInChildren<Text>()[2].text = attack.ToString();
		panel_stat_weapon.GetComponentsInChildren<Text>()[4].text = speed_attack.ToString("F2");
		panel_stat_weapon.GetComponentsInChildren<Text>()[6].text = range.ToString("F2");
		panel_stat_weapon.GetComponentsInChildren<Text>()[8].text = "Common";
	}

	// Update is called once per frame
	void Update () {
	
	}
}
