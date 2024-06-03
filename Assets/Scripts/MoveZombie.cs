using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZombie : MonoBehaviour
{
    private float Speed = 50f; // 이동 속도

    void Start()
    {
        // 5초 뒤 삭제
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }
}
