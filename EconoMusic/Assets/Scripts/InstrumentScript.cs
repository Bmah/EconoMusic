using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InstrumentScript : MonoBehaviour {

	private AudioSource[] audioSources;
	private bool useFirstAudioSource = true;

	public AudioClip Instrument;
	int NumberOfNotes = 36;

	public float volume;

	public List<Vector3> RawData;
	public List<float> Notes;
	private int currentNote = 0;

	public float noteValue = 0.5f;
	private bool playedNoteRecently = false;

	public Slider TempoSlider;
	public Slider VolumeSlider;
	public Toggle LoopToggle;
	public Slider TimeSlider;
	public Image Graph;

	public bool loop = false;
	public bool play = false;

	private SoundLibrary soundLibrary;
	private TracingScript tracingScript;

	// Use this for initialization
	void Start () {
		//temporary notes
		for (int i = 0; i < 100; i++) {
			Notes.Add(Random.value);
		}//for

		audioSources = this.GetComponents<AudioSource> ();
		audioSources[0].clip = Instrument;
		audioSources[1].clip = Instrument;

		TimeSlider.maxValue = Notes.Count - 1;

		GameObject temp = GameObject.FindGameObjectWithTag ("SoundLibrary");
		if (temp != null) {
			soundLibrary = temp.GetComponent<SoundLibrary> ();
		}//if

		GameObject temp2 = GameObject.FindGameObjectWithTag("Trace");
		if (temp2 != null) {
			tracingScript = temp2.GetComponent<TracingScript> ();
			if(tracingScript == null){
				Debug.LogError("tracing script is null");
			}
		}//if
		else {
			Debug.Log ("Nothing Found");
		}

		LoadDataForInstrument (tracingScript.GetSprite(),tracingScript.GetLinePoints());
	}//Start
	
	// Update is called once per frame
	void Update () {
		noteValue = TempoSlider.value;
		volume = VolumeSlider.value;
		audioSources[0].volume = volume;
		audioSources[1].volume = volume;
		loop = LoopToggle.isOn;

		if (currentNote < Notes.Count && play) {
			PlayMusic ();
		}//if

		if (!play) {
			audioSources[0].Stop();
			audioSources[1].Stop();
		}//if

		if (currentNote == Notes.Count) {
			if(loop){
				currentNote = 0;
				TimeSlider.value = 0;
			}//if
			else{
				audioSources[0].Stop();
				audioSources[1].Stop();
			}//else
		}//if
	}//Update

	/// <summary>
	/// Plays the music.
	/// </summary>
	void PlayMusic ()
	{
		if ((Time.time % noteValue) < 0.05f && !playedNoteRecently)//if a turn's length has passed
		{
			float pitchThreshold = Mathf.Pow (NumberOfNotes, -1);
			int currentPitch = 0;

			//while the note is less than the current pitch skip forwards in the music 1 measure(2 seconds) also add 1 note to the pitch
			while (Notes [currentNote] > pitchThreshold) {
				pitchThreshold += Mathf.Pow (NumberOfNotes, -1);
				currentPitch += 2;
			}//while
//			Debug.Log ("Played note " + currentPitch);

			if(noteValue < 0.25f){
				currentPitch += (NumberOfNotes*4);
			}//if
			else if(noteValue < 0.5f){
				currentPitch += (NumberOfNotes*2);
			}//else if

			if(useFirstAudioSource){
				audioSources[0].time = currentPitch;
				audioSources[0].Play();
			}//if
			else{
				audioSources[1].time = currentPitch;
				audioSources[1].Play();
			}//else

			if(noteValue > 1f){
				useFirstAudioSource = true;
				audioSources[1].Stop();
			}//if
			else{
				useFirstAudioSource = !useFirstAudioSource;
			}//else

			playedNoteRecently = true;
			currentNote++;
			TimeSlider.value = currentNote;
		}//if
		if ((Time.time % noteValue) > 0.05f && (Time.time % noteValue) < 0.1f) {
			playedNoteRecently = false;
		}//if
	}//PlayMusic

	/// <summary>
	/// Plays the instument.
	/// </summary>
	public void PlayInstument(){
		play = true;
	}//PlayInstrument

	/// <summary>
	/// Pauses the instrument.
	/// </summary>
	public void PauseInstrument(){
		play = false;
	}//PauseInstrument

	/// <summary>
	/// Called when the Time Slider is changed and is used to alter the current note.
	/// </summary>
	public void UseTimeSlider(){
		currentNote = Mathf.RoundToInt(TimeSlider.value);
	}//UseTimeSlider

	/// <summary>
	/// Loads the instrument.
	/// </summary>
	public void LoadInstrument(int choice){
		switch (choice) {
		case 0:
			Instrument = soundLibrary.vibraphone;
			break;
		case 1:
			Instrument = soundLibrary.altoSaxophone;
			break;
		}
		audioSources [0].clip = Instrument;
		audioSources [1].clip = Instrument;
		NumberOfNotes = Mathf.RoundToInt(Instrument.length)/6;
	}

	public void LoadDataForInstrument(Sprite GraphImage, List<Vector3> GraphData){
		Notes = new List<float>();
		float max = GraphData[0].y;
		float min = GraphData[0].y;
		Graph.sprite = GraphImage;
		RawData = GraphData;
		for (int i = 0; i < GraphData.Count; i++) {
			if(GraphData[i].y > max){
				max = GraphData[i].y;
			}//if
			else if(GraphData[i].y < min){
				min = GraphData[i].y;
			}//else if
		}//for

		for (int i = 0; i < GraphData.Count; i++) {
			Notes.Add(Mathf.InverseLerp(min,max,GraphData[i].y));
		}//for
	}//LoadDataForInstrument
}//InstrumentScript
