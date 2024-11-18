using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int space = 1;
    public Queue<Space> spacesToMove = new Queue<Space>();

    private int currentSpaceNumber = -1;

    MovementController movementController = new MovementController();

    private void Start()
    {
        CustomisableBehaviour custom = new HopBehaviour();
        //custom.SetScaleFunctions("x", CustomisableBehaviour.Pulse);
        //custom.SetScaleFunctions("z", CustomisableBehaviour.Pulse);
        
        movementController.SetBehaviour(custom);
    }

    private void Update()
    {

        if (spacesToMove.Count > 0)
        {
            move();
        }

        //Deubg
        if (Input.GetKeyDown(KeyCode.P))
        {
            int spaceNumber = 30;
            MoveToSpace(30);
        }

        movementController.Update(transform);

    }

    public void MoveToSpace(int spaceNumber)
    {
        Space targetSpace = BoardManager.instance.Spaces[spaceNumber - 1];
        Vector3 spacePos = CalculateTargetPosition(targetSpace);
        Vector3 startPosition = transform.position;

        CustomisableBehaviour tempBehaviour = new MoveAcrossBoardBehaviour();
        movementController.SetTemporaryBehaviour(tempBehaviour, 1);
        space = spaceNumber;
        movementController.SetStartAndEnd(startPosition, spacePos);
    }

    void move()
    {
        if (spacesToMove.Count > 0)
        {

            Space targetSpace = spacesToMove.Peek();
            if (currentSpaceNumber == -1 || currentSpaceNumber != targetSpace.spaceNumber)
            {
                currentSpaceNumber = targetSpace.spaceNumber;

                Vector3 targetPosition = CalculateTargetPosition(targetSpace);
                Vector3 startPosition = transform.position;

                movementController.SetStartAndEnd(startPosition, targetPosition);
            }


            if (movementController.MovementComplete())
            {
                spacesToMove.Dequeue();
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
