using UnityEngine;

namespace App.Main.Player
{
    public class PlayerMove
    {
        Rigidbody2D rb;
        PlayerDatastore playerDatastore;
        
        public PlayerMove(Rigidbody2D rb, PlayerDatastore playerDatastore)
        {
            this.rb = rb;
            this.playerDatastore = playerDatastore;
        }

        public void Move()
        {
            if(Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-playerDatastore.GetMoveSpeedValue(), 0);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(playerDatastore.GetMoveSpeedValue(), 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}