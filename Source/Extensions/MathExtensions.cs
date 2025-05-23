using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace GUtils.Extensions
{
    public static class MathExtensions
    {
        public const float Deg2Rad = 0.01745329f;
        public const float Rad2Deg = 57.29578f;
        
        /// <summary>
        /// Retruns the normalized progress from a current value, between a 0 and max values.
        /// <example>(Current=2, Max=4) -> Result=0.5</example>
        /// </summary>
        public static float GetNormalizedFactor(int current, int max)
        {
            if (current == max)
            {
                return 1f;
            }

            current = Math.Max(0, current);
            current = Math.Min(current, max);

            return SafeDivide(current, max);
        }

        /// <summary>
        /// Retruns the normalized progress from a current value, between a 0 and max values.
        /// <example>(Current=2.5, Max=5) -> Result=0.5</example>
        /// </summary>
        public static float GetNormalizedFactor(float current, float max)
        {
            return GetNormalizedFactor(current, 0, max);
        }

        /// <summary>
        /// Retruns the normalized progress from a current value, between a min and max values.
        /// </summary>
        /// <example>(Current=2.5, Min=0, Max=5) -> Result=0.5</example>
        public static float GetNormalizedFactor(float current, float min, float max)
        {
            current = Math.Max(min, current);
            current = Math.Min(current, max);

            float displacedMax = max - min;
            float displacedCurrent = current - min;

            return SafeDivide(displacedCurrent, displacedMax);
        }

        /// <summary>
        /// Divides two values, avoiding undefined behaviour from divide by zero.
        /// </summary>
        public static float SafeDivide(float a, float b)
        {
            if (b == 0f)
            {
                return 0;
            }

            return a / b;
        }

        /// <summary>
        /// Divides two values, avoiding undefined behaviour from divide by zero.
        /// </summary>
        public static float SafeDivide(int a, int b)
        {
            if (b == 0)
            {
                return 0;
            }

            return (float)a / b;
        }

        /// <summary>
        /// Calculates hypotenuse using Pitagoras's algorithm.
        /// </summary>
        public static float Hypotenuse(float side1, float side2)
        {
            return (float)Math.Sqrt(side1 * side1 + side2 * side2);
        }

        /// <summary>
        /// Calculates the summation for a positive integer value value which is  sum of all numbers from 1 to that number.
        /// </summary>
        public static int Summation1ToN(int value)
        {
            return (value * (value + 1)) / 2;
        }

        /// <summary>
        /// Calculates the direction vector given an angle in degrees.
        /// </summary>
        public static Vector2 GetDirectionFromAngle(float angleDegrees)
        {
            return new Vector2(
                (float)Math.Cos(angleDegrees * Deg2Rad),
                (float)Math.Sin(angleDegrees * Deg2Rad)
            );
        }

        /// <summary>
        /// Calculates the angle in degrees given a direction vector.
        /// </summary>
        public static float GetAngleFromDirection(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X) * Rad2Deg;
        }
        
        public static int Clamp(int current, int min, int max)
        {
            current = Math.Max(current, min);
            current = Math.Min(current, max);

            return current;
        }

        public static float Clamp(float current, float min, float max)
        {
            current = Math.Max(current, min);
            current = Math.Min(current, max);

            return current;
        }
        
        public static float Clamp01(float current)
        {
            return Clamp(current, 0f, 1f);
        }
        
        /// <summary>
        ///   <para>Linearly interpolates between a and b by t.</para>
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="t">The interpolation value between the two doubles.</param>
        /// <returns>
        ///   <para>The interpolated double result between the two double values.</para>
        /// </returns>
        public static double Lerp(double a, double b, float t) => a + (b - a) * Clamp01(t);
        
        /// <summary>
        /// Returns a value with the magnitude of x and the sign of y.
        /// </summary>
        public static double CopySign(double magnitude, double sign)
        {
            return sign > 0 ? Math.Abs(magnitude) : -Math.Abs(magnitude);
        }

        public static bool IsEpsilonEqualsZero(float value)
        {
            return Math.Abs(value) <= Mathf.Epsilon;
        }
        
        public static bool AreEpsilonEquals(float value1, float value2)
        {
            return Math.Abs(value1 - value2) <= Mathf.Epsilon;
        }
        
        public static bool AreEpsilonEquals(double value1, double value2)
        {
            return Math.Abs(value1 - value2) <= Mathf.Epsilon;
        }
    }
}
