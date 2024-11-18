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

    [Header("Temporary Behaviour")]
    private CustomisableBehaviour temporaryBehaviour;
    private int temporaryMoves = 0;
    private bool temporaryBehaviourActive = false;

    public void SetBehaviour(CustomisableBehaviour behaviour)
    {
        currentBehaviour = behaviour;
    }

    public void SetTemporaryBehaviour(CustomisableBehaviour behaviour, int numberOfTempMoves)
    {
        temporaryBehaviour = behaviour;
        temporaryMoves = numberOfTempMoves;
        temporaryBehaviourActive = true;
    }

    public CustomisableBehaviour GetBehaviour() { return currentBehaviour; }

    public bool MovementComplete() => isMovementComplete;

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;
        startTime = Time.time;
        isMovementComplete = false;

        if(temporaryBehaviourActive)
        {
            temporaryBehaviour.SetStartAndEnd(start, end);
        }
        else
        {
            currentBehaviour.SetStartAndEnd(start, end);
        }
    }

    public void Update(Transform currentTransform)
    {
        if (isMovementComplete) return;

        if (startTime == 0) return;

        if(temporaryBehaviourActive)
        {
            UpdateTempBehaviour(currentTransform);
        }
        else
        {
            UpdateCurrentBehaviour(currentTransform);
        }
    }

    private void UpdateCurrentBehaviour(Transform currentTransform)
    {
        float elapsedTime = Time.time - startTime;

        duration = currentBehaviour.GetMovementDuration();
        float progress = Mathf.Clamp01(elapsedTime / duration);

        currentBehaviour.EvaluateTransform(currentTransform, progress);

        if (progress >= 1f)
        {
            isMovementComplete = true;
            startTime = 0;
        }
    }

    private void UpdateTempBehaviour(Transform currentTransform)
    {
        float elapsedTime = Time.time - startTime;

        duration = temporaryBehaviour.GetMovementDuration();
        float progress = Mathf.Clamp01(elapsedTime / duration);

        temporaryBehaviour.EvaluateTransform(currentTransform, progress);

        if (progress >= 1f)
        {
            temporaryMoves--;
        }

        if(temporaryMoves <= 0)
        {
            temporaryBehaviourActive = false;
            isMovementComplete = true;
            startTime = 0;
        }
    }


}
