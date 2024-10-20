using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool canTeleport = true;
    private bool isNormalStage = true; // ���������� ���� �� �̻����� ����
    private const int numOfStages = 4;
    private const int numOfPhenomenons = 18;
    private int currentStage = 0;// ���� ���� ��ȣ
    private int abnormalPhenomenonNumber = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
    public bool[] isAbnormalOccured = new bool[20]; // �̻����� ��ȣ�� ���� �߻� ����

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
        Debug.Log("�� ó�� ���������� �Ϲ� ���������Դϴ�.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ������ ���� Ȥ�� �ڷ� ���ư��� �� ���俩�� �Ǻ�
    public void ChangeStage(bool IsFront) 
    {
        bool isCorrect = false;
        if ((IsFront && isNormalStage) || (!IsFront && !isNormalStage))
        {
            Debug.Log("����!");
            isCorrect = true;
            currentStage++;

            if (currentStage == numOfStages)
            {
                Debug.Log("���� ����");
                stageInfoManagerInstance.SetAllPanelInactive();
                Invoke("EndGame", 0.5f);
            }
        }
        else
        {
            Debug.Log("����!");
            currentStage = 0;
        }

        GetRandomStage(currentStage, isCorrect);
        stageInfoManagerInstance.ChangeStageInfoPanel();
        stageInfoManagerInstance.ChangePrevStageResultPanel(isCorrect);
    }

    // �������� ���濡 ���� �������� �� �̻����� ���� ��÷
    void GetRandomStage(int StageNumber, bool IsCorrectDirection)
    {
        if (!IsCorrectDirection)
        {
            Debug.Log("�����Դϴ�!");
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

        if (Result > Boundary && !isNormalStage) // ���� ���������� �̻����� ���������̰� ��÷ ����� �Ϲ� ��������
        {
            Debug.Log("�̹� ���������� �Ϲ� ���������Դϴ�.");
            abnormalPhenomenonNumber = -1;
            isNormalStage = true;
        }

        else if (Result <= Boundary || isNormalStage) // ���� ���������� �Ϲ� ���������ų� ��÷ ����� �̻����� ��������
        {
            Debug.Log("�̹� ���������� �̻����� ���������Դϴ�.");
            abnormalPhenomenonNumber = Random.Range(0, 20);
            while (isAbnormalOccured[abnormalPhenomenonNumber])
            {
                abnormalPhenomenonNumber = Random.Range(0, 20);
            }
            isAbnormalOccured[abnormalPhenomenonNumber] = true;
            isNormalStage = false;
        }
        // ���⼭ ��÷ ����� ���� ��ȣ�� ������Ʈ�� �̻����� �߻� Version�� Spawn����� �Ѵ�.
        phenomenonManager.GetComponent<PhenomenonManager>().SetPhenomenon(abnormalPhenomenonNumber, isNormalStage);


        // �̻����� Version�� ������Ʈ�� ������ ������ ������Ʈ�� �������ش�. �����صΰ� ���ܳ��� ����� �����.
        // ��� �׷��� �̻����� �߻� ������Ʈ�� ���� ������ �����ְ�, ������ �߻��� �̻����� ������Ʈ�� �����ְ� ����������� �ٽ� Spawn�ϴ� ���� �ʿ��ϴ�.
    }

    void EndGame()
    {
        stageInfoManagerInstance.GameEndPanel.SetActive(true);
        Time.timeScale = 0;
    }









    // VR Experiment Version������ ���
    // �������� ����
    // 0�� : Ʃ�丮�� ��������
    // 1 ~ 5�� : ���� ����Ŭ1
    // 6�� : �Ϲ� ��������(�̻����� X, �޽� ���ϱ�)
    // 7 ~ 11�� : ���� ����Ŭ2
    // 12�� : �Ϲ� ��������(�̻����� X, �޽� ���ϱ�)
    // 13�� ~ 17�� : ���� ����Ŭ3
    int NumOfCorrectAnswer = 0; // ���� ���� ���� ����
    int[,] ExperimentAPArray = new int[3, 5]; // ���������� �����ų �̻����� ��ȣ or �Ϲ� ��������
    private GameObject SafetyAlarmBoard;

    void LotteryPhenomenon() // ���� ���� ��, ���������� �̻����� ������ �̻����� ������ �̸� ��÷�ϴ� �Լ�
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
        int APNumber = -1; // �̻����� ��ȣ
        if (StageNumber == 6 || StageNumber == 12) // ���� ����Ŭ ������ �޽Ŀ� �Ϲ� ��������
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
            Debug.Log("�̻����� ��ȣ(-1�̸� �Ϲ� ��������) : " + ExperimentAPArray[Cycle, Index]);
            APNumber = ExperimentAPArray[Cycle, Index];
            if (APNumber != -1) isNormalStage = false;
        }

        // ���⼭ ���� �������� ��ȣ�� �´� �̻����� �߻� Version ������Ʈ�� Spawn����� �Ѵ�.
        phenomenonManager.GetComponent<PhenomenonManager>().SetPhenomenon(APNumber, isNormalStage);
    }
}
