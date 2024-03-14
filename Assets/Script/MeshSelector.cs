using UnityEngine;

[CreateAssetMenu(fileName = "MeshSelector", menuName = "ScriptableObjects/MeshSelector", order = 1)]
public class MeshSelector : ScriptableObject
{
    public MeshFilter[] Ships;
    public int index;
}