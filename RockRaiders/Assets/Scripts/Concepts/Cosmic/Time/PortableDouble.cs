using System;
using System.Globalization;

namespace Assets.Scripts.Concepts.Cosmic.Time
{
    public class PortableDouble
    {
        internal double Value;

        /// <summary>Represents the smallest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
        public const double MinValue = double.MinValue;

        /// <summary>Represents the largest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
        public const double MaxValue = double.MaxValue;

        /// <summary>Represents the smallest positive <see cref="T:System.Double" /> value that is greater than zero. This field is constant.</summary>
        public const double Epsilon = double.Epsilon;

        /// <summary>Represents negative infinity. This field is constant.</summary>
        public const double NegativeInfinity = double.NegativeInfinity;

        /// <summary>Represents positive infinity. This field is constant.</summary>
        public const double PositiveInfinity = double.PositiveInfinity;

        /// <summary>Represents a value that is not a number (NaN). This field is constant.</summary>
        public const double NaN = double.NaN;

        public PortableDouble(double value)
        {
            Value = value;
        }

        public PortableDouble()
        {
        }

        /// <summary>Compares this instance to a specified double-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified double-precision floating-point number.</summary>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Double.NaN" />), <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />). </returns>
        /// <param name="value">A double-precision floating-point number to compare. </param>
        public int CompareTo(double value) => Value.CompareTo(value);

        /// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Double" /> object represent the same value.</summary>
        /// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
        /// <param name="obj">A <see cref="T:System.Double" /> object to compare to this instance.</param>
        public bool Equals(double obj) => Value.Equals(obj);

        /// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
        /// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Double" /> and equals the value of this instance; otherwise, false.</returns>
        /// <param name="obj">An object to compare with this instance. </param>
        public override bool Equals(object obj) => Value.Equals(obj);

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity </summary>
        /// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />; otherwise, false.</returns>
        /// <param name="d">A double-precision floating-point number. </param>
        public static bool IsInfinity(double d) => double.IsInfinity(d);

        /// <summary>Returns a value that indicates whether the specified value is not a number (<see cref="F:System.Double.NaN" />).</summary>
        /// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.NaN" />; otherwise, false.</returns>
        /// <param name="d">A double-precision floating-point number. </param>
        public static bool IsNaN(double d) => double.IsNaN(d);

        /// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
        /// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.NegativeInfinity" />; otherwise, false.</returns>
        /// <param name="d">A double-precision floating-point number. </param>
        public static bool IsNegativeInfinity(double d) => double.IsNegativeInfinity(d);

        /// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
        /// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" />; otherwise, false.</returns>
        /// <param name="d">A double-precision floating-point number. </param>
        public static bool IsPositiveInfinity(double d) => double.IsPositiveInfinity(d);

        /// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are equal.</summary>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
        /// <param name="left">The first value to compare. </param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator ==(PortableDouble left, PortableDouble right) => left.Value == right.Value;

        /// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than another specified <see cref="T:System.Double" /> value.</summary>
        /// <returns>true if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, false.</returns>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator >(PortableDouble left, PortableDouble right) => left.Value > right.Value;

        /// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than or equal to another specified <see cref="T:System.Double" /> value.</summary>
        /// <returns>true if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, false.</returns>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator >=(PortableDouble left, PortableDouble right) => left.Value >= right.Value;

        /// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are not equal.</summary>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator !=(PortableDouble left, PortableDouble right) => left.Value != right.Value;

        /// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than another specified <see cref="T:System.Double" /> value.</summary>
        /// <returns>true if <paramref name="left" /> is less than <paramref name="right" />; otherwise, false.</returns>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator <(PortableDouble left, PortableDouble right) => left.Value < right.Value;

        /// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than or equal to another specified <see cref="T:System.Double" /> value.</summary>
        /// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        public static bool operator <=(PortableDouble left, PortableDouble right) => left.Value <= right.Value;

        /// <summary>Converts the string representation of a number to its double-precision floating-point number equivalent.</summary>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
        /// <param name="s">A string that contains a number to convert. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="s" /> is null. </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="s" /> does not represent a number in a valid format. </exception>
        /// <exception cref="T:System.OverflowException">
        /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
        public static double Parse(string s) => double.Parse(s);

        /// <summary>Converts the string representation of a number in a specified style to its double-precision floating-point number equivalent.</summary>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
        /// <param name="s">A string that contains a number to convert. </param>
        /// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is a combination of <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="s" /> is null. </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="s" /> does not represent a number in a valid format. </exception>
        /// <exception cref="T:System.OverflowException">
        /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value. </exception>
        public static double Parse(string s, NumberStyles style) => double.Parse(s, style);

        /// <summary>Converts the string representation of a number in a specified style and culture-specific format to its double-precision floating-point number equivalent.</summary>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
        /// <param name="s">A string that contains a number to convert. </param>
        /// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="s" /> is null. </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="s" /> does not represent a numeric value. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
        /// <exception cref="T:System.OverflowException">
        /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
        public static double Parse(string s, NumberStyles style, IFormatProvider provider) => double.Parse(s, style, provider);

        /// <summary>Converts the string representation of a number in a specified culture-specific format to its double-precision floating-point number equivalent.</summary>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
        /// <param name="s">A string that contains a number to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="s" /> is null. </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="s" /> does not represent a number in a valid format. </exception>
        /// <exception cref="T:System.OverflowException">
        /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
        public static double Parse(string s, IFormatProvider provider) => double.Parse(s, provider);

        /// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
        /// <returns>The string representation of the value of this instance.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString() => Value.ToString();

        /// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
        /// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
        /// <param name="provider">An object that supplies culture-specific formatting information. </param>
        /// <filterpriority>1</filterpriority>
        public string ToString(IFormatProvider provider) => Value.ToString(provider);

        /// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
        /// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
        /// <param name="format">A numeric format string.</param>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="format" /> is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        public string ToString(string format) => Value.ToString(format);

        /// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
        /// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information. </param>
        /// <filterpriority>1</filterpriority>
        public string ToString(string format, IFormatProvider provider) => Value.ToString(format, provider);

        // User-defined conversion from PortableDouble to double
        public static implicit operator double(PortableDouble d) => d.Value;
        //  User-defined conversion from double to PortableDouble
        public static implicit operator PortableDouble(double d) => new PortableDouble(d);
    }
}