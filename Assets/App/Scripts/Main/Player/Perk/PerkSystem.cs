using App.Main.Player;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace App.Main.Player.Perk
{
    public class PerkSystem
    {
        PerkList PerkList;
        int AmountPerk;

        private List<int> RandomPerkList = new List<int>();
        private GameObject PerkPanelPrefab;
        private GameObject PerkPanel1;
        private GameObject PerkPanel2;
        private GameObject PerkPanel3;

        public PerkSystem(PlayerDatastore playerDatastore , GameObject PerkPanelPrefab)
        {
            PerkList = new PerkList(playerDatastore);
            AmountPerk = PerkList.AmountPerk;
            this.PerkPanelPrefab = PerkPanelPrefab;
        }

        private void ChoseRandomPerk()
        {
            System.Random random = new System.Random();
            RandomPerkList.Clear();
            // AllPerkListからランダムで重複なしでPerkを3つ選択する
            while(RandomPerkList.Count < 3)
            {
                int PerkId = random.Next(0, AmountPerk);
                if(!RandomPerkList.Contains(PerkId))
                {
                    RandomPerkList.Add(PerkId);
                }
            }            
        }

        private void CreatePerkPanel()
        {
            // 3つのPerkを画面に表示する
            PerkPanel1 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(-5, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel1.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[0], this);

            PerkPanel2 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel2.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[1], this);

            PerkPanel3 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(5, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel3.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[2], this);
            
        }

        /// <summary>
        /// <para>ランダムでPerkを取得する</para>
        /// </summary>
        public void ChoosePerk()
        {
            ChoseRandomPerk();
            CreatePerkPanel();
        }

        public void SusideAll()
        {
            PerkPanel1.GetComponent<ChoosePerkPanel>().Suside();
            PerkPanel2.GetComponent<ChoosePerkPanel>().Suside();
            PerkPanel3.GetComponent<ChoosePerkPanel>().Suside();
        }

        public void GetPerk(int PerkId)
        {
            PerkList.GetPerk(PerkId);
        }

        /// <summary>
        /// <para>取得したPerkの効果を発動する。PerkのIDを引数として取る。IDはPerkListから確認してね</para>
        /// </summary>
        public void UsePerkEffects(int PerkId)
        {
            PerkList.AllPerkList[PerkId].Effect();
        }
    }
}