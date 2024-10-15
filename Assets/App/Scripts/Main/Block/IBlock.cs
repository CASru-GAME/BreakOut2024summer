using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Block
{
    public interface IBlock
    {   
        StageSystem stageSystem{ get; set;}
        void TakeDamage(int damage);
        void SetStage(StageSystem stage);
        void Healed(int healAmount);
        void AddPoisonStack(int stack);
        void AddWeaknessPoint(int point);
    }
}
