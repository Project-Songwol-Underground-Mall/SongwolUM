using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
    public bool IsFixedType;
    // IsFixedType이 true : Spawner를 통한 별도의 작업 불필요, SetActive로 활성화 비활성화만
    // IsFixedType이 false : Spawner를 통한 Spawn, 별도의 스크립트 작업 필요
}

public class PhenomenonManagement : MonoBehaviour
{
    // 이상현상 종류가 FixedType이냐, DynamicType이냐에 따라 작동방식에 차이를 줘야 할 듯.
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
        // isNormal이 true일 때에는 모든 스포너가 정상 버전의 오브젝트를 소환한다.
        // isNormal이 false, 즉 이상현상 스테이지일 때는 PhenomenonNumber에 해당하는 스포너만 이상현상 버전의 오브젝트를 스폰한다.


        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            // 스폰은 세현이가 인스펙터에 오브젝트 다 넣으면 주석 풀기
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
