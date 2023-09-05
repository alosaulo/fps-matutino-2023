using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txtMunicao;

    public TextMeshProUGUI txtScore;

    public Image barraHP;

    int score = 0;

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

    public void AtualizarTMPMunicao(int valor) 
    { 
        txtMunicao.text = valor.ToString();
    }

    public void AtualizarScore() 
    {
        score++;
        txtScore.text = "Score: " + score.ToString();
    }

}
