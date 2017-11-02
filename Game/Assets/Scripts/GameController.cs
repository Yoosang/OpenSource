using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject BarrierPrefab, BarrierMakePosition;
    public Text ScoreText;

    int score = 0;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score++;  // 프레임마다 1점씩 증가
        if (score % 100 == 0)
        {  //100프레임마다 적 생성
            GameObject x = Instantiate(BarrierPrefab, BarrierMakePosition.transform.position, Quaternion.identity);
            x.gameObject.tag = "test";
        }

        ScoreText.text = "score : " + (score) / 10;  // 점수 증가 & 표시   
    }
}
