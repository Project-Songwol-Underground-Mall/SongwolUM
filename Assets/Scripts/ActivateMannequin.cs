using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMannequin : MonoBehaviour
{
    public GameObject Player;
    public float ActivationDistance = 50f; // �ִϸ��̼� �۵� ����
    public GameObject[] MannequinEyes = new GameObject[2];
    public Material[] MannequinEyesMaterial = new Material[2];
    public AudioSource MannequinSound;
    bool isAnimationActivated = false;
    private Animator MannequinAnimator;
    private Renderer LeftMERenderer;
    private Renderer RightMERenderer;

    void Start()
    {

    }

    private void OnEnable()
    {
        isAnimationActivated = false;
        MannequinAnimator = GetComponent<Animator>();
        LeftMERenderer = MannequinEyes[0].GetComponent<Renderer>();
        RightMERenderer = MannequinEyes[1].GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        isAnimationActivated = false;
        MannequinAnimator = GetComponent<Animator>();
        LeftMERenderer = MannequinEyes[0].GetComponent<Renderer>();
        RightMERenderer = MannequinEyes[1].GetComponent<Renderer>();
        LeftMERenderer.material = MannequinEyesMaterial[0];
        RightMERenderer.material = MannequinEyesMaterial[0];
    }

    void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if (Distance <= ActivationDistance && !isAnimationActivated)
        {
            if (MannequinAnimator != null)
            {
                // ����ŷ �ִϸ��̼� ��� �� �� ���� ����
                isAnimationActivated = true;
                MannequinAnimator.Play("scare_mnq");
                LeftMERenderer.material = MannequinEyesMaterial[1];
                RightMERenderer.material = MannequinEyesMaterial[1];
                Invoke("PlaySound", 0.5f);

                StartCoroutine(ChangeStateAfterDelay(3f));
            }
            else
            {
                Debug.Log("MannequinAnimator�� ã�� �� ����");
            }
        }
        else
        {
            
        }
    }

    void PlaySound()
    { 
        MannequinSound.Play();
    }

    void StopSound()
    {
        MannequinSound.Stop();
    }


    IEnumerator ChangeStateAfterDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        // Animator�� ���¸� ����
        StopSound();

        // ����ŷ �� ������ ������� ����
        // LeftMERenderer.material = MannequinEyesMaterial[0];
        // RightMERenderer.material = MannequinEyesMaterial[0];
    }

}
