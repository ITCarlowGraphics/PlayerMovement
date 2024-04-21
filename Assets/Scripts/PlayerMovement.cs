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
        Vector3 dir;
        if (rollDirectionBackwards.Peek() == false)
        {
            dir = GetDirection(spacesToMove.Peek().spaceNumber, false);
        }
        else
        {
            dir = -GetDirection(spacesToMove.Peek().spaceNumber, true);
        }

        Vector3 posToCheck = new Vector3(BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber - 1].transform.position.x,
                                            transform.position.y,
                                            BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber - 1].transform.position.z);

        switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber - 1].side)
        {
            case Space.Side.Bottom:
                posToCheck.x = transform.position.x;
                break;
            case Space.Side.Top:
                posToCheck.x = transform.position.x;
                break;
            case Space.Side.Left:
                posToCheck.x = transform.position.z;
                break;
            case Space.Side.Right:
                posToCheck.x = transform.position.z;
                break;
            case Space.Side.Corner:
                Space.Side s;
                if (rollDirectionBackwards.Peek() == false)
                {
                    s = BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber - 2].side;
                }
                else
                {
                    s = BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].side;
                }
                switch (s)
                {
                    case Space.Side.Bottom:
                        posToCheck.x = transform.position.x;
                        break;
                    case Space.Side.Top:
                        posToCheck.x = transform.position.x;
                        break;
                    case Space.Side.Left:
                        posToCheck.x = transform.position.z;
                        break;
                    case Space.Side.Right:
                        posToCheck.x = transform.position.z;
                        break;
                }
                break;
        }

        transform.Translate(dir * Time.deltaTime * speed);

        if (Vector3.Distance(posToCheck, transform.position) < 0.1f)
        {
            spacesToMove.Dequeue();
            rollDirectionBackwards.Dequeue();
        }
    }

    Vector3 GetDirection(int _space, bool backwards)
    {
        switch (BoardManager.instance.Spaces[_space - 1].side)
        {
            case Space.Side.Bottom:
                return new Vector3(0, 0, -1);
            case Space.Side.Right:
                return new Vector3(1, 0, 0);
            case Space.Side.Top:
                return new Vector3(0, 0, 1);
            case Space.Side.Left:
                return new Vector3(-1, 0, 0);
            case Space.Side.Corner:
                if (!backwards)
                    return GetDirection(_space - 1, false);
                else
                    return GetDirection(_space + 1, true);
            default:
                return Vector3.down;
        }
    }

}
