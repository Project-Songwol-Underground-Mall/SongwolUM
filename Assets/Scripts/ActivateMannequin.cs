using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMannequin : MonoBehaviour
{
    public GameObject Player;
    public float ActivationDistance = 50f; // 애니메이션 작동 범위
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

        if (Distance <= ActivationDistance && !isAnimationActivated)
        {
            if (MannequinAnimator != null)
            {
                // 마네킹 애니메이션 재생
                isAnimationActivated = true;
                MannequinAnimator.Play("scare_mnq");

                // 3초 후에 상태 변경
                StartCoroutine(ChangeStateAfterDelay(3f));
            }
            else
            {
                Debug.Log("MannequinAnimator를 찾을 수 없음");
            }
        }
        else
        {
            
        }
    }

    IEnumerator ChangeStateAfterDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        // Animator의 상태를 변경
        MannequinAnimator.Play("idle_mnq");
        isAnimationActivated = false;
    }

}
