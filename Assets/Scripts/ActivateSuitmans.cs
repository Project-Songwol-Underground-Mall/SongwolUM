using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuitmans : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] SuitMans = new GameObject[5];
    bool isAnimationActivated = false;
    private Animator[] SuitManAnimators = new Animator[5];
    private float ActivationDistance = 50f; // 애니메이션 작동 범위
    private float MoveDistance = 30f; // 정장남 이동 거리
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        isAnimationActivated = false;
        for (int i = 0; i < SuitManAnimators.Length; i++)
        {
            SuitManAnimators[i] = SuitMans[i].GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = transform.position.x - Player.transform.position.x;

        if (Distance <= ActivationDistance && !isAnimationActivated)
        {
            if (SuitManAnimators[0] != null)
            {
                // 정장남 애니메이션 재생
                isAnimationActivated = true;
                for (int i = 0; i < SuitManAnimators.Length; i++)
                {
                    SuitManAnimators[i].Play("move_suit");
                    Debug.Log("춤추는 애니메이션 재생");
                }
                MoveSuitMans();
            }
            else
            {
                Debug.Log("SuitManAnimator를 찾을 수 없음");
            }
        }
    }

    void MoveSuitMans()
    {
        StartCoroutine(MoveOverSeconds(gameObject, MoveDistance, 0.5f));
    }

    IEnumerator MoveOverSeconds(GameObject ObjectToMove, float distance, float seconds)
    {
        float ElapsedTime = 0;
        Vector3 StartPosition = ObjectToMove.transform.position;
        Vector3 TargetPosition = new Vector3(StartPosition.x - distance, StartPosition.y, StartPosition.z);

        while (ElapsedTime < seconds)
        {
            ObjectToMove.transform.position = Vector3.Lerp(StartPosition, TargetPosition, (ElapsedTime / seconds));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectToMove.transform.position = TargetPosition;
    }

    IEnumerator ChangeStateAfterDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        
        for (int i = 0; i < SuitManAnimators.Length; i++)
        {
            SuitManAnimators[i].Play("stand_suit");
        }
    }
}
