using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Main.Player.Perk
{
    public class ChoosePerkPanel : MonoBehaviour
    {
        private int PerkId = 0;
        private PerkSystem PerkSystem;
        [SerializeField] private GameObject perkIconPanelPos;
        [SerializeField] private GameObject perkIconPanelPrefab;
        [SerializeField] private Canvas canvas_Perk;

        //デバッグ用
        [SerializeField] private int debugPerkId;
        //

        public void Initialize(int perkId, PerkSystem perkSystem)
        {
            this.PerkId = perkId;
            this.PerkSystem = perkSystem;

            //デバッグ用
            if(debugPerkId != 0) this.PerkId = debugPerkId;
            //

            CreatePerkIconPanel();
        }

        private void CreatePerkIconPanel()
        {
            GameObject newPerkPanelPrefab = Instantiate(perkIconPanelPrefab, perkIconPanelPos.transform.position, Quaternion.identity);
            newPerkPanelPrefab.transform.SetParent(perkIconPanelPos.transform);
            newPerkPanelPrefab.transform.localScale = perkIconPanelPos.transform.localScale;
            newPerkPanelPrefab.GetComponent<PerkIconPanel>().Initialize(PerkId);
        }

        public void OnClick()
        {
            PerkSystem.GetPerk(PerkId);
            PerkSystem.SuicideAll();
        }

        public void Suside()
        {
            Destroy(this.gameObject);
        }
    }
}
