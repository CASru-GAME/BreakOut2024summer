using UnityEngine;

namespace App.Main.Block
{
    public class BlockDataStore : MonoBehaviour
    {   
        public BlockHp Hp { get; private set; }

        public void InitializeBlock(int initialHp)
        {
            Hp = new BlockHp(initialHp);
        }

        public void SetHp(BlockHp bhp)
        {
            Hp = bhp;
        }    
    }
}
