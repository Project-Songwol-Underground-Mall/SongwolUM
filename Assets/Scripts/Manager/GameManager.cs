using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool canTeleport { get; set; } = true;
    public bool isNormalStage { get; set; } = true; // ���������� ���� �� �̻����� ����
    public int currentStage { get; set; } = 0;// ���� ���� ��ȣ
    public int abnormalPhenomenonNumber { get; set; } = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
    public bool[] isAbnormalOccured = new bool[20]; // �̻����� ��ȣ�� ���� �߻� ����

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


    public GameObject SpawnManager;
    public GameObject ElevatorDoor;
    public GameObject GameOverPanel;
    public TextMeshProUGUI TMPNumOfCorrectAnswer;


    /*VR ������� �߰� ����*/
    // �������� ����
    // 0�� : Ʃ�丮�� ��������
    // 1 ~ 5�� : ���� ����Ŭ1
    // 6�� : �Ϲ� ��������(�̻����� X, �޽� ���ϱ�)
    // 7 ~ 11�� : ���� ����Ŭ2
    // 12�� : �Ϲ� ��������(�̻����� X, �޽� ���ϱ�)
    // 13�� ~ 17�� : ���� ����Ŭ3

    int NumOfCorrectAnswer = 0; // ���� ���� ���� ����
    int[,] ExperimentAPArray = new int[3, 5]; // ���������� �����ų �̻����� ��ȣ or �Ϲ� ��������

    public GameObject SafetyAlarmBoard;
    public GameObject StageInfoPanel;
    public GameObject PrevStageResultPanel;
    public GameObject NumOfCorrectAnswerPanel;
    public Material[] StageInfoMaterial = new Material[18]; // �������� ���� �г� ���׸��� �迭
    public Material[] PrevStageResultMaterial = new Material[2]; // ���� �������� ���� ���� �г� ���׸��� �迭
    public Material[] NumOfCorrectAnswerMaterial = new Material[16]; // ���� ���� ���� �г� ���׸��� ���

    // Start is called before the first frame update
    void Start()
    {
        LotteryPhenomenon();
        PrevStageResultPanel.SetActive(false);
        NumOfCorrectAnswerPanel.SetActive(false);
        SafetyAlarmBoard.SetActive(false);
        Debug.Log("�� ó�� ���������� �Ϲ� ���������Դϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeStage(bool IsFront) // ������ ���� Ȥ�� �ڷ� ���ư��� �� �������� ���� �� ���俩�� �Ǻ�
    {
        currentStage++;
        Debug.Log("CurrentStage : " + currentStage);

        if (currentStage == 1)
        {
            GetNextStage(currentStage);
            ChangeStageInfoPanel();
            ChangePSRPanel(true);
            return;
        }

        if (currentStage == 18)
        {
            Debug.Log("���� ����");
            ChangePSRPanel(false);
            ChangeStageInfoPanel();
            ChangeNOCAPanel();
            Invoke("EndGame", 0.5f);
        }


        if ((IsFront && isNormalStage) || (!IsFront && !isNormalStage))
        {
            PrevStageResultPanel.SetActive(true);
            NumOfCorrectAnswer++;
 
            if (currentStage == 7 || currentStage == 13)
            {
                NumOfCorrectAnswer--;
                PrevStageResultPanel.SetActive(false);
                GetNextStage(currentStage);
            }

            else
            {
                Debug.Log("����!");
                // GetRandomStage(CurrentStage, true);
                GetNextStage(currentStage);
                ChangePSRPanel(true);
            }
        }

        else
        {
            Debug.Log("����!");
            // GetRandomStage(CurrentStage, false);
            PrevStageResultPanel.SetActive(true);
            GetNextStage(currentStage);
            ChangePSRPanel(false);
        }
        ChangeStageInfoPanel();
        ChangeNOCAPanel();
    }

    /* �ƴ� ���� ���� ��� ������ ���� �Լ�, VR ���� �������� LotteryPhenomenon()�� GetNextStage()�� ���*/
    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// �������� ���濡 ���� �������� �� �̻����� ���� ��÷
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
        SpawnManager.GetComponent<PhenomenonManager>().SetPhenomenon(abnormalPhenomenonNumber, isNormalStage);


        // �̻����� Version�� ������Ʈ�� ������ ������ ������Ʈ�� �������ش�. �����صΰ� ���ܳ��� ����� �����.
        // ��� �׷��� �̻����� �߻� ������Ʈ�� ���� ������ �����ְ�, ������ �߻��� �̻����� ������Ʈ�� �����ְ� ����������� �ٽ� Spawn�ϴ� ���� �ʿ��ϴ�.
    }

    public void ResetTeleport()
    {
        Debug.Log("�ڷ���Ʈ Ȱ��ȭ");
        canTeleport = true;
    }

    void EndGame()
    {
        Debug.Log("EndGame �Լ� ����");
        if (TMPNumOfCorrectAnswer != null)
        {
            TMPNumOfCorrectAnswer.gameObject.SetActive(true);
            TMPNumOfCorrectAnswer.text = "�� ���� ���� : " + NumOfCorrectAnswer + " / 15";
        }

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }
        Time.timeScale = 0;
    }


    void LotteryPhenomenon() // VR ���� �������� ���� ���� ��, ���������� �̻����� ������ �̻����� ������ �̸� ��÷�ϴ� �Լ�
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
        SpawnManager.GetComponent<PhenomenonManager>().SetPhenomenon(APNumber, isNormalStage);
    }

    void ChangeStageInfoPanel()
    {
        if (currentStage == 18)
        {
            StageInfoPanel.SetActive(false);
            return;
        }
        Renderer SIPRenderer = StageInfoPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[currentStage];
        }
    }

    void ChangePSRPanel(bool IsCorrect)
    {
        if (currentStage == 18)
        {
            PrevStageResultPanel.SetActive(false);
            return;
        }

        if (currentStage == 7 || currentStage == 13)
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
        if (currentStage != 1) NumOfCorrectAnswerPanel.SetActive(true);

        if (currentStage == 18)
        {
            NumOfCorrectAnswerPanel.SetActive(false);
            return;
        }

        Renderer SIPRenderer = NumOfCorrectAnswerPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = NumOfCorrectAnswerMaterial[NumOfCorrectAnswer];
        }
    }
}
