// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Channels
{
    internal interface IDebugEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }

    internal class DebugEnumeratorDebugView<T>(IDebugEnumerable<T> enumerable)
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items { get; } = [.. enumerable];
    }

    internal sealed class DebugEnumeratorDebugView<T, TStorage>(IDebugEnumerable<T> enumerable) 
        : DebugEnumeratorDebugView<T>(enumerable);
}
