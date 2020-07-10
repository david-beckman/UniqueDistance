//-----------------------------------------------------------------------
// <copyright file="Translation.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    internal enum Translation
    {
        None,
        ReflectDiagonallyDownward,
        ReflectDiagonallyUpward,
        ReflectHorizontally,
        ReflectThroughCenter,
        ReflectVertically,
        Rotate90DegreesClockwise,
        Rotate180DegreesClockwise,
        Rotate270DegreesClockwise,
    }
}
