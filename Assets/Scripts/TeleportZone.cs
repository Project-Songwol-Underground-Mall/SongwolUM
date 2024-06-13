using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{

    public GameObject Player;
    public GameObject TutorialGuide1;
    public GameObject TutorialGuide2;
    public Transform Destination;
    public bool IsFront;
    public float CoolDown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TutorialGuide1 != null) 
        {
            Destroy(TutorialGuide1);
        }

        if (TutorialGuide2 != null)
        {
            Destroy(TutorialGuide2);
        }


        if (other.CompareTag("Player") && CheckCanTeleport())
        {
            Vector3 PlayerPosition = other.transform.position;
            Vector3 DestinationPosition = Destination.position;
            Vector3 Normal = new Vector3(transform.position.x - PlayerPosition.x,
                0f,
                transform.position.z - PlayerPosition.z);
            if (IsFront)
            {
                other.transform.position = DestinationPosition - Normal;
            }
            else
            {
                other.transform.position = new Vector3(DestinationPosition.x + Normal.x - 0.2f, DestinationPosition.y + Normal.y,
                    DestinationPosition.z + Normal.z);
                Vector3 NewRotation = other.transform.eulerAngles;
                NewRotation.y -= 180f;
                other.transform.eulerAngles = NewRotation;
            }
            // GPM의 ChangeStage 호출, 부딪힌 텔레포트존의 앞뒤 여부와 현재 스테이지 이상현상 유무에 따른 처리 진행
            Player.GetComponent<GamePlayManager>().ChangeStage(IsFront); 
            Invoke("ResetTeleport", CoolDown);
        }
        else Debug.Log("아직 텔레포트 불가");
    }

    private bool CheckCanTeleport() 
    {

        GamePlayManager GPM = Player.GetComponent<GamePlayManager>();
        if (GPM != null && GPM.CanTeleport)
        {
            GPM.CanTeleport = false;
            return true;
        }
        return false;
    }

    private void ResetTeleport()
    {
        GamePlayManager GPM = Player.GetComponent<GamePlayManager>();
        if (GPM != null)
        {
            GPM.ResetTeleport();
        }
    }
}
