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
        m_MovementBehaviour = new HopBehaviour();
    }

    public void SetMovementBehaviour(System.Type movementBehaviourType)
    {
        // Ensure the type is compatible
        if (typeof(IMovementBehaviour).IsAssignableFrom(movementBehaviourType)) {
            m_MovementBehaviour = (IMovementBehaviour)Activator.CreateInstance(movementBehaviourType);
        }
        else {
            Debug.LogError("Invalid Movement Behaviour Type!");
        }
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
    }
}
