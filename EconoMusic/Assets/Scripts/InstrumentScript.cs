using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InstrumentScript : MonoBehaviour {

	private AudioSource audioSource;

	public List<AudioClip> InstrumentEigthNotes;
	public List<AudioClip> InstrumentQuarterNotes;
	public List<AudioClip> InstrumentHalfNotes;

	public AudioClip Instrument;

	public float volume;

	public List<float> Notes;
	private int currentNote = 0;

	public float noteValue = 0.5f;
	private bool playedNoteRecently = false;

	public Slider TempoSlider;
	public Slider VolumeSlider;
	public Toggle LoopToggle;
	public Slider TimeSlider;

	public bool loop = false;
	public bool play = false;

	private SoundLibrary soundLibrary;

	// Use this for initialization
	void Start () {
		//temporary NoteGenerator
		for (int i = 0; i < 100; i++) {
			Notes.Add(Random.value);
		}//for

		audioSource = this.GetComponent<AudioSource> ();

		TimeSlider.maxValue = Notes.Count - 1;

		GameObject temp = GameObject.FindGameObjectWithTag ("SoundLibrary");
		if (temp != null) {
			soundLibrary = temp.GetComponent<SoundLibrary> ();
		}
	}//Start
	
	// Update is called once per frame
	void Update () {
		noteValue = TempoSlider.value;
		volume = VolumeSlider.value;
		loop = LoopToggle.isOn;

		if (currentNote < Notes.Count && play) {
			PlayMusic ();
		}//if
		if (currentNote == Notes.Count && loop) {
			currentNote = 0;
			TimeSlider.value = 0;
		}//if
	}//Update

	/// <summary>
	/// Plays the music.
	/// </summary>
	void PlayMusic ()
	{
		if ((Time.time % noteValue) < 0.05f && !playedNoteRecently)//if a turn's length has passed
		{
			float pitchThreshold = Mathf.Pow (InstrumentQuarterNotes.Count, -1);
			int currentPitch = 0;
			while (Notes [currentNote] > pitchThreshold) {
				pitchThreshold += Mathf.Pow (InstrumentQuarterNotes.Count, -1);
				currentPitch++;
			}//while
//			Debug.Log ("Played note " + currentPitch);

			currentNote *= 2;

			if(noteValue < 0.25f){
				currentNote += (InstrumentQuarterNotes.Count*2*2);
				//audioSource.PlayOneShot (InstrumentEigthNotes [currentPitch], volume);
			}//if
			else if(noteValue < 0.5f){
				currentNote += (InstrumentQuarterNotes.Count*2);
				//audioSource.PlayOneShot (InstrumentQuarterNotes [currentPitch], volume);
			}//else if
			else{
				//audioSource.PlayOneShot (InstrumentHalfNotes [currentPitch], volume);
			}//else

			audioSource.Play(Instrument);

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
		List<List<AudioClip>> NewInstrument = new List<List<AudioClip>>();
		switch (choice) {
		case 1:
			NewInstrument = soundLibrary.vibraphone;
			break;
		}
		InstrumentEigthNotes = NewInstrument [0];
		InstrumentQuarterNotes = NewInstrument [1];
		InstrumentHalfNotes = NewInstrument [2];
	}
}//InstrumentScript
