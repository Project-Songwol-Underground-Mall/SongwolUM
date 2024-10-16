using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingAudio : MonoBehaviour
{
    public AudioSource WalkingAudio;
    public AudioClip[] WalkingAC = new AudioClip[4];
    private int ClipIndex = 0;
    private Vector3 PreviousPosition;

    // Start is called before the first frame update
    void Start()
    {
        PreviousPosition = transform.position;
        WalkingAudio.clip = WalkingAC[ClipIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CurrentPosition = transform.position;

        if (CurrentPosition.x != PreviousPosition.x || CurrentPosition.z != PreviousPosition.z)
        {
            if (!WalkingAudio.isPlaying)
            {
                PlayNextClip();
            }
            else if (WalkingAudio.time >= WalkingAudio.clip.length)
            {
                // 현재 클립이 끝났다면 다음 클립 재생
                PlayNextClip();
            }
        }
        else
        {
            if (WalkingAudio.isPlaying)
            {
                WalkingAudio.Stop();
            }
        }


        PreviousPosition = CurrentPosition;
    }

    void PlayNextClip()
    {
        ClipIndex = (ClipIndex + 1) % WalkingAC.Length; // 인덱스 증가 및 순환
        WalkingAudio.clip = WalkingAC[ClipIndex];
        WalkingAudio.Play();
    }

}
