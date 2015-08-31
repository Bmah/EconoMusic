//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphReceiver : MonoBehaviour, IDropHandler{

	//Holds the TracingScript so the drawObject can be set (AAJ)
	public TracingScript tracingScript;

	//Holds the tracing panel so it can be enabled (AAJ)
	public GameObject tracingScreen;

	//Holds the tracing graph so it can be given the graph (AAJ)
	public Image tracingGraph;

	//Holds a draw object to pass to the Tracing Script (AAJ)
	private GameObject drawObject;

	//Holds the master insturment so it can be disabled (AAJ)
	private GameObject[] instruments;

	//returns the first child (AAJ)
	public GameObject item{

		get{

			if (transform.childCount > 0){

				return transform.GetChild(0).gameObject;
			}//if
			return null;
		}//get
	}//item

	//This used to change the parent of the object being dragged
	//Now it changes the image being dropped on to the image being dropped (AAJ)
	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData){

		if(!item){

			//Copies the image from the dragged item to the instruments graph image (AAJ)
			GetComponent<Image>().sprite = DragAndDrop.itemBeingDragged.GetComponent<Image>().sprite;

			if(DragAndDrop.itemBeingDragged.GetComponent<LoadTexture>() != null){
				
				//Sets the tracing graphs sprite and enables the tracing screen (AAJ) 
				tracingGraph.sprite = DragAndDrop.itemBeingDragged.GetComponent<Image>().sprite;
				tracingScreen.SetActive(true);

				//Finds the DrawObject that will be used to trace the graph (AAJ)
				drawObject = GameObject.FindGameObjectWithTag("Draw");

				//Sets the DrawObject that will be used to trace the graph in the TracingScript (AAJ)
				tracingScript.SetDrawObject(drawObject);
				drawObject.GetComponent<DrawLine>().beingEdited = true;

				//Sets the name of the file (AAJ)
				tracingScript.SetFileName(DragAndDrop.itemBeingDragged.GetComponent<LoadTexture>().fileName);

				//Finds the instruments (AAJ)
				instruments = GameObject.FindGameObjectsWithTag("Instrument");
				
				//Moves any instruments that were moved up back down (AAJ)
				for(int i = 0; i < instruments.Length; i++){

					instruments[i].GetComponent<InstrumentScript>().MoveInsturmentUp();
				}//for

				//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
			}//if
			/*else{

				//Test Print
				Debug.Log("You are from an instrument!");
			}//esle*/
		}//if
	}//OnDrop
	#endregion
}