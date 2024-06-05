using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerDynamicType : MonoBehaviour
{
    public GameObject Player;
    public GameObject Mannequin;
    public GameObject SuitMan;
    public GameObject CleaningPanel;
    public GameObject ZombieSpawner;
    public GameObject Zombie;
    public GameObject LightSystem;
    public GameObject Floor;
    public AudioClip SirenSound;
    public AudioClip GhostSound;

    private int phenomenonNumber = -1;
    private bool IsCoroutineRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateAP(int PhenomenonNumber, bool isNormal)
    {
        phenomenonNumber = PhenomenonNumber;
        if (PhenomenonNumber == 4) AnimateMannequin(isNormal);
        if (PhenomenonNumber == 5) MoveSuitMan(isNormal);
        if (PhenomenonNumber == 6) SpawnZombie(isNormal);
        if (PhenomenonNumber == 7) TurnOffLight(isNormal);
        if (PhenomenonNumber == 8) Emergency(isNormal);
        if (PhenomenonNumber == 9) ComeUpFloor(isNormal);
        if (PhenomenonNumber == 10) PlayGhostSound(isNormal);
        if (PhenomenonNumber == 11) EyeBallCeiling(isNormal);
    }

    // �����̴� ����ŷ �̻�����
    public void AnimateMannequin(bool isNormal)
    {
        ActivateMannequin AM = Mannequin.GetComponent<ActivateMannequin>();
        // ����ŷ�� �޷� �ִ� ActivateMannequin ��ũ��Ʈ�� Ȱ��ȭ
        if (AM != null)
        {
            if (!isNormal) AM.enabled = true;
            else AM.enabled = false;
        }
    }

    // ���峲�� �̻�����
    public void MoveSuitMan(bool isNormal)
    {
        if (isNormal)
        {
            SuitMan.SetActive(false);
        }
        else
        {
            SuitMan.SetActive(true);
        }
    }

    // �÷��̾ ȭ��� �տ� �������� �� ���� �����Ǵ� �̻�����
    public void SpawnZombie(bool isNormal)
    {
        if (isNormal)
        {
            ZombieSpawner.SetActive(false);
            CleaningPanel.SetActive(true);
        }
        else
        {
            ZombieSpawner.SetActive(true);
            CleaningPanel.SetActive(false);
        }
    }

    // ������ �����ٰ� ������ �̻�����
    public void TurnOffLight(bool isNormal)
    {
        if (isNormal)
        {
            LightSystem.SetActive(true);
        }
        else
        {
            LightSystem.SetActive(false);
        }
    }

    // ������ �Ӱ� ���ϰ� ���̷� �Ҹ��� �鸮�� �̻�����
    public void Emergency(bool isNormal)
    {
        if (isNormal)
        {
            LightSystem.SetActive(true);

        }
        else
        {
            LightSystem.SetActive(false);
        }
    }

    // �ٴ��� �ö���� �̻�����
    public void ComeUpFloor(bool isNormal)
    {
        if (!isNormal)
        {
            if (!IsCoroutineRunning)
            {
                StartCoroutine(MoveFloorUp());
            }
        }
        else
        {
            if (!IsCoroutineRunning)
            {
                StartCoroutine(MoveFloorDown());
            }
        }
    }
    // �������� �ͽ� ���� ��� �̻�����
    public void PlayGhostSound(bool isNormal)
    {
        if (isNormal)
        {

        }
        else
        {

        }
    }

    public void EyeBallCeiling(bool isNormal)
    {

    }

    public int GetPhenomenonNumber()
    {
        return phenomenonNumber;
    }

    public void Init()
    {

    }


    IEnumerator MoveFloorUp()
    {
        IsCoroutineRunning = true;

        while (Floor.transform.position.y < 2)
        {
            Vector3 NewPosition = Floor.transform.position + Vector3.up * Time.deltaTime;
            Floor.transform.position = NewPosition;

            Vector3 PlayerNewPosition = new Vector3(Player.transform.position.x, Floor.transform.position.y + 7, Player.transform.position.z);
            Player.transform.position = PlayerNewPosition;

            yield return null;
        }

        IsCoroutineRunning = false;
    }

    IEnumerator MoveFloorDown()
    {
        IsCoroutineRunning = true;

        while (Floor.transform.position.y > -5)
        {
            Vector3 NewPosition = Floor.transform.position + Vector3.down * Time.deltaTime;
            Floor.transform.position = NewPosition;

            Vector3 PlayerNewPosition = new Vector3(Player.transform.position.x, Floor.transform.position.y + 7, Player.transform.position.z);
            Player.transform.position = PlayerNewPosition;

            yield return null;
        }

        IsCoroutineRunning = false;
    }
}
