using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walladv : MonoBehaviour
{
    public int wallsp;
    public GameObject player; 
    public bool Critwall;
    public int i;
    public bool Nearwall;
    public bool Hasfirewall;
    private Rigidbody rb;
    private Scoremanager vari;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        vari = player.GetComponent<Scoremanager>();
    }

    void Update()
    {
        Vector3 spd = new Vector3(0.0f, 0.0f, 2.0f);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, spd.z * wallsp);

        if (Hasfirewall == true)
        {
            wallsp = 0;
            i += 1;
            if(i == 700)
            {
                wallsp = 10;
                Hasfirewall = false;
            }
        }
        if (player.transform.position.z >= transform.position.z + 20 && Hasfirewall == false) 
        {
            wallsp = 10;
            Nearwall = false;
            Critwall = false;
        }
        else if (player.transform.position.z <= transform.position.z + 20 && player.transform.position.z >= transform.position.z + 5 && Hasfirewall == false)
        {
            Nearwall = true;
            Critwall = false;
        }
        else if (player.transform.position.z <= transform.position.z + 5 && Hasfirewall == false)
        {
            Critwall = true;
            Nearwall = false;
        }

        else if(player.transform.position.z < transform.position.z)
        {
            vari.score -= 1;
        }
        else if(Hasfirewall == false)
        {
            wallsp = 2;
        }
        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("firewall"))
        {
            Hasfirewall = true;
        }
    }
}
