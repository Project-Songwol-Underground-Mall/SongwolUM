using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject ElevatorDoor;
    public bool CanTeleport = true;

    int CurrentStage = 0; // 현재 구역 번호
    bool IsNormalStage = true; // 스테이지의 정상 및 이상현상 여부
    int PrevAbnormalNumber = -1;
    int AbnormalNumber = -1; // 이상현상 스테이지에서 발생시킬 이상현상 번호
    bool[] IsAbnormalOccured = new bool[20]; // 이상현상 번호에 따른 발생 여부


    /*VR 실험버전 추가 내용*/
    // 스테이지 조직
    // 0번 : 튜토리얼 스테이지
    // 1 ~ 5번 : 실험 사이클1
    // 6번 : 일반 스테이지(이상현상 X, 휴식 취하기)
    // 7 ~ 11번 : 실험 사이클2
    // 12번 : 일반 스테이지(이상현상 X, 휴식 취하기)
    // 13번 ~ 17번 : 실험 사이클3

    int NumOfCorrectAnswer = 0; // 현재 맞춘 정답 개수
    int[,] ExperimentAPArray = new int[2, 5]; // 스테이지별 등장시킬 이상현상 번호 or 일반 스테이지


    public GameObject StageInfoPanel;
    public GameObject PrevStageResultPanel;
    public GameObject NumOfCorrectAnswerPanel;
    public Material[] StageInfoMaterial = new Material[14];
    public Material[] PrevStageResultMaterial = new Material[2];
    public Material[] NumOfCorrectAnswerMaterial = new Material[11];

    // Start is called before the first frame update
    void Start()
    {
        LotteryPhenomenon();
        PrevStageResultPanel.SetActive(false);
        NumOfCorrectAnswerPanel.SetActive(false);
        Debug.Log("맨 처음 스테이지는 일반 스테이지입니다.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(bool IsFront) // 앞으로 전진 혹은 뒤로 돌아갔을 시 스테이지 증가 및 정답여부 판별
    {
        CurrentStage++;
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            NumOfCorrectAnswer++;
            if (CurrentStage == 1 || CurrentStage == 7) NumOfCorrectAnswer--;

            if (CurrentStage == 12)
            {
                ElevatorDoor.SetActive(false);
                EndGame();
            }
            else
            {
                Debug.Log("정답!");
                // GetRandomStage(CurrentStage, true);
                GetNextStage(CurrentStage, true);
                ChangePSRPanel(true);
            }
        }

        else
        {
            Debug.Log("오답!");
            ElevatorDoor.SetActive(true);
            // GetRandomStage(CurrentStage, false);
            GetNextStage(CurrentStage, false);
            ChangePSRPanel(false);
        }
        ChangeStageInfoPanel();
        ChangeNOCAPanel();
    }

    /* 아닌 실제 게임 출시 버전에 사용될 함수, VR 실험 버전에는 LotteryPhenomenon()과 GetNextStage()를 사용*/
    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// 스테이지 변경에 따른 스테이지 및 이상현상 랜덤 추첨
    {


        if (!IsCorrectDirection)
        {
            Debug.Log("오답입니다!");
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

        if (Result > Boundary && !IsNormalStage) // 이전 스테이지가 이상현상 스테이지이고 추첨 결과가 일반 스테이지
        {
            Debug.Log("이번 스테이지는 일반 스테이지입니다.");
            AbnormalNumber = -1;
            IsNormalStage = true;
        }

        else if (Result <= Boundary || IsNormalStage) // 이전 스테이지가 일반 스테이지거나 추첨 결과가 이상현상 스테이지
        {
            Debug.Log("이번 스테이지는 이상현상 스테이지입니다.");
            AbnormalNumber = Random.Range(0, 20);
            while (IsAbnormalOccured[AbnormalNumber])
            {
                AbnormalNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalNumber] = true;
            IsNormalStage = false;
        }
        // 여기서 추첨 결과로 나온 번호의 오브젝트의 이상현상 발생 Version을 Spawn해줘야 한다.
        SpawnManager.GetComponent<PhenomenonManagement>().SetPhenomenon(AbnormalNumber, IsNormalStage);



        // 이상현상 Version의 오브젝트를 제외한 나머지 오브젝트를 스폰해준다. 스폰해두고 남겨놓는 방법도 고려중.
        // 대신 그러면 이상현상 발생 오브젝트의 이전 버전은 지워주고, 이전에 발생한 이상현상 오브젝트도 지워주고 정상버전으로 다시 Spawn하는 수고가 필요하다.
    }

    public void ResetTeleport()
    {
        Debug.Log("5초 후 CanTeleport 리셋");
        CanTeleport = true;
    }

    void EndGame()
    {
        Debug.Log("게임 종료");
    }


    void LotteryPhenomenon() // VR 실험 버전에서 게임 시작 전, 스테이지별 이상현상 유무와 이상현상 종류를 미리 추첨하는 함수
    {
        for (int i = 0; i < 2; i++)
        {
            int NumOfNormalStage = Random.Range(0, 3);
            bool isAddedPuzzleAP = false;

            for (int j = 0; j < NumOfNormalStage; j++)
            {
                ExperimentAPArray[i, j] = -1;
            }

            for (int j = NumOfNormalStage; j < 5; j++)
            {
                // 퍼즐 유형 이상현상이 아직 추가되지 않았을 때 - 전체 이상현상에서 랜덤 추첨
                if (!isAddedPuzzleAP)
                {
                    AbnormalNumber = Random.Range(0, 13);
                    if (AbnormalNumber <= 3 && !IsAbnormalOccured[AbnormalNumber]) isAddedPuzzleAP = true;
                }

                // 퍼즐 유형 이상현상이 추가되었을 때 - 공포 유형 이상현상에서만 랜덤 추첨
                else AbnormalNumber = Random.Range(4, 13);


                if (!IsAbnormalOccured[AbnormalNumber])
                {
                    ExperimentAPArray[i, j] = AbnormalNumber;
                    IsAbnormalOccured[AbnormalNumber] = true;
                }
                else j--;
            }

            for (int j = 4; j > 0; j--)
            {
                int k = Random.Range(0, j + 1);
                int temp = ExperimentAPArray[i, j];
                ExperimentAPArray[i, j] = ExperimentAPArray[i, k];
                ExperimentAPArray[i, k] = temp;
            }

        }
    }

    void GetNextStage(int StageNumber, bool IsCorrectDirection)
    {
        IsNormalStage = true;
        int APNumber = -1; // 이상현상 번호
        if (StageNumber == 6 || StageNumber == 12) // 실험 사이클 종료후 휴식용 일반 스테이지
        {
            IsNormalStage = true;
            // 현재 휴식 스테이지 임을 나타내는 안내판 스폰, 바닥 표식 변경이 필요

        }

        else
        {
            int Cycle = 0, Index = 0;
            if (StageNumber < 6)
            {
                Index = StageNumber - 1;
            }

            if (6 < StageNumber && StageNumber < 12)
            {
                Cycle = 1;
                Index = StageNumber - 7;
            }
            Debug.Log("StageNumber : " + StageNumber);
            Debug.Log("Cycle : " + Cycle + "Index : " + Index);
            Debug.Log("이상현상 번호(-1이면 일반 스테이지) : " + ExperimentAPArray[Cycle, Index]);
            if (ExperimentAPArray[Cycle, Index] <= 3 && ExperimentAPArray[Cycle, Index] != -1) Debug.Log("퍼즐 이상현상입니다.");
            APNumber = ExperimentAPArray[Cycle, Index];
            if (APNumber != -1) IsNormalStage = false;
        }

        // 여기서 현재 스테이지 번호에 맞는 이상현상 발생 Version 오브젝트를 Spawn해줘야 한다.
        SpawnManager.GetComponent<PhenomenonManagement>().SetPhenomenon(APNumber, IsNormalStage);
    }

    void ChangeStageInfoPanel()
    {
        Renderer SIPRenderer = StageInfoPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[CurrentStage];
        }
    }

    void ChangePSRPanel(bool IsCorrect)
    {
        if (CurrentStage == 7)
        {
            PrevStageResultPanel.SetActive(false);
            return;
        }
        else
        {
            PrevStageResultPanel.SetActive(true);
        }

        Renderer SIPRenderer = PrevStageResultPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            if (IsCorrect) SIPRenderer.material = PrevStageResultMaterial[0];
            else SIPRenderer.material = PrevStageResultMaterial[1];
        }
    }

    void ChangeNOCAPanel()
    {
        if (CurrentStage != 1) NumOfCorrectAnswerPanel.SetActive(true);


        Renderer SIPRenderer = NumOfCorrectAnswerPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = NumOfCorrectAnswerMaterial[NumOfCorrectAnswer];
        }
    }
}
