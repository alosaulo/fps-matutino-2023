using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txtMunicao;

    public TextMeshProUGUI txtScore;

    public TextMeshProUGUI txtScoreGameOver;

    public GameObject pnlGameOver;

    public Image barraHP;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Pausar();
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
        txtScoreGameOver.text = "Score: " + score.ToString();
    }

    public void Pausar()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver() 
    {
        Pausar();
        pnlGameOver.SetActive(true);
    }

}
