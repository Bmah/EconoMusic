using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstrumentSliderScript : MonoBehaviour {

	public Slider numberOfNotes;
	public Text displayText;

	// Use this for initialization
	void Start () {
		displayText.text = numberOfNotes.value.ToString ();
	}

	public void ChangeDisplayText(){
		displayText.text = numberOfNotes.value.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
