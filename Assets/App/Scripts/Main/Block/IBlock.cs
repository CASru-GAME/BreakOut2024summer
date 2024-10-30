using App.Main.Player;
using App.Main.Stage;
using App.Common.Audio;

namespace App.Main.Block
{
    public interface IBlock
    {   
        StageSystem StageSystem{ get; set;}
        float PoisonStack{ get; set;}
        int WeaknessPoint{ get; set;}
        void AddWholeSeCollector(WholeSECollector wholeSeCollector);
        void TakeDamage(int damage);
        void SetStage(StageSystem StageSystem);
        void Healed(int healAmount);
        void AddPoisonStack(int stack);
        void AddWeaknessPoint(int point);
    }
}
