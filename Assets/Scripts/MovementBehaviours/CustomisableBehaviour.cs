using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CustomisableBehaviour
{
    protected Vector3 startPosition, endPosition;
    protected float duration;
    protected Dictionary<string, Func<float, float>> movementFunctions = new();
    protected Dictionary<string, Func<float, float>> rotationFunctions = new();
    protected Dictionary<string, Func<float, float>> scaleFunctions = new();
    public void SetMovementDuration(float movementDuration) =>  duration = movementDuration;
    public float GetMovementDuration() { return duration; }


    public void SetStartAndEnd(Vector3 v_start, Vector3 v_end)
    {
        startPosition = v_start;
        endPosition = v_end;
    }
    public void SetScaleFunctions(string axis, Func<float, float> function)
    {
        scaleFunctions[axis] = function;
    }

    public void SetMovementFunction(string axis, Func<float, float> function)
    {
        movementFunctions[axis] = function;
    }

    public void SetRotationFunction(string axis, Func<float, float> function)
    {
        rotationFunctions[axis] = function;
    }

    public void EvaluateTransform(Transform transform, float progress)
    {
        Vector3 relativeOffset = Vector3.zero;

        if (movementFunctions.ContainsKey("forward"))
        {
            float forwardDistance = movementFunctions["forward"](progress);
            Vector3 pathDirection = (endPosition - startPosition).normalized;
            relativeOffset += pathDirection * forwardDistance;
        }

        if (movementFunctions.ContainsKey("side"))
        {
            float sideOffset = movementFunctions["side"](progress);
            Vector3 sideDirection = Vector3.Cross(Vector3.up, (endPosition - startPosition).normalized).normalized;
            relativeOffset += sideDirection * sideOffset;
        }

        if (movementFunctions.ContainsKey("up"))
        {
            float upOffset = movementFunctions["up"](progress);
            relativeOffset += Vector3.up * upOffset;
        }

        Vector3 newPosition = startPosition + relativeOffset;

        Quaternion rotation = Quaternion.identity;

        if (rotationFunctions.ContainsKey("pitch"))
        {
            Vector3 pitchAxis = Vector3.Cross(Vector3.up, (endPosition - startPosition).normalized).normalized;
            if (pitchAxis != Vector3.zero)
                rotation *= Quaternion.AngleAxis(rotationFunctions["pitch"](progress), pitchAxis);
        }

        if (rotationFunctions.ContainsKey("roll"))
        {
            Vector3 rollAxis = (endPosition - startPosition).normalized;
            rotation *= Quaternion.AngleAxis(rotationFunctions["roll"](progress), rollAxis);
        }

        if (rotationFunctions.ContainsKey("yaw"))
        {
            rotation *= Quaternion.AngleAxis(rotationFunctions["yaw"](progress), Vector3.up);
        }

        transform.position = newPosition;
        transform.rotation = rotation;


        Vector3 localScale = transform.localScale;
        if (scaleFunctions.ContainsKey("x"))
            localScale.x = scaleFunctions["x"](progress);
        if (scaleFunctions.ContainsKey("y"))
            localScale.y = scaleFunctions["y"](progress);
        if (scaleFunctions.ContainsKey("z"))
            localScale.z = scaleFunctions["z"](progress);

        transform.localScale = localScale;
    }

    public float Hop(float progress)
    {
        const float hopHeight = 1.3f; 
        float hopOffset = Mathf.Sin(progress * Mathf.PI) * hopHeight;

        return hopOffset;
    }
    public float BackAndForth(float progress)
    {
        const float rotationSpeed = 360f; 
        float angle = Mathf.Sin(progress * Mathf.PI) * rotationSpeed;
        return angle;
    }
    public float SpeedUpContinueAndSlowDown(float progress)
    {
        const float rotationSpeed = 360f; 
        float angle = Mathf.Cos(progress * Mathf.PI) * rotationSpeed;
        return -angle;
    }
    public float SpeedUpSlowDownSpin(float progress)
    {
        const float rotation = 360f;
        const float frequencyFactor = 0.5f; 
        const float easingPower = 2f; 

        float easedProgress = Mathf.Pow(progress, easingPower);

        float angle = Mathf.Tan(easedProgress * Mathf.PI * frequencyFactor) * rotation;

        return angle;
    }
    public float Bounce(float progress)
    {
        const float bounceIntensity = 5.0f; // Higher values make it "bouncier"
        float easedProgress = 2 * Mathf.Pow(progress, 2) - 1 * Mathf.Pow(progress, 3); // Smooth easing
        float bounce = Mathf.Abs(Mathf.Sin(easedProgress * Mathf.PI * bounceIntensity)); // Oscillate with decaying amplitude
        return bounce;
    }
    public float Wobble(float progress)
    {
        const float wobbleIntensity = 0.5f; // Wobble angle in degrees
        float easedProgress = Mathf.SmoothStep(0, 1, progress); // Smooth start and stop
        float wobble = Mathf.Sin(progress * Mathf.PI * 3) * Mathf.Lerp(wobbleIntensity, 0, progress); // Decay wobble over time
        return wobble;
    }
    public float ZigZag(float progress)
    {
        const float zigzagWidth = 0.7f; // Distance of each zig or zag
        int numZigZags = 3; // Number of zigzag cycles
        float zigzag = Mathf.Sin(progress * Mathf.PI * numZigZags) * zigzagWidth;
        return zigzag;
    }
    static public float Pulse(float progress)
    {
        const float maxPulseSize = 1.2f; // Maximum scale
        const float frequencyFactor = 0.5f; // Reduce to slow down , 0.5 = half-speed
        const float easingPower = 3f;

        float easedProgress = Mathf.Pow(progress, easingPower);

        float pulse = 1 + Mathf.Sin(easedProgress * Mathf.PI * 2 * frequencyFactor) * (maxPulseSize - 1);
        return pulse;
    }
    public float Spiral(float progress)
    {
        const float spiralIntensity = 360f; 
        float angle = spiralIntensity * progress * progress; 
        return angle;
    }
    public float Shake(float progress)
    {
        const float shakeAmplitude = 0.5f; 
        float decay = 1 - progress; 
        float shake = Mathf.PerlinNoise(progress * 10, 0) * 2 - 1; 
        return shake * shakeAmplitude * decay;
    }
    public float ElasticOvershoot(float progress)
    {
        const float overshootMagnitude = 1.5f; 
        float easedProgress = 3 * Mathf.Pow(progress, 2) - 2 * Mathf.Pow(progress, 3); 
        float overshoot = Mathf.Sin(easedProgress * Mathf.PI * 2) * overshootMagnitude * (1 - progress); 
        return 1 + overshoot;
    }
    public float SineWaveMovement(float progress)
    {
        const float waveAmplitude = 1.0f; 
        const float waveFrequency = 1.0f; 
        float sineWave = Mathf.Sin(progress * Mathf.PI * 2 * waveFrequency) * waveAmplitude;
        return sineWave;
    }

    public float ScaleDownAndUp(float progress)
    {
        const float smallestScale = 0.25f;
        float actualScale = 1 - smallestScale;
        float scale = 1 - (Mathf.Sin( Mathf.Pow(progress,2.0f) * Mathf.PI) * actualScale);
        return scale;
    }

    public float ScaleUpandDown(float progress)
    {
        const float maxScale = 1.5f;
        float scale = 1 + (Mathf.Sin(Mathf.Pow(progress, 2.0f) * Mathf.PI) * maxScale);
        return scale;
    }

}

