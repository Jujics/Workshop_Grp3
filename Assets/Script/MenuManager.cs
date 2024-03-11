using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int lenclig;
    public GameObject txtclig;
    public GameObject Mainmn;
    public GameObject Startmn;
    public bool hasclic;
    int i = 0;
    int ih = 0;

    private bool isonmain;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKey("Lngame") == 1.0f)
        {
            hasclic = true;
        }
        if (hasclic == true && lenclig == 600)
        {
            lenclig /= 3;
        }
        i += 1;
        if (hasclic == true)
        {
            ih += 1;
            if(ih == 600)
            {
                Mainmn.SetActive(true);
                Startmn.SetActive(false);
            }
        }
        if(i == lenclig)
        {
            if (txtclig.activeSelf)
            {
                txtclig.SetActive(false);
                i = 0;
            }
            else
            {
                txtclig.SetActive(true);
                i = 0;
            }
        }
    }
}
