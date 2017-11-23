using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public Slider HpSlider;
    public GameController GC;
	public GameObject charac1, charac2,charac3;
	Renderer rend;
	public GameObject bulletPrefab,bulletPosition;
	GameObject bull;
	Quaternion rot;

	void Start(){ //캐릭터 선택시 한 명씩 나오게
		rot = transform.localRotation;
		rot.eulerAngles = new Vector3 (0, 0, -90);
		if (SelectCharacter.characterNumber == 1) {
			charac1.transform.Translate (Vector2.right * 2.3f);
			Destroy (charac2);
			Destroy (charac3);
		}
		else if (SelectCharacter.characterNumber == 2) {
			charac2.transform.Translate (Vector2.right * 2.3f);
			Destroy (charac1);
			Destroy (charac3);
		}
		else if (SelectCharacter.characterNumber == 3) {
			charac3.transform.Translate (Vector2.right * 5.0f);
			Destroy (charac1);
			Destroy (charac2);
		}
		rend = charac3.GetComponent<Renderer> ();

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

		if (GameController.Gaugeflag == false) {  // 캐릭터 선택에 따른 필살기 

			if (SelectCharacter.characterNumber == 1) {  // 슈팅
				Shooting();
			}
			else if (SelectCharacter.characterNumber == 3) {
				rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.4f);  // 투명화

			}

		} 
		else if (GameController.Gaugeflag == true) {
			rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, 1f); // 원래 색으로 
		}

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals("Coin"))
        {
            
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.tag == "test")
		{

			if(GameController.Gaugeflag == true)
				HpSlider.value -= 0.01f;
			else if (GameController.Gaugeflag == false && SelectCharacter.characterNumber==1) { // 1번 캐릭터 제외, 필살기를 사용중에는 hp가 닳지 않음
				HpSlider.value -= 0.01f;
			} 
			Destroy (col.gameObject);

		}
    }

    IEnumerator JumpBtn()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 330f);
        yield return new WaitForSeconds(1.0f);
    }

	void Shooting(){ // 슈팅 함수
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			bull = Instantiate(bulletPrefab, bulletPosition.transform.position, Quaternion.identity);
			bull.transform.localRotation = rot;  // 총알그림을 90도 회전 시킴
		} 
		bull.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100f);  // 총알을 오른쪽으로 나게 함
	}
}
