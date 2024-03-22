using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Checksmanager : MonoBehaviour
{
    public string[] TexteAfficheHolder;
    public GameObject Canvastuto;
    public GameObject ImageBg;
    public TMP_Text TextAffiche;
    public bool IsDisplayTextIn;
    public bool IsDisplayTextOut;
    public bool InAdslIn;
    public bool InAdslOut;
    private float RefTime;
    private int i = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
        RefTime = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDisplayTextIn)
        {
            Canvastuto.SetActive(true);
            ImageBg.SetActive(true);
            TextAffiche.text = TexteAfficheHolder[i];
            Time.timeScale = 0.05f;
        }
        else if (!IsDisplayTextIn)
        {
            Canvastuto.SetActive(false);
            ImageBg.SetActive(false);
            Time.timeScale = RefTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("InTextZone"))
        {
            IsDisplayTextIn = true;
        }
        if (other.gameObject.CompareTag("InAdslIn"))
        {
            InAdslIn = true;
        }
        if (other.gameObject.CompareTag("InAdslOut"))
        {
            InAdslIn = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("InTextZone"))
        {
            IsDisplayTextIn = false;
            i += 1;
        }
    }
}
