using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {
	//Enumeration of the various instruments available. Currently only Vibraphone has audio files.
	public enum Instrument {vibraphone, politician, glaciar, mayonnaise, horseradish};

	//Lists of each of the notes for vibraphone that gets placed in the nested List.
	public List<AudioClip> vibraphoneE = new List<AudioClip>();
	public List<AudioClip> vibraphoneH = new List<AudioClip>();
	public List<AudioClip> vibraphoneQ = new List<AudioClip>();
	//Nested list for vibraphone that contains all of the notes.
	public List<List<AudioClip>> vibraphone = new List<List<AudioClip>>();
	
	void Awake () {
		//Fills the nested Array with its component parts.
		for (int i = 0; i < 36; i++) {
			vibraphone.Add (vibraphoneE);
			vibraphone.Add (vibraphoneH);
			vibraphone.Add (vibraphoneQ);
		}
	}

	//This function returns the List of sound files you're looking for by inputting the instrument enum.
	public List<List<AudioClip>> GetSoundFiles(Instrument instrumentName) {

		switch (instrumentName) {

		case Instrument.vibraphone:

			return vibraphone;

		default:

			return vibraphone;
		}

	}
}
