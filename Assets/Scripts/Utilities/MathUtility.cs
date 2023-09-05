using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class MathUtility
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

        int indexOfClosestEnemy = MathUtility.GetIndexOfMin(distancesToEnemies);
        return enemiesFound[indexOfClosestEnemy].transform;
    }
}
