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
    public Material material;
    private Checksmanager Checks;
    private Rigidbody rb;
    private int v = 1;
    private int m = 1;
    private Scoremanager vari;
    private PlayerController PL;
    

    

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        vari = player.GetComponent<Scoremanager>();
        Checks = player.GetComponent<Checksmanager>();
        PL = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        Vector3 spd = new Vector3(0.0f, 0.0f, 1.0f);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, wallsp);

        if (Hasfirewall == true && Checks.InAdslIn == true)
        {
            wallsp = 0;
            i += 1;
            if(i == 700)
            {
                wallsp = 10;
                Hasfirewall = false;
            }
        }
        if(player.transform.position.z < transform.position.z)
        {
            vari.score -= 1;
        }
        if (player.transform.position.z >= transform.position.z + 30 && Hasfirewall == false) 
        {
            wallsp = 110;
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

        
        if(Hasfirewall == false && player.transform.position.z <= transform.position.z + 30 && Hasfirewall == false)
        {
            wallsp = 75;
        }
        if (Nearwall) 
        {
            v = 1;
            if (m == 1)
            {
                PL.GameSound[7].volume = PL.SoundLevel/2;
                PL.GameSound[7].Play();
                PL.GameSound[7].loop = true;
            }    
            m += 1;
            float InterpolationFactorNoise = 3.0f;
            float InterpolationFactorScanline = 0.1f;
            material.SetFloat("_noise_amount", Mathf.Lerp(material.GetFloat("_noise_amount"), 50f, InterpolationFactorNoise * Time.deltaTime));
            material.SetFloat("_Scan_Line_Strength", Mathf.Lerp(material.GetFloat("_Scan_Line_Strength"), 0.5f, InterpolationFactorScanline * Time.deltaTime));
        } 
        else if (Critwall) 
        {
            m = 1;
            if (v == 1)
            {
                PL.GameSound[7].volume = PL.SoundLevel;
                PL.GameSound[7].Play();
                PL.GameSound[7].loop = true;
            }    
            v += 1;
            float InterpolationFactorNoise = 3.0f;
            float InterpolationFactorScanline = 0.1f;
            material.SetFloat("_noise_amount", Mathf.Lerp(material.GetFloat("_noise_amount"), 100f, InterpolationFactorNoise * Time.deltaTime));
            material.SetFloat("_Scan_Line_Strength", Mathf.Lerp(material.GetFloat("_Scan_Line_Strength"), 0f, InterpolationFactorScanline * Time.deltaTime));
        } 
        else 
        {
            m = 1;
            v = 1;
            PL.GameSound[7].loop = false;
            float InterpolationFactorNoise = 3.0f;
            float InterpolationFactorScanline = 0.1f;
            material.SetFloat("_noise_amount", Mathf.Lerp(material.GetFloat("_noise_amount"), 0f, InterpolationFactorNoise * Time.deltaTime));
            material.SetFloat("_Scan_Line_Strength", Mathf.Lerp(material.GetFloat("_Scan_Line_Strength"), 1f, InterpolationFactorScanline * Time.deltaTime));
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
