using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	public void SceneChangeRun(){
		SceneManager.LoadScene ("play");
	}

	public void SceneChangeSetting(){
		SceneManager.LoadScene ("Setting");
	}

	public void SceneChangeMenu(){
		SceneManager.LoadScene ("Menu");
	}

	public void SceneChangeSelect(){
		SceneManager.LoadScene ("SelectChar");
	}
}
