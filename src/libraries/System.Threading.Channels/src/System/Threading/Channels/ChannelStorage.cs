// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Channels
{
    /// <summary>Provides item storage independently of an unbounded channel's reader coordination.</summary>
    internal interface IChannelStorage<T> : IDebugEnumerable<T>
    {
        bool IsEmpty { get; }
        int Count { get; }
        void Add(T item); // serialization is assumed
        bool TryTake([MaybeNullWhen(false)] out T item);
        bool TryPeek([MaybeNullWhen(false)] out T item);
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
    internal readonly struct ConcurrentQueueChannelStorage<T> : IChannelStorage<T>
    {
        private readonly ConcurrentQueue<T> _items;

        public ConcurrentQueueChannelStorage() => _items = new ConcurrentQueue<T>();

        public bool IsEmpty => _items.IsEmpty;
        public int Count => _items.Count;
        public void Add(T item) => _items.Enqueue(item);
        public bool TryTake([MaybeNullWhen(false)] out T item) => _items.TryDequeue(out item);
        public bool TryPeek([MaybeNullWhen(false)] out T item) => _items.TryPeek(out item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
    internal readonly struct SingleConsumerQueueChannelStorage<T> : IChannelStorage<T>
    {
        private readonly SingleProducerSingleConsumerQueue<T> _items;

        public SingleConsumerQueueChannelStorage() => _items = new SingleProducerSingleConsumerQueue<T>();

        public bool IsEmpty => _items.IsEmpty;
        public int Count => _items.Count;
        public void Add(T item) => _items.Enqueue(item);
        public bool TryTake([MaybeNullWhen(false)] out T item) => _items.TryDequeue(out item);
        public bool TryPeek([MaybeNullWhen(false)] out T item) => _items.TryPeek(out item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
    internal readonly struct ConcurrentStackChannelStorage<T> : IChannelStorage<T>
    {
        private readonly ConcurrentStack<T> _items;

        public ConcurrentStackChannelStorage() => _items = new ConcurrentStack<T>();

        public bool IsEmpty => _items.IsEmpty;
        public int Count => _items.Count;
        public void Add(T item) => _items.Push(item);
        public bool TryTake([MaybeNullWhen(false)] out T item) => _items.TryPop(out item);
        public bool TryPeek([MaybeNullWhen(false)] out T item) => _items.TryPeek(out item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    }

    // TODO: come up with an optimized version as in queue
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
    internal readonly struct SingleConsumerStackChannelStorage<T> : IChannelStorage<T>
    {
        private readonly ConcurrentStack<T> _items;

        public SingleConsumerStackChannelStorage() => _items = new ConcurrentStack<T>();

        public bool IsEmpty => _items.IsEmpty;
        public int Count => _items.Count;
        public void Add(T item) => _items.Push(item);
        public bool TryTake([MaybeNullWhen(false)] out T item) => _items.TryPop(out item);
        public bool TryPeek([MaybeNullWhen(false)] out T item) => _items.TryPeek(out item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    }
}
