//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TracingScript : MonoBehaviour {

	//Holds the selected image (AAJ)
	public Image selectdImage;

	//Holds the panel where the tracing will be done
	public GameObject tracingScreen;

	//Holds the image that will be displayed on the screen
	public Image tracingGraph;

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){

		tracingGraph.sprite = selectdImage.sprite;
	}

	/// <summary>
	/// Confirms the tracing (AAJ)
	/// </summary>
	public void ConfirmTrace(){

		tracingScreen.SetActive(false);
	}
}