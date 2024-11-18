using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInOutBehaviour : IMovementBehaviour
{
    public float speed = 5f;
    Vector3 p0;
    Vector3 p1;

    Vector3 currentPosition;
    private float movementProgress; // Tracks progress manually
    private float totalDistance; // Total distance to target

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        p0 = start;
        p1 = end;

        totalDistance = Vector3.Distance(p0, p1);
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

    // Update is called once per frame
    public void Update()
    {
        movementProgress += speed * Time.deltaTime / totalDistance;
        movementProgress = Mathf.Clamp01(movementProgress);
        float adjustedProgress = movementProgress; // Start with linear

        //Quadratic Movement
        //adjustedProgress = movementProgress * movementProgress; // Ease-In
        //adjustedProgress = 1 - Mathf.Pow(1 - movementProgress, 2); // Ease-Out
        adjustedProgress = (movementProgress < 0.5f)
            ? 2 * movementProgress * movementProgress // Ease-In-Out (first half)
            : 1 - 2 * Mathf.Pow(1 - movementProgress, 2); // Ease-In-Out (second half)

        currentPosition = Vector3.Lerp(p0, p1, adjustedProgress);
    }
}
