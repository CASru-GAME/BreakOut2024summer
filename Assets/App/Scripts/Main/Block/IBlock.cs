using App.Main.Player;
using App.Main.Stage;

namespace App.Main.Block
{
    public interface IBlock
    {   
        StageSystem StageSystem{ get; set;}
        void TakeDamage(int damage);
        void SetStage(StageSystem StageSystem);
        void Healed(int healAmount);
        void AddPoisonStack(int stack);
        void AddWeaknessPoint(int point);
    }
}
