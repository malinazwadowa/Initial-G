using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    private static Transform myTransform;
    
    private void Awake()
    {
        myTransform = transform;
    }

    public static Transform GetTransform()
    {
        return myTransform;
    }
}
