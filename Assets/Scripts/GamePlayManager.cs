using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject FrontTeleportZone;
    public GameObject BackTeleportZone;

    public bool CanTeleport = true;

    int CurrentStage = 0; // ���� ���� ��ȣ
    int AbnormalNumber = -1; // �̻����� ������������ �߻���ų �̻����� ��ȣ
    bool[] IsAbnormalOccured = new bool[20]; // �̻����� ��ȣ�� ���� �߻� ����
    bool IsNormalStage = true; // ���������� ���� �� �̻����� ����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeStage(bool IsFront) // ������ ���� Ȥ�� �ڷ� ���ư��� �� ���� ���ο� ���� �������� ����
    {
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            CurrentStage++;
            if (CurrentStage == 5)
            {
                EndGame();
            }
            GetRandomStage(CurrentStage, true); // ������ ����
        }

        else
        {
            CurrentStage = 0;
            GetRandomStage(CurrentStage, false); // ����
        }
    }


    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// �������� ���濡 ���� �������� �� �̻����� ���� ��÷
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

        if (Result <= Boundary || IsNormalStage) // ���� ���������� �Ϲ� ���������ų� ��÷ ����� �̻����� ��������
        {
            while (IsAbnormalOccured[AbnormalNumber])
            {
                AbnormalNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalNumber] = true;
            IsNormalStage = false;
            // ���⼭ ��÷ ����� ���� ��ȣ�� ������Ʈ�� �̻����� �߻� Version�� Spawn����� �Ѵ�.

        }

        if (Result > Boundary && !IsNormalStage) // ���� ���������� �̻����� ���������̰� ��÷ ����� �Ϲ� ��������
        {
            AbnormalNumber = -1;
            IsNormalStage = true;
        }

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

    }
}
