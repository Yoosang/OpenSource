using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public Slider HpSlider;
    public GameController GC;
	public GameObject charac1, charac2,charac3;

	void Start(){ //캐릭터 선택시 한 명씩 나오게
		if (SelectCharacter.characterNumber == 1) {
			charac1.transform.Translate (Vector2.right * 2.0f);
		}
		else if (SelectCharacter.characterNumber == 2) {
			charac2.transform.Translate (Vector2.right * 2.3f);
		}
		else if (SelectCharacter.characterNumber == 3) {
			charac3.transform.Translate (Vector2.right * 2.6f);
		}
	}
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Animator>().Play("jump");
            StartCoroutine(JumpBtn());
        }
        if (HpSlider.value == 0)
        {
            GameController.Instance().GameOver();
            Time.timeScale = 0; // 죽었을 때 캐릭터 모습이 없어서 정지 기킨 것
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals("Coin"))
        {
            GC.coin++;
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "test")
        {
            HpSlider.value -= 0.5f;
            Destroy(col.gameObject);
        }
    }

    IEnumerator JumpBtn()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 330f);
        yield return new WaitForSeconds(1.0f);
    }

}
