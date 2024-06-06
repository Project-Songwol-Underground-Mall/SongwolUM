using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingAudio : MonoBehaviour
{
    public AudioSource WalkingAudio;

    private Vector3 PreviousPosition;

    // Start is called before the first frame update
    void Start()
    {
        PreviousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CurrentPosition = transform.position; // 현재 위치 저장

        // 현재 위치와 이전 위치가 다른지 확인
        if (CurrentPosition != PreviousPosition)
        {
            // 위치가 변했으면 오디오 재생
            if (!WalkingAudio.isPlaying)
            {
                WalkingAudio.Play();
            }
        }
        else
        {
            // 위치가 변하지 않았으면 오디오 멈춤
            if (WalkingAudio.isPlaying)
            {
                WalkingAudio.Stop();
            }
        }

        // 이전 위치 갱신
        PreviousPosition = CurrentPosition;
    }
}
