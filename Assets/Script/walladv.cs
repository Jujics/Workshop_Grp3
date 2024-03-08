using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAdvancement : MonoBehaviour
{
    public int wallSpeed;        // Vitesse de déplacement du mur
    public GameObject player;    // Référence au joueur
    private Rigidbody rb;         // Composant Rigidbody du mur
    private Scoremanager scoreManager;  // Référence au gestionnaire de score

    void Start()
    {
        // Initialisation du composant Rigidbody et du gestionnaire de score au démarrage
        rb = GetComponent<Rigidbody>();
        scoreManager = player.GetComponent<Scoremanager>();
    }

    void Update()
    {
        // Déplacement du mur vers l'avant
        Vector3 speed = new Vector3(0.0f, 0.0f, 2.0f);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed.z * wallSpeed);

        // Logique pour ajuster la vitesse du mur en fonction de la position du joueur
        if (player.transform.position.z >= transform.position.z + 20)
        {
            // Si le joueur est devant le mur, augmenter la vitesse
            wallSpeed = 10;
        }
        else if (player.transform.position.z < transform.position.z)
        {
            // Si le joueur est derrière le mur, décrémenter le score
            scoreManager.score -= 1;
        }
        else
        {
            // Sinon, maintenir une vitesse normale
            wallSpeed = 2;
        }
    }
}
