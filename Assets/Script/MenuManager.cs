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
        // Initialization if needed
    }

    void Update()
    {
        // Check if the key "Lngame" is pressed
        if (Input.GetKey("Lngame"))
        {
            hasclic = true;
        }

        // Check conditions and modify lenclig if needed
        if (hasclic && lenclig == 600)
        {
            lenclig /= 3;
        }

        i += 1;

        // Check if hasclic is true
        if (hasclic)
        {
            ih += 1;
            if (ih == 600)
            {
                Mainmn.SetActive(true);
                Startmn.SetActive(false);
            }
        }

        // Check if i equals lenclig
        if (i == lenclig)
        {
            // Toggle the visibility of txtclig
            txtclig.SetActive(!txtclig.activeSelf);
            i = 0;
        }
    }
}
