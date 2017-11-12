using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject enemyMakePosition;
    public GameObject SpikePrefab, BarrierPrefab;
    public Text ScoreText;
    public Slider HpSlider, GaugeSlider;
    int score = 0;
	bool flag = true;
    public GameObject enemyBarrier, enemySpike;

    void Update()
    {
        if (HpSlider.value != 0)
        {
            score++;  // 프레임마다 1점씩 증가
            if (score % 100 == 0)
            {  //100프레임마다 적 생성
                int enemyNum = Random.Range(1, 3);
                switch (enemyNum)
                {
                    case 1:
                        enemyBarrier = Instantiate(BarrierPrefab, enemyMakePosition.transform.position, Quaternion.identity);
                        enemyBarrier.gameObject.tag = "test"; 
                        break;
                    case 2:
                        enemySpike = Instantiate(SpikePrefab, enemyMakePosition.transform.position, Quaternion.identity);
                        enemySpike.gameObject.tag = "test"; 
                        break;
                }
            }
            
        }
        //  spike만 멈춤, spike를 밑에 쓰면 barrier만 멈춤
        
        if (HpSlider.value == 0){
            enemyBarrier.GetComponent<MoveObject>().speed = 0f;
            enemySpike.GetComponent<MoveObject>().speed = 0f;
        }

        ScoreText.text = "score : " + (score) / 10;  // 점수 증가 & 표시   

		if (flag == true && HpSlider.value != 0) {   //게이지 채우기
			GaugeSlider.value += 0.0015f;
			if (GaugeSlider.value == 1) {
				flag = false;
			}
		}

		else if (flag == false && HpSlider.value != 0) {   //게지가 꽉 찬 후 다시 줄어들기
			GaugeSlider.value -= 0.0025f;
			if (GaugeSlider.value == 0) {
				flag = true;
			}
		}
    }
}
