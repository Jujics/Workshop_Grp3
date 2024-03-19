using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folderspin : MonoBehaviour
{
    // Variables pour le mouvement de flottement
    public float floatingSpeed = 1f;
    public float floatingHeight = 0.5f;
    private Vector3 startPosition;

    // Variables pour le mouvement de rotation
    public float rotationSpeed = 30f;

    private void Start()
    {
        // Sauvegarde de la position de départ
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcule la nouvelle position en ajoutant un décalage sinusoidal
        float yOffset = Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        // Calcule la rotation
        float rotationOffset = Time.deltaTime * rotationSpeed;
        transform.Rotate(Vector3.up, rotationOffset);
    }
}
