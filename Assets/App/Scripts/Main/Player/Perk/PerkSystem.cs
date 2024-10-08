using App.Main.Player;
using App.Main.Stage;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace App.Main.Player.Perk
{
    public class PerkSystem
    {
        public PerkList PerkList;
        int AmountPerk;

        private List<int> RandomPerkList = new List<int>();
        private ProcessSystem ProcessSystem;
        /*private GameObject PerkPanelPrefab;
        private GameObject PerkPanel1;
        private GameObject PerkPanel2;
        private GameObject PerkPanel3;
        */
        private System.Random random = new System.Random(); // Move Random instance to class level

        public bool IsPerkChoosing = false;

        private Canvas perkCanvas;
        private List<GameObject> perkPanelList;
        private PlayerDatastore playerDatastore;
        private ProcessSystem processSystem;

        /*public PerkSystem(PlayerDatastore playerDatastore, GameObject PerkPanelPrefab)
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
        }*/
        public PerkSystem(PlayerDatastore playerDatastore, Canvas perkCanvas, List<GameObject> perkPanelList, ProcessSystem processSystem)
        {
            this.processSystem = processSystem;
            PerkList = new PerkList(playerDatastore, processSystem);
            AmountPerk = PerkList.AmountPerk;
            this.playerDatastore = playerDatastore;
            this.perkCanvas = perkCanvas;
            this.perkPanelList = perkPanelList;
        }

        private void ChoseRandomPerk()
        {
            RandomPerkList.Clear();
            // AllPerkListからランダムで重複なしでPerkを3つ選択する
            var PerkIDList = new List<int>();
            for (int i = 1; i < AmountPerk; i++)
            {
                PerkIDList.Add(i-1);
            }
            PerkIDList = PerkIDList.OrderBy(a => Guid.NewGuid()).ToList();
            for(int i = 0; i < 3; i++)
            {
                RandomPerkList.Add(PerkIDList[i]);
            }
        }



        private void CreatePerkPanel()
        {
            // 3つのPerkを画面に表示する
            /*PerkPanel1.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[0], this);
            PerkPanel2.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[1], this);
            PerkPanel3.GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[2], this);
            PerkPanel1.SetActive(true);
            PerkPanel2.SetActive(true);
            PerkPanel3.SetActive(true);*/
            for(int i = 0; i < 3; i++)
            {
                perkPanelList[i].GetComponent<ChoosePerkPanel>().Initialize(RandomPerkList[i], this);
            }
        }



        /// <summary>
        /// <para>ランダムでPerkを取得する</para>
        /// </summary>
        public IEnumerator ChoosePerk()
        {
            perkCanvas.enabled = true;
            UnityEngine.Debug.Log("ChoosePerk");
            IsPerkChoosing = true;
            UnityEngine.Time.timeScale = 0;
            ChoseRandomPerk();
            CreatePerkPanel();
            yield return new WaitUntil(() => IsPerkChoosing == false);
            
            
        }

        public void SuicideAll()
        {
            /*PerkPanel1.GetComponent<ChoosePerkPanel>().Suside();
            PerkPanel2.GetComponent<ChoosePerkPanel>().Suside();
            PerkPanel3.GetComponent<ChoosePerkPanel>().Suside();*/
            perkCanvas.enabled = false;
            UnityEngine.Time.timeScale = 1;
        }

        public void GetPerk(int PerkId)
        {
            PerkList.GetPerk(PerkId);
            EffectWhenAcquiredPerk(PerkId);
            Debug.Log("PerkId: " + PerkId);
        }

        private void EffectWhenAcquiredPerk(int PerkId)
        {
            if(PerkId == 2)
            {
                UsePerkEffects(2);
            }
            if(PerkId == 4)
            {
                UsePerkEffects(4);
            }
            if(PerkId == 8)
            {
                UsePerkEffects(8);
            }
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