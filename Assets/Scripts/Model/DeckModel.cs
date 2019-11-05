using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class DeckModel
{
    private int count;
    private string name;
    private Occupation occupationType;
    private List<ICard> cardList = new List<ICard>();
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            this.count = value;
        }
    }
    public List<ICard> CardList
    {
        get
        {
            return cardList;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Occupation OccupationType
    {
        get
        {
            return occupationType;
        }

        set
        {
            occupationType = value;
        }
    }

    private ICard cardVlociraptar = new CardVelociraptor();
    private ICard cardVlociraptar2 = new CardVelociraptor();
    private ICard cardVlociraptar3 = new CardVelociraptor();
    private ICard cardKillCommand = new CardKillCommand();
    private ICard cardKillCommand2= new CardKillCommand();
    private ICard cardKillCommand3 = new CardKillCommand();
    public void Init()
    {
        Name = "TestDeck";
        Count = 6;
        OccupationType = Occupation.Warlock;
        CardList.Add(cardVlociraptar);
        CardList.Add(cardVlociraptar2);
        CardList.Add(cardVlociraptar3);
        CardList.Add(cardKillCommand);
        CardList.Add(cardKillCommand2);
        CardList.Add(cardKillCommand3);
    }

}

