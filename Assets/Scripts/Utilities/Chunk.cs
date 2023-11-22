using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    private bool isActive;
    private GameObject mapPrefab;

    public Vector2 positionOnMatrix;
    private Vector3 chunkSize;
    private Vector3 positionInWorld;

    private Dictionary<GameObject, List <Vector3>> objectsOnGround = new Dictionary<GameObject, List<Vector3>>();

    private int lootableItemsID = 9;
    private int containersLayerID = 12;
    private LayerMask groundedObjectsLayer;

    public void Initialize(Vector2 positionOnMatrix, Vector2 size)
    {
        this.positionOnMatrix = positionOnMatrix;
        this.chunkSize = size;
        this.positionInWorld = new Vector3(positionOnMatrix.x * size.x, positionOnMatrix.y * size.y, 0);
        isActive = false;

        groundedObjectsLayer = 1 << lootableItemsID | 1 << containersLayerID;
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

        if(objectsOnGround.Count > 0)
        {
            foreach (var kvp in objectsOnGround)
            {
                GameObject objectToSpawn = kvp.Key;
                List<Vector3> positions = kvp.Value;

                foreach (Vector3 position in positions)
                {
                    ObjectPooler.Instance.SpawnObject(objectToSpawn, position);
                }

                positions.Clear();
            }
        } 
    }

    public void Deactivate()
    {
        if (!isActive)
        {
            return;
        }
        
        Collider2D[] colliders = Physics2D.OverlapAreaAll(positionInWorld, positionInWorld + chunkSize, groundedObjectsLayer);
        
        foreach (Collider2D collider in colliders)
        {
            if (objectsOnGround.ContainsKey(collider.gameObject))
            {
                objectsOnGround[collider.gameObject].Add(collider.transform.position);
            }
            else
            {
                List<Vector3> positions = new List<Vector3>();
                positions.Add(collider.transform.position);
                objectsOnGround.Add(collider.gameObject, positions);
            }
            ObjectPooler.Instance.DeSpawnObject(collider.gameObject);
        }

        MapManager.Instance.availableMaps.Enqueue(mapPrefab);
        isActive = false;
    }
}
