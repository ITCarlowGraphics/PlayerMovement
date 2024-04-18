using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buttontest : MonoBehaviour
{
    public TMP_InputField input;
    int player = 0;
    
    public void movepsaces()
    {
        string inputtext = input.text;
        int num = int.Parse(inputtext);
        if (num > 0 && num <= 6 ||
            num < 0 && num >= -6)
        {
            BoardManager.instance.Move(player, num);
        }
    }

    public void changeplayer()
    {
        player++;
        if (player > 3)
            player = 0;
    }
}
