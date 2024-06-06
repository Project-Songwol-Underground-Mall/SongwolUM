using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZombie : MonoBehaviour
{
    public AudioSource ZombieSound;
    private float Speed = 50f; // 이동 속도

    void Start()
    {
        // 5초 뒤 삭제
        ZombieSound.Play();
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }


    void PlaySound()
    {
        ZombieSound.Play();
    }

    void StopSound()
    {
        ZombieSound.Stop();
    }
}
