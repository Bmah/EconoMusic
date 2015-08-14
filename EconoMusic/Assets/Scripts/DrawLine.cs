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
	
	Vector3 lastPos = Vector3.one * float.MaxValue;
	//Vector3 prevPos = lastPos;
	
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
				}
				lastPos = mouseWorld;
				if (linePoints == null) 
					linePoints = new List<Vector3> ();
				Debug.Log ("checking :");
				Debug.Log (mouseWorld);
				linePoints.Add (mouseWorld);
				Debug.Log ("Added: ");
				Debug.Log (mouseWorld);
				UpdateLine ();
				flushed = true;
			}
		}
		if(Input.GetMouseButtonDown(1))
			ToggleDraw();
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			linePoints.RemoveAt (lineCount - 1);
			Debug.Log ("before: ");
			Debug.Log (lineCount-1);
			lineCount = lineCount - 1;
			Debug.Log (lineCount-1);
			lastPos = linePoints [lineCount - 1];
			Debug.Log (linePoints[lineCount-1]);
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

}