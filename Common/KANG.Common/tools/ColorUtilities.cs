
using System;
using System.Drawing;

namespace KANG.Common.tools
{
    /// <summary>
    /// Color Utilities.
    /// </summary>
    public static class ColorUtilities
    {
        /// <summary>
        /// Field ColorRandom.
        /// </summary>
        private static readonly Random ColorRandom = new Random();

        /// <summary>
        /// Returns a random color.
        /// </summary>
        /// <returns>The result color.</returns>
        public static Color GetRandomColor()
        {
            return Color.FromKnownColor((KnownColor)ColorRandom.Next(1, 174));
        }
    }
}
