using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InstrumentScript : MonoBehaviour {

	private AudioSource audioSource;

	public List<AudioClip> InstrumentEigthNotes;
	public List<AudioClip> InstrumentQuarterNotes;
	public List<AudioClip> InstrumentHalfNotes;

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

	// Use this for initialization
	void Start () {
		//temporary NoteGenerator
		for (int i = 0; i < 100; i++) {
			Notes.Add(Random.value);
		}

		audioSource = this.GetComponent<AudioSource> ();

		TimeSlider.maxValue = Notes.Count - 1;
	}
	
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

			if(noteValue < 0.25f){
				audioSource.PlayOneShot (InstrumentEigthNotes [currentPitch], volume);
			}//if
			else if(noteValue < 0.5f){
				audioSource.PlayOneShot (InstrumentQuarterNotes [currentPitch], volume);
			}//else if
			else{
				audioSource.PlayOneShot (InstrumentHalfNotes [currentPitch], volume);
			}//else

			playedNoteRecently = true;
			currentNote++;
			TimeSlider.value = currentNote;
		}//if
		if ((Time.time % noteValue) > 0.05f && (Time.time % noteValue) < 0.1f) {
			playedNoteRecently = false;
		}//if
	}//PlayMusic

	public void PlayInstument(){
		play = true;
	}//PlayInstrument

	public void PauseInstrument(){
		play = false;
	}//PauseInstrument

	public void UseTimeSlider(){
		currentNote = Mathf.RoundToInt(TimeSlider.value);
	}//UseTimeSlider
}//InstrumentScript
