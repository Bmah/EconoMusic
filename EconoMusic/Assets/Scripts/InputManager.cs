// This is a temporary solution for hiding seperate UI's easily. 
// I know this wasn't part of the design document so we can delete this if needed; however would just need to do a complicated UI hiding method (similar to Buypartisan)

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Canvas dropDownMenu;

	private bool isToggled = true;

	// Use this for initialization
	void Start () {
		ToggleDropDownMenu ();			// Toggles the Drop Down Menu (so hides it initially)
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")) {
			ToggleDropDownMenu();
		}
	}

	public void ToggleDropDownMenu() {
		if (isToggled) {
			dropDownMenu.gameObject.SetActive(false);
			isToggled = false;
		} 
		else {
			dropDownMenu.gameObject.SetActive(true);
			isToggled = true;
		}
	}
}
