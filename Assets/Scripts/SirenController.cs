using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenController : MonoBehaviour
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

    public void StartSirenEffect()
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

    public void ResetLights()
    {
        foreach (GameObject lightObject in squareLights)
        {
            LightSiren lightSiren = lightObject.GetComponent<LightSiren>();

            if (lightSiren != null)
            {
                lightSiren.ResetLight();
            }
        }
    }
}
