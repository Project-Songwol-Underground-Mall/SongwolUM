using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhenomenonManager : MonoBehaviour
{
    private SpawnStaticType spawnFixedTypeInstance;
    private SpawnDynamicType spawnDynamicTypeInstance;
    private PhenomenonInformation phenomenonInformationInstance;

    private void Awake()
    {
        spawnFixedTypeInstance = GetComponent<SpawnStaticType>();
        spawnDynamicTypeInstance = GetComponent<SpawnDynamicType>();
        phenomenonInformationInstance = GetComponent<PhenomenonInformation>();
        for (int i = 0; i < phenomenonInformationInstance.phenomenonArray.Count; i++)
        {
            PhenomenonArray currentPhenomenon = phenomenonInformationInstance.phenomenonArray[i];
            if (currentPhenomenon.gameObjectArray.Length == 2)
            {
                currentPhenomenon.SetIsStaticType(true);
            }
            else
            {
                currentPhenomenon.SetIsStaticType(false);
            }
        }
    }

    void Start()
    {
        SetPhenomenon(-1, true);
    }

    public void SetPhenomenon(int abnormalPhenomenonNumber, bool isNormal)
    {
        // isNormal�� true�� ������ ��� �����ʰ� ���� ������ ������Ʈ�� ��ȯ�Ѵ�.
        // isNormal�� false, �� �̻����� ���������� ����
        // PhenomenonNumber�� �ش��ϴ� ��ȣ�� ���� �̻����� ������ ������ �����Ѵ�.
        // FixedType�̶�� �ܼ� �̻����� ������ ������Ʈ�� Ȱ��ȭ�Ѵ�.
        // DynamicType�̶�� �ش� ��ȣ�� �̻����� ���� ���� �Լ��� ȣ���Ѵ�.

        for (int i = 0; i < phenomenonInformationInstance.phenomenonArray.Count; i++)
        {
            PhenomenonArray currentPhenomenon = phenomenonInformationInstance.phenomenonArray[i];
            if (currentPhenomenon.GetIsStaticType())
            {
                if (i == abnormalPhenomenonNumber)
                {
                    spawnFixedTypeInstance.SpawnObject(currentPhenomenon.GetPhenomenonArray(), false);
                }
                else
                {
                    spawnFixedTypeInstance.SpawnObject(currentPhenomenon.GetPhenomenonArray(), true);
                }
            }
            else
            {
                if (i == abnormalPhenomenonNumber)
                {
                    spawnDynamicTypeInstance.ActivateAbnormalPhenomenon(i, false);
                }
                else
                {
                    spawnDynamicTypeInstance.ActivateAbnormalPhenomenon(i, true);
                }
            }
        }
    }
}
