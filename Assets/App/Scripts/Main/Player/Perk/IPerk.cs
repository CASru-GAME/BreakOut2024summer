namespace App.Main.Player.Perk
{
    public interface IPerk
    {
        void AddStackCount();
        int GetStackCount();
        void Effect();
        int GetId();
        int IntEffect();
        float FloatEffect();
    }
}