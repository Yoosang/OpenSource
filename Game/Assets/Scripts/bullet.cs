using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > 10.33f)// 화면 밖으로 가면 삭제
        {
            Destroy(gameObject);
        }
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "enemy")
		{
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}

}
