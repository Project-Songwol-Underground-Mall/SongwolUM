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

    // IsFixedType이 true : Spawner를 통한 별도의 작업 불필요, SetActive로 활성화 비활성화만
    // IsFixedType이 false : Spawner를 통한 Spawn 및 별도 로직을 구현해야 한다.
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
        // isNormal이 true일 때에는 모든 스포너가 정상 버전의 오브젝트를 소환한다.
        // isNormal이 false, 즉 이상현상 스테이지일 때는 PhenomenonNumber에 해당하는 스포너만 이상현상 버전의 오브젝트를 스폰한다.

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
