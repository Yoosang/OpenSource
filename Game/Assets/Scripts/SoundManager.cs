using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip coinSound;

    // 효과음 함수
    public void JumpSound()
    {
        AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);
    }

    public void CoinSound()
    {
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
    }
}
