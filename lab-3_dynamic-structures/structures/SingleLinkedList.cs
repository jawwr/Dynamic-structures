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
        public bool IsEmpty() => Count == 0;

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

        public T this[int index] => Find(index).Value;

        public SingleLinkedListNode<T> First() => Head;

        public SingleLinkedListNode<T> Find(int index)
        {
            if (this.Head == null)
            {
                return null;
            }

            SingleLinkedListNode<T> node = this.Head;
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
            }

            return node;
        }

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

        public Enumerator GetEnumerator() => new(this);

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

        public void Remove(SingleLinkedListNode<T> element)
        {
            var node = Find(element);
            SingleLinkedListNode<T> nodeBefore;
            if (this.Head.Equals(node))
            {
                var rmNode = Head;
                Head = null;
            }
            else
            {
                nodeBefore = FindNodeBefore(node);
                nodeBefore.Next = node.Next;
            }
            --this.Count;
        }

        private SingleLinkedListNode<T> FindNodeBefore(SingleLinkedListNode<T> node)
        {
            if (this.Head == null)
            {
                return null;
            }

            SingleLinkedListNode<T> nodeBefore = this.Head;
            while (nodeBefore != null)
            {
                if (nodeBefore.Next.Equals(node))
                {
                    return nodeBefore;
                }

                nodeBefore = nodeBefore.Next;
            }

            return null;
        }

        public SingleLinkedListNode<T> Find(SingleLinkedListNode<T> findNode)
        {
            if (this.Head == null)
            {
                return null;
            }else if (this.Head.Equals(findNode))
            {
                return Head;
            }

            SingleLinkedListNode<T> node = this.Head;
            while ((node = node.Next) != null)
            {
                if (node.Equals(findNode))
                {
                    return node;
                }
            }

            return null;
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

        public T Value { get; }
        public SingleLinkedListNode<T> Next { get; set; }
    }
}