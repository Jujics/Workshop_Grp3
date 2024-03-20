using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Powerupmanager : MonoBehaviour
{
    public bool isvpn; 
    public GameObject recupere; 
    public GameObject Player;
    private VisualEffect visualEffect;
    private PlayerController PL;
    void Start()
    {
        Application.targetFrameRate = 60;
        visualEffect = recupere.GetComponent<VisualEffect>();
        PL = Player.GetComponent<PlayerController>();
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
            PL.GameSound[10].Play();
            isvpn = true;
            visualEffect.Play();
            other.gameObject.SetActive(false);
        }
    }
}
