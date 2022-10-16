using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace lab_3_dynamic_structures.structures
{
    public class SingleLinkedList<T> : IEnumerable<T>
    {
        internal SingleLinkedListNode<T> Head { get; set; }
        internal int Count { get; private set; }

        public SingleLinkedList()
        {
        }

        public SingleLinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            foreach (T obj in collection)
                this.AddLast(obj);
        }

        public void Add(T value) => AddLast(value);

        public SingleLinkedListNode<T> First() => Head;

        private SingleLinkedListNode<T> AddLast(T value)
        {
            SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(value);
            if (this.Head == null)
            {
                InsertNodeToEmptyList(newNode);
            }
            else
            {
                SingleLinkedListNode<T> node = Last();
                InsertNodeAfter(node, newNode);
            }

            return newNode;
        }

        public SingleLinkedListNode<T> Last()
        {
            if (this.Head == null)
            {
                return null;
            }
            SingleLinkedListNode<T> node = this.Head;
            while (node.Next != null)
            {
                node = node.Next;
            }

            return node;
        }

        public SingleLinkedListNode<T> Find(T value)
        {
            if (this.Head == null)
            {
                return null;
            }
            SingleLinkedListNode<T> node = this.Head;
            while ((node = node.Next) != null)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        private void InsertNodeAfter(SingleLinkedListNode<T> nodeBefore, SingleLinkedListNode<T> newNode)
        {
            newNode.Next = nodeBefore.Next;
            nodeBefore.Next = newNode;
            ++this.Count;
        }

        private void InsertNodeToEmptyList(SingleLinkedListNode<T> newNode)
        {
            this.Head = newNode;
            ++this.Count;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

        public SingleLinkedList<T>.Enumerator GetEnumerator() => new SingleLinkedList<T>.Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator :
            IEnumerator<T>,
            IDisposable,
            IEnumerator,
            ISerializable,
            IDeserializationCallback
        {
            private readonly SingleLinkedList<T> _list;
            private SingleLinkedListNode<T> _node;
            private T _current;
            private int _index;

            internal Enumerator(SingleLinkedList<T> list)
            {
                this._list = list;
                this._node = list.Head;
                this._current = default(T);
                this._index = 0;
            }

            public T Current => this._current;

            object IEnumerator.Current
            {
                get => Current;
            }

            public bool MoveNext()
            {
                if (this._node == null)
                {
                    this._index = this._list.Count + 1;
                    return false;
                }

                ++this._index;
                this._current = this._node.Value;
                this._node = this._node.Next;
                if (this._node == this._list.Head)
                    this._node = null;
                return true;
            }

            void IEnumerator.Reset()
            {
                this._current = default(T);
                this._node = this._list.Head;
                this._index = 0;
            }

            public void Dispose()
            {
            }

            void ISerializable.GetObjectData(
                SerializationInfo info,
                StreamingContext context)
            {
                throw new PlatformNotSupportedException();
            }

            void IDeserializationCallback.OnDeserialization(object sender) => throw new PlatformNotSupportedException();
        }
    }

    public class SingleLinkedListNode<T>
    {
        public SingleLinkedListNode(T value)
        {
            this.Value = value;
        }

        public SingleLinkedListNode()
        {
        }

        public T Value { get; set; }
        public SingleLinkedListNode<T> Next { get; set; }
    }
}