using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int lenclig;
    public GameObject txtclig;
    public bool hasclic;
    int i = 0;

    private bool isonmain;
    void Start()
    {
        
    }


    void Update()
    {
        if (hasclic == true)
        {
            lenclig /= 3;
            hasclic = false;
        }
        i += 1;
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
