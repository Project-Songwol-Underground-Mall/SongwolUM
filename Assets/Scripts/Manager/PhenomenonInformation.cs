using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhenomenonArray
{
    public GameObject[] gameObjectArray;
    private bool isStaticype;

    public PhenomenonArray() {}
    public PhenomenonArray(GameObject[] phenomenonArray, bool isFixedType)
    {
        gameObjectArray = phenomenonArray;
        isStaticype = isFixedType;
    }

    public bool GetIsStaticType() 
    { 
        return isStaticype; 
    }
    public void SetIsStaticType(bool val) 
    { 
        isStaticype = val; 
    }
    public GameObject[] GetPhenomenonArray() 
    { 
        return gameObjectArray; 
    }
    public void SetPhenomenonArray(GameObject[] val) 
    { 
        gameObjectArray = val; 
    }
}



public class PhenomenonInformation : MonoBehaviour
{
    [SerializeField]
    public List<PhenomenonArray> phenomenonArray;
    public void Init()
    {
        phenomenonArray = new List<PhenomenonArray>(GameManager.Instance.GetNumOfPhenomenons());
    }
}
