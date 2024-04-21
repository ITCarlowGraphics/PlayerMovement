using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public enum Side
    {
        Left, Right, Top, Bottom, Corner
    };

    public bool corner = false;
    public Side side = Side.Bottom;
    public Space next;
    public int spaceNumber;
    public Space previous;
    public List<Transform> playersOnSpace = new List<Transform>(); // list of players currently on this space

    // add to this space
    public void AddPlayer(Transform player)
    {
        if (!playersOnSpace.Contains(player))
        {
            playersOnSpace.Add(player);
        }
    }

    // remove from this space
    public void RemovePlayer(Transform player)
    {
        if (playersOnSpace.Contains(player))
        {
            playersOnSpace.Remove(player);
        }
    }
}
