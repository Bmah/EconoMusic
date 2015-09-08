//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TracingScript : MonoBehaviour {

	//Holds the panel where the tracing will be done (AAJ)
	public GameObject tracingScreen;

	//Holdas the rect of the image
	public RectTransform tracingGraphRect;

	//Holds the image that will be traced on (AAJ)
	public Image tracingGraph;

	//Holds the confirm button so it cannot be selected (AAJ)
	public Button confrimButton;

	//Holds the New Instrument button so it can be enabled 
	//when a new instrument is ready to be made (AAJ)
	public Button newInstrument;

	//Holds the line that is used for tracing (AAJ)
	private GameObject drawObject;

	//Holds the points from the drawn line (AAJ)
	private List<Vector3> linePoints; 

	//Holds the name of the file so it can be used to display on an instrument (AAJ)
	private string fileName;

	//Holds the insturments so they can be hidden/shown (AAJ)
	private GameObject[] instruments;

	//Holds the Master Instrument so it can be hidden/shown 
	public MasterInstrument masterInstrument;

	// Use this for initialization
	void Start(){

		//disables the the tracing screen at the start of
		//the game in case someone accidentally enables it (AAJ)
		tracingScreen.SetActive(false);
		//Debug.Log ("here");

	}//Start

	/// <summary>
	/// Sets the drawObject so that the user has a 
	/// line to trace with (AAJ)
	/// </summary>
	public void SetDrawObject(GameObject drawObject){

		this.drawObject = drawObject;
	}//SetDrawObject

	/// <summary>
	/// Sets the name of the file (AAJ)
	/// </summary>
	public void SetFileName(string fileName){

		this.fileName = fileName;
	}//SetFileName

	// Update is called once per frame
	void Update(){

		//makes sure that the drawObject is not null (AAJ)
		if(drawObject != null){

			if(drawObject.GetComponent<DrawLine>().linePoints.Count > 0){

				confrimButton.interactable = true;
			}//if
			else{

				confrimButton.interactable = false;
			}//else
		}//if
	}//Upadte

	/// <summary>
	/// Turns tracing on (AAJ)
	/// </summary>
	public void DrawingOn(){

		//makes sure that the drawObject is not null (AAJ)
		if(drawObject != null){

			//Enables the tracing (AAJ)
			drawObject.GetComponent<DrawLine>().drawing = true;
			drawObject.GetComponent<DrawLine>().beingEdited = true;
		}//if
	}//DrawingOn

	/// <summary>
	/// Truns tracing off (AAJ)
	/// </summary>
	public void DrawingOff(){
		
		//makes sure that the drawObject is not null (AAJ)
		if(drawObject != null){
			
			//Disables the tracing (AAJ)
			drawObject.GetComponent<DrawLine>().drawing = false;
		}//if
	}//DrawingOff

	/// <summary>
	///Confirms the tracing (AAJ)
	/// </summary>
	public void ConfirmTrace(){

		//Disables the tracing when the confirm button is pressed (AAJ)
		drawObject.GetComponent<DrawLine>().drawing = false;

		//Finds the instruments (AAJ)
		instruments = GameObject.FindGameObjectsWithTag("Instrument");

		//Moves any instruments that were moved up back down (AAJ)
		for(int i = 0; i < instruments.Length; i++){

			instruments[i].GetComponent<InstrumentScript>().MoveInsturmentDown();
		}//for

		masterInstrument.ShowMasterInstrument ();

		//Gets the line points drawn in the tracing (AAJ)
		linePoints = new List<Vector3>(drawObject.GetComponent<DrawLine>().linePoints);

		//Disables the tracing screen (AAJ)
		tracingScreen.SetActive(false);

		//Clears the previous line's points so they will not render on the screen (AAJ)
		drawObject.GetComponent<DrawLine>().linePoints.Clear();

		//Removes the line from the screen (AAJ)
		drawObject.GetComponent<DrawLine>().UpdateLine(new List<Vector3>());

		//Instantiates a line that can be used to trace a graph (AAJ)
		GameObject newDrawObject = Instantiate(drawObject, new Vector3(-557.7203f,-226.53f,0), Quaternion.identity) as GameObject;
		newDrawObject.transform.SetParent(tracingScreen.transform, true);
		drawObject.GetComponent<DrawLine> ().beingEdited = false;
		
		//Destroys the previous line (AAJ)
		Destroy(drawObject);

		//Enables the New Instrumetn button (AAJ)
		newInstrument.interactable = true;
	}//ConfrimTrace

	/// <summary>
	/// Gets the name of the file (AAJ)
	/// </summary>
	public string GetFileName(){

		//returns the file name so an instrumetn can display it (AAJ)
		return(fileName);
	}//GetFileName

	/// <summary>
	/// Gives the line points to the audio player so it 
	/// has something to play music off of (AAJ)
	/// </summary>
	public List<Vector3> GetLinePoints(){

		//returns the list of points drawn on the screen (AAJ)
		return(linePoints);
	}//GetLinePoints

	/// <summary>
	/// Gives the sprite to an instrument (AAJ)
	/// </summary>
	public Sprite GetSprite(){
		
		//returns the sprite (AAJ)
		return(tracingGraph.sprite);
	}//GetSprite
}