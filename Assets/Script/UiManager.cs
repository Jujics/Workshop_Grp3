using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text SpeedTxt;
    public TMP_Text ComboTxt;
    public TMP_Text TimeTxt;
    public TMP_Text ScoreTxt;
    public GameObject Player;
    private PlayerController Playerc;
    private Scoremanager PlayerSco;
    // Start is called before the first frame update
    void Start()
    {
        Playerc = Player.GetComponent<PlayerController>();
        PlayerSco = Player.GetComponent<Scoremanager>();
    }

    // Update is called once per frame
    void Update()
    {
        string MvtDisplay = Playerc.movementSpeed.ToString("0.00");
        SpeedTxt.text = MvtDisplay ;
        ComboTxt.text = "x"+Playerc.Combo;
        string ScoreDis = PlayerSco.score.ToString();
        ScoreTxt.text = ScoreDis;
    }
}
