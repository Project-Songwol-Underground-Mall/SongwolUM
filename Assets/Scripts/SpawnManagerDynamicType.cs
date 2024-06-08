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
    public GameObject BlackoutSystem;
    public GameObject SirenSystem;
    public GameObject Floor;
    public GameObject Ceiling;
    public GameObject CeilingChangePoint;
    public GameObject GhostSound;
    public GameObject CeilSound;
    public Material[] CeilingMaterial = new Material[2];

    private AudioSource ghostAudioSource;
    private AudioSource ceilAudioSource;

    private int phenomenonNumber = -1;
    private bool IsCoroutineRunning = false;
    private Coroutine CurrentFloorCoroutine = null;

    void Start()
    {
        ghostAudioSource = GhostSound.GetComponent<AudioSource>();
        ceilAudioSource = CeilSound.GetComponent<AudioSource>();
    }

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
        BlackoutController blackoutController = BlackoutSystem.GetComponent<BlackoutController>();
        if (blackoutController != null)
        {
            if (isNormal)
            {
                BlackoutSystem.SetActive(false);
                blackoutController.ResetLights();
            }
            else
            {
                BlackoutSystem.SetActive(true);
                blackoutController.StartBlackoutEffect();
            }
        }
        else
        {
            Debug.LogError("lightBlackout script not found on LightSystem object.");
        }
    }

    // ������ �Ӱ� ���ϰ� ���̷� �Ҹ��� �鸮�� �̻�����
    public void Emergency(bool isNormal)
    {
        SirenController sirenController = SirenSystem.GetComponent<SirenController>();
        if (sirenController != null)
        {
            if (isNormal)
            {
                SirenSystem.SetActive(false);
                sirenController.ResetLights();
            }
            else
            {
                SirenSystem.SetActive(true);
                sirenController.StartSirenEffect();
            }
        }
        else
        {
            Debug.LogError("sirenController script not found on LightSystem object.");
        }
    }

    // �ٴ��� �ö���� �̻�����
    public void ComeUpFloor(bool isNormal)
    {
        if (CurrentFloorCoroutine != null)
        {
            StopCoroutine(CurrentFloorCoroutine);
        }

        if (!isNormal)
        {
            CurrentFloorCoroutine = StartCoroutine(MoveFloorUp());
        }
        else
        {
            CurrentFloorCoroutine = StartCoroutine(MoveFloorDown());
        }
    }
    // �������� �ͽ� ���� ��� �̻�����
    public void PlayGhostSound(bool isNormal)
    {
        
        if (isNormal)
        {
            
            if (ghostAudioSource != null && ghostAudioSource.isPlaying)
            {
                ghostAudioSource.Stop();
            }
        }
        else
        {
            if (ghostAudioSource != null && !ghostAudioSource.isPlaying)
            {
                StartCoroutine(PlayGhostSoundAfterDelay(5.0f));
            }
        }
    }

    // õ�� ������ ���� ������ �ٲ�� �̻�����
    public void EyeBallCeiling(bool isNormal)
    {
        Renderer[] CeilingRenderer = Ceiling.GetComponentsInChildren<Renderer>();

        if (isNormal)
        {
            CeilingChangePoint.SetActive(false);
            foreach (Renderer renderer in CeilingRenderer)
            {
                renderer.material = CeilingMaterial[0];
            }

            if (ceilAudioSource != null && ceilAudioSource.isPlaying)
            {
                ceilAudioSource.Stop();
            }
        }
        else
        {
            CeilingChangePoint.SetActive(true);
        }
    }

    public void ChangeCeilingToEyeBall()
    {
        Renderer[] CeilingRenderer = Ceiling.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in CeilingRenderer)
        {
            renderer.material = CeilingMaterial[1];
        }
     
        if (ceilAudioSource != null)
        {
            ceilAudioSource.Play();
        }
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

    IEnumerator PlayGhostSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ghostAudioSource != null)
        {
            ghostAudioSource.Play();
        }
    }
}
