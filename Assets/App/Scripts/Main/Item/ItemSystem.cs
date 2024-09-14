using UnityEngine;
using System.Collections.Generic;
using System;

namespace App.Main.Item
{
    public class ItemSystem
    {
        private int _allWeight;

        /// <summary>
        /// アイテムのIdと抽選時の重み
        /// </summary>
        
        public ItemData[] AllItems = new ItemData[]
        {
            new ItemData(0, 1),
            new ItemData(1, 2),
            new ItemData(2, 3),
        };
      
         public List<ItemData> ActiveItems= new List<ItemData>()
         {
            new ItemData(0, 1),
            new ItemData(1, 2),
         };
    
        private int rm;

        public ItemSystem()
        {   
            for(int i = 0; i < ActiveItems.Count; i++)
            _allWeight += ActiveItems[i].Weight; 
        }

        /// <summary>
        /// 新しいアイテムを解禁する
        /// </summary>
        /// <param name="id"></param>
        public void UnleashItem(int id)
        {   
            if(!ActiveItems.Contains(AllItems[id]))
            {
                ActiveItems.Add(AllItems[id]);
                _allWeight += AllItems[id].Weight;
            }
        }

        /// <summary>
        /// アイテムのIDを選ぶ
        /// </summary>
        public int SelectItem()
        {   
            rm = UnityEngine.Random.Range(1, _allWeight + 1);

            if(rm > _allWeight)
            {   
                throw new ArgumentException(" Value cannot be greater than _allWeight");
            }
          
            for(int i = 0; i < ActiveItems.Count; i++)
            {
                rm -= ActiveItems[i].Weight;

                if(rm <= 0) return ActiveItems[i].Id;
            }
            
            return 0;
        }
    }
}
