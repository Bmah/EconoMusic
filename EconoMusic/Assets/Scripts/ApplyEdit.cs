//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApplyEdit : MonoBehaviour, IDropHandler{

	//Holds wether or not this is an instrument (AAJ)
	public bool isInstrument = false;

	// Use this for initialization
	void Start ()
	{
		//If you are not the graph receiver, then you
		//are an instrument (AAJ)
		if(GetComponent<GraphReceiver>() == null){

			//Sets this instance of ApplyEdit as an instrument (AAJ)
			isInstrument = true;
		}//if
	}//Start

	//returns the first child (AAJ)
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
						//fooEditMode();
					
						//Test print
						Debug.Log("Editing Mode");
					
						//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
					}
				}//if
			}//if
		}//if
	}//OnDrop
	#endregion
}