using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<Transform> listaSpawns;

    public List<GameObject> listaGameObjects;

    public int qtdSpawn;

    public bool spawn;

    public float SpawnTime;

    public GameObject SpawnPrefab;

    float spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TempoSpawn();
        if (spawn == true && 
            listaGameObjects.Count < qtdSpawn) 
        {
            Spawnar();
        }
    }

    void TempoSpawn() 
    {
        if (spawnCounter <= SpawnTime)
        {
            spawnCounter += Time.deltaTime;
        }
        else
        {
            spawnCounter = 0;
            spawn = true;
        }
    }

    void Spawnar() 
    {
        spawn = false;

        int random = Random.Range(0, listaSpawns.Count);

        GameObject go = Instantiate(SpawnPrefab, listaSpawns[random].position, 
            Quaternion.identity);

        listaGameObjects.Add(go);
    }

    public void MeRemove(GameObject go) 
    { 
        listaGameObjects.Remove(go);
    }
}
