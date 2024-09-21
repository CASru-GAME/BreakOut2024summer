using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Block
{
    public interface IBlock
    {   
        void TakeDamage(AttackPoint damage);
        void SetStage(StageSystem stage);
    }
}
