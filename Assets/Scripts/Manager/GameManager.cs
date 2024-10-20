using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool canTeleport = true;
    private bool isNormalStage = true; // 스테이지의 정상 및 이상현상 여부
    private const int numOfStages = 4;
    private const int numOfPhenomenons = 18;
    private int currentStage = 0;// 현재 구역 번호
    private int abnormalPhenomenonNumber = -1; // 이상현상 스테이지에서 발생시킬 이상현상 번호
    public bool[] isAbnormalOccured = new bool[20]; // 이상현상 번호에 따른 발생 여부

    public GameObject stageInfoManager;
    public StageInfoManager stageInfoManagerInstance;


    public bool GetCanTeleport() { return canTeleport; }
    public bool GetIsNormalStage() { return isNormalStage; }
    public int GetNumOfStages() { return numOfStages; }
    public int GetNumOfPhenomenons() { return numOfPhenomenons; }
    public int GetCurrentStage() { return currentStage; }
    public int GetAbnormalPhenomenonNumber() { return abnormalPhenomenonNumber; }
    public void SetCanTeleport(bool val) { canTeleport = val; }
    public void SetIsNormalStage(bool val) { isNormalStage = val; }
    public void SetCurrentStage(int val) { currentStage = val; }
    public void SetAbnormalPhenomenonNumber(int val) { abnormalPhenomenonNumber = val; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public GameObject phenomenonManager;

    void Start()
    {
        stageInfoManagerInstance = stageInfoManager.GetComponent<StageInfoManager>();
        stageInfoManagerInstance.PrevStageResultPanel.SetActive(false);
        Debug.Log("맨 처음 스테이지는 일반 스테이지입니다.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 앞으로 전진 혹은 뒤로 돌아갔을 시 정답여부 판별
    public void ChangeStage(bool IsFront) 
    {
        bool isCorrect = false;
        if ((IsFront && isNormalStage) || (!IsFront && !isNormalStage))
        {
            Debug.Log("정답!");
            isCorrect = true;
            currentStage++;

            if (currentStage == numOfStages)
            {
                Debug.Log("게임 종료");
                stageInfoManagerInstance.SetAllPanelInactive();
                Invoke("EndGame", 0.5f);
            }
        }
        else
        {
            Debug.Log("오답!");
            currentStage = 0;
        }

        GetRandomStage(currentStage, isCorrect);
        stageInfoManagerInstance.ChangeStageInfoPanel();
        stageInfoManagerInstance.ChangePrevStageResultPanel(isCorrect);
    }

    // 스테이지 변경에 따른 스테이지 및 이상현상 랜덤 추첨
    void GetRandomStage(int StageNumber, bool IsCorrectDirection)
    {
        if (!IsCorrectDirection)
        {
            Debug.Log("오답입니다!");
            abnormalPhenomenonNumber = -1;
            isNormalStage = true;
            for (int i = 0; i < isAbnormalOccured.Length; i++) isAbnormalOccured[i] = false;
            return;
        }

        int Boundary = 0;
        if (StageNumber == 1) Boundary = 69;
        if (StageNumber == 2) Boundary = 79;
        if (StageNumber == 3) Boundary = 84;
        if (StageNumber == 4) Boundary = 89;

        int Result = Random.Range(0, 100);

        if (Result > Boundary && !isNormalStage) // 이전 스테이지가 이상현상 스테이지이고 추첨 결과가 일반 스테이지
        {
            Debug.Log("이번 스테이지는 일반 스테이지입니다.");
            abnormalPhenomenonNumber = -1;
            isNormalStage = true;
        }

        else if (Result <= Boundary || isNormalStage) // 이전 스테이지가 일반 스테이지거나 추첨 결과가 이상현상 스테이지
        {
            Debug.Log("이번 스테이지는 이상현상 스테이지입니다.");
            abnormalPhenomenonNumber = Random.Range(0, 20);
            while (isAbnormalOccured[abnormalPhenomenonNumber])
            {
                abnormalPhenomenonNumber = Random.Range(0, 20);
            }
            isAbnormalOccured[abnormalPhenomenonNumber] = true;
            isNormalStage = false;
        }
        // 여기서 추첨 결과로 나온 번호의 오브젝트의 이상현상 발생 Version을 Spawn해줘야 한다.
        phenomenonManager.GetComponent<PhenomenonManager>().SetPhenomenon(abnormalPhenomenonNumber, isNormalStage);


        // 이상현상 Version의 오브젝트를 제외한 나머지 오브젝트를 스폰해준다. 스폰해두고 남겨놓는 방법도 고려중.
        // 대신 그러면 이상현상 발생 오브젝트의 이전 버전은 지워주고, 이전에 발생한 이상현상 오브젝트도 지워주고 정상버전으로 다시 Spawn하는 수고가 필요하다.
    }

    void EndGame()
    {
        stageInfoManagerInstance.GameEndPanel.SetActive(true);
        Time.timeScale = 0;
    }









    // VR Experiment Version에서만 사용
    // 스테이지 조직
    // 0번 : 튜토리얼 스테이지
    // 1 ~ 5번 : 실험 사이클1
    // 6번 : 일반 스테이지(이상현상 X, 휴식 취하기)
    // 7 ~ 11번 : 실험 사이클2
    // 12번 : 일반 스테이지(이상현상 X, 휴식 취하기)
    // 13번 ~ 17번 : 실험 사이클3
    int NumOfCorrectAnswer = 0; // 현재 맞춘 정답 개수
    int[,] ExperimentAPArray = new int[3, 5]; // 스테이지별 등장시킬 이상현상 번호 or 일반 스테이지
    private GameObject SafetyAlarmBoard;

    void LotteryPhenomenon() // 게임 시작 전, 스테이지별 이상현상 유무와 이상현상 종류를 미리 추첨하는 함수
    {
        for (int i = 0; i < 3; i++) ExperimentAPArray[0, i] = -1;
        for (int i = 3; i < 15; i++) ExperimentAPArray[i / 5, i % 5] = i - 3;

        for (int i = 14; i >= 0; i--)
        {
            int k = Random.Range(0, 15);
            int temp = ExperimentAPArray[i / 5, i % 5];
            ExperimentAPArray[i / 5, i % 5] = ExperimentAPArray[k / 5, k % 5];
            ExperimentAPArray[k / 5, k % 5] = temp;
        }
        ExperimentAPArray[0, 0] = 6;

    }
    void GetNextStage(int StageNumber)
    {
        isNormalStage = true;
        int APNumber = -1; // 이상현상 번호
        if (StageNumber == 6 || StageNumber == 12) // 실험 사이클 종료후 휴식용 일반 스테이지
        {
            isNormalStage = true;
            SafetyAlarmBoard.SetActive(true);
        }

        else
        {
            SafetyAlarmBoard.SetActive(false);
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

            if (12 < StageNumber && StageNumber < 18)
            {
                Cycle = 2;
                Index = StageNumber - 13;
            }
            Debug.Log("Cycle : " + Cycle + "Index : " + Index);
            Debug.Log("이상현상 번호(-1이면 일반 스테이지) : " + ExperimentAPArray[Cycle, Index]);
            APNumber = ExperimentAPArray[Cycle, Index];
            if (APNumber != -1) isNormalStage = false;
        }

        // 여기서 현재 스테이지 번호에 맞는 이상현상 발생 Version 오브젝트를 Spawn해줘야 한다.
        phenomenonManager.GetComponent<PhenomenonManager>().SetPhenomenon(APNumber, isNormalStage);
    }
}
