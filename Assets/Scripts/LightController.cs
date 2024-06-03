using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject Player;

    public GameObject spawnManager;
    public GameObject[] squareLights = new GameObject[12];

    private int phenomenonNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        SpawnManagerDynamicType spawnManagerScript = spawnManager.GetComponent<SpawnManagerDynamicType>();
        phenomenonNumber = spawnManagerScript.GetPhenomenonNumber();
        Debug.Log("Phenomenon Number: " + phenomenonNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (phenomenonNumber == 9)
        {
            // ���� ȿ��
            StartBlackoutEffect();
        }
        if (phenomenonNumber == 10)
        {   
            // ���̷� ȿ��
            StartSirenEffect();
        }
    }

    private void StartBlackoutEffect()
    {
        foreach (GameObject lightObject in squareLights)
        {
            LightBlackout lightBlackout = lightObject.GetComponent<LightBlackout>();
            if (lightBlackout != null) // TODO: Player ��ġ�� �°� ����
            {
                lightBlackout.StartBlackout();
            }
        }
    }

    private void StartSirenEffect()
    {
        foreach (GameObject lightObject in squareLights)
        {
            LightSiren lightSiren = lightObject.GetComponent<LightSiren>();
            if (lightSiren != null)
            {
                lightSiren.ChangeToRedAndPulse();
            }
        }
    }
}
