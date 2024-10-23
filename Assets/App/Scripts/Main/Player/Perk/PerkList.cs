using System.Collections.Generic;
using UnityEngine;
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
                new Drill(playerDatastore),//ID:8
                new LotteryTicket(playerDatastore),//ID:9
                new ValuePack(playerDatastore),//ID:10
                new FamilyPhoto(playerDatastore),//ID:11
                new Dumbbell(playerDatastore),//ID:12
                new Pot(playerDatastore),//ID:13
                new YellowSubmarine(playerDatastore),//ID:14
                new TenchiMuyo(playerDatastore),//ID:15
                new WatermelonSeeds(playerDatastore),//ID:16
                new CristmasSocks(playerDatastore),//ID:17
                new ManekiCat(playerDatastore),//ID:18
                new Fireworks(playerDatastore),//ID:19
                new Tepra(playerDatastore),//ID:20
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

        public List<(int id, int stackCount)> GetOwnedPerkList()
        {
            List<(int id, int stackCount)> value = new List<(int id, int stackCount)>();
            for (int i = 0; i < OwnedPerkList.Count; i++)
            {
                value.Add((OwnedPerkList[i].GetId(), OwnedPerkList[i].GetStackCount()));
            }
            return value;
        }

        public void LoadPerkList(List<(int id, int stackCount)> PerkIdList)
        {
            for (int i = 0; i < PerkIdList.Count; i++)
            {
                for (int j = 0; j < PerkIdList[i].stackCount; j++)
                {
                    AllPerkList[PerkIdList[i].id].AddStackCount();

                }
                OwnedPerkList.Add(AllPerkList[PerkIdList[i].id]);
            }
            PerkEffectWhenLoad();
        }

        private void PerkEffectWhenLoad()
        {
            AllPerkList[5].IntEffect();
        }
    }
}