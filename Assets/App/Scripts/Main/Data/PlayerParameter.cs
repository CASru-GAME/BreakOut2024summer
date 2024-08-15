using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerParameter : MonoBehaviour
{
    public int attackPoint;
    public float moveSpeed;

    public PlayerParameter(int attackPointValue, float moveSpeedValue)
    {
        this.attackPoint = attackPointValue;
        this.moveSpeed = moveSpeedValue;
    }

    private static PlayerParameter instance;

    public static PlayerParameter Instance
    {
        get
        {
            if (instance == null)
            {
                // Provide default values for the constructor parameters
                instance = new PlayerParameter(10, 5.0f); // Example default values
            }
            return instance;
        }
    }
}