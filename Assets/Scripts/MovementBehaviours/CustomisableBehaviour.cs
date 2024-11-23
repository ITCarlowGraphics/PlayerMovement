using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HopBehaviour : CustomisableBehaviour
{
    public HopBehaviour()
    {
        SetAxisFunction("x", progress => Mathf.Lerp(startPosition.x, endPosition.x, progress));
        SetAxisFunction("z", progress => Mathf.Lerp(startPosition.z, endPosition.z, progress));
        SetAxisFunction("y", Hop);

        //You can customise the pitch, roll and yaw of the transform as well with a function.
        //SetRotationFunction("yaw", BackAndForth);
        //SetRotationFunction("pitch", SpeedUpContinueAndSlowDown);
        //SetRotationFunction("roll", SpeedUpSlowDownSpin);
    }
}

public class CustomisableBehaviour
{
    protected Vector3 startPosition, endPosition;
    protected Dictionary<string, Func<float, float>> axisFunctions = new();
    protected Dictionary<string, Func<float, float>> rotationFunctions = new();

    public void SetStartAndEnd(Vector3 v_start, Vector3 v_end)
    {
        startPosition = v_start;
        endPosition = v_end;
    }

    public void SetAxisFunction(string axis, Func<float, float> function)
    {
        axisFunctions[axis] = function;
    }

    public void SetRotationFunction(string axis, Func<float, float> function)
    {
        rotationFunctions[axis] = function;
    }

    public void EvaluateTransform(Transform transform, float progress)
    {
        float x = startPosition.x;
        float y = startPosition.y;
        float z = startPosition.z;

        if (axisFunctions.ContainsKey("x"))
            x = axisFunctions["x"](progress);
        if (axisFunctions.ContainsKey("y"))
            y = axisFunctions["y"](progress);
        if (axisFunctions.ContainsKey("z"))
            z = axisFunctions["z"](progress);

        Vector3 newPosition = new Vector3(x, y, z);

        Quaternion rotation = Quaternion.identity;

        if (rotationFunctions.ContainsKey("pitch"))
        {
            Vector3 direction = (endPosition - startPosition).normalized;
            Vector3 pitchAxis = Vector3.Cross(Vector3.up, direction).normalized;
            rotation *= Quaternion.AngleAxis(rotationFunctions["pitch"](progress), pitchAxis); 
        }
        if (rotationFunctions.ContainsKey("roll"))
        {
            Vector3 direction = (endPosition - startPosition).normalized;
            Vector3 rollAxis = direction;
            rotation *= Quaternion.AngleAxis(rotationFunctions["roll"](progress), rollAxis);
        }

        if (rotationFunctions.ContainsKey("yaw"))
        {
            Vector3 yawAxis = Vector3.up;
            rotation *= Quaternion.AngleAxis(rotationFunctions["yaw"](progress), yawAxis);
        }

        transform.position = newPosition;
        transform.rotation = rotation;
    }

    public float Hop(float progress)
    {
        const float hopHeight = 1.3f;

        float arcPoint = Mathf.Lerp(startPosition.y, endPosition.y, progress);
        float height = Mathf.Sin(progress * Mathf.PI) * hopHeight;

        return arcPoint + height;
    }

    public float BackAndForth(float progress)
    {
        const float rotationSpeed = 360f; // One full rotation per progress unit
        float angle = Mathf.Sin(progress * Mathf.PI) * rotationSpeed;
        return angle;
    }

    public float SpeedUpContinueAndSlowDown(float progress)
    {
        const float rotationSpeed = 360f; // One full rotation per progress unit
        float angle = Mathf.Cos(progress * Mathf.PI) * rotationSpeed;
        return -angle;
    }
    public float SpeedUpSlowDownSpin(float progress)
    {
        const float rotationSpeed = 360f; // One full rotation per progress unit
        float angle = Mathf.Tan(progress * Mathf.PI) * rotationSpeed;
        return angle;
    }
}

