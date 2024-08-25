using System.Collections;
using System.Collections.Generic;
using App.Main.Data;


public class PlayerParameter
{
    public AttackPoint AttackPoint { get; private set; }
    public PlayerMoveSpeed MoveSpeed { get; private set; }

    public PlayerParameter(int attackPointValue, float moveSpeedValue)
    {
        this.AttackPoint = new AttackPoint(attackPointValue);
        this.MoveSpeed = new PlayerMoveSpeed(moveSpeedValue);
    }

}