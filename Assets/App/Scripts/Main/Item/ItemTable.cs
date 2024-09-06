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
            items = new ItemEffect[2];

            items[0] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("Debug");
            });
            items[1] = new ItemEffect((PlayerDatastore playerDatastore) =>
            {
                Debug.Log("ActivateItem");
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


