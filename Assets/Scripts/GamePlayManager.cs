using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject StageInfoPlane;
    public Material[] StageInfoMaterial = new Material[5];
    public bool CanTeleport = true;

    int CurrentStage = 0; // 현재 구역 번호
    bool IsNormalStage = true; // 스테이지의 정상 및 이상현상 여부
    int AbnormalNumber = -1; // 이상현상 스테이지에서 발생시킬 이상현상 번호
    bool[] IsAbnormalOccured = new bool[20]; // 이상현상 번호에 따른 발생 여부

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("맨 처음 스테이지는 일반 스테이지입니다.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(bool IsFront) // 앞으로 전진 혹은 뒤로 돌아갔을 시 정답 여부에 따른 스테이지 변경
    {
        if ((IsFront && IsNormalStage) || (!IsFront && !IsNormalStage))
        {
            CurrentStage++;
            if (CurrentStage == 4)
            {
                EndGame();
            }
            else
            {
                Debug.Log("정답!");
                GetRandomStage(CurrentStage, true); // 정답을 맞춤
            }
        }

        else
        {
            CurrentStage = 0;
            Debug.Log("오답!");
            GetRandomStage(CurrentStage, false); // 오답
        }
        ChangePlaneMaterial();
    }


    void GetRandomStage(int StageNumber, bool IsCorrectDirection)// 스테이지 변경에 따른 스테이지 및 이상현상 랜덤 추첨
    {


        if (!IsCorrectDirection)
        {
            Debug.Log("이전 움직임이 오답이였으므로 이번 스테이지는 일반 스테이지입니다.");
            AbnormalNumber = -1;
            IsNormalStage = true;
            return;
        }

        int Boundary = 0;
        if (StageNumber == 1) Boundary = 69;
        if (StageNumber == 2) Boundary = 79;
        if (StageNumber == 3) Boundary = 84;
        if (StageNumber == 4) Boundary = 89;

        int Result = Random.Range(0, 100);

        if (Result > Boundary && !IsNormalStage) // 이전 스테이지가 이상현상 스테이지이고 추첨 결과가 일반 스테이지
        {
            Debug.Log("이번 스테이지는 일반 스테이지입니다.");
            AbnormalNumber = -1;
            IsNormalStage = true;
        }

        else if (Result <= Boundary || IsNormalStage) // 이전 스테이지가 일반 스테이지거나 추첨 결과가 이상현상 스테이지
        {
            Debug.Log("이번 스테이지는 이상현상 스테이지입니다.");
            AbnormalNumber = Random.Range(0, 20);
            while (IsAbnormalOccured[AbnormalNumber])
            {
                AbnormalNumber = Random.Range(0, 20);
            }
            IsAbnormalOccured[AbnormalNumber] = true;
            IsNormalStage = false;
        }
        // 여기서 추첨 결과로 나온 번호의 오브젝트의 이상현상 발생 Version을 Spawn해줘야 한다.
        SpawnManager.GetComponent<PhenomenonManagement>().SpawnPhenomenon(AbnormalNumber, IsNormalStage);



        // 이상현상 Version의 오브젝트를 제외한 나머지 오브젝트를 스폰해준다. 스폰해두고 남겨놓는 방법도 고려중.
        // 대신 그러면 이상현상 발생 오브젝트의 이전 버전은 지워주고, 이전에 발생한 이상현상 오브젝트도 지워주고 정상버전으로 다시 Spawn하는 수고가 필요하다.
    }

    void ChangePlaneMaterial()
    {
        Renderer SIPRenderer = StageInfoPlane.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[CurrentStage];
        }
    }

    public void ResetTeleport()
    {
        Debug.Log("5초 후 CanTeleport 리셋");
        CanTeleport = true;
    }

    void EndGame()
    {
        Debug.Log("마지막 구역인 4번 구역에서는, 엘레베이터를 타고 올라가야 한다.");
    }
}
