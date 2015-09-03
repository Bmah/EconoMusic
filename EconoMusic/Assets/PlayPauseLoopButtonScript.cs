using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayPauseLoopButtonScript : MonoBehaviour {

	public Image play, pause, loop;
	private Color invisible = new Color(0f,0f,0f,0f);
	private Color visible = new Color(1f,1f,1f,1f);

	void Start(){
		play.color = invisible;
		pause.color = visible;
		loop.color = invisible;
	}

	public void PlayButton(){
		play.color = visible;
		pause.color = invisible;
	}

	public void PauseButton(){
		pause.color = visible;
		play.color = invisible;
	}

	public void LoopButton(){
		if (loop.color == invisible) {
			loop.color = visible;
		}
		else {
			loop.color = invisible;
		}
	}
}
