using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefab;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        foreach (GameObject prefab in characterPrefab)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        }
    }
   
}
