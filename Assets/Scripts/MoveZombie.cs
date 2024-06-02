using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZombie : MonoBehaviour
{
    public float Speed = 10f; // 이동 속도

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(0, 0, -Speed * Time.deltaTime);
    }
}
