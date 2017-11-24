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
    public GameObject CoinPrefab, CoinMakePosition;
    public Text ScoreText;
    public Text coinText;
    public Slider HpSlider, GaugeSlider;
    int score = 0;
    public int coin = 0;
    bool flag = true;
	public static bool Gaugeflag = true;
	public static GameObject enemyBarrier, enemySpike;
    bool isPlay = true;
    public Text DieText;
	public Text descrip;
	int count=0;

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
            score++;  // 프레임마다 1점씩 증가
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
            if (score % 100 == 0)
            {  //100프레임마다 적 생성
                if(IsPlaying() == true)
                {
                    int enemyNum = Random.Range(1, 3);
                    switch (enemyNum)
                    {
                        case 1:
                            enemyBarrier = Instantiate(BarrierPrefab, enemyMakePosition.transform.position, Quaternion.identity);
							if (GameController.Gaugeflag == false && SelectCharacter.characterNumber == 2) { // 캐릭터2 필살기 장애물 속도 빠르게 
								enemyBarrier.GetComponent<MoveObject> ().speed = 0.3f;
							}
							enemyBarrier.gameObject.tag = "enemy";
                            break;
                        case 2:
                            enemySpike = Instantiate(SpikePrefab, enemyMakePosition.transform.position, Quaternion.identity);
							if (GameController.Gaugeflag == false && SelectCharacter.characterNumber == 2) {  // 캐릭터2 필살기 장애물 속도 빠르게 
								enemySpike.GetComponent<MoveObject> ().speed = 0.3f;
							}
							enemySpike.gameObject.tag = "enemy";
                            break;
                    }
                }
                
            }
            if (score % 170 == 0)
            {
                GameObject x = Instantiate(CoinPrefab, CoinMakePosition.transform.position, Quaternion.identity);

            }
            coin++;
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
        coinText.text = coin.ToString();
       
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
