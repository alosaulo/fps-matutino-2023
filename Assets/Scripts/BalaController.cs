using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public GameObject prefabColeta;

    public int quantidade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }

    public void Coletar() 
    { 
        GameObject efeitos = 
            Instantiate(prefabColeta,transform.position,Quaternion.identity);
        Destroy(efeitos, 3);
        Destroy(gameObject);
    }

}
