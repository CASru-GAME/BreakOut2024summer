using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Experimental;

namespace App.Main.Player
{
    public class PlayerDatastore : MonoBehaviour
    {
        public Parameter Parameter { get; private set; }

        public void InitializePlayer()
        {
            Parameter = new Parameter(3,1, 5.0f,1,0);  //Parameter(int live,int attackPoint, float moveSpeed, int level , int experiencePoint)のコンストラクタを呼び出す
        }

        public void AddLive(int value)
        {
            Parameter.AddLive(value);
        }

        public void SubtractLive(int value)
        {
            Parameter.SubtractLive(value);
        }
        public bool IsLiveValue(int value)
        {
            return Parameter.IsLiveValue(value);
        }
        public void AddMaxLive(int value)
        {
            Parameter.AddMaxLive(value);
        }

        public void SubtractMaxLive(int value)
        {
            Parameter.SubtractMaxLive(value);
        }

        public int LiveValue()
        {
            return Parameter.LiveValue();
        }

        public void AddAttackPoint(int value)
        {
            Parameter.AddAttackPoint(value);
        }

        public void SubtractAttackPoint(int value)
        {
            Parameter.SubtractAttackPoint(value);
        }

        public void MultiplyAttackPoint(double value)
        {
            Parameter.MultiplyAttackPoint(value);
        }

        public void DivideAttackPoint(double value)
        {
            Parameter.DivideAttackPoint(value);
        }

        public int AttackPointValue()
        {
            return Parameter.AttackPointValue();
        }

        public void AddExperiencePoint(int value)
        {
            Parameter.AddExperiencePoint(value);
        }

        public int ExperiencePointValue()
        {
            return Parameter.ExperiencePointValue();
        }

        public void ReplaceLevel(int value)
        {
            Parameter.ReplaceLevel(value);
        }

        public int LevelValue()
        {
            return Parameter.LevelValue();
        }

        public void AddMoveSpeed(float value)
        {
            Parameter.AddMoveSpeed(value);
        }

        public void SubtractMoveSpeed(float value)
        {
            Parameter.SubtractMoveSpeed(value);
        }

        public float MoveSpeedValue()
        {
            return Parameter.MoveSpeedValue();
        }
        
    }
}