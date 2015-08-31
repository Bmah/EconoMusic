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

	//Holds whether or not to bybass the loaded check for images
	//that are not supposed to load in images (AAJ)
	private bool bypassLoaded = false;

	// Use this for initialization
	void Start(){

		//If this image is not intended to load in images
		//DragAndDrop will still work without LoadTexture(AAJ)
		if(loadTexture == null){
			
			bypassLoaded = true;
		}//if
	}//Start

	//Prepares the drag process (AAJ)
	#region IBeginDragHandler implementation
	public void OnBeginDrag(PointerEventData eventData){

		//This allows LoadTexture to not be a necessary part of DragAndDrop (AAJ)
		if(loadTexture != null){

			if(loadTexture.loaded == true){

				bypassLoaded = true;
			}//if
		}//if

		if(bypassLoaded == true){

			itemBeingDragged = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}//if
	}//OnBeginDrag
	#endregion

	//The actual dragging process(AAJ)
	#region IDragHandler implementation
	public void OnDrag(PointerEventData eventData){

		//This allows LoadTexture to not be a necessary part of DragAndDrop (AAJ)
		if(loadTexture != null){
			
			if(loadTexture.loaded == true){
				
				bypassLoaded = true;
			}//if
		}//if

		if(bypassLoaded == true){

			transform.position = Input.mousePosition;
		}
	}//OnDrag
	#endregion

	//Ends the drag process (AAJ)
	#region IEndDragHandler implementation
	public void OnEndDrag(PointerEventData eventData){

		//This allows LoadTexture to not be a necessary part of DragAndDrop (AAJ)
		if(loadTexture != null){
			
			if(loadTexture.loaded == true){
				
				bypassLoaded = true;
			}//if
		}//if

		if(bypassLoaded == true){

			itemBeingDragged = null;
			GetComponent<CanvasGroup>().blocksRaycasts = true;

			if(transform.parent == startParent){

			transform.position = startPosition;
			}//if
		}
	}//OnEndDrag
	#endregion
}//DragAndDrop