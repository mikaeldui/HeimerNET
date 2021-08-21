# HeimerNET.Lor.DeckCodes

This is a C# implementation of the C# library [RiotGames/LoRDeckCodes](https://github.com/RiotGames/LoRDeckCodes).
The library is more universal, faster and use less memory.

## How to use
```cs
var deck1 = new List<DeckCard>()
{
    new("01DE002", 4),
    new("02BW003", 2),
    new("02BW010", 3),
    ...
}
string code = LorDeckEncoder.GetCodeFromDeck(deck1);
List<DeckCard> deck2 = LorDeckEncoder.GetDeckFromCode("SomeCode");
```

You can also use your own class, all you have to do is implement 2 interface\
`IDeckCard` - to get deck from code\
`IReadOnlyDeckCard` - to get the code from the deck

```cs
class MyCard : IDeckCard, IReadOnlyDeckCard { ... }
List<MyCard> deck = LorDeckEncoder.GetDeckFromCode<MyCard>("SomeCode");
string code = LorDeckEncoder.GetCodeFromDeck(deck);
```
