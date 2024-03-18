using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourneslow : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        currentAngle += 2;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentAngle);
    }
}
