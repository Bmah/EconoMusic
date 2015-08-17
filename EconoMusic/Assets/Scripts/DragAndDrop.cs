//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler{

	//Holds the LoadTexture script so objects without a loaded image cannot be dragged (AAJ)
	public LoadTexture loadTexture;

	//Holds the GameObject being dragged (AJJ)
	public static GameObject itemBeingDragged;

	//Holds the original position of what it is being dragged (AAJ)
	Vector3 startPosition;

	//Holds what the parent of the object is (AAJ)
	Transform startParent;

	//Prepares the drag process (AAJ)
	#region IBeginDragHandler implementation
	public void OnBeginDrag(PointerEventData eventData){

		if(loadTexture.loaded == true){

			itemBeingDragged = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}//OnBeginDrag
	#endregion

	//The actual dragging process(AAJ)
	#region IDragHandler implementation
	public void OnDrag(PointerEventData eventData){

		if(loadTexture.loaded == true){

			transform.position = Input.mousePosition;
		}
	}//OnDrag
	#endregion

	//Ends the drag process (AAJ)
	#region IEndDragHandler implementation
	public void OnEndDrag(PointerEventData eventData){

		if(loadTexture.loaded == true){

			itemBeingDragged = null;
			GetComponent<CanvasGroup>().blocksRaycasts = true;

			if(transform.parent == startParent){

			transform.position = startPosition;
			}//if
		}
	}//OnEndDrag
	#endregion
}//DragAndDrop