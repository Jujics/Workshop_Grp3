using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public int lenclig;
    public GameObject txtclig;
    public GameObject Mainmn;
    public GameObject Startmn;
    public GameObject Options;
    public GameObject Credits;
    public GameObject Button;
    public bool hasclic;
    public int i = 0;
    public int ih = 0;
    public bool onmenu;

    private bool isonmain;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetAxis("Lngame") != 0.0f)
        {
            hasclic = true;
        }

        if (hasclic && lenclig == 600)
        {
            lenclig /= 3;
        }

        i += 1;

        
        if (hasclic && !onmenu)
        {
            ih += 1;
            if (ih == 1300)
            {
                Mainmn.SetActive(true);
                Startmn.SetActive(false);
                onmenu = true;
                EventSystem.current.SetSelectedGameObject(Button);
            }
        }

        
        if (i >= lenclig)
        {
            txtclig.SetActive(!txtclig.activeSelf);
            i = 0;
        }
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnClicOptions()
    {
        Options.SetActive(true);
        Mainmn.SetActive(false);
    }
    public void OnClicCredits()
    {
        Credits.SetActive(true);
        Mainmn.SetActive(false);
    }
    public void OnClicBackCre()
    {
        Credits.SetActive(false);
        Mainmn.SetActive(true);
    }
    public void OnClicBackOpt()
    {
        Options.SetActive(false);
        Mainmn.SetActive(true);
    }

}