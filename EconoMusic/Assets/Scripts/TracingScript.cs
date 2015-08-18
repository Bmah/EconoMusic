//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TracingScript : MonoBehaviour {

	//Holds teh Draw Line script so I can set when the line should be
	//drawn and can get the points that are drawn (AAJ)
	public DrawLine drawLine;

	//Holds the panel where the tracing will be done (AAJ)
	public GameObject tracingScreen;

	//Holds the points from the drawn line (AAJ)
	private List<Vector3> linePoints; 

	// Use this for initialization
	void Start(){
		
		//Disables the tracing at the start of the program (AAJ)
		drawLine.GetComponent<DrawLine>().drawing = false;
	}

	/// <summary>
	/// Turns tracing on (AAJ)
	/// </summary>
	public void DrawingOn(){

		//Enables the tracing (AAJ)
		drawLine.GetComponent<DrawLine>().drawing = true;
	}

	/// <summary>
	/// Truns tracing off (AAJ)
	/// </summary>
	public void DrawingOff(){

		//Disables the tracing (AAJ)
		drawLine.GetComponent<DrawLine>().drawing = false;
	}

	/// <summary>
	/// Confirms the tracing (AAJ)
	/// </summary>
	public void ConfirmTrace(){

		//Disables the tracing when the confirm button is pressed (AAJ)
		drawLine.GetComponent<DrawLine> ().drawing = false;

		//Gets the line points drawn in the tracing (AAJ)
		this.linePoints = drawLine.GetComponent<DrawLine>().linePoints;

		//disables the tracing screen (AAJ)
		tracingScreen.SetActive(false);

		//deletes the line so a new line can be drawn there later (AAJ)
		drawLine.GetComponent<DrawLine>().linePoints.Clear();

		//sets the line points list to null (AAJ)
		drawLine.GetComponent<DrawLine>().linePoints = null;

		//sets the last position to zero (AAJ)
		drawLine.GetComponent<DrawLine>().lastPos = new Vector3(-10000,0,0);

	}

	/// <summary>
	/// Sets the line points so the audio player 
	/// has something to play music off of. (AAJ)
	/// </summary>
	/// <returns>The line points.</returns>
	public List<Vector3> SetLinePoints(){

		//returns the list of points drawn on the screen (AAJ)
		return(linePoints);
	}
}