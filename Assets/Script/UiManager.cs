using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TMP_Text SpeedTxt;
    public TMP_Text ComboTxt;
    public TMP_Text ScoreTxt;
    public GameObject Player;
    public TMP_Text timeText;
    public TMP_Text VPN;
    public Image ElecBar;
    private float startTime;
    private bool isRunning;
    private PlayerController Playerc;
    private Scoremanager PlayerSco;
    private Powerupmanager PW;
    // Start is called before the first frame update
    void Start()
    {
        Playerc = Player.GetComponent<PlayerController>();
        PlayerSco = Player.GetComponent<Scoremanager>();
        PW = Player.GetComponent<Powerupmanager>();
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float outputValue = (float)(Playerc.elec/ 100.000000);
        ElecBar.fillAmount = outputValue;
        string MvtDisplay = Playerc.movementSpeed.ToString("0.00");
        SpeedTxt.text = MvtDisplay ;
        ComboTxt.text = "x"+Playerc.Combo;
        string ScoreDis = PlayerSco.score.ToString();
        ScoreTxt.text = ScoreDis;
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            int milliseconds = (int)((elapsedTime * 1000) % 1000);
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
        if(PW.isvpn)
        {
            VPN.text = "VPN = TRUE";
        }
        else if(!PW.isvpn)
        {
            VPN.text = "VPN = FALSE";
        }
    }
    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        startTime = 0f;
        isRunning = false;
        timeText.text = "00:00:000";
    }
}
