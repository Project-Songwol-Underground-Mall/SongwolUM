using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{

    public GameObject Player;
    public GameObject GuideBoard;
    public Transform Destination;
    public bool IsFront;
    public float CoolDown = 3f;

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
        if (GuideBoard != null) 
        {
            Destroy(GuideBoard);
        }

        Debug.Log("�浹�� ������Ʈ �̸� : " + other.name);
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
                other.transform.position = DestinationPosition + Normal;
                Vector3 NewRotation = other.transform.eulerAngles;
                NewRotation.y -= 180f;
                other.transform.eulerAngles = NewRotation;
            }
            // GPM�� ChangeStage ȣ��, �ε��� �ڷ���Ʈ���� �յ� ���ο� ���� �������� �̻����� ������ ���� ó�� ����
            Player.GetComponent<GamePlayManager>().ChangeStage(IsFront); 
            Invoke("ResetTeleport", CoolDown);
        }
        else Debug.Log("���� �ڷ���Ʈ �Ұ�");
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
