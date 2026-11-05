// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Threading.Channels
{
    /// <summary>Specifies the backing collection used to buffer items in a channel.</summary>
    public enum ChannelBackingCollection
    {
        /// <summary>Reads buffered items in first-in, first-out order.</summary>
        Queue = 0,

        /// <summary>Reads buffered items in last-in, first-out order.</summary>
        Stack = 1,
    }
}
