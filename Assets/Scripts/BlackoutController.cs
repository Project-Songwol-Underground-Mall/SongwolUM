using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutController : MonoBehaviour
{
    public GameObject Player;

    public GameObject SMDT;
    public GameObject[] squareLights = new GameObject[12];

    private int phenomenonNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        SpawnManagerDynamicType spawnManagerScript = SMDT.GetComponent<SpawnManagerDynamicType>();
        phenomenonNumber = spawnManagerScript.GetPhenomenonNumber();
        Debug.Log("Phenomenon Number: " + phenomenonNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBlackoutEffect()
    {
        foreach (GameObject lightObject in squareLights)
        {
            LightBlackout lightBlackout = lightObject.GetComponent<LightBlackout>();
            if (lightBlackout != null) // TODO: Player 위치에 맞게 실행
            {
                lightBlackout.StartBlackout();
            }
        }

    }

    public void ResetLights()
    {
        foreach (GameObject lightObject in squareLights)
        {
            LightBlackout lightBlackout = lightObject.GetComponent<LightBlackout>();

            if (lightBlackout != null)
            {
                lightBlackout.ResetLight();
            }
        }
    }
}
