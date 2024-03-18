using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Scoremanager : MonoBehaviour
{
    public int score = 100000;
    public int OldScore = 100000;
    public GameObject loseui;
    public GameObject recupere; 
    private VisualEffect visualEffect;
    private PlayerController PL;
    private Powerupmanager PW;
    private void Start()
    {
        Application.targetFrameRate = 60;
        PL = GetComponent<PlayerController>();
        PW = GetComponent<Powerupmanager>();
        visualEffect = recupere.GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (OldScore < score)
        {
            PL.Combo += 1;
            PL.LastComboTime = 0;
        }
        else
        {
            PL.LastComboTime += 1;
        }
        if (score <= -1)
        {
            loseui.SetActive(true);
        }
        OldScore = score;   
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
            visualEffect.Play();
        }
        else if (other.gameObject.CompareTag("bgscoreobj"))
        {
            score += 100;
            other.gameObject.SetActive(false);
            PL.HasCombo = true;
            visualEffect.Play();
        }
    }
}
