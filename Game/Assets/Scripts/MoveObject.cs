using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
	GameObject effect;
	public GameObject destroyEffect;
    public float speed ;
    void Start()
    {
        

    }
    void Update()
    {
		
        if (GameController.Instance().IsPlaying() == false)
            return;

        transform.Translate(Vector2.left * speed);
        if (transform.position.x < -50f || transform.position.y < -50f)// 장애물을 넘어서 화면 밖으로 가면 삭제
        {
            Destroy(gameObject);
        }

    }


	public void DestroyEffect(){
		if (destroyEffect != null) {
			Instantiate(destroyEffect, transform.position, Quaternion.identity);
		}
	}
	/*
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag.Equals("Coin"))   // 장애물이랑 코인이랑 겹치지 않게
		{
			Destroy(col.gameObject);
		}

	}
*/
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "character")
        {
            DestroyEffect();   
        }
		if (destroyEffect != null && col.transform.tag.Equals("Coin"))   // 장애물이랑 코인이랑 겹치지 않게
		{
			Destroy(col.gameObject);
		}
    }

}
