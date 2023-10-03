using UnityEngine;

public class Chunk 
{
    //Dcitionary of items
    public Vector2 positionOnMatrix;
    private bool isActive;
    private GameObject mapPrefab;
    private Vector3 positionInWorld;

    public void Initialize(Vector2 positionOnMatrix, Vector2 size)
    {
        this.positionOnMatrix = positionOnMatrix;
        this.positionInWorld = new Vector3(positionOnMatrix.x * size.x, positionOnMatrix.y * size.y, 0);
        isActive = false;
    }

    public void Activate()
    {
        if (isActive)
        {
            return;
        }
        isActive = true;
        
        mapPrefab = MapManager.Instance.availableMaps.Dequeue();
        mapPrefab.transform.position = positionInWorld;
    }

    public void Deactivate()
    {
        if (!isActive)
        {
            return;
        }
        MapManager.Instance.availableMaps.Enqueue(mapPrefab);
        isActive = false;
    }
}
