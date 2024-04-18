using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float speed = 10f;
    public int space = 1;
    public Queue<Space> spacesToMove = new Queue<Space>();

    private void Update()
    {
        if (spacesToMove.Count > 0)
        {
            move();
        }
    }

    void move()
    {
        if (spacesToMove.Peek().spaceNumber > space)
        {
            switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].side)
            {
                case Space.Side.Bottom:
                    transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                    break;
                case Space.Side.Right:
                    transform.Translate(-Vector3.left * Time.deltaTime * speed);
                    break;
                case Space.Side.Top:
                    transform.Translate(-Vector3.back * Time.deltaTime * speed);
                    break;
                case Space.Side.Left:
                    transform.Translate(-Vector3.right * Time.deltaTime * speed);
                    break;
                case Space.Side.Corner:
                    switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber + 1].side)
                    {
                        case Space.Side.Bottom:
                            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                            break;
                        case Space.Side.Right:
                            transform.Translate(-Vector3.left * Time.deltaTime * speed);
                            break;
                        case Space.Side.Top:
                            transform.Translate(-Vector3.back * Time.deltaTime * speed);
                            break;
                        case Space.Side.Left:
                            transform.Translate(-Vector3.right * Time.deltaTime * speed);
                            break;
                    }
                    break;
            }
        }
        else
        {
            switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].side)
            {
                case Space.Side.Bottom:
                    transform.Translate(-Vector3.back * Time.deltaTime * speed);
                    break;
                case Space.Side.Right:
                    transform.Translate(-Vector3.right * Time.deltaTime * speed);
                    break;
                case Space.Side.Top:
                    transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                    break;
                case Space.Side.Left:
                    transform.Translate(-Vector3.left * Time.deltaTime * speed);
                    break;
                case Space.Side.Corner:
                    switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber + 1].side)
                    {
                        case Space.Side.Bottom:
                            transform.Translate(-Vector3.back * Time.deltaTime * speed);
                            break;
                        case Space.Side.Right:
                            transform.Translate(-Vector3.right * Time.deltaTime * speed);
                            break;
                        case Space.Side.Top:
                            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                            break;
                        case Space.Side.Left:
                            transform.Translate(-Vector3.left * Time.deltaTime * speed);
                            break;
                    }
                    break;
            }
        }

        Vector3 posToCheck = new Vector3(BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].transform.position.x,
                                            transform.position.y,
                                            BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].transform.position.z);
        switch(BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber].side)
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
                switch (BoardManager.instance.Spaces[spacesToMove.Peek().spaceNumber + 1].side)
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
        //Debug.Log(Vector3.Distance(posToCheck, transform.position));
        if (Vector3.Distance(posToCheck, transform.position) < 0.1f)
        {
            space = spacesToMove.Dequeue().spaceNumber;
        }
    }
}
