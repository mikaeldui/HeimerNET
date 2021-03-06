/*THIS CODE ADAPTED FROM
VarintBitConverter: https://github.com/topas/VarintBitConverter 
Copyright(c) 2011 Tomas Pastorek, Ixone.cz. All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
 1.Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
 2. Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.
THIS SOFTWARE IS PROVIDED BY TOMAS PASTOREK AND CONTRIBUTORS ``AS IS'' 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL TOMAS PASTOREK OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
THE POSSIBILITY OF SUCH DAMAGE. 
*/

using System;
using System.Collections.Generic;

namespace HeimerNET.Lor.DeckCodes;

static class VarintTranslator
{
    private const byte AllButMSB = 0x7f;
    private const byte JustMSB = 0x80;

    /// <exception cref="ArgumentException"></exception>
    public static int PopVarint(Queue<byte> bytes)
    {
        ulong result = 0;
        int currentShift = 0;
        while (bytes.TryDequeue(out var item))
        {
            var current = (ulong)item & AllButMSB;
            result |= current << currentShift;
            if ((item & JustMSB) != JustMSB)
            {
                return (int)result;
            }
            currentShift += 7;
        }
        throw new ArgumentException("Byte array did not contain valid varints.", nameof(bytes));
    }

    public static byte[] GetVarint(ulong value)
    {
        if (value == 0)
            return new byte[1] { 0 };
        var buff = new byte[10];
        var currentIndex = 0;
        while (value != 0)
        {
            var byteVal = value & AllButMSB;
            value >>= 7;
            if (value != 0)
                byteVal |= 0x80;
            buff[currentIndex++] = (byte)byteVal;
        }
        Array.Resize(ref buff, currentIndex);
        return buff;
    }

    public static byte[] GetVarint(int value)
    {
        return GetVarint((ulong)value);
    }
}
