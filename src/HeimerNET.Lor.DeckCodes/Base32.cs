/*
 * Derived from https://github.com/google/google-authenticator-android/blob/master/java/com/google/android/apps/authenticator/util/Base32String.java
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HeimerNET.Lor.DeckCodes
{
    /// <summary>
    /// Encodes arbitrary byte arrays as case-insensitive base-32 strings
    /// </summary>
    static class Base32
    {
        static readonly Regex padding = new("[=]*$");
        private static readonly string DIGITS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        private static readonly int MASK = DIGITS.Length - 1;
        private static readonly int SHIFT = NumberOfTrailingZeros(DIGITS.Length);
        private static readonly Dictionary<char, int> CHAR_MAP = new(DIGITS.Length);
        private const string SEPARATOR = "-";

        static Base32()
        {
            for (int i = 0; i < DIGITS.Length; i++)
                CHAR_MAP[DIGITS[i]] = i;
        }

        private static int NumberOfTrailingZeros(int i)
        {
            // HD, Figure 5-14
            if (i == 0) return 32;
            int y, n = 31;
            y = i << 16; if (y != 0) { n -= 16; i = y; }
            y = i << 8; if (y != 0) { n -= 8; i = y; }
            y = i << 4; if (y != 0) { n -= 4; i = y; }
            y = i << 2; if (y != 0) { n -= 2; i = y; }
            return n - (int)((uint)(i << 1) >> 31);
        }

        /// <summary>
        /// Encode string to byte array.
        /// </summary>
        /// <exception cref="DecodingException"></exception>
        public static byte[] Decode(string encoded)
        {
            // Remove whitespace and separators
            encoded = encoded.Trim().Replace(SEPARATOR, string.Empty, StringComparison.Ordinal);

            // Remove padding. Note: the padding is used as hint to determine how many
            // bits to decode from the last incomplete chunk (which is commented out
            // below, so this may have been wrong to start with).
            encoded = padding.Replace(encoded, string.Empty);

            if (encoded.Length == 0)
                return Array.Empty<byte>();
            // Canonicalize to all upper case
            encoded = encoded.ToUpperInvariant();
            int outLength = encoded.Length * SHIFT / 8,
                buffer = 0,
                next = 0,
                bitsLeft = 0;
            var result = new byte[outLength];
            foreach (var item in encoded)
            {
                if (!CHAR_MAP.TryGetValue(item, out var value))
                    throw new DecodingException($"Illegal character: {item}");
                buffer <<= SHIFT;
                buffer |= value & MASK;
                bitsLeft += SHIFT;
                if (bitsLeft >= 8)
                {
                    result[next++] = (byte)(buffer >> (bitsLeft - 8));
                    bitsLeft -= 8;
                }
            }
            // We'll ignore leftover bits for now.
            //
            // if (next != outLength || bitsLeft >= SHIFT) {
            //  throw new DecodingException("Bits left: " + bitsLeft);
            // }
            return result;
        }

        /// <summary>
        /// Encode byte array to string.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Encode(byte[]? data, bool padOutput = false)
        {
            if (data is null || data.Length == 0)
            {
                return string.Empty;
            }

            // SHIFT is the number of bits per output character, so the length of the
            // output is the length of the input multiplied by 8/SHIFT, rounded up.
            if (data.Length >= (1 << 28))
            {
                // The computation below will fail, so don't do it.
                throw new ArgumentOutOfRangeException(nameof(data));
            }

            int outputLength = (data.Length * 8 + SHIFT - 1) / SHIFT;
            var result = new StringBuilder(outputLength);

            int buffer = data[0],
                next = 1,
                bitsLeft = 8;
            while (bitsLeft > 0 || next < data.Length)
            {
                if (bitsLeft < SHIFT)
                {
                    if (next < data.Length)
                    {
                        buffer <<= 8;
                        buffer |= data[next++] & 0xff;
                        bitsLeft += 8;
                    }
                    else
                    {
                        int pad = SHIFT - bitsLeft;
                        buffer <<= pad;
                        bitsLeft += pad;
                    }
                }
                int index = MASK & (buffer >> (bitsLeft - SHIFT));
                bitsLeft -= SHIFT;
                result.Append(DIGITS[index]);
            }
            if (padOutput)
            {
                int padding = 8 - (result.Length % 8);
                if (padding > 0) result.Append('=', padding == 8 ? 0 : padding);
            }
            return result.ToString();
        }
    }
}
