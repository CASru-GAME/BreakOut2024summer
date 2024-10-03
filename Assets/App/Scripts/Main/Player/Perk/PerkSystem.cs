using App.Main.Player;
using System;
using System.Collections.Generic;
using System.Collections;
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
        private System.Random random = new System.Random(); // Move Random instance to class level

        public bool IsPerkChoosing = false;

        public PerkSystem(PlayerDatastore playerDatastore, GameObject PerkPanelPrefab)
        {
            PerkList = new PerkList(playerDatastore);
            AmountPerk = PerkList.AmountPerk;
            this.PerkPanelPrefab = PerkPanelPrefab;

            PerkPanel1 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(-5, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel1.SetActive(false);

            PerkPanel2 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel2.SetActive(false);

            PerkPanel3 = UnityEngine.Object.Instantiate(
                PerkPanelPrefab,
                new Vector3(5, 0, 0),
                Quaternion.identity
            ) as GameObject;
            PerkPanel3.SetActive(false);
        }

        private void ChoseRandomPerk()
        {
            RandomPerkList.Clear();
            // AllPerkListからランダムで重複なしでPerkを3つ選択する
            while (RandomPerkList.Count < 3)
            {
                int PerkId = random.Next(0, AmountPerk);
                if (!RandomPerkList.Contains(PerkId))
                {
                    RandomPerkList.Add(PerkId);
                }
            }
        }



        private void CreatePerkPanel()
        {
            // 3つのPerkを画面に表示する
            PerkPanel1.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[0], this);
            PerkPanel2.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[1], this);
            PerkPanel3.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[2], this);
            PerkPanel1.SetActive(true);
            PerkPanel2.SetActive(true);
            PerkPanel3.SetActive(true);
        }



        /// <summary>
        /// <para>ランダムでPerkを取得する</para>
        /// </summary>
        public IEnumerator ChoosePerk()
        {
            
            UnityEngine.Debug.Log("ChoosePerk");
            IsPerkChoosing = true;
            UnityEngine.Time.timeScale = 0;
            ChoseRandomPerk();
            CreatePerkPanel();
            yield return new WaitUntil(() => IsPerkChoosing == false);
            
            
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