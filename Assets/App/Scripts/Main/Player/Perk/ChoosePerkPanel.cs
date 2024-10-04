using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Main.Player.Perk
{
    public class ChoosePerkPanel : MonoBehaviour
    {
        private int PerkId = 0;
        private PerkSystem PerkSystem;
       
        void Start()
        {

        }

        public void Initialize(int PerkId, PerkSystem PerkSystem)
        {
            this.PerkId = PerkId;
            this.PerkSystem = PerkSystem;

        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    PerkSystem.GetPerk(PerkId);
                    PerkSystem.IsPerkChoosing = false;
                    UnityEngine.Time.timeScale = 1;
                    PerkSystem.SusideAll();
                    
                }
            }
        }

        public void Suside()
        {
            this.gameObject.SetActive(false);
        }
    }
}
