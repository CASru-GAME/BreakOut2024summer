using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Main.Player.Perk
{
    public class PerkIconPanel : MonoBehaviour
    {
        [SerializeField] private List<Sprite> perkSpriteList;

        public void Initialize(int perkId)
        {
            if(perkId == 0) return;
            GetComponent<Image>().sprite = perkSpriteList[perkId - 1];
        }
    }
}