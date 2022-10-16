using System;
using lab_3_dynamic_structures.structures;

namespace lab_3_dynamic_structures
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleLinkedList<int> ints = new SingleLinkedList<int>();
            ints.Add(1);
            ints.Add(2);
            ints.Add(3);
            ints.Add(4);
            ints.Add(0);
            foreach (var i in ints)
            {
                Console.WriteLine(i);
            }
        }
    }
}