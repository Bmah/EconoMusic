/// <summary>
/// Draw line by Daniel Schlesinger
/// </summary>
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
	List<Vector3> linePoints = new List<Vector3>();
	LineRenderer lineRenderer;//draws the line
	public float startWidth = 1.0f;//width of the line, adjustable
	public float endWidth = 1.0f;//width of the line, match it with start
	public float threshold = 0.001f;//distance between notes
	Camera thisCamera;//not sure
	int lineCount = 0;//number of points?  not sure
	private bool drawing;//toggles left click drawing on and off
	private bool flushed;//sets true after first point, to flush the garbage values
	public float xThreshold = 0.001f;//controls minimum distance in x between two points
	Vector3 lastPos = Vector3.one * float.MaxValue;
	public float performanceSeconds;
	public float noteTimesTest = 1f;
	public GameObject test;
	
	void Awake()
	{
		thisCamera = Camera.main;
		lineRenderer = GetComponent<LineRenderer>();
		drawing = true;
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
				UpdateLine ();
				flushed = true;
			}
		}
		if(Input.GetMouseButtonDown(1))
			ToggleDraw();
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if(lineCount == 0)
				return;
			linePoints.RemoveAt (lineCount - 1);
			//Debug.Log ("before: ");
			//Debug.Log (lineCount-1);
			lineCount = lineCount - 1;
			//Debug.Log (lineCount-1);
			if(lineCount == 0) {
				lastPos = new Vector3(0,0,0);
				return;
			}
			lastPos = linePoints [lineCount - 1];
			//Debug.Log (linePoints[lineCount-1]);
			UpdateLine();
		}
	}


	void UpdateLine()
	{
		lineRenderer.SetWidth(startWidth, endWidth);
		lineRenderer.SetVertexCount(linePoints.Count);
		
		for(int i = lineCount; i < linePoints.Count; i++)
		{
			lineRenderer.SetPosition(i, linePoints[i]);

		}
		lineCount = linePoints.Count;
	}

	void ToggleDraw() {
		drawing = !drawing;
		Debug.Log (drawing);
	}

	public void Normalize(/*pass in variable for Note time*/) {
		int numBeats = Mathf.RoundToInt(performanceSeconds / noteTimesTest);
		List<Vector3> normalized = new List<Vector3> (numBeats);//good
		float drawingDistance = linePoints [lineCount - 1].x - linePoints [0].x;
		float xSpacing = drawingDistance / ((float)numBeats);
		for (int i = 0; i < numBeats; i++) {
			Vector3 toAdd = new Vector3 (0, 0, 0);
			toAdd.z = thisCamera.nearClipPlane;
			toAdd.x = xSpacing * i;
			float behindX = 1000000f;
			float inFrontX = 0f;
			float behindY = 0f;
			float inFrontY = 0f;
			bool notFound = true;
			for (int j = 1; j < linePoints.Count; j++) {
				float LPDist = linePoints [j].x - linePoints [0].x;
				if (LPDist >= toAdd.x && notFound) {
					//Debug.Log (LPDist);
					//Debug.Log (toAdd.x);
					inFrontX = linePoints [j].x;
					inFrontY = linePoints [j].y;
					//Debug.Log (inFrontY);
					behindX = linePoints [j - 1].x;
					behindY = linePoints [j - 1].y;
					notFound = false;
					//Debug.Log (behindY);
				}
			}//good
			toAdd.y = behindY;
			float distBetweenX = inFrontX - behindX;
			float toSub = behindX - linePoints [0].x;
			float relativeDistIn = toAdd.x - toSub;
			float relativePercent = relativeDistIn / distBetweenX;
			float distBetweenY = inFrontY - behindY;
			//good
			bool up;
			if (distBetweenY > 0) {
				distBetweenY = Mathf.Abs (distBetweenY);
				up = true;
			} else {
				distBetweenY = Mathf.Abs (distBetweenY);
				up = false;
			}

			//Debug.Log (toAdd.y);
			if (up)
				toAdd.y = toAdd.y + (distBetweenY * relativePercent);
			else
				toAdd.y = toAdd.y - (distBetweenY * relativePercent);
			Debug.Log (toAdd);
			normalized.Add(toAdd);
		}

		TestDraw (normalized);

		
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