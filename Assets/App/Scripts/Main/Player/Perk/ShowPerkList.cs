using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Player.Perk
{
    public class ShowPerkList : MonoBehaviour
    {
        [SerializeField] private GameObject perkListPanel;
        
        private void Start()
        {
            perkListPanel.SetActive(false);
        }

        public void ShowPerkListPanel()
        {
            perkListPanel.SetActive(true);
        }

        public void HidePerkListPanel()
        {
            perkListPanel.SetActive(false);
        }
    }
}
