using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject StageInfoPlane;
    public Material[] StageInfoMaterial = new Material[5];
    public bool CanTeleport = true;

    int CurrentStage = 0; // ���� ���� ��ȣ
    bool IsNormalStage = true; // ���������� ���� �� �̻����� ����
    int AbnormalNumber = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
    bool[] IsAbnormalOccured = new bool[20]; // �̻����� ��ȣ�� ���� �߻� ����

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�� ó�� ���������� �Ϲ� ���������Դϴ�.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(bool IsFront) // ������ ���� Ȥ�� �ڷ� ���ư��� �� ���� ���ο� ���� �������� ����
    {
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            CurrentStage++;
            if (CurrentStage == 4)
            {
                EndGame();
            }
            else
            {
                Debug.Log("����!");
                GetRandomStage(CurrentStage, true); // ������ ����
            }
        }

        else
        {
            CurrentStage = 0;
            Debug.Log("����!");
            GetRandomStage(CurrentStage, false); // ����
        }
        ChangePlaneMaterial();
    }


    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// �������� ���濡 ���� �������� �� �̻����� ���� ��÷
    {


        if (!IsCorrectDirection)
        {
            Debug.Log("���� �������� �����̿����Ƿ� �̹� ���������� �Ϲ� ���������Դϴ�.");
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
        SpawnManager.GetComponent<PhenomenonManagement>().SpawnPhenomenon(AbnormalNumber, IsNormalStage);



        // �̻����� Version�� ������Ʈ�� ������ ������ ������Ʈ�� �������ش�. �����صΰ� ���ܳ��� ����� �����.
        // ��� �׷��� �̻����� �߻� ������Ʈ�� ���� ������ �����ְ�, ������ �߻��� �̻����� ������Ʈ�� �����ְ� ����������� �ٽ� Spawn�ϴ� ���� �ʿ��ϴ�.
    }

    void ChangePlaneMaterial()
    {
        Renderer SIPRenderer = StageInfoPlane.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[CurrentStage];
        }
    }

    public void ResetTeleport()
    {
        Debug.Log("5�� �� CanTeleport ����");
        CanTeleport = true;
    }

    void EndGame()
    {
        Debug.Log("������ ������ 4�� ����������, ���������͸� Ÿ�� �ö󰡾� �Ѵ�.");
    }
}
