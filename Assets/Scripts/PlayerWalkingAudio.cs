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

        if (CurrentPosition != PreviousPosition)
        {
            // ��ġ�� �������� ����� ���
            if (!WalkingAudio.isPlaying)
            {
                PlayNextClip();
            }
            else if (WalkingAudio.time >= WalkingAudio.clip.length)
            {
                // ���� Ŭ���� �����ٸ� ���� Ŭ�� ���
                PlayNextClip();
            }
        }
        else
        {
            // ��ġ�� ������ �ʾ����� ����� ����
            if (WalkingAudio.isPlaying)
            {
                WalkingAudio.Stop();
            }
        }

        PreviousPosition = CurrentPosition;
    }

    void PlayNextClip()
    {
        ClipIndex = (ClipIndex + 1) % WalkingAC.Length; // �ε��� ���� �� ��ȯ
        WalkingAudio.clip = WalkingAC[ClipIndex];
        WalkingAudio.Play();
    }

}
