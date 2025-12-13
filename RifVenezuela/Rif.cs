using System;
using System.Diagnostics;

namespace RifVenezuela
{
    /// <summary>
    /// Represents a Registro de Información Fiscal (RIF).
    /// </summary>
    [DebuggerDisplay("{ToString(\"B\")}")]
    public struct Rif
    {
        private char _kind;
        private int _identifier;
        private byte _checksum;

        /// <summary>
        /// Initializes a new instance of the RifVenezuela.Rif structure by using a kind and identifier.
        /// </summary>
        /// <param name="kind">Subject's kind</param>
        /// <param name="identifier">Subject's identifier</param>
        /// <exception cref="ArgumentOutOfRangeException">When the identifier is not between 1 and 99,999,999</exception>
        public Rif(char kind, int identifier)
        {
            if (identifier < 1 || identifier > 99_999_999)
                throw new ArgumentOutOfRangeException(nameof(identifier), "Identifier must be between 1 and 99,999,999");

            kind = char.ToUpperInvariant(kind);

            if (kind != 'V' && kind != 'E' && kind != 'J' && kind != 'C' && kind != 'P' && kind != 'G')
                throw new ArgumentOutOfRangeException(nameof(kind), "Invalid kind");

            _kind = kind;
            _identifier = identifier;
            _checksum = Checksum(kind, identifier);
        }

        private static byte Checksum(char kind, int identifier)
        {
            var sum = 0;

            if (kind == 'V') sum = 4;
            else if (kind == 'E') sum = 8;
            else if (kind == 'J' || kind == 'C') sum = 12;
            else if (kind == 'P') sum = 16;
            else if (kind == 'G') sum = 20;
            else throw new ArgumentOutOfRangeException(nameof(kind), "Invalid kind");

            var digits = new int[8];
            var position = 1;

            while (identifier > 0)
            {
                digits[8 - position] = identifier % 10;
                identifier /= 10;
                position++;
            }

            sum += digits[0] * 3;
            sum += digits[1] * 2;
            sum += digits[2] * 7;
            sum += digits[3] * 6;
            sum += digits[4] * 5;
            sum += digits[5] * 4;
            sum += digits[6] * 3;
            sum += digits[7] * 2;

            var checksum = 11  - sum % 11;

            if (checksum > 9)
                checksum = 0;

            return (byte)checksum;
        }

        /// <summary>
        /// Returns a string representation of the value of this instance in the standard format.
        /// </summary>
        /// <returns>The value of this RifVenezuela.Rif, formatted as follows: $-########-#</returns>
        public override string ToString()
        {
            return ToString("D");
        }

        /// <summary>
        /// Returns a string representation of the value of this Rif instance, according to the provided format specifier.
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this Rif. The format parameter can be "N", "D", "B". If format is null or an empty string (""), "D" is used.</param>
        /// <returns>The value of this Rif, represented with an uppercase prefix followed by 8 digits, and the check digit</returns>
        /// <exception cref="FormatException">The format supplied is not supported</exception>
        public string ToString(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                format = "D";

            switch (format)
            {
                case "D":
                    return $"{_kind}-{_identifier:D8}-{_checksum}";
                case "N":
                    return $"{_kind}{_identifier:D8}{_checksum}";
                case "B":
                    return $"{{{_kind}-{_identifier:D8}-{_checksum}}}";
                default:
                    throw new FormatException();
            }
        }
    }
}
