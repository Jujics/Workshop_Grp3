using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walladv : MonoBehaviour
{
    public int wallsp;
    public GameObject player; 
    private Rigidbody rb;
    private Scoremanager vari;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vari = player.GetComponent<Scoremanager>();
    }

    void Update()
    {
        Vector3 spd = new Vector3(0.0f, 0.0f, 2.0f);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, spd.z * wallsp);

        
        if (player.transform.position.z >= transform.position.z + 20) 
        {
            wallsp = 10;
        }
        else if(player.transform.position.z < transform.position.z)
        {
            vari.score -= 1;
        }
        else
        {
            wallsp = 2;
        }

    }
}
