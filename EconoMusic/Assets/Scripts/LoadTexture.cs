//Alex Jungroth
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadTexture : MonoBehaviour{

	public FileBrowser browser;
	public FileSelectMode mode;
	public string[] searchPatterns;
	bool selected;
	FileInfo sel;
	string path = "";

	//Holds the file path so textures can be loaded in (AAJ)
	string filePath;

	//Holds this objects sprite (AAJ)
	public Sprite loadedSprite;

	//Prevents more than one texture from being loaded on the same object (AAJ)
	public bool loaded = false;

	//Holds the prefab for a new slot (AAJ)
	public GameObject slot;

	//Holds a the panel where the slots are stored (AAJ)
	public GameObject slotPanel;

	// Use this for initialization
	void Start ()
	{
		//resets the loaded variable for new instantiations of slot (AAJ)
		loaded = false;

	}//Start

	public void OnMouseDown(){

		if(loaded == false){

			selected = false;
			if(!browser.isShowing){

				browser.Show (path, searchPatterns, this, mode);
			}
		}
	}
	
	// The FileBrowser will send a message to this MonoBehaviour when the user selects a file
	// Set the 'SelectEventName' in the inspector to the name of the function you want to receive the message
	void OnFileSelected(FileInfo info){
			
			sel = info;
			
			//Loads in the texture (AAJ)
			load(sel.path);
	}
	
	void OnFileChange(FileInfo file){

		Debug.Log("File section changed to: " + file.name);
	}
	
	void OnBrowseCancel(){

		Debug.Log("You have cancelled");
	}

	/// <summary>
	/// Load the specified texture. (AAJ)
	/// </summary>
	/// <param name="filePath">File path.</param>
	void load(string filePath){

		//Prevents multiple textures being loaded onto the same object
		//but only once something has been loaded (AAJ)
		loaded = true;

		//Gets the sprite from the file path (AAJ)
		byte[] bytes = System.IO.File.ReadAllBytes(filePath);
		Texture2D tex = new Texture2D(1, 1);
		tex.LoadImage(bytes);

		//Generates a sprite dynamically (AAJ)
		Rect rect = new Rect(0, 0, tex.width, tex.height);
		Vector2 pivot = new Vector2(0.5f, 0.5f);
		loadedSprite = Sprite.Create(tex, rect, pivot);

		//Loads the new sprite into the object's sprite component (AAJ)
		GetComponent<Image>().sprite = loadedSprite;
		
		//instantiates a new slot and texture that can load another image (AAJ)
		GameObject newSlot = Instantiate(slot, new Vector3(0,0,0), Quaternion.identity) as GameObject;

		//sets the new slots parent to the slot panel (AAJ)
		newSlot.transform.SetParent(slotPanel.transform, false);

	}
}