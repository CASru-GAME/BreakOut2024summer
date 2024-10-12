using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Main.Block
{
    public class BlockFall : MonoBehaviour,IFall
    {   
        [SerializeField] private float _fallingSpeed;
        List<IFall> HitBlockList = new List<IFall>();
        

        void Start()
        {
            _fallingSpeed = 0.00016f;       
        }

        void Update()
        {
            //Fall();
        }

        
        public void Fall()
        {   
            HitBlockList.Remove(null);
            if(transform.parent.gameObject.transform.position.y > -2.8f && HitBlockList.Count == 0)
            transform.parent.gameObject.transform.position += new Vector3(0f,-_fallingSpeed,0f);         
        }

            void OnTriggerEnter2D(Collider2D other) 
            {   
                IFall block = other.GetComponent<IFall>();
                if(block != null && !HitBlockList.Contains(block))
                HitBlockList.Add(block);
            }

            void OnTriggerExit2D(Collider2D other) 
            {
                IFall block = other.GetComponent<IFall>();
                if(block != null && HitBlockList.Contains(block))
                HitBlockList.Remove(block);
            }
    }
}
