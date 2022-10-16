using System;
using System.Collections.Generic;
using System.Text;

namespace lab_3_dynamic_structures.structures
{
    public sealed class StackOnSingleLinkedList<T>
    {
        private SingleLinkedList<T> List { get; }

        public int Count() => this.List.Count;

        public StackOnSingleLinkedList(IEnumerable<T> collection) => this.List = new SingleLinkedList<T>(collection);

        public StackOnSingleLinkedList()
        {
            this.List = new SingleLinkedList<T>();
        }

        public bool IsEmpty() => this.List.IsEmpty();

        public void Push(T element) => this.List.Add(element);

        public T Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Stack empty");
            }
            var element = this.List.Last();
            this.List.Remove(element);
            return element.Value;
        }

        public SingleLinkedListNode<T> Top() => this.List.Last();

        public void Print() => Console.WriteLine(this.ToString());
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in this.List)
            {
                sb.Append(element).Append(' ');
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}