using UnityEngine;

public class CirclePlacement : MonoBehaviour
{
    public GameObject centerObject; 
    public GameObject parentObject;
    public float radius = 5f; 

    void Start()
    {
        Application.targetFrameRate = 60;
        PlaceObjectsOnCircle();
    }

    void PlaceObjectsOnCircle()
    {
        Vector3 center = parentObject.transform.position; 

        int numberOfObjects = parentObject.transform.childCount;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * (360f / numberOfObjects);
            float x = center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = center.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 position = new Vector3(x, y, center.z);

            parentObject.transform.GetChild(i).position = position;
        }
    }
}