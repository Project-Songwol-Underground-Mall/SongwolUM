using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCeilingMaterial : MonoBehaviour
{
    public GameObject DTPM;
    bool isChanged = false;

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
        if (other.CompareTag("Player") && !isChanged)
        {
            DTPM.GetComponent<SpawnManagerDynamicType>().ChangeCeilingToEyeBall();
            isChanged = true;
        }
        else Debug.Log("ºÎµúÈù ´ë»óÀÌ ÇÃ·¹ÀÌ¾î°¡ ¾Æ´Ô");
    }
}
