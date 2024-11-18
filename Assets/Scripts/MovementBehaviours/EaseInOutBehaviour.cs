using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInOutBehaviour : IMovementBehaviour
{
    public float speed = 2.5f;
    public float power = 2f;
    Vector3 p0;
    Vector3 p1;

    Vector3 currentPosition;
    private float movementProgress; // Tracks progress manually
    private float startTime;
    public float duration = 0.5f;  // Fixed duration to reach the destination

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        p0 = start;
        p1 = end;

        movementProgress = 0f; // Reset progress
        startTime = Time.time;
    }

    public Vector3 GetCurrentPosition()
    {
        return currentPosition;
    }

    public bool MovementComplete()
    {
        if (movementProgress >= 1f)
        {
            return true;
        }
        return false;
    }

    public void Update()
    {
        float elapsedTime = Time.time - startTime;

        movementProgress = Mathf.Clamp01(elapsedTime / duration);

        // Generalized power-based Ease-In-Out function
        float adjustedProgress;
        if (movementProgress < 0.5f)
        {
            // First half: Ease-In
            adjustedProgress = Mathf.Pow(movementProgress * 2, power) / 2f;
        }
        else
        {
            // Second half: Ease-Out
            adjustedProgress = 1f - Mathf.Pow(2f * (1f - movementProgress), power) / 2f;
        }

        currentPosition = Vector3.Lerp(p0, p1, adjustedProgress);
    }
}
