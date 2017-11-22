using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour {
	public static int characterNumber =1;

	public void SelectCharac(int characterNumber){
		SelectCharacter.characterNumber = characterNumber;
		SceneManager.LoadScene ("play");
	}
}
