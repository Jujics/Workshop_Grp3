using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    public int score = 100000;
    public GameObject loseui;
    private PlayerController PL;
    private Powerupmanager PW;
    private void Start()
    {
        Application.targetFrameRate = 60;
        PL = GetComponent<PlayerController>();
        PW = GetComponent<Powerupmanager>();
    }

    private void Update()
    {
        if (score <= -1)
        {
            loseui.SetActive(true);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("dmgin"))
        {
            if(PW.isvpn == true)
            {
                PW.isvpn = false;
            }
            else
            {
                score -= 100;
                PL.isslowingout = true;
            }
            
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            score += 100;
            PL.isboosingout = true;
        }
        else if (other.gameObject.CompareTag("froll") && other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            PL.isslowingout = true;
        }
        else if (other.gameObject.CompareTag("smscoreobj"))
        {
            score += 20;
            other.gameObject.SetActive(false);
            PL.HasCombo = true;
        }
        else if (other.gameObject.CompareTag("bgscoreobj"))
        {
            score += 100;
            other.gameObject.SetActive(false);
            PL.HasCombo = true;
        }
    }
}
