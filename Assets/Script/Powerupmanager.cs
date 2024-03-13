using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerupmanager : MonoBehaviour
{
    public bool isvpn;  
    void Start()
    {
        Application.targetFrameRate = 60;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("vpn"))
        {
            isvpn = true;
            other.gameObject.SetActive(false);
        }
    }
}
