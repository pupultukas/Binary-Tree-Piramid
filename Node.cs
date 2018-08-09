using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreePyramid
{
    class Node
    {
        public Node(int value)
        {
            Value = value;
        }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public bool IsVisited { get; set; }
        public int Value { get; set; }
    }
}
