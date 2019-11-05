using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RoundModel
{

    Occupation playerOccupation;
    Occupation opponentOccupation;
    public static Action PlayerHandler;
    public static Action opponentHandler;
    bool isPlayerRound;

    public Occupation PlayerOccupation
    {
        get
        {
            return playerOccupation;
        }

        set
        {
            playerOccupation = value;
        }
    }

    public Occupation OpponentOccupation
    {
        get
        {
            return opponentOccupation;
        }

        set
        {
            opponentOccupation = value;
        }
    }

    public bool IsPlayerRound
    {
        get
        {
            return isPlayerRound;
        }

        set
        {
            isPlayerRound = value;
        }
    }

    public void BeginWith(bool isPlayer)
    {
        if (isPlayer)
        {
            if (PlayerHandler!=null)
            {
                PlayerHandler();
            }
        }
        else
        {
            if (opponentHandler!=null)
            {
                opponentHandler();
            }
        }
    }
}

