using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Player.Perk
{
    public class PerkListPanel : MonoBehaviour
    {
        [SerializeField] private GameObject perkListPanel;
        private bool isShowing = false;
        
        private void Start()
        {
            perkListPanel.SetActive(false);
        }

        public void SetPerkListPanel()
        {
            if(!isShowing)
            {
                isShowing = true;
                perkListPanel.SetActive(true);
            }
            else
            {
                isShowing = false;
                perkListPanel.SetActive(false);
            }
        }

        
    }
}
