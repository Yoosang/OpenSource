﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {


	public float scrollSpeed = 0.03f; 
	//스크롤할 속도를 상수로 지정해 줍니다.
	private Material thisMaterial;
	//Quad의 Material 데이터를 받아올 객체를 선언합니다
	// Use this for initialization
	void Start () {
		//객체가 생성될때 최초 1회만 호출 되는 함수 입니다.
		thisMaterial = GetComponent<Renderer>().material; 
		//현재 객체의 Component들을 참조해 Renderer라는 컴포넌트의 Material정보를 받아옵니다.

	}

	// Update is called once per frame
	void Update () {
		if (GameController.Instance().Gaugeflag == true)
			scrollSpeed = 0.03f; 
		else if (GameController.Instance().Gaugeflag == false && SelectCharacter.characterNumber == 2) {  // 부스터 상태일 때 배경 속도 빠르게 
			scrollSpeed = 0.3f;
		}
		Vector2 newOffset = thisMaterial.mainTextureOffset;
		// 새롭게 지정해줄 OffSet 객체를 선언합니다.
		newOffset.Set(newOffset.x + (scrollSpeed * Time.deltaTime),0 );
		// x부분에 현재 x값에 속도에 프레임 보정을 해서 더해줍니다.
		thisMaterial.mainTextureOffset = newOffset;
		//그리고 최종적으로 Offset값을 지정해줍니다.

	}
}