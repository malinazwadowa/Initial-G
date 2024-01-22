using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector2 RoundVector2D(Vector2 vectorToRound)
    {
        // Check the absolute values of x and y components
        float absX = Mathf.Abs(vectorToRound.x);
        float absY = Mathf.Abs(vectorToRound.y);

        // Determine the rounded vector components
        float roundedX = absX >= absY ? Mathf.Sign(vectorToRound.x) : 0;
        float roundedY = absX < absY ? Mathf.Sign(vectorToRound.y) : 0;

        // Return the rounded vector 
        return new Vector2(roundedX, roundedY);
    }
    public static int GetIndexOfMin(List<float> list)
    {
        int indexOfMin = 0;

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] < list[indexOfMin])
            {
                indexOfMin = i;
            }
        }
        return indexOfMin;
    }
    
    public static UnityEngine.Transform GetClosestEnemy(Vector3 position)
    {
        List<Collider2D> enemiesFound = new List<Collider2D>();
        float scanRadius = 1;

        while (enemiesFound.Count == 0)
        {
            enemiesFound.AddRange(Physics2D.OverlapCircleAll(position, scanRadius, 1 << 6));
            scanRadius *= 2;
            //Debug.Log("ScanRadius: " + scanRadius +" Center position: " + position);
            if (scanRadius > 150)
            {
                Debug.LogWarning("No enemies avilable");
                return null;
            }
        }

        List<float> distancesToEnemies = new List<float>();

        foreach (Collider2D enemy in enemiesFound)
        {
            float distance = Vector2.Distance(enemy.transform.position, position);
            distancesToEnemies.Add(distance);
        }

        int indexOfClosestEnemy = Utilities.GetIndexOfMin(distancesToEnemies);
        return enemiesFound[indexOfClosestEnemy].transform;
    }

    public static Vector3 GetRandomPositionOutsideOfCameraView(float distanceOffset)
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float randomizedOffset = Random.Range(0.5f, distanceOffset);

        Vector3 cameraWorldPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        Vector3 localSpawnPosition = Vector3.zero;
        Vector3 foundPosition;

        bool isViable = false;

        //Sets random side.
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            case 1:
                localSpawnPosition = new Vector3(cameraWidth + randomizedOffset, 0);
                break;
            case 2:
                localSpawnPosition = new Vector3(-cameraWidth - randomizedOffset, 0);
                break;
            case 3:
                localSpawnPosition = new Vector3(0, cameraHeight + randomizedOffset);
                break;
            case 4:
                localSpawnPosition = new Vector3(0, -cameraHeight - randomizedOffset);
                break;
            default:
                Debug.Log("Failed to choose side to spawn");
                break;
        }

        //Sets random position on chosen side.
        if (localSpawnPosition.x == 0)
        {
            localSpawnPosition.x = Random.Range((-cameraWidth - randomizedOffset), (cameraWidth + randomizedOffset));
        }
        else
        {
            localSpawnPosition.y = Random.Range((-cameraHeight - randomizedOffset), (cameraHeight + randomizedOffset));
        }

        foundPosition = localSpawnPosition + cameraWorldPosition;

        int layerMask = 1 << LayerMask.NameToLayer("EnviromentObjects");

        while(isViable == false)
        {
            Collider2D collider = Physics2D.OverlapPoint(foundPosition, layerMask);
            if(collider == null)
            {
                isViable = true;
                break;
            }

            foundPosition += new Vector3(1, 0, 0);
        }

        return foundPosition;
    }

    public static bool IsObjectInView(float distanceOffset, Vector3 position)
    {
        Vector3 objectPosition = Camera.main.WorldToViewportPoint(position);
        float offset = distanceOffset / Camera.main.orthographicSize;

        return objectPosition.x > -offset && objectPosition.x < 1 + offset
            && objectPosition.y > -offset && objectPosition.y < 1 + offset;
    }

    public static ObjectWithWeight GetRandomOutOfCollection(List<ObjectWithWeight> weightedCollection)
    {
        int totalWeight = 0;
        foreach (var item in weightedCollection)
        {
            totalWeight += item.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (var item in weightedCollection)
        {
            if (randomValue < item.weight)
            {
                return item;
            }

            randomValue -= item.weight;
        }
        return null;
    }

    public static void RemoveChildren(Transform parent)
    {
        // Iterate through each child of the parent
        foreach (Transform child in parent)
        {
            Object.Destroy(child.gameObject);
        }

        // Clear the list of children in the parent Transform
        parent.DetachChildren();
    }
}
