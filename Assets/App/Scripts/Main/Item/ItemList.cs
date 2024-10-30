using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Item
{
    public class ItemList
    {   
        public ItemData[] AllExps = new ItemData[]
        {
            new ItemData(1, 1),
            new ItemData(2, 2),
            new ItemData(3, 3),
            new ItemData(4, 4),
            new ItemData(5, 5),
        };
      
        public List<ItemData> ActiveExps= new List<ItemData>()
        {
            new ItemData(1, 1),
            new ItemData(1, 1),
            new ItemData(1, 1),
            new ItemData(1, 1),
            new ItemData(1, 1),
            new ItemData(1, 1),
        };

        public List<ItemData> AllItems = new List<ItemData>()
        {
            new ItemData(11, 7),
            new ItemData(12, 1),
            new ItemData(13, 6),    
            new ItemData(14, 0),
            new ItemData(15, 6),
            new ItemData(16, 6),
        };
        public List<ItemData> ActiveItems = new List<ItemData>()
        {
            new ItemData(11, 6),
            new ItemData(12, 1),
            new ItemData(13, 4),
        };  
        public List<ItemData> OwnedItems = new List<ItemData>();
    }
}
