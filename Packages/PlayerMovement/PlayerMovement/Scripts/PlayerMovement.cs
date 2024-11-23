using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float speed = 5f;
    public int space = 1;
    public Queue<Space> spacesToMove = new Queue<Space>();
    public Queue<bool> rollDirectionBackwards = new Queue<bool> ();

    private int currentSpaceNumber = -1;
    MovementController movementController = new MovementController();

    private void Start()
    {
        CustomisableBehaviour custom = new HopBehaviour();
        float movementDuration = 0.5f;

        movementController.SetBehaviour(custom);
        movementController.SetMovementDuration(movementDuration);
    }

    private void Update()
    {
        if (spacesToMove.Count > 0)
        {
            move();
        }
    }

    void move()
    {
        if (spacesToMove.Count > 0)
        {
            movementController.Update();

            Space targetSpace = spacesToMove.Peek();
            if (currentSpaceNumber == -1 || currentSpaceNumber != targetSpace.spaceNumber)
            {
                currentSpaceNumber = targetSpace.spaceNumber;
                Vector3 targetPosition = CalculateTargetPosition(targetSpace);
                Vector3 startPosition = transform.position;

                movementController.SetStartAndEnd(startPosition, targetPosition);
            }

            transform.position = movementController.GetCurrentPosition();

            if (movementController.MovementComplete())
            {
                spacesToMove.Dequeue();
                rollDirectionBackwards.Dequeue();
            }
        }

    }


    Vector3 CalculateTargetPosition(Space targetSpace)
    {
        List<Transform> playersOnSpace = targetSpace.playersOnSpace;
        if (playersOnSpace.Count == 0)
        {
            // central position as fallback
            return targetSpace.transform.position;
        }

        Vector3 basePosition = new Vector3(targetSpace.transform.position.x, transform.position.y, targetSpace.transform.position.z);
        int index = playersOnSpace.IndexOf(transform);
        if (index == -1)
        {
            // central position as fallback
            return basePosition;
        }

        // offset for multiple on one space
        float spacing = 1.4f; // spacing between each player
        float positionOffset = (index - playersOnSpace.Count / 2.0f) * spacing;

        Vector3 offset = Vector3.zero;
        switch (targetSpace.side)
        {
            case Space.Side.Left:
            case Space.Side.Right:
                // vertically
                offset = new Vector3(0, 0, positionOffset);
                break;
            case Space.Side.Top:
            case Space.Side.Bottom:
                // horizontally
                offset = new Vector3(positionOffset, 0, 0);
                break;
        }

        return basePosition + offset;
    }
}
