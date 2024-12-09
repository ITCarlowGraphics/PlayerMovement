using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExampleOne()
    {
        PlayerMovement[] playerMovements = GameObject.FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[];
        for (int i = 0; i < playerMovements.Length; i++)
        {
            playerMovements[i].SetExampleOne();
        }
    }

    public void ExampleTwo()
    {
        PlayerMovement[] playerMovements = GameObject.FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[];
        for (int i = 0; i < playerMovements.Length; i++)
        {
            playerMovements[i].SetExampleTwo();
        }
    }

    public void ExampleThree()
    {
        PlayerMovement[] playerMovements = GameObject.FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[];
        for (int i = 0; i < playerMovements.Length; i++)
        {
            playerMovements[i].SetExampleThree();
        }
    }
    public void ExampleFour()
    {
        PlayerMovement[] playerMovements = GameObject.FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[];
        for (int i = 0; i < playerMovements.Length; i++)
        {
            playerMovements[i].SetExampleFour();
        }
    }

    public void ExampleFive()
    {
        PlayerMovement[] playerMovements = GameObject.FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[];
        for (int i = 0; i < playerMovements.Length; i++)
        {
            playerMovements[i].SetExampleFive();
        }
    }

}
