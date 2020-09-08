using System;
using System.Net;
using UNOProjectCO3.Games;

namespace UNOProjectCO3
{
    public class Player
    {
        public readonly GameHost Host;
        public readonly long ID = IDGenerator.GenerateID(); 
        public readonly string Name;
        public IPEndPoint Address;
        bool Ready = false;

        public bool ReadyToPlay // Used in game lobby so each user knows when a player is ready to begin
        {
            get
            {
                return Ready;
            }
            set
            {
                if (Host.State != GameState.WaitingForPlayers)
                    throw new InvalidOperationException("Can't modify player state outside of lobby");
                Ready = value;
            }
        }

        public Player(GameHost host, string name) // assigns the player class with these paramaters.
        {
            Name = name;
            Host = host;
            host.GameStateChanged += OnGameStateChanged;
        }

        public virtual void OnGameStateChanged(object sender, GameStateChangedArgs e) // Finishes the game.
        {
            if (e.NewState == GameState.GameFinished)
                Ready = false;
        }
    }
}
