using App.Main.Player;

namespace App.Main.Player.Park
{
    public class ParkSystem
    {
        ParkList parkList;
        int AmountPark;
        public ParkSystem()
        {
            parkList = new ParkList();
            AmountPark = parkList.AmountPark;
        }

        /// <summary>
        /// <para>ランダムでParkを取得する</para>
        /// </summary>
        public void GetPark()
        {
            parkList.GetPark(0); // Debug用。本来はランダムで選択させる
        }

        /// <summary>
        /// <para>取得したParkの効果を発動する。parkのIDを引数として取る。IDはParkListから確認してね</para>
        /// </summary>
        public void UseParkEffects(int parkId)
        {
            parkList.AllParkList[parkId].Effect();
        }
    }
}