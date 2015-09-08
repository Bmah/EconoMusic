using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class FileGrabberScript : MonoBehaviour {

	bool done;
	int i = 0;
	string[] AllFilesInMyPictures;

	public LoadTexture loadTexture;

	//public Image ImageTemp;


	// Use this for initialization
	void Start () {
		AllFilesInMyPictures = Directory.GetFiles ((System.Environment.GetFolderPath (System.Environment.SpecialFolder.Desktop)) + "/Economusic");
		done = false;

//		Texture2D temp = Resources.Load("Put Images In Here/pic") as Texture2D;
//		Debug.Log (temp);
//
//		//Generates a sprite dynamically (AAJ)
//		Rect rect = new Rect(0, 0, temp.width, temp.height);
//		Vector2 pivot = new Vector2(0.5f, 0.5f);
//		ImageTemp.sprite = Sprite.Create(temp, rect, pivot);
	}

	// Update is called once per frame
	void Update () {
		if (Time.time % 0.2 < 0.1 && !done) {
			done = true;
			if(i < AllFilesInMyPictures.Length){
				if (AllFilesInMyPictures [i].Contains (".jpg") || AllFilesInMyPictures [i].Contains (".png")) {
				
					string filePath = "";
					string fileName = "";
					string[] SplitFilePath = AllFilesInMyPictures [i].Split ('\\');
					for (int j = 0; j < SplitFilePath.Length - 1; j++) {
						filePath += SplitFilePath [j] + "\\";
					}
					fileName = SplitFilePath [SplitFilePath.Length - 1];
					Debug.Log (filePath);
					Debug.Log (fileName);
				
					FileInfo newImageFile = new FileInfo (AllFilesInMyPictures [i], fileName, true);
					loadTexture.OnFileSelected (newImageFile);
				}
				i++;
			}
		}

		if (Time.time % 0.2 > 0.1 && done) {
			done = false;
		}
	}
}
