using Codice.CM.Common;
using log4net.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementController
{
    IMovementBehaviour m_MovementBehaviour;
    public MovementController()
    {
        m_MovementBehaviour = new EaseInOutBehaviour();
    }
    
    public Vector3 GetCurrentPosition() {
        return m_MovementBehaviour.GetCurrentPosition();
    }

    public bool MovementComplete()
    {
        return m_MovementBehaviour.MovementComplete();
    }

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        m_MovementBehaviour.SetStartAndEnd(start, end);
    }

    public void Update()
    {

        m_MovementBehaviour.Update();
        //// Cubic Movement
        //// adjustedProgress = movementProgress * movementProgress * movementProgress; // Ease-In
        //// adjustedProgress = 1 - Mathf.Pow(1 - movementProgress, 3); // Ease-Out
        //adjustedProgress = 3 * movementProgress * movementProgress - 2 * movementProgress * movementProgress * movementProgress; // Ease-In-Out
        
    }
}
