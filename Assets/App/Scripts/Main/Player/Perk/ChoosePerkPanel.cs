using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Main.Player.Perk
{
    public class ChoosePerkPanel : MonoBehaviour
    {
        private int PerkId = 0;
        private PerkSystem PerkSystem;

        public void Initialize(int PerkId, PerkSystem PerkSystem)
        {
            this.PerkId = PerkId;
            this.PerkSystem = PerkSystem;
        }

        /*void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    PerkSystem.GetPerk(PerkId);
                    PerkSystem.SusideAll();
                    
                }
            }
        }*/

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
