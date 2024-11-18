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
            default: return; // invalid player case
        }

        PlayerMovement pm = playerToMove.GetComponent<PlayerMovement>();
        if (pm == null) return; // no player controller found

        int totalSpaces = Spaces.Count;
        if (pm.space < 0 || pm.space >= totalSpaces)
        {
            // Invalid current space index
            Debug.LogError($"Invalid space index for player {player}: {pm.space}");
            return;
        }

        int currentIndex = pm.space; // No conversion needed, already 0-based
        int newIndex = currentIndex + amount;

        // Clamp the newIndex to 0 if it goes below 0
        if (newIndex < 0)
        {
            newIndex = 0;
        }
        else
        {
            newIndex %= totalSpaces; // Handle wrapping only if newIndex >= 0
        }

        Space currentSpace = Spaces[currentIndex];
        Space newSpace = Spaces[newIndex];

        currentSpace.RemovePlayer(playerToMove); // Remove from the old space
        newSpace.AddPlayer(playerToMove); // Add to the new space

        pm.spacesToMove.Clear(); // Clear existing movement queue

        // Add spaces to the queue for animation/movement
        bool isMovingBackwards = amount < 0;
        int step = isMovingBackwards ? -1 : 1;

        // Traverse the spaces correctly, stopping at newIndex
        for (int i = currentIndex; i != newIndex; i += step)
        {
            if (i < 0 || i >= totalSpaces) break; // Avoid out-of-bounds traversal
            pm.spacesToMove.Enqueue(Spaces[i]);
        }

        // Add the final destination to the queue
        pm.spacesToMove.Enqueue(newSpace);

        // Update the player's current space
        pm.space = newIndex; // No conversion needed, still 0-based
    }



}