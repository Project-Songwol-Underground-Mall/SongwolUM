using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject ElevatorDoor;
    public bool CanTeleport = true;

    int CurrentStage = 0; // ���� ���� ��ȣ
    bool IsNormalStage = true; // ���������� ���� �� �̻����� ����
    int PrevAbnormalNumber = -1;
    int AbnormalNumber = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
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
    int[,] ExperimentAPArray = new int[2, 5]; // ���������� �����ų �̻����� ��ȣ or �Ϲ� ��������


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
        PrevStageResultPanel.setActive(false);
        NumOfCorrectAnswerPanel.setActive(false);
        Debug.Log("�� ó�� ���������� �Ϲ� ���������Դϴ�.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(bool IsFront) // ������ ���� Ȥ�� �ڷ� ���ư��� �� �������� ���� �� ���俩�� �Ǻ�
    {
        CurrentStage++;
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            NumOfCorrectAnswer++;
            if (CurrentStage == 12)
            {
                ElevatorDoor.SetActive(false);
                EndGame();
            }
            else
            {
                Debug.Log("����!");
                // GetRandomStage(CurrentStage, true);
                GetNextStage(CurrentStage - 1, true);
                ChangePSRPanel(true);
            }
        }

        else
        {
            Debug.Log("����!");
            ElevatorDoor.SetActive(true);
            // GetRandomStage(CurrentStage, false);
            GetNextStage(CurrentStage - 1, false);
            ChangePSRPanel(false);
        }
        ChangeStageInfoPanel();
        ChangeNOCAPanel();
    }

    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// �������� ���濡 ���� �������� �� �̻����� ���� ��÷
    {


        if (!IsCorrectDirection)
        {
            Debug.Log("�����Դϴ�!");
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

        if (Result > Boundary && !IsNormalStage) // ���� ���������� �̻����� ���������̰� ��÷ ����� �Ϲ� ��������
        {
            Debug.Log("�̹� ���������� �Ϲ� ���������Դϴ�.");
            AbnormalNumber = -1;
            IsNormalStage = true;
        }

        else if (Result <= Boundary || IsNormalStage) // ���� ���������� �Ϲ� ���������ų� ��÷ ����� �̻����� ��������
        {
            Debug.Log("�̹� ���������� �̻����� ���������Դϴ�.");
            AbnormalNumber = Random.Range(0, 20);
            while (IsAbnormalOccured[AbnormalNumber])
            {
                AbnormalNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalNumber] = true;
            IsNormalStage = false;
        }
        // ���⼭ ��÷ ����� ���� ��ȣ�� ������Ʈ�� �̻����� �߻� Version�� Spawn����� �Ѵ�.
        SpawnManager.GetComponent<PhenomenonManagement>().SetPhenomenon(AbnormalNumber, IsNormalStage);



        // �̻����� Version�� ������Ʈ�� ������ ������ ������Ʈ�� �������ش�. �����صΰ� ���ܳ��� ����� �����.
        // ��� �׷��� �̻����� �߻� ������Ʈ�� ���� ������ �����ְ�, ������ �߻��� �̻����� ������Ʈ�� �����ְ� ����������� �ٽ� Spawn�ϴ� ���� �ʿ��ϴ�.
    }

    public void ResetTeleport()
    {
        Debug.Log("5�� �� CanTeleport ����");
        CanTeleport = true;
    }

    void EndGame()
    {
        Debug.Log("���� ����");
    }

    void LotteryPhenomenon()
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
                // ���� ���� �̻������� ���� �߰����� �ʾ��� �� - ��ü �̻����󿡼� ���� ��÷
                if (!isAddedPuzzleAP)
                {
                    AbnormalNumber = Random.Range(0, 13);
                    if (AbnormalNumber <= 3 && !IsAbnormalOccured[AbnormalNumber]) isAddedPuzzleAP = true;
                }

                // ���� ���� �̻������� �߰��Ǿ��� �� - ���� ���� �̻����󿡼��� ���� ��÷
                else AbnormalNumber = Random.Range(4, 13);


                if (!IsAbnormalOccured[AbnormalNumber])
                {
                    ExperimentAPArray[i, j] = AbnormalNumber;
                    IsAbnormalOccured[AbnormalNumber] = true;
                }
                else j--;
            }
        }
    }

    void GetNextStage(int StageNumber, bool IsCorrectDirection)
    {
        int APNumber = -1; // �̻����� ��ȣ
        if (StageNumber == 6 || StageNumber == 12) // ���� ����Ŭ ������ �޽Ŀ� �Ϲ� ��������
        {
            IsNormalStage = true;
            // ���� �޽� �������� ���� ��Ÿ���� �ȳ��� ����, �ٴ� ǥ�� ������ �ʿ�

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
            APNumber = ExperimentAPArray[Cycle, Index];
            if (APNumber != -1) IsNormalStage = false;
        }

        // ���⼭ ���� �������� ��ȣ�� �´� �̻����� �߻� Version ������Ʈ�� Spawn����� �Ѵ�.
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
            PrevStageResultPanel.setActive(false);
            return;
        }
        else
        {
            PrevStageResultPanel.setActive(true);
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
        if (CurrentStage != 1) NumOfCorrectAnswerPanel.setActive(true);
        

        Renderer SIPRenderer = NumOfCorrectAnswerPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = NumOfCorrectAnswerMaterial[NumOfCorrectAnswer];
        }
    }
}
