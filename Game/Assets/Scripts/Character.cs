using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public Slider HpSlider;
   
	public GameObject charac1, charac2,charac3;
	Renderer rend;
	public GameObject bulletPrefab,bulletPosition;
	GameObject bull;
	Quaternion rot;
    bool isGround = false; //캐릭터가 점프 중인지를 확인하기 위한 변수
    int jumpCount = 2; // 2단 점프를 위한 점프수 제한
    public SoundManager soundManager; //효과음 변수

    void Start(){
		
		rot = transform.localRotation;
		rot.eulerAngles = new Vector3 (0, 0, -90);
		rend = charac3.GetComponent<Renderer> ();
        jumpCount = 0; //시작 할때 점프수를 0으로 초기화

		selectCharac ();
    }
    // Update is called once per frame
    void Update()
    {

        Time.timeScale = 1.3f;

        if (isGround)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * 270f);
					GetComponent<Animator> ().Play ("jump");
                    jumpCount--; //점프 시 점프수를 하나씩 감소
                    soundManager.JumpSound(); //점프 효과음
                }
            }
        }
        if (HpSlider.value == 0|| transform.position.y < -5)
        {
            GameController.Instance().GameOver();
            Time.timeScale = 0; // 죽었을 때 캐릭터 모습이 없어서 정지 기킨 것
        }

		if (GameController.Instance ().CanShooting()) {
			Shooting ();
		}
		else if(GameController.Instance().CanTransparency()){
			rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.4f);  // 투명화
		}
		else if (SelectCharacter.characterNumber == 3 && GameController.Instance().Gaugeflag == true) {
			rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, 1f); // 원래 색으로 
		}

    }

	   void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals("bigCoin"))
        {
           
            GameController.Instance().bigCoin++;
            Destroy(col.gameObject);
            soundManager.CoinSound(); //동전효과음
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.tag == "enemy")
		{
			if(GameController.Instance().Gaugeflag == true)
				HpSlider.value -= 0.1f;
			else if (GameController.Instance().CanShooting()) { // 1번 캐릭터 제외, 필살기를 사용중에는 hp가 닳지 않음
				HpSlider.value -= 0.1f;
			}
			Destroy (col.gameObject);

		}
        if (col.gameObject.tag == "ground") // 캐릭터가 땅에 있는지를 확인하기 위해서 floor에 ground 라는 tag를 만들어서 넣어줬다
        {
            isGround = true;
            jumpCount = 2; // 땅에 있으면 점프수 2로 초기화
        }

		if (col.transform.tag.Equals("Coin"))
		{
			GameController.Instance().coin++;
			Destroy(col.gameObject);
			soundManager.CoinSound(); //동전효과음
		}

    }

    

	void Shooting(){ // 슈팅 함수
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Vector3 bulletPos = transform.position;
			bulletPos.x += 1;
			bull = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
			bull.transform.localRotation = rot;  // 총알그림을 90도 회전 시킴
		} 
		bull.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100f);  // 총알을 오른쪽으로 나게 함
	}


	void selectCharac(){
		if (SelectCharacter.characterNumber == 1) {
			Destroy (charac2);
			Destroy (charac3);
		}
		else if (SelectCharacter.characterNumber == 2) {
			Destroy (charac1);
			Destroy (charac3);
		}
		else if (SelectCharacter.characterNumber == 3) {
			Destroy (charac1);
			Destroy (charac2);
		}
	}

}
