using System.Collections;
using System.Collections.Generic;
using App.Main.Data;


public class PlayerParameter
{
    public AttackPoint attackPoint {get;}
    public PlayerMoveSpeed moveSpeed {get;}

    public PlayerParameter(int attackPointValue, float moveSpeedValue)
    {
        this.attackPoint = new AttackPoint(attackPointValue);
        this.moveSpeed = new PlayerMoveSpeed(moveSpeedValue);
    }

}