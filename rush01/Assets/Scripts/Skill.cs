using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	public int		id;
	public string	skill_name;
	public int		level = 0;
	public int 		level_max = 5;
	public int 		req_lv = 0;
	public bool		is_available = false;
	public int		cost = 0;
	public int		damage = 1;
	public float 	skill_range;
	public float 	cast_time;

	public bool		passive = false;
	public bool		active = true;	
	public int		increase_per_lv = 1;

	public string	tooltip_text;

	public virtual void		use(){
		return;
	}

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
}
