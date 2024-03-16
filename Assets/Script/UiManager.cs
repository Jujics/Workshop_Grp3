using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text SpeedTxt;
    public TMP_Text ComboTxt;
    public TMP_Text TimeTxt;
    public TMP_Text PourcentTxt;
    public GameObject Player;
    private PlayerController Playerc;
    // Start is called before the first frame update
    void Start()
    {
        Playerc = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        string MvtDisplay = Playerc.movementSpeed.ToString("0.00");
        SpeedTxt.text = MvtDisplay ;
        ComboTxt.text = "x"+Playerc.Combo;
    }
}
