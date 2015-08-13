using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MasterInstrument : MonoBehaviour {

	public Canvas GameCanvas;
	public GameObject InstrumentTemplate;
	private List<InstrumentScript> Instruments = new List<InstrumentScript> ();

	public Slider MasterTempoSlider;
	public Slider MasterVolumeSlider;
	public Toggle MasterLoopToggle;

	int offset = 0;

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
			Instruments[i].TempoSlider.value = MasterTempoSlider.value;
		}
	}

	public void UpdateVolume(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].volume = MasterVolumeSlider.value;
			Instruments[i].VolumeSlider.value = MasterVolumeSlider.value;
		}
	}

	public void UpdateLoop(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].loop = MasterLoopToggle.isOn;
			Instruments[i].LoopToggle.isOn = MasterLoopToggle.isOn;
		}
	}

	public void NewInstrument(){
		GameObject NewInstrument = Instantiate(InstrumentTemplate, new Vector3(offset, 200, 0), Quaternion.identity) as GameObject;
		NewInstrument.transform.parent = GameCanvas.transform;
		offset += 200;
		Instruments.Add (NewInstrument.GetComponent<InstrumentScript> ());
	}
}
