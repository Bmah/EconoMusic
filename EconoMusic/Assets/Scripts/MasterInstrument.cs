using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MasterInstrument : MonoBehaviour {

	public GameObject InstrumentTemplate;
	private List<InstrumentScript> Instruments;

	public Slider MasterTempoSlider;
	public Slider MasterVolumeSlider;
	public Toggle MasterLoopToggle;
	public Slider MasterTimeSlider;

	// Use this for initialization
	void Start () {
		if (InstrumentTemplate == null) {
			Debug.LogError("Please Insert InstrumentTemplate into MasterInstrument Script");
		}//if
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAll(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].play = true;
		}
	}

	public void PauseAll(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].play = false;
		}
	}

	public void UpdateTempo(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].noteValue = MasterTempoSlider.value;
		}
	}

	public void UpdateVolume(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].volume = MasterVolumeSlider.value;
		}
	}

}
