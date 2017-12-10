using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour {
	public float delayTime = 3;
    public SoundManager sound;
	// Use this for initialization
	IEnumerator Start () {
        sound.StartSound();
     

		yield return new WaitForSeconds(delayTime);
    
		Application.LoadLevel ("Menu");
	}


}
