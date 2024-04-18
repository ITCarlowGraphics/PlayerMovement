using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    public Transform Player0;
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;

    public List<Space> Spaces;

    private void Start()
    {
        if (instance)
            Destroy(instance.gameObject);
        instance = this;
        SetPreviousAndNext();
    }

    public void SetPreviousAndNext()
    {
        Transform boardSpaces = transform.GetChild(1);
        Space previous = null;
        for (int i = 0; i < boardSpaces.childCount; i++)
        {
            Space s = boardSpaces.GetChild(i).GetComponent<Space>();
            s.spaceNumber = i + 1;
            if (previous != null)
            {
                s.previous = previous;
            }
            if (i != boardSpaces.childCount - 1)
            {
                s.next = boardSpaces.GetChild(i + 1).GetComponent<Space>();
            }
            previous = s;
            Spaces.Add(s);
        }
    }

    public void Move(int player, int amount)
    {
        Transform playerToMove = null;
        switch (player)
        {
            case 0:
                playerToMove = Player0;
                break;
            case 1:
                playerToMove = Player1;
                break;
            case 2:
                playerToMove = Player2;
                break;
            case 3:
                playerToMove = Player3;
                break;
            default:
                return;
        }

        PlayerMovement pm = playerToMove.GetComponent<PlayerMovement>();
        if (amount > 0)
        {
            for (int i = 1; i == amount; i++)
            {
                pm.spacesToMove.Enqueue(Spaces[pm.space - 2 + i]);
            }
        }
        else
        {
            for (int i = 1; i == amount; i++)
            {
                pm.spacesToMove.Enqueue(Spaces[pm.space - 2 - i]);
            }
        }
    }
}
