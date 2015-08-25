using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InstrumentScript : MonoBehaviour {

	public bool DebugMode;
	public int instrumentNumber;

	private AudioSource[] audioSources;
	private bool useFirstAudioSource = true;

	public AudioClip Instrument;
	int NumberOfNotes;

	public float volume;

	public List<Vector3> RawData;
	public List<float> Notes;
	private int currentNote = 0;
	int PerformanceLength = 60;

	public float noteValue = 0.5f;
	private bool playedNoteRecently = false;

	public Slider TempoSlider;
	public Slider VolumeSlider;
	public Toggle LoopToggle;
	public Slider TimeSlider;
	public Image Graph;

	float yLocation,downYLocation;
	float scrollSpeed = 1000f;
	bool ShowInstrumentControls = true;
	float scrollHeight = 520f;

	public bool loop = false;
	public bool play = false;
	
	private SoundLibrary soundLibrary;
	private TracingScript tracingScript;
	private DrawLine drawLine;
	public MasterInstrument masterInstrument;

	Camera mainCamera;
	// Use this for initialization
	void Start () {
		yLocation = this.transform.GetChild (0).transform.position.y + scrollHeight;
		downYLocation = yLocation - scrollHeight;

		mainCamera = Camera.main;

		audioSources = this.GetComponents<AudioSource> ();
		audioSources[0].clip = Instrument;
		audioSources[1].clip = Instrument;

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
		}//else

		GameObject DrawObject = GameObject.FindGameObjectWithTag("Draw");
		if (DrawObject != null) {
			drawLine = DrawObject.GetComponent<DrawLine> ();
		}//if
		else {
			Debug.Log ("Did not find object tagged Draw");
		}//else

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

		if (ShowInstrumentControls && this.transform.GetChild(0).transform.position.y > downYLocation) {
			for(int i = 0; i < this.transform.childCount; i++) {
				this.transform.GetChild(i).transform.Translate(new Vector3(0,-scrollSpeed * ((this.transform.GetChild(0).transform.position.y - downYLocation)/scrollHeight),0)*Time.deltaTime);
			}//foreach
		}//if
		else if(!ShowInstrumentControls && this.transform.GetChild(0).transform.position.y < yLocation){
			for(int i = 0; i < this.transform.childCount; i++) {
				this.transform.GetChild(i).transform.Translate(new Vector3(0,scrollSpeed * ((yLocation - this.transform.GetChild(0).transform.position.y)/scrollHeight),0)*Time.deltaTime);
			}//foreach
		}//else if

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
				if(currentPitch < NumberOfNotes*2 - 2){
					currentPitch += 2;
				}//if
			}//while

			if(noteValue < 0.5f){
				currentPitch += (NumberOfNotes*2*2);
				if(DebugMode){
					Debug.Log ("Played Eigth note " + currentPitch + " seconds");
				}//if
			}//if
			else if(noteValue < 1f){
				currentPitch += (NumberOfNotes*2);
				if(DebugMode){
					Debug.Log ("Played Quarter note " + currentPitch + " seconds");
				}//if
			}//else if
			else{
				if(DebugMode){
					Debug.Log ("Played Half note " + currentPitch + " seconds");
				}//if
			}//else

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

	/// <summary>
	/// Loads the data for instrument.
	/// </summary>
	/// <param name="GraphImage">Graph image.</param>
	/// <param name="GraphData">Graph data.</param>
	public void LoadDataForInstrument(Sprite GraphImage, List<Vector3> GraphData){
		Notes = new List<float>();
		float max = GraphData[0].y;
		float min = GraphData[0].y;
		Graph.sprite = GraphImage;
		RawData = new List<Vector3>(GraphData);

		GraphData = Normalize (PerformanceLength, GraphData);
		Debug.Log (Instrument.length);
		NumberOfNotes = Mathf.RoundToInt(Instrument.length)/6;
		if (DebugMode) {
			Debug.Log("NumberOfNotes " + NumberOfNotes);
		}
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

		TimeSlider.maxValue = Notes.Count - 1;
	}//LoadDataForInstrument

	public List<Vector3> Normalize(int performanceSeconds, List<Vector3> drawnPoints){
		
		int numBeats = Mathf.RoundToInt(performanceSeconds / TempoSlider.value);
		List<Vector3> normalized = new List<Vector3> (numBeats);//good
		float drawingDistance = drawnPoints [drawnPoints.Count - 1].x - drawnPoints [0].x;
		float xSpacing = drawingDistance / ((float)numBeats);
		
		for(int i = 0; i < numBeats; i++){
			
			Vector3 toAdd = new Vector3 (0, 0, 0);
			toAdd.z = mainCamera.nearClipPlane;
			toAdd.x = xSpacing * i;
			float behindX = 1000000f;
			float inFrontX = 0f;
			float behindY = 0f;
			float inFrontY = 0f;
			bool notFound = true;
			
			for(int j = 1; j < drawnPoints.Count; j++){
				
				float LPDist = drawnPoints [j].x - drawnPoints [0].x;
				
				if(LPDist >= toAdd.x && notFound){
					
					//Debug.Log (LPDist);
					//Debug.Log (toAdd.x);
					inFrontX = drawnPoints [j].x;
					inFrontY = drawnPoints [j].y;
					//Debug.Log (inFrontY);
					behindX = drawnPoints [j - 1].x;
					behindY = drawnPoints [j - 1].y;
					notFound = false;
					//Debug.Log (behindY);
				}
			}//good
			
			toAdd.y = behindY;
			float distBetweenX = inFrontX - behindX;
			float toSub = behindX - drawnPoints [0].x;
			float relativeDistIn = toAdd.x - toSub;
			float relativePercent = relativeDistIn / distBetweenX;
			float distBetweenY = inFrontY - behindY;
			//good
			bool up;
			
			if(distBetweenY > 0){
				
				distBetweenY = Mathf.Abs (distBetweenY);
				up = true;
			} 
			else{
				
				distBetweenY = Mathf.Abs (distBetweenY);
				up = false;
			}
			
			//Debug.Log (toAdd.y);
			if (up)
				toAdd.y = toAdd.y + (distBetweenY * relativePercent);
			else
				toAdd.y = toAdd.y - (distBetweenY * relativePercent);
			//Debug.Log (toAdd);
			normalized.Add(toAdd);
		}
		
		//Returns the normalized list of vector3's (AAJ)
		return(normalized);
	}//Normalize

	/// <summary>
	/// Delete this instance.
	/// </summary>
	public void Delete(){
		masterInstrument.DeleteInstrument (instrumentNumber);
		GameObject.Destroy(this.gameObject);
	}

	/// <summary>
	/// Shows the controls.
	/// </summary>
	public void ShowControls(){
		ShowInstrumentControls = true;
	}//ShowControls

	/// <summary>
	/// Hides the controls.
	/// </summary>
	public void HideControls(){
		ShowInstrumentControls = false;
	}//HideControls
}//InstrumentScript
