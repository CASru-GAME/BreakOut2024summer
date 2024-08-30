namespace App.Main.Player
{
    public class Parameter
    {
        public Live Live { get; private set; }
        public AttackPoint AttackPoint { get; private set; }
        public MoveSpeed MoveSpeed { get; private set; }
        public Level Level { get; private set; }
        public ExperiencePoint ExperiencePoint { get; private set; }

        public Parameter(int live,int attackPoint, float moveSpeed, int level , int experiencePoint)
        {
            Live = new Live(live);
            AttackPoint = new AttackPoint(attackPoint);
            MoveSpeed = new MoveSpeed(moveSpeed);
            Level = new Level(level);
            ExperiencePoint = new ExperiencePoint(experiencePoint);
        }
    }
}
