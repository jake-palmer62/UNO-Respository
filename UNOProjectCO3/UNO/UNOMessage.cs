using System;

namespace UNOProjectCO3.UNO
{
    public enum UNOMessage : Byte
    {
        DrawCardFromStack=1, PressedUNO, SkipTurn, PlaceCard, ActionNotAllowed=0, YouAreNext, GameStates, GameFinished,
    }
}
