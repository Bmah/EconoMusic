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
	public float NumberOfInstruments = 1;

	//Holds the name of the file so ApplyEdit can get it(AAJ)
	public string fileName;

	//Holds the text box that displays the file name on the screen (AAJ)
	public Text fileNameText;

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
	public Slider NotesSlider;

	//Holds the previous position that child 0 was located at (AAJ)
	float previousPosition;
	float yLocation,downYLocation;
	float scrollSpeed = 4000f;
	bool ShowInstrumentControls = true;
	bool instrumentMoved = false;
	float scrollHeight = 520f;

	public bool loop = false;
	public bool play = false;
	
	private SoundLibrary soundLibrary;
	private TracingScript tracingScript;
	//private DrawLine drawLine;
	public MasterInstrument masterInstrument;
	public GameObject drawObject;
	public GameObject graphSuspended;
	//Holds the image for the instrument (AAJ)
	public GameObject graphImage;
	public Material Mat1;
	public Material Mat2;
	public Material Mat3;
	public Material Mat4;
	public Material Mat5;

	Camera mainCamera;

	// Use this for initialization

	void Start () {
		CreateBGGraph ();

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
		/*
		GameObject DrawObject = GameObject.FindGameObjectWithTag("Draw");
		if (DrawObject != null) {
			drawLine = DrawObject.GetComponent<DrawLine> ();
		}//if
		else {
			Debug.Log ("Did not find object tagged Draw");
		}//else
		*/
		LoadDataForInstrument(tracingScript.GetSprite(),tracingScript.GetLinePoints(),tracingScript.GetFileName());

		graphSuspended.GetComponent<DrawLine> ().UpdateLine (RawData);
	}//Start
	
	// Update is called once per frame
	void Update () {
		ColorSet ();
		noteValue = TempoSlider.value;
		if (NumberOfInstruments == 1) {
			volume = VolumeSlider.value;
		}
		else {
			volume = VolumeSlider.value/(NumberOfInstruments - 1);
		}
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

		if(ShowInstrumentControls && this.transform.position.y > downYLocation) {

			this.transform.Translate(new Vector3(0,-scrollSpeed * ((this.transform.position.y - downYLocation)/scrollHeight),0)*Time.deltaTime);

			//If the instrument is moving hide the image, other wise, show it
			if(previousPosition == transform.position.y){
				
				//Reveals the instruments image so it can be interacted with (AAJ)
				graphImage.SetActive(true);
			}//if
		}//if
		else if(!ShowInstrumentControls && this.transform.position.y < yLocation){

			this.transform.Translate(new Vector3(0,scrollSpeed * ((yLocation - this.transform.position.y)/scrollHeight),0)*Time.deltaTime);

			//Hides the instruments image so it cannot be interacted with (AAJ)
			graphImage.SetActive(false);
		}//else if

		//Updates previousPosition with the last place the child 0 was located (AAJ)
		previousPosition = transform.position.y;

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

	public void SetData(){
		PerformanceLength = Mathf.RoundToInt(NotesSlider.value);
		LoadDataForInstrument (Graph.sprite, RawData, this.fileName);
	}

	/// <summary>
	/// Loads the data for instrument.
	/// </summary>
	/// <param name="GraphImage">Graph image.</param>
	/// <param name="GraphData">Graph data.</param>
	public void LoadDataForInstrument(Sprite GraphImage, List<Vector3> GraphData, string fileName){
		Notes = new List<float>();
		float max = GraphData[0].y;
		float min = GraphData[0].y;
		Graph.sprite = GraphImage;
		RawData = new List<Vector3>(GraphData);
		//Indirectly gets the file name from Tracing Scripts getter function (AAJ)
		this.fileName = fileName;

		//Updates the file name text box (AAJ)
		fileNameText.text = fileName;

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

		//alters the slider so no out of bounds errors
		TimeSlider.maxValue = Notes.Count - 1;
		if (TimeSlider.value >= TimeSlider.maxValue) {
			TimeSlider.value = TimeSlider.maxValue;
			currentNote = Mathf.RoundToInt(TimeSlider.maxValue);
		}//if
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
		GameObject.Destroy (graphSuspended);
		GameObject.Destroy(this.gameObject);
	}

	/// <summary>
	/// Toggles the controls. (AAJ)
	/// </summary>
	public void ToggleControls(){
		
		if(ShowInstrumentControls == true){
			
			ShowInstrumentControls = false;
		}//if
		else{
			
			ShowInstrumentControls = true;
		}//else
	}//ToggleControls

	/// <summary>
	/// Moves the insturment up when a line has been traced(AAJ)
	/// </summary>
	public void MoveInsturmentUp(){
		
		if(ShowInstrumentControls == true){

			ShowInstrumentControls = false;
			instrumentMoved = true;
		}//if
	}//MoveInsturmentUp

	/// <summary>
	/// Moves the insturment down once tracing is finished (AAJ)
	/// </summary>
	public void MoveInsturmentDown(){

		if(instrumentMoved == true){

			ShowInstrumentControls = true;
			instrumentMoved = false;
		}//if
	}//MoveInstrumentDown

	public void ColorSet() {
		switch (instrumentNumber) {
		case 0:
			if(graphSuspended.GetComponent<DrawLine>().lineRenderer.material == Mat1)
				break;
			graphSuspended.GetComponent<DrawLine> ().lineRenderer.material = Mat1;
			graphSuspended.GetComponent<DrawLine>().UpdateLine(RawData);
			break;
		case 1:
			if(graphSuspended.GetComponent<DrawLine>().lineRenderer.material == Mat2)
				break;
			graphSuspended.GetComponent<DrawLine> ().lineRenderer.material = Mat2;
			graphSuspended.GetComponent<DrawLine>().UpdateLine(RawData);
			break;
		case 2:
			if(graphSuspended.GetComponent<DrawLine>().lineRenderer.material == Mat3)
				break;
			graphSuspended.GetComponent<DrawLine> ().lineRenderer.material = Mat3;
			graphSuspended.GetComponent<DrawLine>().UpdateLine(RawData);
			break;
		case 3:
			if(graphSuspended.GetComponent<DrawLine>().lineRenderer.material == Mat4)
				break;
			graphSuspended.GetComponent<DrawLine> ().lineRenderer.material = Mat4;
			graphSuspended.GetComponent<DrawLine>().UpdateLine(RawData);
			break;
		case 4:
			if(graphSuspended.GetComponent<DrawLine>().lineRenderer.material == Mat5)
				break;
			graphSuspended.GetComponent<DrawLine> ().lineRenderer.material = Mat5;
			graphSuspended.GetComponent<DrawLine>().UpdateLine(RawData);
			break;
		}
	}//ColorSet
	public void CreateBGGraph() {
		graphSuspended = Instantiate (drawObject, this.transform.position, this.transform.rotation) as GameObject;
		graphSuspended.GetComponent<DrawLine> ().drawing = false;
		graphSuspended.GetComponent<DrawLine> ().lineRenderer = graphSuspended.GetComponent<LineRenderer> ();
		yLocation = this.transform.position.y + scrollHeight;
		downYLocation = yLocation - scrollHeight;
		
		//Inititalizes previous position with the start postition (AAJ)
		previousPosition = transform.position.y;
	}//CreateBGGraph
}//InstrumentScript