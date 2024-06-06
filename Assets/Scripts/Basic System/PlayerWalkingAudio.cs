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
        Vector3 CurrentPosition = transform.position; // ���� ��ġ ����

        // ���� ��ġ�� ���� ��ġ�� �ٸ��� Ȯ��
        if (CurrentPosition != PreviousPosition)
        {
            // ��ġ�� �������� ����� ���
            if (!WalkingAudio.isPlaying)
            {
                WalkingAudio.Play();
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

        // ���� ��ġ ����
        PreviousPosition = CurrentPosition;
    }
}
