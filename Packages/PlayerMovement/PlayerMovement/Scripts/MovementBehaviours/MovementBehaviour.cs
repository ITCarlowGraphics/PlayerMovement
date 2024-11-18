using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementBehaviour
{
    void SetStartAndEnd(Vector3 start, Vector3 end);
    void Update();
    bool MovementComplete();
    Vector3 GetCurrentPosition();
}
