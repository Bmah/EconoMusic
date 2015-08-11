using UnityEngine;
using System.Collections.Generic;

public class InstrumentScript : MonoBehaviour {

	private AudioSource audioSource;

	public AudioClip[] InstrumentEigthNotes;
	public AudioClip[] InstrumentQuarterNotes;
	public AudioClip[] InstrumentHalfNotes;

	public float volume;

	public List<float> Notes;
	private int currentNote = 0;

	public float noteValue = 0.5f;
	private bool playedNoteRecently = false;

	// Use this for initialization
	void Start () {
		//temporary NoteGenerator
		for (int i = 0; i < 100; i++) {
			Notes.Add(Random.value);
		}

		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentNote < Notes.Count) {
			PlayMusic ();
		}
	}

	/// <summary>
	/// Plays the music.
	/// </summary>
	void PlayMusic ()
	{
		if ((Time.time % noteValue) < 0.1f && !playedNoteRecently)//if a turn's length has passed
		{
			float pitchThreshold = Mathf.Pow (InstrumentQuarterNotes.Length, -1);
			int currentPitch = 0;
			while (Notes [currentNote] > pitchThreshold) {
				pitchThreshold += Mathf.Pow (InstrumentQuarterNotes.Length, -1);
				currentPitch++;
			}
			Debug.Log ("Played note " + currentPitch);

			audioSource.PlayOneShot (InstrumentQuarterNotes [currentPitch], volume);

			playedNoteRecently = true;
			currentNote++;
		}
		if ((Time.time % noteValue) > 0.1f && (Time.time % noteValue) < 1f) {
			playedNoteRecently = false;
		}
	}
}
