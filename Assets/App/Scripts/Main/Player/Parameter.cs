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

        public void AddLive(int value)
        {
            Live = Live.AddCurrentValue(new Live(value));
        }

        public void AddMaxLive(int value)
        {
            Live = Live.AddMaxValue(new Live(value));
        }
        public void SubtractLive(int value)
        {
            Live = Live.SubtractCurrentValue(new Live(value));
        }

        public void SubtractMaxLive(int value)
        {
            Live = Live.SubtractMaxValue(new Live(value));
        }
        public bool IsLiveValue(int value)
        {
            return Live.CurrentValue == value;
        }

        public int LiveValue()
        {
            return Live.CurrentValue;
        }

        public void AddAttackPoint(int value)
        {
            AttackPoint = AttackPoint.AddCurrentValue(new AttackPoint(value));
        }
        public void SubtractAttackPoint(int value)
        {
            AttackPoint = AttackPoint.SubtractCurrentValue(new AttackPoint(value));
        }
        public void MultiplyAttackPoint(double value)
        {
            AttackPoint = AttackPoint.MultiplyCurrentValue(value);
        }
        public void DivideAttackPoint(double value)
        {
            AttackPoint = AttackPoint.DivideCurrentValue(value);
        }

        public int AttackPointValue()
        {
            return AttackPoint.CurrentValue;
        }

        public void AddMoveSpeed(float value)
        {
            MoveSpeed = MoveSpeed.AddCurrentValue(new MoveSpeed(value));
        }
        public void SubtractMoveSpeed(float value)
        {
            MoveSpeed = MoveSpeed.SubtractCurrentValue(new MoveSpeed(value));
        }

        public float MoveSpeedValue()
        {
            return MoveSpeed.Speed;
        }

        public void ReplaceLevel(int value)
        {
            Level = new Level(value);
        }

        public int LevelValue()
        {
            return Level.CurrentValue;
        }


        public void AddExperiencePoint(int value)
        {
            ExperiencePoint = ExperiencePoint.AddCurrentValue(new ExperiencePoint(value));
        }


        public int ExperiencePointValue()
        {
            return ExperiencePoint.CurrentValue;
        }


    }
}
