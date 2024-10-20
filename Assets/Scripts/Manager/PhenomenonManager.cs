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
        // isNormal이 true일 때에는 모든 스포너가 정상 버전의 오브젝트를 소환한다.
        // isNormal이 false, 즉 이상현상 스테이지일 때는
        // PhenomenonNumber에 해당하는 번호의 현상만 이상현상 버전의 로직을 수행한다.
        // FixedType이라면 단순 이상현상 버전의 오브젝트를 활성화한다.
        // DynamicType이라면 해당 번호의 이상현상 로직 수행 함수를 호출한다.

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
