using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Powerupmanager : MonoBehaviour
{
    public bool isvpn; 
    public GameObject recupere; 
    private VisualEffect visualEffect;
    void Start()
    {
        Application.targetFrameRate = 60;
        visualEffect = recupere.GetComponent<VisualEffect>();
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
            visualEffect.Play();
            other.gameObject.SetActive(false);
        }
    }
}
