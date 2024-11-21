using UnityEngine;

public class EaseInOutBehaviour : IMovementBehaviour
{
    public float duration = 0.5f; // Fixed duration to reach the destination

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    private float startTime;
    private bool isMovementComplete;

    // Delegate to define easing functions
    public delegate float EasingFunction(float progress);

    // Default easing functions
    public static float EaseInQuadratic(float progress) => Mathf.Pow(progress, 2);
    public static float EaseOutQuadratic(float progress) => 1 - Mathf.Pow(1 - progress, 2);

    private EasingFunction easingFunctionX;
    private EasingFunction easingFunctionZ;

    public void SetStartAndEnd(Vector3 start, Vector3 end,
        EasingFunction easingX = null, EasingFunction easingZ = null)
    {
        startPosition = start;
        endPosition = end;
        startTime = Time.time;
        isMovementComplete = false;

        // Assign easing functions or default to ease-in quadratic
        easingFunctionX = easingX ?? EaseInQuadratic;
        easingFunctionZ = easingZ ?? EaseInQuadratic;
    }

    public Vector3 GetCurrentPosition()
    {
        return currentPosition;
    }

    public bool MovementComplete()
    {
        return isMovementComplete;
    }

    public void Update()
    {
        if (isMovementComplete) return;

        float elapsedTime = Time.time - startTime;
        float progress = Mathf.Clamp01(elapsedTime / duration);

        // Apply easing to x and z axes
        float easedX = easingFunctionX(progress);
        float easedZ = easingFunctionZ(progress);

        currentPosition.x = Mathf.Lerp(startPosition.x, endPosition.x, easedX);
        currentPosition.z = Mathf.Lerp(startPosition.z, endPosition.z, easedZ);
        currentPosition.y = Mathf.Lerp(startPosition.y, endPosition.y, progress); // Linear for Y-axis

        // Check if movement is complete
        if (progress >= 1f)
        {
            isMovementComplete = true;
        }
    }
}
