using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HopBehaviour : CustomisableBehaviour
{
    private float HopFunction(float progress)
    {
        const float hopHeight = 1.3f;

        float arcPoint = Mathf.Lerp(startPosition.y, endPosition.y, progress);
        float height = Mathf.Sin(progress * Mathf.PI) * hopHeight;

        return arcPoint + height;
    }

    public HopBehaviour()
    {
        SetAxisFunction("x", progress => Mathf.Lerp(startPosition.x, endPosition.x, progress)); // BAsic linear interpolation
        SetAxisFunction("z", progress => Mathf.Lerp(startPosition.z, endPosition.z, progress)); // Basic linear interpolation
        SetAxisFunction("y", HopFunction);
    }
}

public class CustomisableBehaviour
{
    protected Vector3 startPosition, endPosition;
    protected Dictionary<string, Func<float, float>> axisFunctions = new();

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;
    }

    public void SetAxisFunction(string axis, Func<float, float> function)
    {
        axisFunctions[axis] = function;
    }

    public Vector3 EvaluatePosition(float progress)
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

        return new Vector3(x, y, z);
    }
}

