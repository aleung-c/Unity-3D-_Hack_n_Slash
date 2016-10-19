using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**/
using UnityEngine.UI;

public class weapon_manager : MonoBehaviour {

	public GameObject prefab_weapon;
	public static weapon_manager instance { get; private set;}
	public List<GameObject> list_inventory_player = new List<GameObject>();
	public List<GameObject> list_weapon = new List<GameObject>();
	public GameObject panel_inventory;


	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
	
	}

	public void add_to_inventory(GameObject weapon)
	{
		if (list_inventory_player.Count <= 12)
		{
			list_inventory_player.Add(weapon);
			list_weapon.Remove(weapon);
			weapon.gameObject.SetActive(false);
			refresh_inventory_panel();
		}
	}

	public void refresh_inventory_panel()
	{
		int i = 1;
		Image[] image_tmp = panel_inventory.GetComponentsInChildren<Image>();

		while (i < image_tmp.Length)
		{
			if (i <= list_inventory_player.Count)
			{
				image_tmp[i].enabled = true;
				weapon weapon_tmp = list_inventory_player.ToArray()[i-1].GetComponent<weapon>();
				image_tmp[i].sprite = weapon_tmp.list_sprite_weapon.ToArray()[weapon_tmp.random];
				image_tmp[i].color = new Color(255, 255, 255, 255);
			}
			else
				image_tmp[i].enabled = false;
			i++;
		}

		/*foreach (Image sprite_image in panel_inventory.GetComponentsInChildren<Image>())
		{
			if (i < list_inventory_player.Count)
			{
				weapon weapon_tmp = list_inventory_player.ToArray()[i].GetComponent<weapon>();
				sprite_image.sprite = weapon_tmp.list_sprite_weapon.ToArray()[weapon_tmp.random];
			}
			else
			{
//				sprite_image;
				sprite_image.color = new Color(255, 255, 255, 75);
			}
			i++;
		}*/
	}

	public void create_weapon(Vector3 position, Quaternion angle)
	{
		list_weapon.Add((GameObject)Instantiate(prefab_weapon, position, angle));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
