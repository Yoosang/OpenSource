using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
	GameObject effect;
	public GameObject destroyEffect;
    public float speed ;
	int count=0;
    void Start()
    {
        

    }
    void Update()
    {

		count++;
        if (GameController.Instance().IsPlaying() == false)
            return;

        transform.Translate(Vector2.left * speed);
        if (transform.position.x < -100f)// 장애물을 넘어서 화면 밖으로 가면 삭제
        {
            Destroy(gameObject);
        }
		if (count == 10)  //effect삭제
			Destroy (effect); 
    }


	public void DestroyBarrier(){
		if (destroyEffect != null) {
			effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
		}
		count = 0;  
	}

}
