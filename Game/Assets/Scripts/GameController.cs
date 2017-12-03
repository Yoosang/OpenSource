using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // 싱글톤
    static GameController instance; 

    public static GameController Instance() { return instance; }
    public GameObject enemyMakePosition;
    public GameObject SpikePrefab, BarrierPrefab;
    public GameObject birdPrefab;
    public GameObject CoinPrefab,CoinPrefab1, CoinMakePosition, CoinMakePosition1, CoinMakePosition2, CoinMakePosition3, CoinMakePosition4;
    public GameObject FloorPrefab;
    public Text ScoreText;
    public Text coinText;
    public Slider HpSlider, GaugeSlider;
    int score = 0;
	int frame=0;
    public int coin = 0;
    public int coin1 = 0;
    bool flag = true;
	public static bool Gaugeflag = true;
	public static GameObject enemyBarrier, enemySpike;
    bool isPlay = true;
    public Text DieText;
	public Text descrip;
	int count=0;
    float timer;
    Vector3 pos;

    public bool IsPlaying()
    {
        return isPlay;
    }

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        DieText.enabled = false;
		descrip.enabled = false;

        if (IsPlaying() == true)
        {
            timer += 0.025f;
            pos = new Vector3(27, -4, 0);

            if (timer > 8f) //시간에 따라 땅 생성
            {
                Instantiate(FloorPrefab, pos, Quaternion.identity);
                timer = 0;
            }

            frame++;  // 프레임마다 1점씩 증가
			if (Gaugeflag == false && SelectCharacter.characterNumber == 2) { // 부스터 사용시에는 점수가 빨리 오름
				score += 3;
			} else {
				score++;
			}
			if (Gaugeflag == true && HpSlider.value != 0) {   //게이지 채우기
				GaugeSlider.value += 0.0015f;  //0.0015
				if (GaugeSlider.value == 1) {
					Gaugeflag = false;
				}
			}
			else if (Gaugeflag == false && HpSlider.value != 0) {   //게지가 꽉 찬 후 다시 줄어들기
				GaugeSlider.value -= 0.0025f;  //0.0025
				if (GaugeSlider.value == 0) {
					Gaugeflag = true;
				}
			}
			if (frame % 100 == 0)
            {  //100프레임마다 적 생성
                if(IsPlaying() == true)
                {
                    int enemyNum = Random.Range(1, 3);
                    Vector3 birdEnemyPos = new Vector3(10.45f, 0.5f, 0);
                    switch (enemyNum)
                    {   
                        case 1:
                            enemyBarrier = Instantiate(BarrierPrefab, enemyMakePosition.transform.position, Quaternion.identity);
							enemyBarrier.GetComponent<BoxCollider2D> ().isTrigger = false; 
							if (Gaugeflag == false && SelectCharacter.characterNumber == 2) { // 캐릭터2 필살기 장애물 속도 빠르게 
								enemyBarrier.GetComponent<MoveObject> ().speed = 0.3f;
							}
							else if (Gaugeflag == false && SelectCharacter.characterNumber == 3) { // 투명화 상태에서는 장애물을 파괴하지 않게 
								enemyBarrier.GetComponent<BoxCollider2D> ().isTrigger = true; 
							}
							enemyBarrier.gameObject.tag = "enemy";
                            break;
                        case 2:
                            enemySpike = Instantiate(SpikePrefab, enemyMakePosition.transform.position, Quaternion.identity);
                            Instantiate(birdPrefab, birdEnemyPos, Quaternion.identity);
                            enemySpike.GetComponent<BoxCollider2D> ().isTrigger = false; 
							if (Gaugeflag == false && SelectCharacter.characterNumber == 2) {  // 캐릭터2 필살기 장애물 속도 빠르게 
								enemySpike.GetComponent<MoveObject> ().speed = 0.3f;
							}
							else if (Gaugeflag == false && SelectCharacter.characterNumber == 3) { // 투명화 상태에서는 장애물을 파괴하지 않게 
								enemySpike.GetComponent<BoxCollider2D> ().isTrigger = true; 
							}
							enemySpike.gameObject.tag = "enemy";
                            break;
                    }
                }
                
            }
			if (frame % 130 == 0)
            {
                GameObject x = Instantiate(CoinPrefab, CoinMakePosition.transform.position, Quaternion.identity);

            }

			if (frame % 170 == 0)
            {
                GameObject x = Instantiate(CoinPrefab1, CoinMakePosition1.transform.position, Quaternion.identity);

            }
			if (frame % 150 == 0)
            {
                GameObject x = Instantiate(CoinPrefab, CoinMakePosition2.transform.position, Quaternion.identity);

            }
			if (frame % 120 == 0)
            {
                GameObject x = Instantiate(CoinPrefab, CoinMakePosition3.transform.position, Quaternion.identity);

            }
			if (frame % 160 == 0)
            {
                GameObject x = Instantiate(CoinPrefab, CoinMakePosition4.transform.position, Quaternion.identity);

            }
            if (Gaugeflag == false && SelectCharacter.characterNumber == 1 && count<100) {  // 필살기 사용 설명 나옴
				count++;
				descrip.enabled = true;
			}
        }
        if (IsPlaying() == false)
        {
            GameOver();
        }

        ScoreText.text = "score : " + (score) / 10;  // 점수 증가 & 표시   
		coinText.text = "coin : " + ((coin*10)+(coin1*50));
        

    }

    public void GameOver()
    {
        isPlay = false;
        DieText.enabled = true;
        enabled = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }
    }
}
