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
        Transform playerToMove;
        switch (player)
        {
            case 0: playerToMove = Player0; break;
            case 1: playerToMove = Player1; break;
            case 2: playerToMove = Player2; break;
            case 3: playerToMove = Player3; break;
            default: return; // case for invalid
        }

        PlayerMovement pm = playerToMove.GetComponent<PlayerMovement>();
        if (pm == null) return; // no playerc ontroller found

        Space currentSpace = Spaces[pm.space - 1]; //current space in index form
        Space newSpace = Spaces[(pm.space - 1 + amount) % Spaces.Count]; // space it is going towards

        currentSpace.RemovePlayer(playerToMove); // remove olld
        newSpace.AddPlayer(playerToMove); // add new

        pm.spacesToMove.Clear(); // clear existing queues
        pm.rollDirectionBackwards.Clear();

        bool isMovingBackwards = amount < 0;
        int step = isMovingBackwards ? -1 : 1; // if backwwards or not
        int newSpaceIndex = (pm.space - 1 + amount) % Spaces.Count; // new space in index form
        for (int i = pm.space - 1; i != newSpaceIndex; i += step)
        {
            if (i >= Spaces.Count) i -= Spaces.Count; // if wrapped (this shouldn't happen)
            if (i < 0) i += Spaces.Count; // same as above

            pm.spacesToMove.Enqueue(Spaces[i]);
            pm.rollDirectionBackwards.Enqueue(isMovingBackwards);
        }

        // final destination
        pm.spacesToMove.Enqueue(newSpace);
        pm.rollDirectionBackwards.Enqueue(isMovingBackwards);

        // update player
        pm.space = newSpaceIndex + 1;
    }
}