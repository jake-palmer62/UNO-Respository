using System;
using UNOProjectCO3.Game_Connection_Algorithms;

namespace UNOProjectCO3.UNO
{
    public class UNOHostCreator : IGameHostCreation
    {
        public readonly static UNOHostCreator Instance = new UNOHostCreator();

        public GameHost Create()
        {
            return new UNOHost();
        }

        public gameConnection CreateConnection()
        {
            return new UNOGameConnection();
        }
    }
}
