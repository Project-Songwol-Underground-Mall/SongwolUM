using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMannequin : MonoBehaviour
{
    public GameObject Player;
    public float ActivationDistance = 40f; // 애니메이션 작동 범위
    public GameObject[] MannequinEyes = new GameObject[2];
    public Material[] MannequinEyesMaterial = new Material[2];
    public AudioSource MannequinSound;

    private bool isAnimationActivated = false;
    private float MoveDistance = 13f; // 이상현상 발생시 x축 대쉬 거리
    private Vector3 OriginalPosition;
    private Animator MannequinAnimator;
    private Renderer LeftMERenderer;
    private Renderer RightMERenderer;

    void Start()
    {
        OriginalPosition = transform.position;
    }

    private void OnEnable()
    {
        isAnimationActivated = false;
        MannequinAnimator = GetComponent<Animator>();
        LeftMERenderer = MannequinEyes[0].GetComponent<Renderer>();
        RightMERenderer = MannequinEyes[1].GetComponent<Renderer>();
        OriginalPosition = transform.position;
    }

    private void OnDisable()
    {
        isAnimationActivated = false;
        MannequinAnimator = GetComponent<Animator>();
        LeftMERenderer = MannequinEyes[0].GetComponent<Renderer>();
        RightMERenderer = MannequinEyes[1].GetComponent<Renderer>();
        LeftMERenderer.material = MannequinEyesMaterial[0];
        RightMERenderer.material = MannequinEyesMaterial[0];
        transform.position = OriginalPosition;
    }

    void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if (Distance <= ActivationDistance && !isAnimationActivated)
        {
            if (MannequinAnimator != null)
            {
                // 마네킹 애니메이션 재생 및 눈 색깔 변경
                isAnimationActivated = true;
                MannequinAnimator.Play("scare_mnq");
                LeftMERenderer.material = MannequinEyesMaterial[1];
                RightMERenderer.material = MannequinEyesMaterial[1];
                Invoke("PlaySound", 0.5f);
                StartCoroutine(MoveOverSeconds(gameObject, MoveDistance, 0.5f));
                StartCoroutine(ChangeStateAfterDelay(3f));
            }
            else
            {
                Debug.Log("MannequinAnimator를 찾을 수 없음");
            }
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
        StopSound();
    }

    IEnumerator MoveOverSeconds(GameObject ObjectToMove, float distance, float seconds)
    {
        float ElapsedTime = 0;
        Vector3 StartPosition = ObjectToMove.transform.position;
        Vector3 TargetPosition = new Vector3(StartPosition.x + distance, StartPosition.y, StartPosition.z);

        while (ElapsedTime < seconds)
        {
            ObjectToMove.transform.position = Vector3.Lerp(StartPosition, TargetPosition, (ElapsedTime / seconds));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectToMove.transform.position = TargetPosition;
    }
}
