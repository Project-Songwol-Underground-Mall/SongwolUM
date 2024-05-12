using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
}

public class PhenomenonManagement : MonoBehaviour
{
    // �̻����� ������ FixedType�̳�, DynamicType�̳Ŀ� ���� �۵���Ŀ� ���̸� ��� �� ��.
    public PhenomenonArr[] PhenomenonSpawner = new PhenomenonArr[20];
    public bool IsNormal = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPhenomenon(int PhenomenonNumber, bool isNormal)
    {
        // isNormal�� true�� ������ ��� �����ʰ� ���� ������ ������Ʈ�� ��ȯ�Ѵ�.
        // isNormal�� false, �� �̻����� ���������� ���� PhenomenonNumber�� �ش��ϴ� �����ʸ� �̻����� ������ ������Ʈ�� �����Ѵ�.


        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            if (i == PhenomenonNumber && !isNormal) Spawn(i, false);
            else Spawn(i, true);
        }

    }

    public void Spawn(int PhenomenonNumber, bool isNormal)
    {
        if (isNormal)
        {
            Instantiate(PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[1],
                PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[0].transform.position,
                PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[0].transform.rotation);
        }
        else
        {
            Instantiate(PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[2],
                PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[0].transform.position,
                PhenomenonSpawner[PhenomenonNumber].PhenomenonArray[0].transform.rotation);
        }

    }
}
