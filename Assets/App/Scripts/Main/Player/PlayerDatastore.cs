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

        public void SubtractLive(int value)
        {
            Parameter.SubtractLive(value);
        }
        public bool IsLiveValue(int value)
        {
            return Parameter.IsLiveValue(value);
        }
    }
}