//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GraphReceiver : MonoBehaviour, IDropHandler{

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

			DragAndDrop.itemBeingDragged.transform.SetParent(transform);
		}
	}
	#endregion
}