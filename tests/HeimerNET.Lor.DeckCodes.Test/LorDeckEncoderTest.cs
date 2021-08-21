using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace HeimerNET.Lor.DeckCodes.Test
{
    [TestClass]
    public class LorDeckEncoderTest
    {
        [TestMethod]
        public void EncodeDecodeRecommendedDecks()
        {
            var codes = new List<string>();
            var decks = new List<List<DeckCard>>();
            //Load the test data from file.
            string? line;
            using (var myReader = new StreamReader("DeckCodesTestData.txt"))
            {
                while ((line = myReader.ReadLine()) is not null)
                {
                    codes.Add(line);
                    var newDeck = new List<DeckCard>();
                    while (!string.IsNullOrEmpty(line = myReader.ReadLine()))
                    {
                        var parts = line.Split(':');
                        newDeck.Add(new() { CardCode = parts[1], Count = int.Parse(parts[0]) });
                    }
                    decks.Add(newDeck);
                }
            }

            //Encode each test deck and ensure it's equal to the correct string.
            //Then decode and ensure the deck is unchanged.
            for (int i = 0; i < decks.Count; i++)
            {
                string encoded = LorDeckEncoder.GetCodeFromDeck(decks[i]);
                Assert.AreEqual(codes[i], encoded);
                var decoded = LorDeckEncoder.GetDeckFromCode(encoded);
                Assert.IsTrue(VerifyRehydration(decks[i], decoded));
            }
        }

        [TestMethod]
        public void SmallDeck()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 1}
            };
            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void LargeDeck()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 3 },
                new() { CardCode = "01DE003", Count = 3 },
                new() { CardCode = "01DE004", Count = 3 },
                new() { CardCode = "01DE005", Count = 3 },
                new() { CardCode = "01DE006", Count = 3 },
                new() { CardCode = "01DE007", Count = 3 },
                new() { CardCode = "01DE008", Count = 3 },
                new() { CardCode = "01DE009", Count = 3 },
                new() { CardCode = "01DE010", Count = 3 },
                new() { CardCode = "01DE011", Count = 3 },
                new() { CardCode = "01DE012", Count = 3 },
                new() { CardCode = "01DE013", Count = 3 },
                new() { CardCode = "01DE014", Count = 3 },
                new() { CardCode = "01DE015", Count = 3 },
                new() { CardCode = "01DE016", Count = 3 },
                new() { CardCode = "01DE017", Count = 3 },
                new() { CardCode = "01DE018", Count = 3 },
                new() { CardCode = "01DE019", Count = 3 },
                new() { CardCode = "01DE020", Count = 3 },
                new() { CardCode = "01DE021", Count = 3 }
            };

            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void DeckWithCountsMoreThan3Small()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4}
            };

            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void DeckWithCountsMoreThan3Large()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 3 },
                new() { CardCode = "01DE003", Count = 3 },
                new() { CardCode = "01DE004", Count = 3 },
                new() { CardCode = "01DE005", Count = 3 },
                new() { CardCode = "01DE006", Count = 4 },
                new() { CardCode = "01DE007", Count = 5 },
                new() { CardCode = "01DE008", Count = 6 },
                new() { CardCode = "01DE009", Count = 7 },
                new() { CardCode = "01DE010", Count = 8 },
                new() { CardCode = "01DE011", Count = 9 },
                new() { CardCode = "01DE012", Count = 3 },
                new() { CardCode = "01DE013", Count = 3 },
                new() { CardCode = "01DE014", Count = 3 },
                new() { CardCode = "01DE015", Count = 3 },
                new() { CardCode = "01DE016", Count = 3 },
                new() { CardCode = "01DE017", Count = 3 },
                new() { CardCode = "01DE018", Count = 3 },
                new() { CardCode = "01DE019", Count = 3 },
                new() { CardCode = "01DE020", Count = 3 },
                new() { CardCode = "01DE021", Count = 3 }
            };

            string code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void SingleCard40Times()
        {
            var deck = new List<DeckCard>
            {
                new(){ CardCode = "01DE002", Count =  40 }
            };

            string code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void WorstCaseLength()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "01DE003", Count = 4 },
                new() { CardCode = "01DE004", Count = 4 },
                new() { CardCode = "01DE005", Count = 4 },
                new() { CardCode = "01DE006", Count = 4 },
                new() { CardCode = "01DE007", Count = 5 },
                new() { CardCode = "01DE008", Count = 6 },
                new() { CardCode = "01DE009", Count = 7 },
                new() { CardCode = "01DE010", Count = 8 },
                new() { CardCode = "01DE011", Count = 9 },
                new() { CardCode = "01DE012", Count = 4 },
                new() { CardCode = "01DE013", Count = 4 },
                new() { CardCode = "01DE014", Count = 4 },
                new() { CardCode = "01DE015", Count = 4 },
                new() { CardCode = "01DE016", Count = 4 },
                new() { CardCode = "01DE017", Count = 4 },
                new() { CardCode = "01DE018", Count = 4 },
                new() { CardCode = "01DE019", Count = 4 },
                new() { CardCode = "01DE020", Count = 4 },
                new() { CardCode = "01DE021", Count = 4 }
            };

            string code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void OrderShouldNotMatter1()
        {
            var deck1 = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 1 },
                new() { CardCode = "01DE003", Count = 2 },
                new() { CardCode = "02DE003", Count = 3 }
            };

            var deck2 = new List<DeckCard>
            {
                new() { CardCode = "01DE003", Count = 2 },
                new() { CardCode = "02DE003", Count = 3 },
                new() { CardCode = "01DE002", Count = 1 }
            };

            var code1 = LorDeckEncoder.GetCodeFromDeck(deck1);
            var code2 = LorDeckEncoder.GetCodeFromDeck(deck2);

            Assert.AreEqual(code1, code2);

            var deck3 = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "01DE003", Count = 2 },
                new() { CardCode = "02DE003", Count = 3 }
            };

            var deck4 = new List<DeckCard>
            {
                new() { CardCode = "01DE003", Count = 2 },
                new() { CardCode = "02DE003", Count = 3 },
                new() { CardCode = "01DE002", Count = 4 }
            };

            string code3 = LorDeckEncoder.GetCodeFromDeck(deck3);
            string code4 = LorDeckEncoder.GetCodeFromDeck(deck4);

            Assert.AreEqual(code3, code4);
        }

        [TestMethod]
        public void OrderShouldNotMatter2()
        {
            //importantly this order test includes more than 1 card with counts >3, which are sorted by card code and appending to the <=3 encodings.
            var deck1 = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "01DE003", Count = 2 },
                new() { CardCode = "02DE003", Count = 3 },
                new() { CardCode = "01DE004", Count = 5 }
            };

            var deck2 = new List<DeckCard>
            {
                new () { CardCode = "01DE004", Count = 5 },
                new () { CardCode = "01DE003", Count = 2 },
                new () { CardCode = "02DE003", Count = 3 },
                new () { CardCode = "01DE002", Count = 4 }
            };

            var code1 = LorDeckEncoder.GetCodeFromDeck(deck1);
            var code2 = LorDeckEncoder.GetCodeFromDeck(deck2);

            Assert.AreEqual(code1, code2);
        }

        [TestMethod]
        public void BilgewaterSet()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "02BW003", Count = 2 },
                new() { CardCode = "02BW010", Count = 3 },
                new() { CardCode = "01DE004", Count = 5 }
            };

            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void ShurimaSet()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "02BW003", Count = 2 },
                new() { CardCode = "02BW010", Count = 3 },
                new() { CardCode = "04SH047", Count = 5 }
            };

            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void MtTargonSet()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 4 },
                new() { CardCode = "03MT003", Count = 2 },
                new() { CardCode = "03MT010", Count = 3 },
                new() { CardCode = "02BW004", Count = 5 }
            };

            var code = LorDeckEncoder.GetCodeFromDeck(deck);
            var decoded = LorDeckEncoder.GetDeckFromCode(code);
            Assert.IsTrue(VerifyRehydration(deck, decoded));
        }

        [TestMethod]
        public void BadVersion()
        {
            // make sure that a deck with an invalid version fails
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE002", Count = 2 },
                new() { CardCode = "01DE003", Count = 4 },
                new() { CardCode = "02DE003", Count = 3 },
                new() { CardCode = "01DE004", Count = 5 }
            };

            var bytesFromDeck = Base32.Decode(LorDeckEncoder.GetCodeFromDeck(deck));

            //var result = new List<byte>();
            var formatAndVersion = (byte)88; // arbitrary invalid format/version
                                             //result.Add(formatAndVersion);

            //bytesFromDeck.RemoveAt(0); // remove the actual format/version
            //result.Concat(bytesFromDeck); // replace with invalid one
            var result = bytesFromDeck;
            result[0] = formatAndVersion;

            try
            {
                var badVersionDeckCode = Base32.Encode(result);
                var deckBad = LorDeckEncoder.GetDeckFromCode(badVersionDeckCode);
            }
            catch (ArgumentException e)
            {
                var expectedErrorMessage = "The provided code requires a higher version of this library; please update.";
                Console.WriteLine(e.Message);
                Assert.AreEqual(expectedErrorMessage, e.Message);
            }
        }

        [TestMethod]
        public void BadCardCodes()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode = "01DE02", Count = 1 }
            };
            bool failed = false;
            try
            {
                string code = LorDeckEncoder.GetCodeFromDeck(deck);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");

            deck.Clear();
            deck.Add(new() { CardCode = "01XX002", Count = 1 });

            failed = false;
            try
            {
                LorDeckEncoder.GetCodeFromDeck(deck);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");


            failed = false;
            deck.Clear();
            deck.Add(new() { CardCode = "01DE002", Count = 0 });
            try
            {
                LorDeckEncoder.GetCodeFromDeck(deck);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");
        }

        [TestMethod]
        public void BadCount()
        {
            var deck = new List<DeckCard>
            {
                new() { CardCode="01DE002", Count = 0 }
            };
            bool failed = false;
            try
            {
                string code = LorDeckEncoder.GetCodeFromDeck(deck);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");

            failed = false;
            deck.Clear();
            deck.Add(new() { CardCode = "01DE002", Count = -1 });
            try
            {
                string code = LorDeckEncoder.GetCodeFromDeck(deck);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");
        }

        [TestMethod]
        public void GarbageDecoding()
        {
            var badEncodingNotBase32 = "I'm no card code!";
            var badEncoding32 = "ABCDEFG";
            var badEncodingEmpty = string.Empty;

            bool failed = false;
            try
            {
                LorDeckEncoder.GetDeckFromCode(badEncodingNotBase32);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");

            failed = false;
            try
            {
                LorDeckEncoder.GetDeckFromCode(badEncoding32);
            }
            catch (ArgumentException)
            {
                failed = true;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected to throw an ArgumentException, but it threw {e}.");
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");

            failed = false;
            try
            {
                LorDeckEncoder.GetDeckFromCode(badEncodingEmpty);
            }
            catch
            {
                failed = true;
            }
            Assert.IsTrue(failed, "Expected to throw an ArgumentException, but it succeeded.");
        }

        [TestMethod]
        [DataRow("DE", 1)]
        [DataRow("FR", 1)]
        [DataRow("IO", 1)]
        [DataRow("NX", 1)]
        [DataRow("PZ", 1)]
        [DataRow("SI", 1)]
        [DataRow("BW", 2)]
        [DataRow("MT", 2)]
        [DataRow("SH", 3)]
        [DataRow("BC", 4)]
        public void DeckVersionIsTheMinimumLibraryVersionThatSupportsTheContainedFactions(string faction, int expectedVersion)
        {
            List<DeckCard> deck = new();
            deck.Add(new() { CardCode = "01DE001", Count = 1 });
            deck.Add(new() { CardCode = $"01{faction}002", Count = 1 });
            deck.Add(new() { CardCode = "01FR001", Count = 1 });
            var deckCode = LorDeckEncoder.GetCodeFromDeck(deck);

            int minSupportedLibraryVersion = ExtractVersionFromDeckCode(deckCode);

            Assert.AreEqual(expectedVersion, minSupportedLibraryVersion);
        }

        private static int ExtractVersionFromDeckCode(string deckCode)
        {
            var bytes = Base32.Decode(deckCode);
            return bytes[0] & 0xF;
        }

        [TestMethod]
        public void ArgumentExceptionOnFutureVersion()
        {
            const string singleCardDeckWithVersion5 = "CUAAAAIBAUAAC";
            Assert.ThrowsException<ArgumentException>(() => LorDeckEncoder.GetDeckFromCode(singleCardDeckWithVersion5));
        }

        public static bool VerifyRehydration(IReadOnlyCollection<DeckCard> d, IReadOnlyCollection<DeckCard> rehydratedList)
        {
            if (d.Count != rehydratedList.Count)
                return false;

            foreach (var cd in rehydratedList)
            {
                bool found = false;
                foreach (var cc in d)
                {
                    if (cc.CardCode == cd.CardCode && cc.Count == cd.Count)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }
            return true;
        }
    }
}
