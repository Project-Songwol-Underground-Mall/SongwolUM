using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
    public bool IsFixedType;
    // IsFixedType�� true : Spawner�� ���� ������ �۾� ���ʿ�, SetActive�� Ȱ��ȭ ��Ȱ��ȭ��
    // IsFixedType�� false : Spawner�� ���� Spawn, ������ ��ũ��Ʈ �۾� �ʿ�
}

public class PhenomenonManagement : MonoBehaviour
{
    // �̻����� ������ FixedType�̳�, DynamicType�̳Ŀ� ���� �۵���Ŀ� ���̸� ��� �� ��.
    public PhenomenonArr[] PhenomenonSpawner = new PhenomenonArr[20];
    public bool IsNormal = true;


    // Start is called before the first frame update
    void Start()
    {
        SetPhenomenon(0, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyPhenomenon()
    {
        
    }

    public void SetPhenomenon(int PhenomenonNumber, bool isNormal)
    {
        // isNormal�� true�� ������ ��� �����ʰ� ���� ������ ������Ʈ�� ��ȯ�Ѵ�.
        // isNormal�� false, �� �̻����� ���������� ���� PhenomenonNumber�� �ش��ϴ� �����ʸ� �̻����� ������ ������Ʈ�� �����Ѵ�.


        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            // ������ �����̰� �ν����Ϳ� ������Ʈ �� ������ �ּ� Ǯ��
            if (i == PhenomenonNumber && !isNormal)
            {
                if (PhenomenonSpawner[i].IsFixedType) Spawn(i, false);
                else PhenomenonSpawner[i].PhenomenonArray[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, false);
            }
            else
            {
                if (PhenomenonSpawner[i].IsFixedType) Spawn(i, true);
                else PhenomenonSpawner[i].PhenomenonArray[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, true);
            }
        }

    }


    public void Spawn(int PhenomenonNumber, bool isNormal)
    {
        if (isNormal)
        {
            PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[1].SetActive(true);
            PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[2].SetActive(false);

        }
        else
        {
            PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[1].SetActive(false);
            PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[2].SetActive(true);
        }

    }
}
