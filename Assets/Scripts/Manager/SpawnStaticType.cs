using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStaticType : MonoBehaviour
{
    public void SpawnObject(GameObject[] gameObjectArray, bool isNormal)
    {
        if (isNormal)
        {
            gameObjectArray[0].SetActive(true);
            gameObjectArray[1].SetActive(false);
        }
        else
        {
            gameObjectArray[0].SetActive(false);
            gameObjectArray[1].SetActive(true);
        }
    }
}
