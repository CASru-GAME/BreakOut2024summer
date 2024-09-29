using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int Id{get; private set;}
    public int Weight{get; private set;}

    public ItemData(int id, int weight)
    {
        Id = id;
        Weight = weight;
    }
}
