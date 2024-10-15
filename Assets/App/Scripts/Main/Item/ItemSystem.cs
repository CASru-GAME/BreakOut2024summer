using UnityEngine;
using System.Collections.Generic;
using System;
using App.Main.Player;

namespace App.Main.Item
{
    public class ItemSystem
    {   
        private int _allExpWeight;
        private int _allItemWeight;
        private ItemList ItemList;

        /// <summary>
        /// アイテムのIdと抽選時の重み
        /// </summary>
        

        private int rm;

        public ItemSystem(PlayerDatastore playerDatastore)
        {   
            ItemList = playerDatastore.ItemList;
            for(int i = 0; i < ItemList.AllItems.Count; i++)
            {   
                _allExpWeight += ItemList.ActiveExps[i].Weight;
                _allItemWeight += ItemList.AllItems[i].Weight; 
            }
        }

        /// <summary>
        /// 新しいExpを解禁する
        /// </summary>
        /// <param name="id"></param>
        public void UnleashExp(int id)
        {   
            if(!ItemList.ActiveExps.Contains(ItemList.AllExps[id]))
            {
                ItemList.ActiveExps.Add(ItemList.AllExps[id]);
                _allExpWeight += ItemList.AllExps[id].Weight;
            }
        }
        /// <summary>
        /// 新しいアイテムを解禁する
        /// </summary>
        /// <param name="id"></param>
        public void UnleashItem(int id)
        {   
            if(!ItemList.ActiveItems.Contains(ItemList.AllItems[id]))
            {
                ItemList.ActiveItems.Add(ItemList.AllItems[id]);
                _allItemWeight += ItemList.AllItems[id].Weight;
            }
        }

        /// <summary>
        /// ExpのIDを選ぶ
        /// </summary>
        public int SelectExp()
        {   
            rm = UnityEngine.Random.Range(1, _allExpWeight + 1);

            if(rm > _allExpWeight)
            {   
                throw new ArgumentException(" Value cannot be greater than _allWeight");
            }
          
            for(int i = 0; i < ItemList.ActiveExps.Count; i++)
            {
                rm -= ItemList.ActiveExps[i].Weight;

                if(rm <= 0) return ItemList.ActiveExps[i].Id;
            }
            
            return 0;
        }
        /// <summary>
        /// アイテムのIDを選ぶ
        /// </summary>
        public int SelectItem()
        {   
            rm = UnityEngine.Random.Range(1, _allItemWeight + 1);

            if(rm > _allItemWeight)
            {   
                throw new ArgumentException(" Value cannot be greater than _allWeight");
            }
          
            for(int i = 0; i < ItemList.AllItems.Count; i++)
            {
                rm -= ItemList.AllItems[i].Weight;

                if(rm <= 0) return ItemList.AllItems[i].Id;
            }
            
            return 0;
        }
    }
}
