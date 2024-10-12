using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
    private bool IsFixedType;

    PhenomenonArr(GameObject[] phenomenonArray, bool isFixedType)
    {
        PhenomenonArray = phenomenonArray;
        IsFixedType = isFixedType;
    }

    public bool GetIsFixedType()
    { 
        return IsFixedType; 
    }

    public void SetIsFixedType(bool val)
    {
        IsFixedType = val; 
    }

    public GameObject[] GetPhenomenonArray()
    {
        return PhenomenonArray;
    }

    public void SetPhenomenonArray(GameObject[] val)
    { 
        PhenomenonArray = val;
    }

    // IsFixedType�� true : Spawner�� ���� ������ �۾� ���ʿ�, SetActive�� Ȱ��ȭ ��Ȱ��ȭ��
    // IsFixedType�� false : Spawner�� ���� Spawn �� ���� ������ �����ؾ� �Ѵ�.
}

public class PhenomenonManagement : MonoBehaviour
{
    public PhenomenonArr[] PhenomenonSpawner = new PhenomenonArr[12];

    private void Awake()
    {
        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            PhenomenonArr curPhenomenon = PhenomenonSpawner[i];
            if (curPhenomenon.GetPhenomenonArray()[0].GetComponent<SpawnManagerFixedType>() != null)
            {
                curPhenomenon.SetIsFixedType(true);
            }
            else
            {
                curPhenomenon.SetIsFixedType(false);
            }
        }
    }

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
                if (curPhenomenon.GetIsFixedType())
                {
                    curPhenomenon.GetPhenomenonArray()[0].GetComponent<SpawnManagerFixedType>().SpawnObject(curPhenomenon.GetPhenomenonArray(), isNormal);
                }
                else
                {
                    curPhenomenon.GetPhenomenonArray()[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, isNormal);
                }
            }
        }
    }
    public void DestroyPhenomenon()
    {

    }
}
