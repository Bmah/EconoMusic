using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {
	//These are the variables for the various sound files.
	public AudioClip altoSaxophone, bassoon, cello, clarinet, digeredoo, doubleBass, guitar;
	public AudioClip harp, piano, theremin, timpani, trombone, trumpet, vibraphone;
	public AudioClip violin, vocalBass, vocalSoprano;


	////////////////////EVERYTHING BELOW IS LEGACY CODE
//	//Enumeration of the various instruments available. Currently only Vibraphone has audio files.
//	public enum Instrument {vibraphone, theramin, sax, digeredoo, trumpet, clarinet, politician, glaciar, mayonnaise, horseradish};
//
//	//Lists of each of the notes for vibraphone that gets placed in the nested List.
//	public List<AudioClip> vibraphoneE, vibraphoneQ, vibraphoneH = new List<AudioClip>();
//	//Nested list for vibraphone that contains all of the notes.
//	public List<List<AudioClip>> vibraphone = new List<List<AudioClip>>();

//	public List<AudioClip> theraminE, theraminQ, theraminH = new List<AudioClip>();
//	public List<List<AudioClip>> theramin = new List<List<AudioClip>> ();
//
//	public List<AudioClip> saxE, saxQ, saxH = new List<AudioClip>();
//	public List<List<AudioClip>> sax = new List<List<AudioClip>>();
//
//	public List<AudioClip> digeredooE, digeredooQ, digeredooH = new List<AudioClip>();
//	public List<List<AudioClip>> digeredoo = new List<List<AudioClip>>();
//
//	public List<AudioClip> trumpetE, trumpetQ, trumpetH = new List<AudioClip>();
//	public List<List<AudioClip>> trumpet = new List<List<AudioClip>>();
//
//	public List<AudioClip> clarinetE, clarinetQ, clarinetH = new List<AudioClip>();
//	public List<List<AudioClip>> clarinet = new List<List<AudioClip>>();

//	void Awake () {
//		//Fills the nested Array with its component parts.
//		vibraphone.Add (vibraphoneE);
//		vibraphone.Add (vibraphoneQ);
//		vibraphone.Add (vibraphoneH);
////		theramin.Add (theraminE);
////		theramin.Add (theraminQ);
////		theramin.Add (theraminH);
////		sax.Add (saxE);
////		sax.Add (saxQ);
////		sax.Add (saxH);
////		digeredoo.Add (digeredooE);
////		digeredoo.Add (digeredooQ);
////		digeredoo.Add (digeredooH);
////		trumpet.Add (trumpetE);
////		trumpet.Add (trumpetQ);
////		trumpet.Add (trumpetH);
////		clarinet.Add (clarinetE);
////		clarinet.Add (clarinetQ);
////		clarinet.Add (clarinetH);
//	}

//	//This function returns the List of sound files you're looking for by inputting the instrument enum.
//	public List<List<AudioClip>> GetSoundFiles(Instrument instrumentName) {
//		//vibraphone, theramin, sax, digeredoo, trumpet, clarinet, politician, glaciar, mayonnaise, horseradish
//
//		switch (instrumentName) {
//
//		case Instrument.vibraphone:
//
//			return vibraphone;
//
////		case Instrument.theramin:
////
////			return theramin;
////
////		case Instrument.sax:
////
////			return sax;
////
////		case Instrument.digeredoo:
////
////			return digeredoo;
////
////		case Instrument.trumpet:
////
////			return trumpet;
////
////		case Instrument.clarinet:
////
////			return clarinet;
//
//		default:
//
//			return vibraphone;
//		}
//
//	}
}
