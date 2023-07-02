// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;

namespace System
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object source, object argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
