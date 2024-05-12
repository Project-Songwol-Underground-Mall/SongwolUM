using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{

    public GameObject Player;
    public Rigidbody RB;
    public Transform Destination;
    public Transform PlayerTransform;
    public bool IsFront;
    public float CoolDown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 감지!");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 지점 좌표: " + other.transform.position);
        // Debug.Log("텔레포트 영역 중심 기준으로 얼마나 떨어져 있는가 : " + transform.position - other.transform.position);
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
                Debug.Log("전진하여 텔레포트!");
            }
            else
            {
                other.transform.position = DestinationPosition + Normal;
                Vector3 NewRotation = other.transform.eulerAngles;
                NewRotation.y -= 180f;
                other.transform.eulerAngles = NewRotation;
                Debug.Log("후진하여 텔레포트");
                
            }
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
