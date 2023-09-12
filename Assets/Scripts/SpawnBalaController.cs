using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalaController : MonoBehaviour
{
    public GameObject prefabBala;
    public GameObject balaSpawnada;
    public float spawnTime;
    public float contadorSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorSpawn <= spawnTime && balaSpawnada == null)
        {
            contadorSpawn += Time.deltaTime;
        }
        else if(balaSpawnada == null)
        {
            contadorSpawn = 0;
            balaSpawnada =
                Instantiate(prefabBala, transform.position, Quaternion.identity, transform);
        }
    }
}
