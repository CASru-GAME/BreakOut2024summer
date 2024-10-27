namespace App.Main.Player
{
    public class Parameter
    {
        public Live Live { get; private set; }
        public AttackPoint AttackPoint { get; private set; }
        public BallSpeed BallSpeed { get; private set; }
        public MoveSpeed MoveSpeed { get; private set; }
        public Level Level { get; private set; }
        public ExperiencePoint ExperiencePoint { get; private set; }
        public ComboCount ComboCount { get; private set; }
        public DumbbellPower DummbellPower { get; private set; }

        public Parameter(int live, int attackPoint, float ballSpeed, float moveSpeed, int level , int experiencePoint, int dumbbellPower)
        {
            Live = new Live(live);
            AttackPoint = new AttackPoint(attackPoint);
            BallSpeed = new BallSpeed(ballSpeed);
            MoveSpeed = new MoveSpeed(moveSpeed);
            Level = new Level(level);
            ExperiencePoint = new ExperiencePoint(experiencePoint);
            ComboCount = new ComboCount(0);
            DummbellPower = new DumbbellPower(dumbbellPower);
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

        public int GetLiveValue()
        {
            return Live.CurrentValue;
        }

        public int GetMaxLiveValue()
        {
            return Live.MaxValue;
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

        public int GetAttackPointValue()
        {
            return AttackPoint.CurrentValue;
        }

        public void AddBallSpeed(float value)
        {
            BallSpeed = BallSpeed.AddCurrentValue(new BallSpeed(value));
        }
        public void SubtractBallSpeed(float value)
        {
            BallSpeed = BallSpeed.SubtractCurrentValue(new BallSpeed(value));
        }

        public float GetBallSpeedValue()
        {
            return BallSpeed.Speed;
        }

        public void AddMoveSpeed(float value)
        {
            MoveSpeed = MoveSpeed.AddCurrentValue(new MoveSpeed(value));
        }
        public void SubtractMoveSpeed(float value)
        {
            MoveSpeed = MoveSpeed.SubtractCurrentValue(new MoveSpeed(value));
        }
        public float GetMoveSpeedValue()
        {
            return MoveSpeed.Speed;
        }

        public void ReplaceLevel(int value)
        {
            Level = new Level(value);
        }

        public int GetLevelValue()
        {
            return Level.CurrentValue;
        }


        public void AddExperiencePoint(int value)
        {
            ExperiencePoint = ExperiencePoint.AddCurrentValue(new ExperiencePoint(value));
        }


        public int GetExperiencePointValue()
        {
            return ExperiencePoint.CurrentValue;
        }

        public void AddComboCount()
        {
            ComboCount = ComboCount.AddCurrentValue(new ComboCount(1));
        }

        public void ResetComboCount()
        {
            ComboCount = ComboCount.ResetCurrentValue();
        }

        public int GetComboCount()
        {
            return ComboCount.CurrentValue;
        }

        public int GetDumbbellPower()
        {
            return DummbellPower.Power;
        }

        public void AddDumbbellPower(int addPower, int stackCount)
        {
            DummbellPower = DummbellPower.AddCurrentValue(new DumbbellPower(addPower), stackCount);
        }
    }
}
