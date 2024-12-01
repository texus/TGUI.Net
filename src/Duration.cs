/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2024 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TGUI
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Duration : IEquatable<Duration>
    {
        /// <summary>
        /// Predefined "zero" duration value
        /// </summary>
        public static readonly Duration Zero = FromNanoseconds(0);

        /// <summary>
        /// Construct a <see cref="Duration"/> from a number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds</param>
        /// <returns>Duration constructed from the amount of seconds</returns>
        public static Duration FromSeconds(float seconds) => tguiDuration_fromSeconds(seconds);

        /// <summary>
        /// Construct a <see cref="Duration"/> from a number of milliseconds
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds</param>
        /// <returns>Duration constructed from the amount of milliseconds</returns>
        public static Duration FromMilliseconds(int milliseconds) => tguiDuration_fromMilliseconds(milliseconds);

        /// <summary>
        /// Construct a <see cref="Duration"/> from a number of microseconds
        /// </summary>
        /// <param name="microseconds">Number of microseconds</param>
        /// <returns>Duration constructed from the amount of microseconds</returns>
        public static Duration FromMicroseconds(long microseconds) => tguiDuration_fromMicroseconds(microseconds);

        /// <summary>
        /// Construct a <see cref="Duration"/> from a number of nanoseconds
        /// </summary>
        /// <param name="nanoseconds">Number of nanoseconds</param>
        /// <returns>Duration constructed from the amount of nanoseconds</returns>
        public static Duration FromNanoseconds(long nanoseconds) => tguiDuration_fromNanoseconds(nanoseconds);

        /// <summary>
        /// Construct a <see cref="Duration"/> from a <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan">A TimeSpan representing the duration to represent</param>
        /// <returns>Duration constructed from an existing TimeSpan</returns>
        public static Duration FromTimeSpan(TimeSpan timeSpan) => tguiDuration_fromMicroseconds(timeSpan.Ticks / (TimeSpan.TicksPerMillisecond / 1000));

        /// <summary>
        /// Returns the <see cref="Duration"/> as a number of seconds
        /// </summary>
        public float AsSeconds() => (float)(_nanoseconds / 1000000000.0);

        /// <summary>
        /// Returns the <see cref="Duration"/> as a number of milliseconds
        /// </summary>
        public int AsMilliseconds() => (int)(_nanoseconds / 1000000);

        /// <summary>
        /// Returns the <see cref="Duration"/> as a number of microseconds
        /// </summary>
        public long AsMicroseconds() => _nanoseconds / 1000;

        /// <summary>
        /// Returns the <see cref="Duration"/> as a number of nanoseconds
        /// </summary>
        public long AsNanoseconds() => _nanoseconds;

        /// <summary>
        /// Returns the <see cref="Duration"/> as a TimeSpan
        /// </summary>
        public TimeSpan ToTimeSpan() => TimeSpan.FromMilliseconds(_nanoseconds / 1000000.0);

        /// <summary>
        /// Implicit conversion from <see cref="TimeSpan"/> to <see cref="Duration"/>, allowing intuitive use
        /// </summary>
        public static implicit operator Duration(TimeSpan timeSpan) => FromTimeSpan(timeSpan);

        /// <summary>
        /// Compare two durations and checks if they are equal
        /// </summary>
        /// <returns>Durations are equal</returns>
        public static bool operator ==(Duration left, Duration right) => left.Equals(right);

        /// <summary>
        /// Compare two durations and checks if they are not equal
        /// </summary>
        /// <returns>Durations are not equal</returns>
        public static bool operator !=(Duration left, Duration right) => !left.Equals(right);

        /// <summary>
        /// Compare duration and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and duration are equal</returns>
        public override bool Equals(object? obj) => (obj is Duration) && Equals((Duration)obj);

        /// <summary>
        /// Compare two durations and checks if they are equal
        /// </summary>
        /// <param name="other">Duration to check</param>
        /// <returns>Durations are equal</returns>
        public bool Equals(Duration other) => _nanoseconds == other._nanoseconds;

        /// <summary>
        /// Overload of &lt; operator to compare two duration values
        /// </summary>
        /// <returns>True if left is lesser than right</returns>
        public static bool operator <(Duration left, Duration right) => left._nanoseconds < right._nanoseconds;

        /// <summary>
        /// Overload of &lt;= operator to compare two duration values
        /// </summary>
        /// <returns>True if left is lesser or equal than right</returns>
        public static bool operator <=(Duration left, Duration right) => left._nanoseconds <= right._nanoseconds;

        /// <summary>
        /// Overload of &gt; operator to compare two duration values
        /// </summary>
        /// <returns>True if left is greater than right</returns>
        public static bool operator >(Duration left, Duration right) => left._nanoseconds > right._nanoseconds;

        /// <summary>
        /// Overload of &gt;= operator to compare two duration values
        /// </summary>
        /// <returns>True if left is greater or equal than right</returns>
        public static bool operator >=(Duration left, Duration right) => left._nanoseconds >= right._nanoseconds;

        /// <summary>
        /// Overload of binary - operator to subtract two duration values
        /// </summary>
        /// <returns>Difference of the two duration values</returns>
        public static Duration operator -(Duration left, Duration right) => FromNanoseconds(left._nanoseconds - right._nanoseconds);

        /// <summary>
        /// Overload of binary + operator to add two duration values
        /// </summary>
        /// <returns>Sum of the two duration values</returns>
        public static Duration operator +(Duration left, Duration right) => FromNanoseconds(left._nanoseconds + right._nanoseconds);

        /// <summary>
        /// Overload of binary * operator to scale a duration value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        public static Duration operator *(Duration left, float right) => FromSeconds(left.AsSeconds() * right);

        /// <summary>
        /// Overload of binary * operator to scale a duration value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        public static Duration operator *(Duration left, long right) => FromNanoseconds(left._nanoseconds * right);

        /// <summary>
        /// Overload of binary * operator to scale a duration value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        public static Duration operator *(float left, Duration right) => FromSeconds(left * right.AsSeconds());

        /// <summary>
        /// Overload of binary * operator to scale a duration value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        public static Duration operator *(long left, Duration right) => FromNanoseconds(left * right._nanoseconds);

        /// <summary>
        /// Overload of binary / operator to scale a duration value
        /// </summary>
        /// <returns>left divided by the right</returns>
        public static float operator /(Duration left, Duration right) => left.AsSeconds() / right.AsSeconds();

        /// <summary>
        /// Overload of binary / operator to scale a duration value
        /// </summary>
        /// <returns>left divided by the right</returns>
        public static Duration operator /(Duration left, float right) => FromSeconds(left.AsSeconds() / right);

        /// <summary>
        /// Overload of binary / operator to scale a duration value
        /// </summary>
        /// <returns>left divided by the right</returns>
        public static Duration operator /(Duration left, long right) => FromNanoseconds(left._nanoseconds / right);

        /// <summary>
        /// Overload of binary % operator to compute remainder of a duration value
        /// </summary>
        /// <returns>left modulo of right</returns>
        public static Duration operator %(Duration left, Duration right) => FromNanoseconds(left._nanoseconds % right._nanoseconds);

        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode() => _nanoseconds.GetHashCode();

        private readonly long _nanoseconds;

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Duration tguiDuration_fromSeconds(float amount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Duration tguiDuration_fromMilliseconds(int amount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Duration tguiDuration_fromMicroseconds(long amount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Duration tguiDuration_fromNanoseconds(long amount);

        #endregion
    };
}
