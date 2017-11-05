using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public Slider HpSlider;
    public GameObject barriertest;
    public Text DieText;

    // Update is called once per frame
    void Update()
    {
        DieText.enabled = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Animator>().Play("Jump");
            StartCoroutine(JumpBtn());
        }
        if (HpSlider.value == 0)
        {
            GameOver();
            Time.timeScale = 0; // 죽었을 때 캐릭터 모습이 없어서 정지 기킨 것
        }
    }

    private void GameOver()
    {
        DieText.enabled = true;
        enabled = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
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
