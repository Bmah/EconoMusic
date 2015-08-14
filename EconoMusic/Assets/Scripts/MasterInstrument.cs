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
	}//Start
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Plays all instruments.
	/// </summary>
	public void PlayAll(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].play = true;
		}//for
	}//PlayAll

	/// <summary>
	/// Pauses all instruments.
	/// </summary>
	public void PauseAll(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].play = false;
		}//for
	}//PauseAll

	/// <summary>
	/// Updates the tempo of the instrument and their slider's values.
	/// </summary>
	public void UpdateTempo(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].noteValue = MasterTempoSlider.value;
			Instruments[i].TempoSlider.value = MasterTempoSlider.value;
		}
	}

	/// <summary>
	/// Updates the volume of the instrument and their slider's values.
	/// </summary>
	public void UpdateVolume(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].volume = MasterVolumeSlider.value;
			Instruments[i].VolumeSlider.value = MasterVolumeSlider.value;
		}//for
	}//UpdateVolume

	/// <summary>
	/// updates all of the loop values of the insruments as well as their toggle's values
	/// </summary>
	public void UpdateLoop(){
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].loop = MasterLoopToggle.isOn;
			Instruments[i].LoopToggle.isOn = MasterLoopToggle.isOn;
		}//for
	}//UpdateLoop

	/// <summary>
	///  Creates a new instruemnt if there are less than 5 instruments in the list already
	///  Adds that instrument into the list of instruments and places it within the scene.
	/// </summary>
	public void NewInstrument(){
		if (Instruments.Count < 5) {
			GameObject NewInstrument = Instantiate (InstrumentTemplate, new Vector3 (offset, 200, 0), Quaternion.identity) as GameObject;
			NewInstrument.transform.parent = GameCanvas.transform;
			offset += 200;
			Instruments.Add (NewInstrument.GetComponent<InstrumentScript> ());
		}//if
	}//NewInstrument
}//MasterInstrument
