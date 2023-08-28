using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image barraHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizarBarraHP(float vidaAtual, float vidaMax) 
    {
        float fill = vidaAtual / vidaMax;
        barraHP.fillAmount = fill;
    }

}
