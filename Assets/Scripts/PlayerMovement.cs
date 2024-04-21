using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float speed = 10f;
    public int space = 1;
    public Queue<Space> spacesToMove = new Queue<Space>();
    public Queue<bool> rollDirectionBackwards = new Queue<bool> ();

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
            Space targetSpace = spacesToMove.Peek();
            Vector3 targetPosition = CalculateTargetPosition(targetSpace);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
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
