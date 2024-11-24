using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopBehaviour : CustomisableBehaviour
{
    public HopBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", Hop);
    }
}
public class FlippingBehaviour : CustomisableBehaviour
{
    public FlippingBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", progress =>
        {
            const float flipHeight = 1.6f;
            return Mathf.Sin(progress * Mathf.PI) * flipHeight;
        });
        SetRotationFunction("pitch", SpeedUpContinueAndSlowDown);
    }
}

public class WobbleBehaviour : CustomisableBehaviour
{
    public WobbleBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetScaleFunctions("side", Wobble);
    }
}

public class BouncingBehaviour : CustomisableBehaviour
{
    public BouncingBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", Bounce);
    }
}