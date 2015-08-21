//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ApplyEdit : MonoBehaviour, IDropHandler{
	
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
		
		if (!item) {

			if(DragAndDrop.itemBeingDragged.GetComponent<GraphReceiver>() != null){

				//Calls a function that will apply an edit to an instrument (AAJ)
				//fooEdit();

				//Test print
				Debug.Log("Edit Applied");

				//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
			}//if
		}//if
	}//OnDrop
	#endregion
}