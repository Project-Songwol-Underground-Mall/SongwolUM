using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    int CurrentStage = 0; // 현재 구역 번호
    int AbnormalNumber = -1; // 이상현상 스테이지에서 발생시킬 이상현상 번호
    bool[] IsAbnormalOccured = new bool[20]; // 이상현상 번호에 따른 발생 여부
    bool IsNormalStage = true; // 스테이지의 정상 및 이상현상 여부

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeStage(bool IsFront) // 앞으로 전진 혹은 뒤로 돌아갔을 시 정답 여부에 따른 스테이지 변경
    {
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            CurrentStage++;
            if (CurrentStage == 5)
            {
                EndGame();
            }
            GetRandomStage(CurrentStage, true); // 정답을 맞춤
        }

        else
        {
            CurrentStage = 0;
            GetRandomStage(CurrentStage, false); // 오답
        }
    }


    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// 스테이지 변경에 따른 스테이지 및 이상현상 랜덤 추첨
    {
        if (!IsCorrectDirection)
        {
            AbnormalNumber = -1;
            IsNormalStage = true;
            return;
        }

        int Boundary = 0;
        if (StageNumber == 1) Boundary = 69;
        if (StageNumber == 2) Boundary = 79;
        if (StageNumber == 3) Boundary = 84;
        if (StageNumber == 4) Boundary = 89;

        int Result = Random.Range(0, 100);

        if (Result <= Boundary || IsNormalStage) // 이전 스테이지가 일반 스테이지거나 추첨 결과가 이상현상 스테이지
        {
            while (IsAbnormalOccured[AbnormalNumber])
            {
                AbnormalNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalNumber] = true;
            IsNormalStage = false;
            // 여기서 추첨 결과로 나온 번호의 오브젝트의 이상현상 발생 Version을 Spawn해줘야 한다.
            // UPhenomenonObject PhenomenonObject = ~~~
            // SpawnObject(AbnormalNumber, false)
        }

        if (Result > Boundary && !IsNormalStage) // 이전 스테이지가 이상현상 스테이지이고 추첨 결과가 일반 스테이지
        {
            AbnormalNumber = -1;
            IsNormalStage = true;
        }

        // 이상현상 Version의 오브젝트를 제외한 나머지 오브젝트를 스폰해준다. 스폰해두고 남겨놓는 방법도 고려중.
        // 대신 그러면 이상현상 발생 오브젝트의 이전 버전은 지워주고, 이전에 발생한 이상현상 오브젝트도 지워주고 정상버전으로 다시 Spawn하는 수고가 필요하다.
        // UPhenomenonObject PhenomenonObject = ~~~
        // SpawnObject(AbnormalNumber, true)
    }

    void EndGame()
    {

    }
}
