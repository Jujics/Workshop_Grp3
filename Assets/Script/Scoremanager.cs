using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    public int score = 100000;

    
    void Start()
    {
        
    }

    void Update()
    {
        if (score <= -1)
        {
            loseui.SetActive(true);
        }   
    }
    void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            isslowingout = true;
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            score += 100;
            isboosingout = true;
        }
        else if (other.gameObject.CompareTag("froll") && other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            isslowingout = true;
        }
        else if (other.gameObject.CompareTag("smscoreobj"))
        {
            score += 20;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("bgscoreobj"))
        {
            score += 100;
            other.gameObject.SetActive(false);
        }
    }
}
