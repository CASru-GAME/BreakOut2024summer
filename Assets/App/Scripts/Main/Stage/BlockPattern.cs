using System;
using UnityEngine;
using System.Collections.Generic;

namespace App.Main.Stage
{
    public class BlockPattern : MonoBehaviour
    {
        ///<summary>
        ///生成するノーマルブロックの座標のリスト(IDに対応したリスト)を返す。
        ///</summary>
        ///<exception cref="ArgumentException">IDが0未満になる場合に発生します。</exception>
        public List<Vector2> EnumerateBlockPositions(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id cannot be negative");
            }

            //IDで分岐
            switch(id){
                case 0:
                    return new List<Vector2>{
                        new Vector2(0, 0),
                        new Vector2(1, 0)};
                case 1:
                    return new List<Vector2>{
                        new Vector2(0, 0),
                        new Vector2(-1, 0)};
            }
            return null;
        }
    }
}
