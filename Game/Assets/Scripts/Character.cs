using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour {
	public Slider HpSlider;
	public GameObject barriertest;

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			gameObject.GetComponent<Animator> ().Play ("Jump");
			StartCoroutine (JumpBtn());
		}
    }

    void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "test") {
			HpSlider.value -= 0.08f;
            Destroy (col.gameObject);
		}
	}

	IEnumerator JumpBtn(){
		GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 330f);
		yield return new WaitForSeconds(1.0f);
	}

}
