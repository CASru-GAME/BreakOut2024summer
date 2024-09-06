using UnityEngine;
using App.Main.Player;
using UnityEngine.Events;

namespace App.Main.Item
{
    public class ItemTable : MonoBehaviour
    {
        public ItemEffect[] items;

        void Start()
        {   
            //1～10までは経験値用(仮)
            items = new ItemEffect[11];

            items[0] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("Debug");
            });
            items[1] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(1);//経験値小
            });
            items[2] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(3);//経験値中
            });
            items[3] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(5);//経験値大
            });
            items[4] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(10);//経験値特大
            });
            items[5] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(15);//経験値超特大
            });
            items[6] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("GetExp");
            });
            items[7] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("GetExp");
            });
            items[8] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("GetExp");
            });
            items[9] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("GetExp");
            });
            items[10] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("GetExp");
            });
        }

        public class ItemEffect
        {
            public UnityAction<PlayerDatastore> effect;
            public ItemEffect(UnityAction<PlayerDatastore> effect)
            {
                this.effect = effect;
            }
        }
    }
}


