using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFixedType : MonoBehaviour
{
    public void SpawnObject(GameObject[] phenomenonArr, bool isNormal)
    {
        if (isNormal)
        {
            phenomenonArr[1].SetActive(true);
            phenomenonArr[2].SetActive(false);
        }
        else
        {
            phenomenonArr[1].SetActive(false);
            phenomenonArr[2].SetActive(true);
        }
    }
}
