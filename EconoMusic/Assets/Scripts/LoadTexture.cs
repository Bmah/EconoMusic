//Alex Jungroth
using UnityEngine;
using System.Collections;

public class LoadTexture : MonoBehaviour{

	public FileBrowser browser;
	public FileSelectMode mode;
	public string[] searchPatterns;
	bool selected;
	FileInfo sel;
	string path = "";

	//holds the file path so textures can be loaded in (AAJ)
	string filePath;

	//holds this objects renderer (AAJ)
	public Renderer rend;

	void Start(){
		rend = GetComponent<Renderer> ();

	}

	void OnMouseEnter(){

		if (Input.GetMouseButtonDown(0)){

			selected = false;
			if (!browser.isShowing) browser.Show(path, searchPatterns, this, mode);
		}
	}
	
	// the FileBrowser will send a message to this MonoBehaviour when the user selects a file
	// Set the 'SelectEventName' in the inspector to the name of the function you want to receive the message
	void OnFileSelected(FileInfo info){
		sel = info;

		//loads in the texture (AAJ)
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

		byte[] bytes = System.IO.File.ReadAllBytes(filePath);
		Texture2D tex = new Texture2D(1, 1);
		tex.LoadImage(bytes);
		rend.material.mainTexture = tex;
	}
}