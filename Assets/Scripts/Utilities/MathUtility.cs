using UnityEngine;
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
}
