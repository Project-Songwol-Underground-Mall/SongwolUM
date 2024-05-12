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
    // 이상현상 종류가 FixedType이냐, DynamicType이냐에 따라 작동방식에 차이를 줘야 할 듯.
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
        // isNormal이 true일 때에는 모든 스포너가 정상 버전의 오브젝트를 소환한다.
        // isNormal이 false, 즉 이상현상 스테이지일 때는 PhenomenonNumber에 해당하는 스포너만 이상현상 버전의 오브젝트를 스폰한다.


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
