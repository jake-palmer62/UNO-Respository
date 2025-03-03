using System;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public enum MessagesforClient : byte
    {
        JoinAllowed = 1, JoinDenied, Kicked, Disconnected, ServerShutdown, Timeout, KeepHostAlive, OtherPlayerLeft, IsReady, ChatMessage, GameStarted, GameFinished, GameData, PlayerInfo, GeneralPlayersInfo,
    }
}
