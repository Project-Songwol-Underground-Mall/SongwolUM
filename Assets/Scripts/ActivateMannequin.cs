using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMannequin : MonoBehaviour
{
    public GameObject Player;
    public float ActivationDistance = 10f; // �ִϸ��̼� �۵� ����
    bool isAnimationActivated = false;
    private Animator MannequinAnimator;

    void Start()
    {

    }

    private void OnEnable()
    {
        isAnimationActivated = false;
        MannequinAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if (Distance <= ActivationDistance && isAnimationActivated)
        {
            if (MannequinAnimator != null)
            {
                // ����ŷ �ִϸ��̼� ���
                isAnimationActivated = true;
                MannequinAnimator.SetTrigger("scare_mnq");

                // 5�� �Ŀ� ���� ����
                StartCoroutine(ChangeStateAfterDelay(5f));
            }
        }
        else
        {
            
        }
    }

    IEnumerator ChangeStateAfterDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        // Animator�� ���¸� ����
        MannequinAnimator.SetTrigger("idle_mnq");
        isAnimationActivated = false;
    }

}
