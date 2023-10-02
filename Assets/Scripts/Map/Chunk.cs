using UnityEngine;

public class Chunk 
{
    public Vector2 positionOnMatrix;
    //Dcitionary of items
    public bool isActive;
    GameObject mapPrefab;
    Vector3 worldPosition;

    public void Initialize(Vector2 positionOnMatrix, Vector2 size)
    {
        this.positionOnMatrix = positionOnMatrix;
        this.worldPosition = new Vector3(positionOnMatrix.x * size.x, positionOnMatrix.y * size.y, 0);
        isActive = false;
    }
    public void Activate()
    {
        if (isActive)
        {
            return;
        }
        isActive = true;
        //GameObject map = Chunker.Instance.availableMaps.Dequeue();
        mapPrefab = Chunker.Instance.availableMaps.Dequeue();
        mapPrefab.transform.position = worldPosition;
    }
    public void Deactivate()
    {
        if (!isActive)
        {
            return;
        }
        Chunker.Instance.availableMaps.Enqueue(mapPrefab);
        isActive = false;
        
    }

}
