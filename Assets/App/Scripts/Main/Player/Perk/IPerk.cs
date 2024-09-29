namespace App.Main.Player.Perk
{
    public interface IPerk
    {
        void AddStackCount();
        int GetStackCount();
        void Effect();
    }
}