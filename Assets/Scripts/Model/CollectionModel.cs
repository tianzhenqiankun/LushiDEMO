using System.Collections;
using System.Collections.Generic;

public class CollectionModel  {

    public Dictionary<string, DeckModel> DeckCollection = new Dictionary<string, DeckModel>();

    public void Init()
    {
        DeckModel TestDeck = new DeckModel();
        TestDeck.Init();
        DeckCollection.Add("TestDeck",TestDeck);
    }
}
