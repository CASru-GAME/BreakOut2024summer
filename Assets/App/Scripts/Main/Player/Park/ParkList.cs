using System.Collections.Generic;

namespace App.Main.Player.Park
{
    public class ParkList
    {
        public int AmountPark;
        public ParkList()
        {
            AmountPark = AllParkList.Length;
        }

        public List<IPark> OwnedParkList = new List<IPark>();
        public readonly IPark[] AllParkList = new IPark[]
        {
            new DebugPark(),//ID:0
        };

        public void GetPark(int ParkId)
        {
            if(!OwnedParkList.Contains(AllParkList[ParkId]))
            {
                OwnedParkList.Add(AllParkList[ParkId]);
            }
            AllParkList[ParkId].AddStackCount();
        }
    }
}