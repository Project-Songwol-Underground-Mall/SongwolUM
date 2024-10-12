using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
    public bool IsFixedType;
    // IsFixedType�� true : Spawner�� ���� ������ �۾� ���ʿ�, SetActive�� Ȱ��ȭ ��Ȱ��ȭ��
    // IsFixedType�� false : Spawner�� ���� Spawn �� ���� ������ �����ؾ� �Ѵ�.
}

public class PhenomenonManagement : MonoBehaviour
{
    public PhenomenonArr[] PhenomenonSpawner = new PhenomenonArr[12];
    public bool IsNormal = true;


    // Start is called before the first frame update
    void Start()
    {
        SetPhenomenon(0, true);
    }

    public void SetPhenomenon(int abnormalPhenomenonNumber, bool isNormal)
    {
        // isNormal�� true�� ������ ��� �����ʰ� ���� ������ ������Ʈ�� ��ȯ�Ѵ�.
        // isNormal�� false, �� �̻����� ���������� ���� PhenomenonNumber�� �ش��ϴ� �����ʸ� �̻����� ������ ������Ʈ�� �����Ѵ�.

        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            PhenomenonArr curPhenomenon = PhenomenonSpawner[i];

            if (i == abnormalPhenomenonNumber)
            {
                if (curPhenomenon.IsFixedType)
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerFixedType>().SpawnObject(curPhenomenon.PhenomenonArray, isNormal);
                }
                else
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, isNormal);
                }
            }
        }
    }
    public void DestroyPhenomenon()
    {

    }
}
