using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseAndWin : MonoBehaviour
{
    public GameObject MainUi;
    public TMP_Text Score;
    public TMP_Text Combo;
    public GameObject Player;
    private Scoremanager scoremanager;
    private PlayerController playerController;

    void Start()
    {
        scoremanager = Player.GetComponent<Scoremanager>(); // Invoke GetComponent method
        playerController = Player.GetComponent<PlayerController>();
        Score.text = scoremanager.score.ToString(); 
        Combo.text = playerController.BestCombo.ToString();
        MainUi.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnRedo()
    {
        SceneManager.LoadScene("Menupr");
    }
}
