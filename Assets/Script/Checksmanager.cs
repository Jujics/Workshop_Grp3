using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checksmanager : MonoBehaviour
{
    public int ColiderIndex;
    public string TexteAfiche;
    public bool IsDisplayTextIn;
    public bool IsDisplayTextOut;
    public bool IsSpore;
    public bool InAdslIn;
    public bool InAdslOut;
    public bool InFibreIn;
    public bool InFibreOut;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDisplayTextIn)
        {
            //set.active(text)
            //text.settext
        }
        else if (IsDisplayTextOut)
        {
            //set.active(text)false
            //text.settext
        }
        else if (IsSpore)
        {
            //do things to the player
        }
    }
}
