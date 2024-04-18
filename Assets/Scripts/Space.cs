using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public enum Side {
        Left, Right, Top, Bottom, Corner
    };

    public bool corner = false;
    public Side side = Side.Bottom;
    public Space next;
    public int spaceNumber;
    public Space previous;
}
