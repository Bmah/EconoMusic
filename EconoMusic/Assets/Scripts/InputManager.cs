// This is a temporary solution for hiding seperate UI's easily. 
// I know this wasn't part of the design document so we can delete this if needed; however would just need to do a complicated UI hiding method (similar to Buypartisan)

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public Canvas dropDownMenu;

	public MasterInstrument masterInstrument;
	private List<bool> playingInstruments;

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

	/// <summary>
	/// Toggles the drop down menu.
	/// </summary>
	public void ToggleDropDownMenu() {
		if (isToggled) {
			dropDownMenu.gameObject.SetActive(false);
			isToggled = false;

			//This part plays all of the instruments that are playing when pausing Brian Mah
			for(int i = 0; i < masterInstrument.Instruments.Count; i++){
				masterInstrument.Instruments[i].play = playingInstruments[i];
			}
		} 
		else {
			dropDownMenu.gameObject.SetActive(true);
			isToggled = true;
			
			//This part keeps track of all the instruments that are playing when pausing Brian Mah
			playingInstruments = new List<bool>();
			for(int i = 0; i < masterInstrument.Instruments.Count; i++){
				playingInstruments.Add(masterInstrument.Instruments[i].play);
			}
			masterInstrument.PauseAll();
		}
	}
}
