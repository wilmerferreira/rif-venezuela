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
        /// Gets the kind of taxpayer represented by this RIF. Valid values are V (Venezuelan natural person), E (foreign natural person), J (legal entity), C (communal organization), P (person identified by a passport), and G (government entity).
        /// </summary>
        public char Kind { get { return _kind; } }

        /// <summary>
        /// Gets the taxpayer's identifier.
        /// </summary>
        public int Identifier { get { return _identifier; } }

        /// <summary>
        /// Gets the check digit for this RIF.
        /// </summary>
        public byte CheckDigit { get { return _checksum; } }

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

            var checksum = 11 - sum % 11;

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

        /// <summary>
        /// Converts the string representation of a RIF to its equivalent Rif value.
        /// </summary>
        /// <param name="input">The string representation of a RIF.</param>
        /// <returns>The parsed Rif value.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="input"/> is null.</exception>
        /// <exception cref="FormatException">The value of <paramref name="input"/> is not a valid RIF.</exception>
        public static Rif Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            Rif result;
            if (!TryParse(input, out result))
                throw new FormatException($"The {nameof(input)} string was not in a valid {nameof(Rif)} format.");

            return result;
        }

        /// <summary>
        /// Converts the string representation of a RIF in the specified format to its equivalent Rif value.
        /// </summary>
        /// <param name="input">The string representation of a RIF.</param>
        /// <param name="format">The format specifier: D, N, or B.</param>
        /// <returns>The parsed Rif value.</returns>
        public static Rif ParseExact(string input, string format)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException(nameof(format));

            Rif result;
            if (!TryParseExact(input, format, out result))
                throw new FormatException($"The {nameof(input)} string was not in a valid {nameof(Rif)} format.");

            return result;
        }

        /// <summary>
        /// Attempts to convert the string representation of a RIF to its equivalent Rif value.
        /// </summary>
        /// <param name="input">The string representation of a RIF.</param>
        /// <param name="result">When this method returns, the parsed value or the default Rif value.</param>
        /// <returns>true if the value was parsed successfully; otherwise, false.</returns>
        public static bool TryParse(string input, out Rif result)
        {
            result = default(Rif);

            if (string.IsNullOrWhiteSpace(input))
                return false;

            return TryParseExact(input, "D", out result)
                || TryParseExact(input, "N", out result)
                || TryParseExact(input, "B", out result);
        }

        /// <summary>
        /// Attempts to convert the string representation of a RIF in the specified format to its equivalent Rif value.
        /// </summary>
        /// <param name="input">The string representation of a RIF.</param>
        /// <param name="format">The format specifier: D, N, or B.</param>
        /// <param name="result">When this method returns, the parsed value or the default Rif value.</param>
        /// <returns>true if the value was parsed successfully; otherwise, false.</returns>
        public static bool TryParseExact(string input, string format, out Rif result)
        {
            result = default(Rif);

            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(format))
                return false;

            if (format.Length != 1)
                return false;

            var normalizedFormat = char.ToUpperInvariant(format[0]);
            var expectedLength = normalizedFormat == 'D' ? 12 : normalizedFormat == 'N' ? 10 : normalizedFormat == 'B' ? 14 : 0;
            if (expectedLength == 0 || input.Length != expectedLength)
                return false;

            var offset = 0;
            if (normalizedFormat == 'B')
            {
                if (input[0] != '{' || input[input.Length - 1] != '}')
                    return false;

                offset = 1;
            }

            if (input[offset] == '-' || input[offset] == '+' || !IsKind(input[offset]))
                return false;

            var kind = char.ToUpperInvariant(input[offset]);
            if (normalizedFormat == 'D' && (input[offset + 1] != '-' || input[offset + 10] != '-'))
                return false;
            if (normalizedFormat == 'N' && offset != 0)
                return false;
            if (normalizedFormat == 'B' && (input[offset + 1] != '-' || input[offset + 10] != '-'))
                return false;

            var identifierStart = normalizedFormat == 'N' ? offset + 1 : offset + 2;
            var checksumIndex = normalizedFormat == 'N' ? offset + 9 : offset + 11;
            for (var i = 0; i < 8; i++)
            {
                if (!IsDigit(input[identifierStart + i]))
                    return false;
            }

            if (!IsDigit(input[checksumIndex]))
                return false;

            var identifier = 0;
            for (var i = 0; i < 8; i++)
                identifier = identifier * 10 + input[identifierStart + i] - '0';

            if (identifier < 1)
                return false;

            var parsed = new Rif(kind, identifier);
            if (parsed.CheckDigit != input[checksumIndex] - '0')
                return false;

            result = parsed;
            return true;
        }

        private static bool IsKind(char value)
        {
            value = char.ToUpperInvariant(value);
            return value == 'V' || value == 'E' || value == 'J' || value == 'C' || value == 'P' || value == 'G';
        }

        private static bool IsDigit(char value)
        {
            return value >= '0' && value <= '9';
        }

        /// <summary>
        /// Converts a Rif to its standard string representation.
        /// </summary>
        /// <param name="rif">The Rif value to convert.</param>
        /// <returns>The Rif formatted as "X-########-#".</returns>
        public static implicit operator string(Rif rif)
        {
            return rif.ToString();
        }
    }
}
