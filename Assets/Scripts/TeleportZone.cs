using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{

    public GameObject Player;
    public Rigidbody RB;
    public Transform Destinaton;
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
        Debug.Log("�浹 ����!");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && CheckCanTeleport())
        {

            if (IsFront)
            {
                other.transform.position = new Vector3(Destinaton.position.x, other.transform.position.y, Destinaton.position.z);
                Debug.Log("�����Ͽ� �ڷ���Ʈ!");
            }
            else
            {
                other.transform.position = new Vector3(Destinaton.position.x, other.transform.position.y, Destinaton.position.z);
                Vector3 NewRotation = other.transform.eulerAngles;
                NewRotation.y -= 180f;
                other.transform.eulerAngles = NewRotation;
                Debug.Log("�����Ͽ� �ڷ���Ʈ");
                
            }
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
