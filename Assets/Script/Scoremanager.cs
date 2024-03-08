using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 100000;      // Score initial
    public GameObject loseUI;       // Interface utilisateur de défaite
    private PlayerController playerController;  // Référence au script PlayerController
    private PowerupManager powerupManager;      // Référence au script PowerupManager

    private void Start()
    {
        // Initialisation des références aux scripts PlayerController et PowerupManager
        playerController = GetComponent<PlayerController>();
        powerupManager = GetComponent<PowerupManager>();
    }

    private void Update()
    {
        // Vérifier si le score est inférieur ou égal à -1
        if (score <= -1)
        {
            // Activer l'interface utilisateur de défaite
            loseUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("dmgin"))
        {
            // Vérifier si le power-up "vpn" est activé
            if (powerupManager.isVPN)
            {
                // Désactiver le power-up "vpn"
                powerupManager.isVPN = false;
            }
            else
            {
                // Si le power-up "vpn" n'est pas activé, décrémenter le score et activer le ralentissement
                score -= 100;
                playerController.isSlowingOut = true;
            }
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            // Incrémenter le score et activer l'accélération
            score += 100;
            playerController.isBoosingOut = true;
        }
        else if (other.gameObject.CompareTag("froll") && other.gameObject.CompareTag("dmgin"))
        {
            // Décrémenter le score et activer le ralentissement
            score -= 100;
            playerController.isSlowingOut = true;
        }
        else if (other.gameObject.CompareTag("smscoreobj"))
        {
            // Incrémenter le score et désactiver l'objet
            score += 20;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("bgscoreobj"))
        {
            // Incrémenter le score et désactiver l'objet
            score += 100;
            other.gameObject.SetActive(false);
        }
    }
}
