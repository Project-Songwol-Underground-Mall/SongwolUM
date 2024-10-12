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
    public PhenomenonArr[] PhenomenonSpawner = new PhenomenonArr[12];
    public bool IsNormal = true;


    // Start is called before the first frame update
    void Start()
    {
        SetPhenomenon(0, true);
    }

    public void SetPhenomenon(int abnormalPhenomenonNumber, bool isNormal)
    {
        // isNormal이 true일 때에는 모든 스포너가 정상 버전의 오브젝트를 소환한다.
        // isNormal이 false, 즉 이상현상 스테이지일 때는 PhenomenonNumber에 해당하는 스포너만 이상현상 버전의 오브젝트를 스폰한다.

        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            PhenomenonArr curPhenomenon = PhenomenonSpawner[i];
            if (i == abnormalPhenomenonNumber && !isNormal)
            {
                if (curPhenomenon.IsFixedType)
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerFixedType>().SpawnObject(curPhenomenon.PhenomenonArray, true);
                }
                else
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, false);
                }
            }
            else
            {
                if (curPhenomenon.IsFixedType)
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerFixedType>().SpawnObject(curPhenomenon.PhenomenonArray, false);
                }
                else
                {
                    curPhenomenon.PhenomenonArray[0].GetComponent<SpawnManagerDynamicType>().ActivateAP(i, true);
                }
            }
        }
    }
    public void DestroyPhenomenon()
    {

    }
}
