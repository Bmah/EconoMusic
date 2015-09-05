using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MasterInstrument : MonoBehaviour {
	
	public GameObject InstrumentTemplate;
	public List<InstrumentScript> Instruments = new List<InstrumentScript> ();

	public Slider MasterTempoSlider;
	public Slider MasterVolumeSlider;
	public Toggle MasterLoopToggle;

	float offset;

	public Image playButtonImage, pauseButtonImage, loopButtonImage;
	private Color invisible = new Color(0f,0f,0f,0f);
	private Color visible = new Color(1f,1f,1f,1f);

	public Sprite[] tabColors;

	// Use this for initialization
	void Start () {
		if (InstrumentTemplate == null) {
			Debug.LogError("Please Insert InstrumentTemplate into MasterInstrument Script");
		}//if
		offset = this.transform.position.x;
	}//Start
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Plays all instruments.
	/// </summary>
	public void PlayAll(){
		playButtonImage.color = visible;
		pauseButtonImage.color = invisible;
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].PlayInstument();
		}//for
	}//PlayAll

	/// <summary>
	/// Pauses all instruments.
	/// </summary>
	public void PauseAll(){
		playButtonImage.color = invisible;
		pauseButtonImage.color = visible;
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].PauseInstrument();
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
		if (MasterLoopToggle.isOn) {
			loopButtonImage.color = visible;
		}//if
		else {
			loopButtonImage.color = invisible;
		}
		for (int i = 0; i < Instruments.Count; i++) {
			Instruments[i].LoopToggle.isOn = MasterLoopToggle.isOn;
		}//for
	}//UpdateLoop

	/// <summary>
	///  Creates a new instruemnt if there are less than 5 instruments in the list already
	///  Adds that instrument into the list of instruments and places it within the scene.
	/// </summary>
	public void NewInstrument(){
		if (Instruments.Count < 5) {
			offset += 200;
			GameObject NewInstrument = Instantiate (InstrumentTemplate, new Vector3 (offset, this.transform.position.y - 120, 0), Quaternion.identity) as GameObject;
			NewInstrument.transform.SetParent(this.transform);
			NewInstrument.GetComponent<InstrumentScript>().masterInstrument = this;
			NewInstrument.GetComponent<InstrumentScript>().instrumentNumber = Instruments.Count;
			NewInstrument.GetComponent<InstrumentScript>().tabImage.sprite = tabColors[Instruments.Count];
			Instruments.Add (NewInstrument.GetComponent<InstrumentScript> ());
			//updates the number of instruments in each instrument
			for(int i = 0; i < Instruments.Count; i++){
				Instruments[i].GetComponent<InstrumentScript>().NumberOfInstruments = Instruments.Count;
			}
		}//if
	}//NewInstrument

	/// <summary>
	/// 
	/// </summary>
	public void DeleteInstrument(int index){
		//InstrumentScript temp = Instruments [index];
		Instruments.RemoveAt (index);
		offset -= 200;
		for (int i = index; i < Instruments.Count; i++) {
			Instruments[i].transform.position = new Vector3(Instruments[i].transform.position.x - 200, Instruments[i].transform.position.y, Instruments[i].transform.position.z);
			Instruments[i].instrumentNumber -= 1;
			Instruments[i].tabImage.sprite = tabColors[Instruments[i].instrumentNumber];
		}//for

		//updates the number of instruments in each instrument
		for(int i = 0; i < Instruments.Count; i++){
			Instruments[i].GetComponent<InstrumentScript>().NumberOfInstruments = Instruments.Count;
		}
	}//Delete Instrument
}//MasterInstrument
