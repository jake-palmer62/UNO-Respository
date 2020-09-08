using System;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public interface IGameHostCreation
    {
        GameHost Create();
        gameConnection CreateConnection();
    }
}
