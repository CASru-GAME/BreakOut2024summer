using System.Collections.Generic;
using App.Main.Player;

namespace App.Main.Player.Perk
{
    public class PerkList
    {
        public int AmountPerk;
        PlayerDatastore playerDatastore;
        public List<IPerk> OwnedPerkList = new List<IPerk>();
        public readonly IPerk[] AllPerkList;

        public PerkList(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
            AllPerkList = new IPerk[]
            {
                new DebugPerk(),//ID:0
                new Strength(playerDatastore),//ID:1
                new IncreaseMoveSpeed(playerDatastore),//ID:2
                new LateBloomer(playerDatastore),//ID:3
                new LastStand(playerDatastore),//ID:4
            };
            AmountPerk = AllPerkList.Length;
        }

        public void GetPerk(int PerkId)
        {
            if (!OwnedPerkList.Contains(AllPerkList[PerkId]))
            {
                OwnedPerkList.Add(AllPerkList[PerkId]);
            }
            AllPerkList[PerkId].AddStackCount();
        }

        public int[,] GetOwnedPerkList()
        {
            int[,] value = new int[22, 2];
            for (int i = 0; i < AllPerkList.Length; i++)
            {
                value[i, 0] = AllPerkList[i].GetStackCount();
            }
            for (int i = 0; i < OwnedPerkList.Count; i++)
            {
                value[OwnedPerkList[i].GetId(), 1] = i;
            }
            return value;
        }

        public void LoadPerkList(List<IPerk> list)
        {
            OwnedPerkList.Clear();
            OwnedPerkList = list;
        }
    }
}