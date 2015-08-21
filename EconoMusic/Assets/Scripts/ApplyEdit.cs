//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApplyEdit : MonoBehaviour, IDropHandler{

	//Holds whether or not this is an instrument (AAJ)
	private bool isInstrument = false;

	//Holds the TracingManger so this script can get
	//the components of the tracing scren (AAJ)
	private GameObject tracingManager;

	//Holds the tracing script so the graphs can be
	//traced and edited (AAJ)
	private TracingScript tracingScript;

	//Holds the tracing screen (AAJ)
	private GameObject tracingScreen;

	//Holds the tracing image (AAJ)
	private Image tracingGraph;

	//Holds the DrawObject so the line can be
	//traced or edited (AAJ)
	private GameObject DrawObject;

	// Use this for initialization
	void Start ()
	{
		//If you are not the graph receiver, then you
		//are an instrument (AAJ)
		if(GetComponent<GraphReceiver>() == null){

			//Sets this instance of ApplyEdit as an instrument (AAJ)
			isInstrument = true;
		}//if

		//Finds the tracing manager (AAJ)
		tracingManager = GameObject.FindGameObjectWithTag("Trace");

		//Gets the components of the tracing screen (AAJ)
		tracingScript = tracingManager.GetComponent<TracingScript>();
		tracingScreen = tracingScript.tracingScreen;
		tracingGraph = tracingScript.tracingGraph;

	}//Start

	//Returns the first child (AAJ)
	public GameObject item{
		
		get{
			
			if (transform.childCount > 0){
				
				return transform.GetChild(0).gameObject;
			}//if
			return null;
		}//get
	}//item
	
	//changes the parent of the object being dragged (AAJ)
	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData){
		
		if(!item){

			if(DragAndDrop.itemBeingDragged.GetComponent<GraphReceiver>() != null){

				//Copies the image from the dragged item to the instruments graph image (AAJ)
				GetComponent<Image>().sprite = DragAndDrop.itemBeingDragged.GetComponent<Image>().sprite;

				//Calls a function that will apply an edit to an instrument (AAJ)
				//fooEdit();

				//Test print
				Debug.Log("Edit Applied");

				//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
			}//if

			if(DragAndDrop.itemBeingDragged.GetComponent<ApplyEdit>() != null){

				if(DragAndDrop.itemBeingDragged.GetComponent<GraphReceiver>() == null){

					if(isInstrument == false){

						//Calls a function that will apply an edit to an instrument (AAJ)
						EditMode();
					
						//Test print
						Debug.Log("Editing Mode");
					
						//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
					}
				}//if
			}//if
		}//if
	}//OnDrop
	#endregion

	/// <summary>
	/// This will allow the user to edit existing instruments (AAJ)
	/// </summary>
	void EditMode(){

		//Enables the tracing screen(AAJ)
		tracingScreen.SetActive(true);

		//Gets the draw object (AAJ)
		DrawObject = GameObject.FindGameObjectWithTag("Draw");

		//Holds of chain of transforms that will be used to get
		//the raw data in instrument script (AAJ)
		Transform graphSlot = DragAndDrop.itemBeingDragged.transform.parent;
		Transform graphPanel = graphSlot.transform.parent;
		Transform instrumentObject = graphPanel.transform.parent;

		//Updates the tracing screen with the editable line (AAJ)
		DrawObject.GetComponent<DrawLine>().UpdateLine(instrumentObject.GetComponent<InstrumentScript>().RawData);

		//Sets the draw object in tracing script (AAJ)
		tracingScript.SetDrawObject(DrawObject);

		//Copies the instruments sprite to the tracing screen (AAJ)
		tracingGraph.sprite = GetComponent<Image>().sprite;

		//Passes the name of the image to the tracing script(AAJ)
		//Instrument scirpt needs to updated to hold the image's name (AAJ)
		//tracingScript.SetFileName();
	}//EditMode
}//ApplyEdit