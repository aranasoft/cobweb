using System;
using System.Linq;
using System.Security.Cryptography;

namespace Cobweb.Testing {
    public static class RandomData {
        public static int GetInt32(int maxValue) {
            if (maxValue < 1) {
                throw new ArgumentException("Value must be greater than 0", "maxValue");
            }

            var seed = new Byte[4];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider()) {
                cryptoServiceProvider.GetBytes(seed);
            }

            var random = new Random(BitConverter.ToInt32(seed, 0));
            return random.Next(maxValue);
        }

        public static string GetString(int length, char[] characters = null) {
            if (length < 1) {
                throw new ArgumentException("Value must be greater than 0", "length");
            }

            var defaultCharacters = new[] {
                '0',
                '2',
                '3',
                '4',
                '5',
                '6',
                '8',
                '9',
                ' ',
                '\r',
                'a',
                'b',
                'c',
                'd',
                'e',
                'f',
                'g',
                'h',
                'j',
                'k',
                'm',
                'n',
                'p',
                'q',
                'r',
                's',
                't',
                'u',
                'v',
                'w',
                'x',
                'y',
                'z',
                ' ',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'J',
                'K',
                'L',
                'M',
                'N',
                'P',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
                ' '
            };

            var characterSet = characters != null && characters.Any() ? characters : defaultCharacters;

            var seed = new Byte[4];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider()) {
                cryptoServiceProvider.GetBytes(seed);
            }

            var random = new Random(BitConverter.ToInt32(seed, 0));
            return new String(Enumerable.Repeat(characterSet, length)
                                        .Select(charSet => charSet[random.Next(charSet.Length)])
                                        .ToArray());
        }
    }
}
