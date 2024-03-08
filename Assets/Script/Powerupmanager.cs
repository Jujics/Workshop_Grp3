using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerupmanager : MonoBehaviour
{
    public bool isvpn;  // Indique si le power-up VPN est activé

    void Start()
    {
        // Aucune initialisation nécessaire au démarrage
    }

    void Update()
    {
        // Aucune logique de mise à jour nécessaire
    }

    private void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("vpn"))
        {
            // Activer le power-up VPN et désactiver l'objet
            isvpn = true;
            other.gameObject.SetActive(false);
        }
    }
}
