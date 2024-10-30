using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using App.Main.Block;
using App.Main.Player;

namespace App.Main.Ball
{
    public class BallTrigger : MonoBehaviour
    {
        [SerializeField] private Ball Ball;
        [SerializeField] private GameObject invisibleBall_forYellowSubmarine;
        [SerializeField] private CreateYellowSubmarineEffect1 _createYellowSubmarineEffect1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IBlock block = other.gameObject.GetComponent<IBlock>();
            if (block != null)
            {
                Ball.AttackHandling(block);
                Ball.DecreasePathThroughCount();
                //黄色い潜水艦の効果
                //透明な丸を作り、ブロックとの衝突判定を取得し、ブロックのtakeDamageを呼び出す。
                //透明な丸は処理が終わると消える
                if(Ball.PlayerDatastore.PerkSystem.PerkList.AllPerkList[14].FloatEffect() == 1 && Ball.YellowSubmarineCoolTime == 0)
                {
                    _createYellowSubmarineEffect1.Create(other.transform.position);
                    GameObject collision_detector = Instantiate(invisibleBall_forYellowSubmarine, other.transform.position, quaternion.identity);
                    collision_detector.GetComponent<CollisionDetector_forYellowSubmarine>().SetDamage(Ball.PlayerDatastore.PerkSystem.PerkList.AllPerkList[14].GetStackCount(), Ball.CalcDamage());
                    Ball.YellowSubmarineCoolTime = 300;
                }
            }
        }
    }
}