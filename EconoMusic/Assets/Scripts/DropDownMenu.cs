using UnityEngine;
using System.Collections;

public class DropDownMenu : MonoBehaviour {

	public GameObject credits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1")){
			ToggleCredits (false);
		}
	}

	public void ExitApplication() {
		Application.Quit();
	}

	public void ToggleCredits(bool toggle) {
		credits.SetActive (toggle);
	}

}
