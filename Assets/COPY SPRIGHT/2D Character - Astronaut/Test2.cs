using UnityEngine;

public class Test2 : MonoBehaviour
{
    public float orbitSpeed = 30.0f; // Adjust the speed in the Unity Inspector

    void Update()
    {
        // Rotate the pivot point (empty GameObject) to make the orbiting object follow
        transform.Rotate(Vector3.forward, orbitSpeed * Time.deltaTime);
        foreach (Transform child in transform)
        {
            child.Rotate(Vector3.back, orbitSpeed * Time.deltaTime);
        }
    }
}