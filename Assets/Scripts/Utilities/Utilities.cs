using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
    // Im thinking of making a list of all active enemies since I will have spawn manager anyway,
    // then it might be better to just go through the list of all active enemies and pick the closes one instead of checking are around player.
    //I guess, to be determined. Probly not since the list would be long with logic to particion it and here we can scan small area around player which should not be that heavy.
    // to be moved to reasonable spot, most likely enemy manager

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
    public static Vector3 GetRandomSpawnPositionOutsideOfCameraView(float distanceOffset)
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnOffset = distanceOffset;

        Vector3 spawnPosition = Vector3.zero;

        //Sets random side.
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            case 1:
                spawnPosition = new Vector3(cameraWidth + spawnOffset, 0);
                break;
            case 2:
                spawnPosition = new Vector3(-cameraWidth - spawnOffset, 0);
                break;
            case 3:
                spawnPosition = new Vector3(0, cameraHeight + spawnOffset);
                break;
            case 4:
                spawnPosition = new Vector3(0, -cameraHeight - spawnOffset);
                break;
            default:
                Debug.Log("Failed to choose side to spawn");
                break;
        }

        //Sets random position on chosen side.
        if (spawnPosition.x == 0)
        {
            spawnPosition.x = Random.Range((-cameraWidth - spawnOffset), (cameraWidth + spawnOffset));
        }
        else
        {
            spawnPosition.y = Random.Range((-cameraHeight - spawnOffset), (cameraHeight + spawnOffset));
        }

        Vector3 offset = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        return spawnPosition + offset;
    }

    public static bool IsObjectInView(float distanceOffset, Vector3 position)
    {
        Vector3 objectPosition = Camera.main.WorldToViewportPoint(position);
        float offset = distanceOffset / Camera.main.orthographicSize;

        return objectPosition.x > -offset && objectPosition.x < 1 + offset
            && objectPosition.y > -offset && objectPosition.y < 1 + offset;
    }
}
