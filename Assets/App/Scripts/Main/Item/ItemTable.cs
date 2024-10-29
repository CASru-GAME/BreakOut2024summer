using UnityEngine;
using App.Main.Player;
using UnityEngine.Events;
using App.Main.Stage;
using App.Main.Ball;
using System.Collections.Generic;
using System;
using Unity.Mathematics;


namespace App.Main.Item
{
    public class ItemTable : MonoBehaviour
    {
        public ItemEffect[] items;

        private CatchBallItem catchBallItem;
        private AddAttackPointItem addAttackPointItem;
        private AddMoveSpeedItem addMoveSpeedItem;

        void Start()
        {
            catchBallItem = new CatchBallItem(transform);
            addAttackPointItem = new AddAttackPointItem(GetComponent<PlayerDatastore>());
            addMoveSpeedItem = new AddMoveSpeedItem(GetComponent<PlayerDatastore>());

            //1～10までは経験値用(仮)
            items = new ItemEffect[17];

            items[0] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[1] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(1);//経験値小
            });
            items[2] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(3);//経験値中
            });
            items[3] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(5);//経験値大
            });
            items[4] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(10);//経験値特大
            });
            items[5] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddExperiencePoint(15);//経験値超特大
            });
            items[6] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[7] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[8] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[9] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[10] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
            });
            items[11] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                stageSystem.CreateBall(new Vector3(-2.2f, -3f, 0));

                perkEffect(stageSystem, playerDatastore);
            });
            items[12] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                playerDatastore.AddLive(1);
            });
            items[13] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                if (!playerDatastore.ItemList.OwnedItems.Contains(playerDatastore.ItemList.AllItems[2]))
                {
                    playerDatastore.ItemList.OwnedItems.Add(playerDatastore.ItemList.AllItems[2]);
                    playerDatastore.gameObject.GetComponent<CapsuleCollider2D>().size += new Vector2(playerDatastore.gameObject.GetComponent<CapsuleCollider2D>().size.x * 0.2f, 0f);
                    playerDatastore.gameObject.GetComponent<SpriteRenderer>().size += new Vector2(playerDatastore.gameObject.GetComponent<SpriteRenderer>().size.x * 0.2f, 0f);
                }
            });
            items[14] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                catchBallItem.IsActive = true;
                catchBallItem.currentDuration = 0f;
            });
            items[15] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                if (!playerDatastore.ItemList.OwnedItems.Contains(playerDatastore.ItemList.AllItems[4]))
                {
                    playerDatastore.ItemList.OwnedItems.Add(playerDatastore.ItemList.AllItems[4]);
                    addAttackPointItem.IsActive = true;
                    addAttackPointItem.AddAttackPoint();
                }
                addAttackPointItem.currentDuration = 0f;
            });
            items[16] = new ItemEffect((StageSystem stageSystem, PlayerDatastore playerDatastore) =>
            {
                if (!playerDatastore.ItemList.OwnedItems.Contains(playerDatastore.ItemList.AllItems[5]))
                {
                    playerDatastore.ItemList.OwnedItems.Add(playerDatastore.ItemList.AllItems[5]);
                    addMoveSpeedItem.IsActive = true;
                    addMoveSpeedItem.AddMoveSpeed();
                }
                addMoveSpeedItem.currentDuration = 0f;
            });
        }

        public List<(int id, float time)> GetTimeList()
        {
            return new List<(int id, float time)>()
            {
                (14, catchBallItem.GetRemainingTime()),
                (15, addAttackPointItem.GetRemainingTime()),
                (16, addMoveSpeedItem.GetRemainingTime()),
            };
        }

        private void perkEffect(StageSystem stageSystem, PlayerDatastore playerDatastore)
        {
            if (playerDatastore.PerkSystem.PerkList.AllPerkList[9].IntEffect() == 1)
            {
                for (int i = 0; i < playerDatastore.PerkSystem.PerkList.AllPerkList[9].GetStackCount(); i++)
                {
                    stageSystem.CreateBall(transform.position + new Vector3(0f,0.15f,0f));
                    stageSystem.IncreaseBallCountonStage();
                }
            }
        }

        public class ItemEffect
        {
            public UnityAction<StageSystem, PlayerDatastore> effect;
            public ItemEffect(UnityAction<StageSystem, PlayerDatastore> effect)
            {
                this.effect = effect;
            }
        }

        void Update()
        {
            //ID:14のアイテムの効果発動時
            if (catchBallItem.IsActive)
            {
                catchBallItem.DecreaseDuration();

                if (catchBallItem.BallList.Count >= 1)
                {
                    catchBallItem.KeepBall(this.gameObject);
                    catchBallItem.ReleaseBall();

                    if (Input.GetMouseButton(0))
                    {
                        catchBallItem.EmitBall(catchBallItem.DecideSpeedVector());
                    }
                }
            }
            //ID:15のアイテムの効果発動時
            if (addAttackPointItem.IsActive)
                addAttackPointItem.DecreaseDuration();
            //ID:15のアイテムの効果発動時
            if (addMoveSpeedItem.IsActive)
                addMoveSpeedItem.DecreaseDuration();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //ID:14のアイテムの効果発動時
            if (catchBallItem.IsActive)
            {
                catchBallItem.StopBall(other);
                if (catchBallItem.BallList.Count >= 1 && (other.GetComponent<Ball.Ball>() != null && !catchBallItem.BallList.Contains(item: other.GetComponent<Ball.Ball>())))
                    catchBallItem.EmitBall(catchBallItem.BallSpeedList[0]);
            }
        }

        /// <summary>
        /// ID:14のアイテム
        /// </summary>
        class CatchBallItem
        {
            private Transform transform;
            public List<Ball.Ball> BallList = new List<Ball.Ball>();
            public List<Vector3> BallSpeedList = new List<Vector3>();
            public bool IsActive;
            public readonly float duration = 20f;
            public float currentDuration = 0f;
            private float xPositionDiffence;
            private readonly float retentionTime = 3f;
            private float currentRetentionTime;

            public CatchBallItem(Transform transform)
            {
                this.transform = transform;
            }

            public void StopBall(Collider2D other)
            {
                Ball.Ball ball = null;
                ball = other.gameObject.GetComponent<Ball.Ball>();

                if (ball != null && !BallList.Contains(ball))
                {
                    BallList.Add(ball);
                    BallSpeedList.Add(ball.GetComponent<Rigidbody2D>().velocity);
                    BallList[0].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    xPositionDiffence = BallList[0].transform.position.x - transform.position.x;
                }
            }
            public void KeepBall(GameObject player)
            {
                BallList[0].transform.position = new Vector3(transform.position.x + xPositionDiffence, transform.position.y + transform.localScale.y / 2 * player.GetComponent<SpriteRenderer>().size.y + BallList[0].transform.localScale.y / 2, 0f);
            }
            public Vector3 DecideSpeedVector()
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var mouseDistance = mousePosition - BallList[0].transform.position;
                Vector3 ballSpeedVector = mouseDistance.normalized;
                return ballSpeedVector;
            }
            public void EmitBall(Vector3 ballSpeedVector)
            {
                BallList[0].GetComponent<Rigidbody2D>().velocity = ballSpeedVector;
                BallList.RemoveAt(0);
                BallSpeedList.RemoveAt(0);
            }
            public void ReleaseBall()
            {
                currentRetentionTime += Time.deltaTime;

                if (currentRetentionTime >= retentionTime)
                {
                    EmitBall(BallSpeedList[0]);
                    currentRetentionTime = 0f;
                }
            }
            public void DecreaseDuration()
            {
                currentDuration += Time.deltaTime;
                if (currentDuration >= duration)
                {
                    currentDuration = 0f;
                    IsActive = false;
                }
            }
            public float GetRemainingTime()
            {
                if (currentDuration <= 0f)
                {
                    return 0f;
                }
                return duration - currentDuration;
            }
        }
    }

    class AddAttackPointItem
    {
        public bool IsActive;
        public readonly float duration = 20f;
        public float currentDuration = 0f;

        PlayerDatastore playerDatastore;
        public AddAttackPointItem(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
        }

        public void AddAttackPoint()
        {
            playerDatastore.Parameter.AddAttackPoint(2);
        }
        public void ResetAttackPoint()
        {
            playerDatastore.Parameter.SubtractAttackPoint(2);
        }
        public void DecreaseDuration()
        {
            currentDuration += Time.deltaTime;
            if (currentDuration >= duration)
            {
                currentDuration = 0f;
                ResetAttackPoint();
                IsActive = false;
                playerDatastore.ItemList.OwnedItems.Remove(playerDatastore.ItemList.AllItems[4]);
            }
        }
        public float GetRemainingTime()
        {
            if (currentDuration <= 0f)
            {
                return 0f;
            }
            return duration - currentDuration;
        }
    }
    class AddMoveSpeedItem
    {
        public bool IsActive;
        public readonly float duration = 20f;
        public float currentDuration = 0f;

        PlayerDatastore playerDatastore;
        public AddMoveSpeedItem(PlayerDatastore playerDatastore)
        {
            this.playerDatastore = playerDatastore;
        }

        public void AddMoveSpeed()
        {
            playerDatastore.Parameter.AddMoveSpeed(2f);
        }
        public void ResetMoveSpeed()
        {
            playerDatastore.Parameter.SubtractMoveSpeed(2f);
        }
        public void DecreaseDuration()
        {
            currentDuration += Time.deltaTime;
            if (currentDuration >= duration)
            {
                currentDuration = 0f;
                ResetMoveSpeed();
                IsActive = false;
                playerDatastore.ItemList.OwnedItems.Remove(playerDatastore.ItemList.AllItems[5]);
            }
        }
        public float GetRemainingTime()
        {
            if (currentDuration <= 0f)
            {
                return 0f;
            }
            return duration - currentDuration;
        }
    }
}


