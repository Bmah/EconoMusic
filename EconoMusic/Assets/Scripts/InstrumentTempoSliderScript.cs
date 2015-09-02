using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstrumentTempoSliderScript : MonoBehaviour {

	private Image note;
	public Slider tempoSlider;
	public Sprite eigthNote, quarterNote, halfNote;

	// Use this for initialization
	void Start () {
		note = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(tempoSlider.value < 0.5f){
			note.sprite = eigthNote;
		}//if
		else if(tempoSlider.value < 1f){
			note.sprite = quarterNote;
		}//else if
		else{
			note.sprite = halfNote;
		}//else

	}
}
