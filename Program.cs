using System;
using System.Collections.Generic;

namespace BinaryTreePyramid
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintMaxPath(SampleData.FillTree2());
            Console.ReadKey();
        }

        private static bool IsLeafElement(Node node)//method for check is it leaf node
        {
            if (node == null) return false;
            if (node.Left == null && node.Right == null) return true;
            return false;
        }

        private static bool OddEvRule(Stack<Node> stack)//method for check odd/even rule
        {
            var isCompliesOddEvenRule = false;
            var tempStack = new Stack<Node>(stack);
            while (tempStack.Count != 1)
                if ((tempStack.Pop().Value + tempStack.Peek().Value) % 2 != 0)
                    isCompliesOddEvenRule = true;
                else
                {
                    isCompliesOddEvenRule = false;
                    break;
                }

            return isCompliesOddEvenRule;
        }

        private static void PrintMaxPath(Node node)//method for finding max root->leaf path
        {
            var correctRootLeaf = new List<int>();
            var max = 0;
            var stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count != 0)//looking for all paths from root to leaf
            {
                var item = stack.Peek();

                if (!item.IsVisited)//if node not visited go left or right
                {
                    var tempLeft = item.Left;
                    if (tempLeft != null && !tempLeft.IsVisited)
                    {
                        stack.Push(tempLeft);
                        continue;
                    }

                    var tempRight = item.Right;
                    if (tempRight != null && !tempRight.IsVisited)
                    {
                        stack.Push(tempRight);
                        continue;
                    }

                    item.IsVisited = true;
                }
                else
                {
                    item.IsVisited = true;
                    if (IsLeafElement(item))//if node was visited, checking is it leaf and then below odd/even rule
                    {
                        var tempMax = 0;
                        if (OddEvRule(stack))
                        {
                            foreach (var value in stack) tempMax += value.Value;

                            if (tempMax > max)
                            {
                                max = tempMax;
                                correctRootLeaf.Clear();
                                foreach (var element in stack) correctRootLeaf.Add(element.Value);
                            }
                        }
                    }
                    stack.Pop();
                }
            }
            Console.WriteLine("The maximum is " + max);
            Console.WriteLine("The winner-path is: ");
            for (var i = correctRootLeaf.Count; i >= 1; i--) Console.Write(correctRootLeaf[i - 1] + "->");
        }
    }
}
