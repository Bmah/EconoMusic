//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphReceiver : MonoBehaviour, IDropHandler{

	//Holds the tracing panel so it can be enabled (AAJ)
	public GameObject tracingScreen;

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

			GetComponent<Image>().sprite = DragAndDrop.itemBeingDragged.GetComponent<Image>().sprite;
			tracingScreen.SetActive(true);
			//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
		}
	}
	#endregion
}