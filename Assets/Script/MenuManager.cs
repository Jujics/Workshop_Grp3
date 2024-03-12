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
    public GameObject Carch;
    public GameObject Button;
    public GameObject Car1;
    public GameObject Car2;
    public bool hasclic;
    public int i = 0;
    public int ih = 0;
    public bool onmenu;
    public int p = 0;

    private bool isonmain;

    void Start()
    {

    }

    void Update()
    {
        hasclic = Input.GetAxis("Lngame") != 0.0f;

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
        if (p == 0)
        {
            Car1.SetActive(true);
            Car2.SetActive(false);
        }
        else if (p == 1)
        {
            Car1.SetActive(false);
            Car2.SetActive(true);
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

    public void OnClicBackCar()
    {
        Carch.SetActive(false);
        Mainmn.SetActive(true);
    }

    public void Onclicleft()
    {
        if (p == 0)
        {
            p = 1;
        }
        else
        {
            p = 0;
        }
    }

    public void Onclicright()
    {
        if (p == 1)
        {
            p = 0;
        }
        else
        {
            p = 1;
        }
    }

    public void OnClicChoixvoiture()
    {
        Carch.SetActive(true);
        Mainmn.SetActive(false);
    }
}
