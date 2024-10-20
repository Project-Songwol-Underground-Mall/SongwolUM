using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArr
{
    public GameObject[] PhenomenonArray = new GameObject[3];
    // 스포너 오브젝트, 정상 오브젝트, 이상현상 오브젝트
    private bool IsFixedType;

    PhenomenonArr(GameObject[] phenomenonArray, bool isFixedType)
    {
        PhenomenonArray = phenomenonArray;
        IsFixedType = isFixedType;
    }

    public bool GetIsFixedType(){ return IsFixedType; }
    public void SetIsFixedType(bool val){ IsFixedType = val; }
    public GameObject[] GetPhenomenonArray(){ return PhenomenonArray; }
    public void SetPhenomenonArray(GameObject[] val){ PhenomenonArray = val; }
}

public class PhenomenonManager : MonoBehaviour
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
        // isNormal이 false, 즉 이상현상 스테이지일 때는 PhenomenonNumber에 해당하는 번호의 현상만 이상현상 버전의 로직을 수행한다.
        // FixedType이라면 단순 이상현상 버전의 오브젝트를 활성화한다.
        // DynamicType이라면 해당 번호의 이상현상 로직 수행 함수를 호출한다.

        for (int i = 0; i < PhenomenonSpawner.Length; i++)
        {
            PhenomenonArr curPhenomenon = PhenomenonSpawner[i];
            if (i == abnormalPhenomenonNumber)
            {
                if (curPhenomenon.GetIsFixedType())
                {
                    curPhenomenon.GetPhenomenonArray()[0].
                        GetComponent<SpawnManagerFixedType>().SpawnObject(curPhenomenon.GetPhenomenonArray(), isNormal);
                }
                else
                {
                    curPhenomenon.GetPhenomenonArray()[0].
                        GetComponent<SpawnManagerDynamicType>().ActivateAP(i, isNormal);
                }
            }
        }
    }




    public void DestroyPhenomenon()
    {

    }
}
