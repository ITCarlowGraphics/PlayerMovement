using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopBehaviour : IMovementBehaviour
{
    public float duration = 0.5f;  // Fixed duration to reach the destination
    public float hopHeight = 1.3f;  
    public float power = 1.1f;  // Ease-In/Ease-Out power for the movement

    private Vector3 p0; 
    private Vector3 p1;  
    private Vector3 currentPosition;  
    private float movementProgress;  
    private float startTime; 

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        p0 = start;
        p1 = end;

        movementProgress = 0f; 
        startTime = Time.time; 
    }

    public Vector3 GetCurrentPosition()
    {
        return currentPosition;
    }

    public bool MovementComplete()
    {
        return movementProgress >= 1f; 
    }

    public void Update()
    {
        float elapsedTime = Time.time - startTime;

        movementProgress = Mathf.Clamp01(elapsedTime / duration); 

        float adjustedProgress;
        if (movementProgress < 0.5f)
        {
            adjustedProgress = Mathf.Pow(movementProgress * 2, power) / 2f;  // Ease-In
        }
        else
        {
            adjustedProgress = 1f - Mathf.Pow(2f * (1f - movementProgress), power) / 2f;  // Ease-Out
        }

        currentPosition = GetPointOnArc(p0, p1, adjustedProgress);
    }

    private Vector3 GetPointOnArc(Vector3 start, Vector3 end, float t)
    {
        Vector3 flatPoint = Vector3.Lerp(start, end, t);

        float height = Mathf.Sin(t * Mathf.PI) * hopHeight;

        return new Vector3(flatPoint.x, flatPoint.y + height, flatPoint.z);
    }
}
