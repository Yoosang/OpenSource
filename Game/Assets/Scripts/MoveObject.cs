using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
	public float speed = 0.1f;
    void Update()
    {
        transform.Translate(Vector2.left * speed);
    }

}
