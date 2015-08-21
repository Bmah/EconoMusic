/// <summary>
/// Draw line by Daniel Schlesinger
/// </summary>
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
	//I made this variable public so I can use it in TracingScript (AAJ)
	public List<Vector3> linePoints = new List<Vector3>();
	LineRenderer lineRenderer;//draws the line
	public float startWidth = 1.0f;//width of the line, adjustable
	public float endWidth = 1.0f;//width of the line, match it with start
	public float threshold = 0.001f;//distance between notes
	Camera thisCamera;//not sure
	int lineCount = 0;//number of points?  not sure

	//I made this variable public so I can use it in GraphReceiver and TracingScript (AAJ)
	public bool drawing;//toggles left click drawing on and off
	public bool flushed;//sets true after first point, to flush the garbage values
	public float xThreshold = 0.001f;//controls minimum distance in x between two points

	//I made this variable public so I can use it in TracingScript (AAJ)
	public Vector3 lastPos = Vector3.one * float.MaxValue;
	//public float performanceSeconds;
	public float noteTimesTest = 1f;
	public GameObject test;

	void Awake()
	{
		thisCamera = Camera.main;
		lineRenderer = GetComponent<LineRenderer>();

		//I am restricting when you can draw (AAJ)
		drawing = false;
		flushed = false;
	}
	
	void Update()
	{
		if (drawing) {
			if (Input.GetMouseButton (0)) {

				Vector3 mousePos = Input.mousePosition;
				Vector3 mouseWorld = thisCamera.ScreenToWorldPoint (mousePos);
				mouseWorld.z = thisCamera.nearClipPlane;

		
				float dist = Vector3.Distance (lastPos, mouseWorld);
		
				if (dist <= threshold)
					return;
				if(flushed) {
					if(mouseWorld.x < lastPos.x)
						return;
					if(mouseWorld.x - lastPos.x <= xThreshold)
						return;
				}
				lastPos = mouseWorld;
				if (linePoints == null) 
					linePoints = new List<Vector3> ();
				//Debug.Log ("checking :");
				//Debug.Log (mouseWorld);
				linePoints.Add (mouseWorld);
				//Debug.Log ("Added: ");
				//Debug.Log (mouseWorld);
				UpdateLine (linePoints);
				flushed = true;
			}
		}
		if(Input.GetMouseButtonDown (1)){
			//I am restricting when you can draw (AAJ)
			//ToggleDraw();
		}
		//isRendered should prevent this part of the scirpt from deleting a line that isn't there (AAJ)
		if(Input.GetKeyDown (KeyCode.LeftArrow)){
			//Debug.Log (lineCount);
			UpdateLine (linePoints);
			//Debug.Log (lineCount);
			if(lineCount == 0)
				return;
			linePoints.RemoveAt (lineCount - 1);
			//Debug.Log ("before: ");
			//Debug.Log (lineCount);
			lineCount = lineCount - 1;
			//Debug.Log (lineCount-1);
			if(lineCount == 0) {
				lastPos = new Vector3(-30f,0,0);
				//Debug.Log (lastPos);
				//Debug.Log ("count of LinePos: ");
				//Debug.Log (linePoints.Count);
				return;
			}
			lastPos = linePoints [lineCount - 1];
			//Debug.Log (lastPos);
			//Debug.Log (linePoints[lineCount-1]);
			UpdateLine(linePoints);
		}
	}


	public void UpdateLine(List<Vector3> incoming)
	{
		//I need this function to public so that the line is
		//updated off the screen when I clear linePoints (AAJ)

		lineRenderer.SetWidth(startWidth, endWidth);
		lineRenderer.SetVertexCount(incoming.Count);
		
		for(int i = lineCount; i < incoming.Count; i++)
		{
			lineRenderer.SetPosition(i, incoming[i]);

		}
		lineCount = incoming.Count;
	}

	void ToggleDraw() {
		drawing = !drawing;
		Debug.Log (drawing);
	}

	public List<Vector3> Normalize(int performanceSeconds, List<Vector3> drawnPoints){

		int numBeats = Mathf.RoundToInt(performanceSeconds / noteTimesTest);
		List<Vector3> normalized = new List<Vector3> (numBeats);//good
		float drawingDistance = drawnPoints [lineCount - 1].x - drawnPoints [0].x;
		float xSpacing = drawingDistance / ((float)numBeats);

		for(int i = 0; i < numBeats; i++){

			Vector3 toAdd = new Vector3 (0, 0, 0);
			toAdd.z = thisCamera.nearClipPlane;
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

		//Throws an error (AAJ)
		//TestDraw(normalized);
	}

	void TestDraw(List<Vector3> normalized)
	{

		for(int i = 0; i < normalized.Count; i++)
		{
			Instantiate(test, normalized[i], Quaternion.identity);
			
		}
		lineCount = normalized.Count;
	}
}