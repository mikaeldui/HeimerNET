using System;
using System.Collections.Generic;

namespace HeimerNET.Lor.DeckCodes;

/// <summary>
/// Converts deck to and from deckcode.
/// </summary>
public static class LorDeckEncoder
{
    /// <summary>
    /// MAX_KNOWN_VERSION
    /// </summary>
    const byte MaxVersion = 4;

    /// <summary>
    /// Format of CardCode.
    /// </summary>
    const int Format = 1;

    /// <summary>
    /// Exact length of CardCode.
    /// </summary>
    const int CardCodeLength = 7;

    const int InitialVersion = 1;

    private static readonly Dictionary<string, int> FactionCodeToIntIdentifier = new()
    {
        { "DE", 0 },
        { "FR", 1 },
        { "IO", 2 },
        { "NX", 3 },
        { "PZ", 4 },
        { "SI", 5 },
        { "BW", 6 },
        { "SH", 7 },
        { "MT", 9 },
        { "BC", 10 }
    };
    private static readonly Dictionary<int, string> IntIdentifierToFactionCode = new();
    private static readonly Dictionary<string, byte> FactionCodeToLibraryVersion = new()
    {
        { "DE", 1 },
        { "FR", 1 },
        { "IO", 1 },
        { "NX", 1 },
        { "PZ", 1 },
        { "SI", 1 },
        { "BW", 2 },
        { "MT", 2 },
        { "SH", 3 },
        { "BC", 4 }
    };

    static LorDeckEncoder()
    {
        foreach (var item in FactionCodeToIntIdentifier)
            IntIdentifierToFactionCode.Add(item.Value, item.Key);
    }

    /// <summary>
    ///  Converts deckcode to deck.
    /// </summary>
    /// <param name="code">The deckcode.</param>
    /// <returns>The deck.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<DeckCard> GetDeckFromCode(string code)
        => GetDeckFromCode<DeckCard>(code);

    /// <summary>
    ///  Converts deckcode to deck.
    /// </summary>
    /// <param name="code">The deckcode.</param>
    /// <returns>The deck.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<T> GetDeckFromCode<T>(string code) where T : IDeckCard, new()
    {
        byte[] bytes;
        try
        {
            bytes = Base32.Decode(code);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid deck code", e);
        }

        //grab format and version
        //int format = bytes[0] >> 4;
        int version = bytes[0] & 0xF;
        if (version > MaxVersion)
        {
            throw new ArgumentException("The provided code requires a higher version of this library; please update.");
        }
        var byteList = new Queue<byte>(bytes);
        byteList.Dequeue();
        var result = new List<T>();
        for (int i = 3; i > 0; i--)
        {
            int numGroupOfs = VarintTranslator.PopVarint(byteList);

            for (int j = 0; j < numGroupOfs; j++)
            {
                int numOfsInThisGroup = VarintTranslator.PopVarint(byteList);
                var setNumber = VarintTranslator.PopVarint(byteList);
                var factionNumber = VarintTranslator.PopVarint(byteList);
                var set = setNumber.ToString().PadLeft(2, '0');
                var faction = IntIdentifierToFactionCode[factionNumber];
                for (int k = 0; k < numOfsInThisGroup; k++)
                {
                    var number = VarintTranslator.PopVarint(byteList);
                    result.Add(new T()
                    {
                        CardCode = set + faction + number.ToString().PadLeft(3, '0'),
                        Count = i
                    });
                }
            }
        }

        //the remainder of the deck code is comprised of entries for cards with counts >= 4
        //this will only happen in Limited and special game modes.
        //the encoding is simply [count] [cardcode]
        while (byteList.Count > 0)
        {
            int count = VarintTranslator.PopVarint(byteList);
            var set = VarintTranslator.PopVarint(byteList);
            int faction = VarintTranslator.PopVarint(byteList);
            var number = VarintTranslator.PopVarint(byteList);
            result.Add(new T()
            {
                CardCode = set.ToString().PadLeft(2, '0') + IntIdentifierToFactionCode[faction] + number.ToString().PadLeft(3, '0'),
                Count = count
            });
        }
        return result;
    }

    /// <summary>
    /// Converts deck to deckcode.
    /// </summary>
    /// <param name="deck">The deck.</param>
    /// <returns><see cref="string"/> represent deckcode.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="deck"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string GetCodeFromDeck(IEnumerable<IReadOnlyDeckCard> deck)
    {
        if (deck is null)
            throw new ArgumentNullException(nameof(deck));
        return Base32.Encode(GetDeckCodeBytes(deck));
    }

    /// <exception cref="ArgumentException"></exception>
    private static byte[] GetDeckCodeBytes(IEnumerable<IReadOnlyDeckCard> deck)
    {
        if (!ValidCardCodesAndCounts(deck))
            throw new ArgumentException("The provided deck contains invalid card codes.");

        var result = new List<byte>
            {
                (byte)(Format << 4 | (GetMinSupportedLibraryVersion(deck) & 0xF))
            };

        var of3 = new List<IReadOnlyDeckCard>();
        var of2 = new List<IReadOnlyDeckCard>();
        var of1 = new List<IReadOnlyDeckCard>();
        var ofN = new List<IReadOnlyDeckCard>();

        foreach (var item in deck)
        {
            if (item.Count == 3)
                of3.Add(item);
            else if (item.Count == 2)
                of2.Add(item);
            else if (item.Count == 1)
                of1.Add(item);
            else if (item.Count < 1)
                throw new ArgumentException($"Invalid count of {item.Count} for card {item.CardCode}");
            else
                ofN.Add(item);
        }

        //build the lists of set and faction combinations within the groups of similar card counts
        var groupedOf3s = GetGroupedOfs(of3);
        var groupedOf2s = GetGroupedOfs(of2);
        var groupedOf1s = GetGroupedOfs(of1);

        //to ensure that the same decklist in any order produces the same code, do some sorting
        SortGroupOf(groupedOf3s);
        SortGroupOf(groupedOf2s);
        SortGroupOf(groupedOf1s);

        //Nofs (since rare) are simply sorted by the card code - there's no optimiziation based upon the card count
        ofN.Sort((x, y) => string.Compare(x.CardCode, y.CardCode, StringComparison.Ordinal));
        //Encode
        EncodeGroupOf(result, groupedOf3s);
        EncodeGroupOf(result, groupedOf2s);
        EncodeGroupOf(result, groupedOf1s);

        //Cards with 4+ counts are handled differently: simply [count] [card code] for each
        EncodeNOfs(result, ofN);
        return result.ToArray();
    }

    private static int GetMinSupportedLibraryVersion(IEnumerable<IReadOnlyDeckCard> deck)
    {
        var max = InitialVersion;
        foreach (var item in deck)
        {
            var facCode = item.CardCode.Substring(2, 2);
            if (!FactionCodeToLibraryVersion.TryGetValue(facCode, out var value))
                return MaxVersion;
            if (value > max)
            {
                if (value == MaxVersion)
                    return MaxVersion;
                max = value;
            }
        }
        return max;
    }

    private static void EncodeNOfs(List<byte> bytes, List<IReadOnlyDeckCard> nOfs)
    {
        for (int i = 0; i < nOfs.Count; i++)
        {
            bytes.AddRange(VarintTranslator.GetVarint(nOfs[i].Count));

            ParseCardCode(nOfs[i].CardCode, out int setNumber, out var factionCode, out int cardNumber);
            int factionNumber = FactionCodeToIntIdentifier[factionCode.ToString()];

            bytes.AddRange(VarintTranslator.GetVarint(setNumber));
            bytes.AddRange(VarintTranslator.GetVarint(factionNumber));
            bytes.AddRange(VarintTranslator.GetVarint(cardNumber));
        }
    }

    //The sorting convention of this encoding scheme is
    //First by the number of set/faction combinations in each top-level list
    //Second by the alphanumeric order of the card codes within those lists.
    private static void SortGroupOf(List<List<IReadOnlyDeckCard>> groupOf)
    {
        groupOf.Sort((x, y) =>
        {
            var compareCount = x.Count.CompareTo(y.Count);
            return compareCount != 0 ? compareCount : string.Compare(x[0].CardCode, y[0].CardCode, StringComparison.Ordinal);
        });
        for (int i = 0; i < groupOf.Count; i++)
        {
            groupOf[i].Sort((x, y) => string.Compare(x.CardCode, y.CardCode, StringComparison.Ordinal));
        }
    }

    private static void ParseCardCode(string code, out int set, out ReadOnlySpan<char> faction, out int number)
    {
        var spanCode = code.AsSpan();
        set = int.Parse(spanCode.Slice(0, 2));
        faction = spanCode.Slice(2, 2);
        number = int.Parse(spanCode.Slice(4, 3));
    }

    private static List<List<IReadOnlyDeckCard>> GetGroupedOfs(List<IReadOnlyDeckCard> list)
    {
        var result = new List<List<IReadOnlyDeckCard>>();
        while (list.Count > 0)
        {
            var currentSet = new List<IReadOnlyDeckCard>();

            //get info from first
            string firstCardCode = list[0].CardCode;
            ParseCardCode(firstCardCode, out int setNumber, out var factionCode, out _);
            //now add that to our new list, remove from old
            currentSet.Add(list[0]);
            list.RemoveAt(0);

            //sweep through rest of list and grab entries that should live with our first one.
            //matching means same set and faction - we are already assured the count matches from previous grouping.
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var currentCardCode = list[i].CardCode.AsSpan();
                int currentSetNumber = int.Parse(currentCardCode.Slice(0, 2));
                var currentFactionCode = currentCardCode.Slice(2, 2);

                if (currentSetNumber == setNumber && factionCode.Equals(currentFactionCode, StringComparison.InvariantCulture))
                {
                    currentSet.Add(list[i]);
                    list.RemoveAt(i);
                }
            }
            result.Add(currentSet);
        }
        return result;
    }

    private static void EncodeGroupOf(List<byte> bytes, List<List<IReadOnlyDeckCard>> groupOf)
    {
        bytes.AddRange(VarintTranslator.GetVarint(groupOf.Count));
        for (int i = 0; i < groupOf.Count; i++)
        {
            //how many cards in current group?
            bytes.AddRange(VarintTranslator.GetVarint(groupOf[i].Count));

            //what is this group, as identified by a set and faction pair
            ParseCardCode(groupOf[i][0].CardCode, out int currentSetNumber, out var currentFactionCode, out int _);
            int currentFactionNumber = FactionCodeToIntIdentifier[currentFactionCode.ToString()];
            bytes.AddRange(VarintTranslator.GetVarint(currentSetNumber));
            bytes.AddRange(VarintTranslator.GetVarint(currentFactionNumber));

            //what are the cards, as identified by the third section of card code only now, within this group?
            for (int i2 = 0; i2 < groupOf[i].Count; i2++)
            {
                var code = groupOf[i][i2].CardCode;
                int sequenceNumber = int.Parse(code.AsSpan(4, 3));
                bytes.AddRange(VarintTranslator.GetVarint(sequenceNumber));
            }
        }
    }

    /// <summary>
    /// Validate a deck.
    /// </summary>
    /// <param name="deck">Deck to validate.</param>
    /// <returns>true is valid, otherwise false</returns>
    public static bool ValidCardCodesAndCounts(IEnumerable<IReadOnlyDeckCard> deck)
    {
        if (deck is null)
            return false;
        foreach (var item in deck)
        {
            if (item.CardCode.Length != CardCodeLength || item.Count < 1)
                return false;

            string faction = item.CardCode.Substring(2, 2);
            if (!FactionCodeToIntIdentifier.ContainsKey(faction))
                return false;
            var span = item.CardCode.AsSpan();
            if (!int.TryParse(span.Slice(0, 2), out _) || !int.TryParse(span.Slice(4, 3), out _)) // set || number
                return false;
        }
        return true;
    }
}
