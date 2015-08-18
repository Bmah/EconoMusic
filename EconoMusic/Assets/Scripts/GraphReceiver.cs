//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphReceiver : MonoBehaviour, IDropHandler{

	//Holds teh Draw Line script so I can set when the line should be drawn (AAJ)
	public DrawLine drawLine;

	//Holds the tracing panel so it can be enabled (AAJ)
	public GameObject tracingScreen;

	//Holds the tracing graph so it can be given the graph (AAJ)
	public Image tracingGraph;

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

			//Sets the tracing graphs sprite and enables the tracing screen (AAJ) 
			tracingGraph.sprite = DragAndDrop.itemBeingDragged.GetComponent<Image>().sprite;
			tracingScreen.SetActive(true);

			//enables tracing (AAJ)
			drawLine.GetComponent<DrawLine>().drawing = true;

			//DragAndDrop.itemBeingDragged.transform.SetParent(transform);
		}
	}
	#endregion
}