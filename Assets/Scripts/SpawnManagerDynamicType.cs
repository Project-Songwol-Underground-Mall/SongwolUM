using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerDynamicType : MonoBehaviour
{
    public GameObject Mannequin;
    public GameObject SuitMan;
    public GameObject Zombie;
    public GameObject LightSystem;
    public GameObject Floor;
    public AudioClip SirenSound;
    public AudioClip GhostSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateAP(int PhenomenonNumber, bool isNormal)
    {
        if (PhenomenonNumber == 6) AnimateMannequin();
        if (PhenomenonNumber == 7) MoveSuitMan();
        if (PhenomenonNumber == 8) AnimateMannequin();
        if (PhenomenonNumber == 9) 
        if (PhenomenonNumber == 10)
        if (PhenomenonNumber == 11)
        if (PhenomenonNumber == 12)
        if (PhenomenonNumber == 13)
    }

    public void AnimateMannequin(bool isNormal)
    {
        ActiveteMannequin AM = Manenequin.GetComponent<ActivateMannequin>();
        if (AM != null)
        {
            if (!isNormal) AM.enabled = true;
            else AM.enabled = false;
        }
    }

    public void MoveSuitMan(bool isNormal)
    {

    }

    public void SpawnAndMoveZombie(bool isNormal)
    {

    }

    public void TurnOffLight(bool isNormal)
    {

    }

    public void Emergency(bool isNormal)
    {

    }

    public void ComeUpFloor(bool isNormal)
    {

    }

    public void PlayGhostSound(bool isNormal)
    {

    }
}
