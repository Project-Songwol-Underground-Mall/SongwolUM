using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject Player;
    public GameObject Zombie;
    public float ActivationDistance = 90f; // 스폰 범위
    bool isZombieSpawned = false;

    private void OnEnable()
    {
        isZombieSpawned = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if (Distance <= ActivationDistance && !isZombieSpawned)
        {
            Quaternion Rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(Zombie, transform.position, Rotation);
            isZombieSpawned = true;
        }
        else
        {

        }
    }
}
