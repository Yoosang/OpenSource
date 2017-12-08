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
	public GameObject SpikePrefab;
	public GameObject BarrierPrefab;
    public GameObject birdPrefab;
	public GameObject CoinPrefab,bigCoinPrefab;
    public GameObject FloorPrefab;
    public Text ScoreText;
    public Text coinText;
    public Slider HpSlider, GaugeSlider;
	public GameObject GaugeEffect;
    int score = 0;
	int frame=0;
   	public int coin = 0;
    public int bigCoin = 0;
    bool flag = true;
	public static bool Gaugeflag = true;
	public static GameObject enemyBarrier, enemySpike;
    bool isPlay = true;
    public Text DieText;
	public Text descrip;
	int count=0;
    float timer;
    Vector3 pos;
	int effectCount=0;  
	int destroyCount =0;  
	GameObject effect1, effect2;
	public Vector3 coinPos;
	Vector3 aboveBarrierPos;
	public static GameObject Coin;
    public bool IsPlaying()
    {
        return isPlay;
    }

    private void Start()
    {
        instance = this;
		aboveBarrierPos = enemyMakePosition.transform.position;;
		aboveBarrierPos.y += 2.3f;
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
			if (frame % 17 == 0) {
				Coin = Instantiate (CoinPrefab, coinPos, Quaternion.identity);
			}
			if (Gaugeflag == false && SelectCharacter.characterNumber == 2) { // 부스터 사용시에는 점수가 빨리 오름
				score += 3;
			} else {
                score += 1;
			}
			if (Gaugeflag == true && HpSlider.value != 0) {   //게이지 채우기
				GaugeSlider.value += 0.0015f;  //0.0015
				if (GaugeSlider.value == 1) {
					Gaugeflag = false;
				}
				effectCount = 0;
				if (destroyCount == 0) { // gauge effect 삭제
					Destroy (effect1);
					Destroy (effect2);
				}
				destroyCount++;
			}
			else if (Gaugeflag == false && HpSlider.value != 0) {   //게지가 꽉 찬 후 다시 줄어들기
				GaugeSlider.value -= 0.0025f;  //0.0025
				if (effectCount == 0) {  // gauge effect 생성
					Vector3 gaugePos1 = new Vector3 (-2.1f, 4.4f,-1);
					Vector3 gaugePos2 = new Vector3 (2.1f, 4.4f, -1);
					effect1 = Instantiate (GaugeEffect, gaugePos1, Quaternion.identity);
					effect2 = Instantiate (GaugeEffect, gaugePos2, Quaternion.identity);
				}
				effectCount++;
				destroyCount = 0;
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
							Instantiate (bigCoinPrefab, aboveBarrierPos, Quaternion.identity);
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
							Instantiate (bigCoinPrefab, aboveBarrierPos, Quaternion.identity);
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
		coinText.text = "coin : " + ((coin*10)+(bigCoin*50));
        


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
