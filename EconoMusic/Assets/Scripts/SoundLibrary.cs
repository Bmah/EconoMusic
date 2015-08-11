using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {
	public enum Instrument {vibraphone, politician, glaciar, mayonnaise, horseradish};
	public Instrument instrument = Instrument.mayonnaise;

	public List<AudioClip> vibraphoneE = new List<AudioClip>();
	public List<AudioClip> vibraphoneH = new List<AudioClip>();
	public List<AudioClip> vibraphoneQ = new List<AudioClip>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
