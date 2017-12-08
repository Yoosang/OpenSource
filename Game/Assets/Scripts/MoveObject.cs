using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
	GameObject effect;
	public GameObject destroyEffect;
    public float speed ;
	int destroyCount;
    void Start()
    {
        

    }
    void Update()
    {
		destroyCount++;
        if (GameController.Instance().IsPlaying() == false)
            return;

        transform.Translate(Vector2.left * speed);
        if (transform.position.x < -100f || transform.position.y < -50f)// 장애물을 넘어서 화면 밖으로 가면 삭제
        {
            Destroy(gameObject);
        }
		//if (destroyCount == 10) {
		//	Debug.Log ("asldjfa;lskjdf");
		//	Destroy (effect);
		//}
    }


	public void DestroyEffect(){
		if (destroyEffect != null) {
			effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
			destroyCount = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag.Equals("Coin"))   // 장애물이랑 코인이랑 겹치지 않게
		{
			Destroy(col.gameObject);
		}

	}
}
