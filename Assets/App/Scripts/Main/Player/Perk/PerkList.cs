using System.Collections.Generic;
using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Player.Perk
{
    public class PerkList
    {
        public int AmountPerk;
        PlayerDatastore playerDatastore;
        public List<IPerk> OwnedPerkList = new List<IPerk>();
        public readonly IPerk[] AllPerkList;
        private ProcessSystem processSystem;

        public PerkList(PlayerDatastore playerDatastore, ProcessSystem processSystem)
        {
            this.playerDatastore = playerDatastore;
            AllPerkList = new IPerk[]
            {
                new DebugPerk(),//ID:0
                new WireCage(playerDatastore),//ID:1
                new LateBloomer(playerDatastore, processSystem),//ID:2
                new Matatabi(playerDatastore),//ID:3
                new IncreaseBallSpeed(playerDatastore),//ID:4
                new OptionParts(playerDatastore),//ID:5
                new Amaryllis(playerDatastore),//ID:6
                new Konjac(playerDatastore),//ID:7
                new DebugPerk(),//ID:8
                new LotteryTicket(playerDatastore),//ID:9
                new ValuePack(playerDatastore),//ID:10
                new FamilyPhoto(playerDatastore),//ID:11
                new DebugPerk(),//ID:12
                new DebugPerk(),//ID:13
                new DebugPerk(),//ID:14
                new DebugPerk(),//ID:15
                new DebugPerk(),//ID:16
                new DebugPerk(),//ID:17
                new ManekiCat(playerDatastore),//ID:18
                new DebugPerk(),//ID:19
                new DebugPerk(),//ID:20
                new Amulet(playerDatastore),//ID:21
                new LastStand(playerDatastore)//ID:22
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
            int[,] value = new int[23, 2];
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    value[i, j] = -1;
                }
            }
            for (int i = 0; i < AllPerkList.Length; i++)
            {
                value[i, 0] = AllPerkList[i].GetStackCount();
                for(int j = 0; j < OwnedPerkList.Count; j++)
                {
                    if (AllPerkList[i].GetId() == OwnedPerkList[j].GetId())
                    {
                        value[i, 1] = j;
                    }
                }

            }
            return value;
        }

        public void LoadPerkList(int[,] value)
        {
            for (int i = 0; i < AllPerkList.Length; i++)
            {
                if (value[i, 0] != -1)
                {
                    for (int j = 0; j < value[i, 0]; j++)
                    {
                        AllPerkList[j].AddStackCount();
                    }
                    //　OwnedPerkListに取得順で戻す

                    for(int j = 0; j < AllPerkList.Length; j++)
                    {
                        if(value[i, 1] == j)
                        {
                            OwnedPerkList.Add(AllPerkList[i]);
                        }
                    }
                }
            }


        }
    }
}