using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementController
{
    private CustomisableBehaviour currentBehaviour;
    private Vector3 startPosition, endPosition;
    private float startTime, duration;
    private bool isMovementComplete = false;

    public void SetBehaviour(CustomisableBehaviour behaviour) => currentBehaviour = behaviour;
    public void SetMovementDuration(float movementDuration) => duration = movementDuration;
    public bool MovementComplete() => isMovementComplete;

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;
        startTime = Time.time;
        isMovementComplete = false;

        currentBehaviour.SetStartAndEnd(start, end);
    }

    public void Update(Transform currentTransform)
    {
        if (isMovementComplete) return;

        float elapsedTime = Time.time - startTime;
        float progress = Mathf.Clamp01(elapsedTime / duration);

        currentBehaviour.EvaluateTransform(currentTransform, progress);

        if (progress >= 1f)
        {
            isMovementComplete = true;
        }
    }
}
