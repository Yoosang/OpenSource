using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject enemyMakePosition;
    public GameObject SpikePrefab, BarrierPrefab;
    public Text ScoreText;
    public Slider HpSlider;
    int score = 0;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HpSlider.value != 0)
        {
            score++;  // 프레임마다 1점씩 증가
            if (score % 100 == 0)
            {  //100프레임마다 적 생성
                int enemyNum = Random.Range(1, 3);
                switch (enemyNum)
                {
                    case 1:
                        GameObject enemybarrier = Instantiate(BarrierPrefab, enemyMakePosition.transform.position, Quaternion.identity);
                        enemybarrier.gameObject.tag = "test";
                        break;
                    case 2:
                        GameObject enemySpike = Instantiate(SpikePrefab, enemyMakePosition.transform.position, Quaternion.identity);
                        enemySpike.gameObject.tag = "test";
                        break;
                }
            }
        }
        if (HpSlider.value == 0)
        {
            Time.timeScale = 0;
        }

        ScoreText.text = "score : " + (score) / 10;  // 점수 증가 & 표시   
    }
}
