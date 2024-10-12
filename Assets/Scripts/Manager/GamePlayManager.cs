using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject ElevatorDoor;
    public GameObject GameOverPanel;
    public TextMeshProUGUI TMPNumOfCorrectAnswer;

    public bool CanTeleport = true;

    int CurrentStage = 0; // ���� ���� ��ȣ
    bool IsNormalStage = true; // ���������� ���� �� �̻����� ����
    int AbnormalPhenomenonNumber = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
    bool[] IsAbnormalOccured = new bool[20]; // �̻����� ��ȣ�� ���� �߻� ����


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
        CurrentStage++;
        Debug.Log("CurrentStage : " + CurrentStage);

        if (CurrentStage == 1)
        {
            GetNextStage(CurrentStage);
            ChangeStageInfoPanel();
            ChangePSRPanel(true);
            return;
        }

        if (CurrentStage == 18)
        {
            Debug.Log("���� ����");
            ChangePSRPanel(false);
            ChangeStageInfoPanel();
            ChangeNOCAPanel();
            Invoke("EndGame", 0.5f);
        }


        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            PrevStageResultPanel.SetActive(true);
            NumOfCorrectAnswer++;
 
            if (CurrentStage == 7 || CurrentStage == 13)
            {
                NumOfCorrectAnswer--;
                PrevStageResultPanel.SetActive(false);
                GetNextStage(CurrentStage);
            }

            else
            {
                Debug.Log("����!");
                // GetRandomStage(CurrentStage, true);
                GetNextStage(CurrentStage);
                ChangePSRPanel(true);
            }
        }

        else
        {
            Debug.Log("����!");
            // GetRandomStage(CurrentStage, false);
            PrevStageResultPanel.SetActive(true);
            GetNextStage(CurrentStage);
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
            AbnormalPhenomenonNumber = -1;
            IsNormalStage = true;
            for (int i = 0; i < IsAbnormalOccured.Length; i++) IsAbnormalOccured[i] = false;
            return;
        }

        int Boundary = 0;
        if (StageNumber == 1) Boundary = 69;
        if (StageNumber == 2) Boundary = 79;
        if (StageNumber == 3) Boundary = 84;
        if (StageNumber == 4) Boundary = 89;

        int Result = Random.Range(0, 100);

        if (Result > Boundary && !IsNormalStage) // ���� ���������� �̻����� ���������̰� ��÷ ����� �Ϲ� ��������
        {
            Debug.Log("�̹� ���������� �Ϲ� ���������Դϴ�.");
            AbnormalPhenomenonNumber = -1;
            IsNormalStage = true;
        }

        else if (Result <= Boundary || IsNormalStage) // ���� ���������� �Ϲ� ���������ų� ��÷ ����� �̻����� ��������
        {
            Debug.Log("�̹� ���������� �̻����� ���������Դϴ�.");
            AbnormalPhenomenonNumber = Random.Range(0, 20);
            while (IsAbnormalOccured[AbnormalPhenomenonNumber])
            {
                AbnormalPhenomenonNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalPhenomenonNumber] = true;
            IsNormalStage = false;
        }
        // ���⼭ ��÷ ����� ���� ��ȣ�� ������Ʈ�� �̻����� �߻� Version�� Spawn����� �Ѵ�.
        SpawnManager.GetComponent<PhenomenonManager>().SetPhenomenon(AbnormalPhenomenonNumber, IsNormalStage);


        // �̻����� Version�� ������Ʈ�� ������ ������ ������Ʈ�� �������ش�. �����صΰ� ���ܳ��� ����� �����.
        // ��� �׷��� �̻����� �߻� ������Ʈ�� ���� ������ �����ְ�, ������ �߻��� �̻����� ������Ʈ�� �����ְ� ����������� �ٽ� Spawn�ϴ� ���� �ʿ��ϴ�.
    }

    public void ResetTeleport()
    {
        Debug.Log("�ڷ���Ʈ Ȱ��ȭ");
        CanTeleport = true;
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
        IsNormalStage = true;
        int APNumber = -1; // �̻����� ��ȣ
        if (StageNumber == 6 || StageNumber == 12) // ���� ����Ŭ ������ �޽Ŀ� �Ϲ� ��������
        {
            IsNormalStage = true;
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
            if (APNumber != -1) IsNormalStage = false;
        }

        // ���⼭ ���� �������� ��ȣ�� �´� �̻����� �߻� Version ������Ʈ�� Spawn����� �Ѵ�.
        SpawnManager.GetComponent<PhenomenonManager>().SetPhenomenon(APNumber, IsNormalStage);
    }

    void ChangeStageInfoPanel()
    {
        if (CurrentStage == 18)
        {
            StageInfoPanel.SetActive(false);
            return;
        }
        Renderer SIPRenderer = StageInfoPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[CurrentStage];
        }
    }

    void ChangePSRPanel(bool IsCorrect)
    {
        if (CurrentStage == 18)
        {
            PrevStageResultPanel.SetActive(false);
            return;
        }

        if (CurrentStage == 7 || CurrentStage == 13)
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

        if (CurrentStage == 18)
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
