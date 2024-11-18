using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAcrossBoardBehaviour : CustomisableBehaviour
{
    public MoveAcrossBoardBehaviour()
    {
        SetMovementDuration(3.0f);
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", progress =>
        {
            const float boardCrossHeight = 6.0f;
            return Mathf.Sin(progress * Mathf.PI) * boardCrossHeight;
        });
    }
}

public class HopBehaviour : CustomisableBehaviour
{
    public HopBehaviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("up", Hop); 
        //SetScaleFunctions("x", ScaleDownAndUp);
        //SetScaleFunctions("z", ScaleDownAndUp);
        //SetScaleFunctions("y", ScaleDownAndUp);
        const float HopDuration = 0.5f;
        SetMovementDuration(HopDuration);
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
        const float FlipDuration = 1.2f;
        SetMovementDuration(FlipDuration);
    }
}

public class SlitherBeahviour : CustomisableBehaviour
{
    public SlitherBeahviour()
    {
        SetMovementFunction("forward", progress => Mathf.Lerp(0, Vector3.Distance(startPosition, endPosition), progress));
        SetMovementFunction("side", ZigZag);
        //SetScaleFunctions("x", ScaleUpandDown);
        //SetScaleFunctions("z", ScaleUpandDown);
        //SetScaleFunctions("y", ScaleDownAndUp);
        const float slitherDuration = 0.9f;
        SetMovementDuration(slitherDuration);
    }
}