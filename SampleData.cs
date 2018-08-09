using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BinaryTreePyramid
{
    class SampleData
    {
        public static Node FillTree()
        {
            var n20 = new Node(20);
            n20.Left = new Node(9);
            n20.Left.Left = new Node(5);
            n20.Left.Right = new Node(12);
            n20.Left.Right.Right = new Node(15);
            n20.Right = new Node(49);
            n20.Right.Left = new Node(23);
            n20.Right.Right = new Node(52);
            n20.Right.Right.Left = new Node(50);
            return n20;
        }

        public static Node FillTree1()
        {
            var n1 = new Node(1)
            {
                Left = new Node(8)
            };
            n1.Left.Left = new Node(1)
            {
                Left = new Node(4),
                Right = new Node(5)
            };
            n1.Left.Right = new Node(5)
            {
                Left = new Node(5),
                Right = new Node(2)
            };
            n1.Right = new Node(9)
            {
                Left = new Node(5)
                {
                    Left = new Node(5),
                    Right = new Node(2)
                },
                Right = new Node(9)
                {
                    Left = new Node(2),
                    Right = new Node(3)
                }
            };
            return n1;
        }

        public static Node FillTree2()
        {
            string[] linesOfFileSplit = File.ReadAllText("data1.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var bTree = new Node(Convert.ToInt32(linesOfFileSplit[0]));
            var parents = new List<Node>{bTree};//the first line of the file is root of pyramid and first parent
            for (int y=1; y<linesOfFileSplit.Length; y++)//iterating for every line from file with data 
            {
                var splitLineByEveryParent = linesOfFileSplit[y].Split(" ");
                var tempCollectParents = new List<Node>();//collection need for collecting parents for this y cicle
                var repeatingParentsDictionary = new Dictionary<int, int[]>();
                for (int i = 0; i < parents.Count; i++)//iterating for every parent in parrent collection
                {
                    if (i == 0)
                    {
                        parents[i].Left = new Node(Convert.ToInt32(splitLineByEveryParent[i]));
                        parents[i].Right = new Node(Convert.ToInt32(splitLineByEveryParent[i + 1]));
                    }
                    else if (i == parents.Count - 1)
                    {
                        parents[i].Left = new Node(Convert.ToInt32(splitLineByEveryParent[splitLineByEveryParent.Length-2]));
                        parents[i].Right = new Node(Convert.ToInt32(splitLineByEveryParent[splitLineByEveryParent.Length - 1]));
                    }
                    else 
                    {
                        if (repeatingParentsDictionary.ContainsKey(Convert.ToInt32(parents[i].Value)))
                        {
                            parents[i].Left = new Node(repeatingParentsDictionary[Convert.ToInt32(parents[i].Value)][0]);
                            parents[i].Right = new Node(repeatingParentsDictionary[Convert.ToInt32(parents[i].Value)][1]);
                        }
                        else if (!repeatingParentsDictionary.ContainsKey(Convert.ToInt32(parents[i].Value)))//if we found repeating member just write his left and right values to dictionary, for future iterates fast access these values
                        {
                            int indexInPreviousSplitLine = Array.FindIndex(linesOfFileSplit[y - 1].Split(" "), x=> x.Contains(parents[i].Value.ToString()));
                            parents[i].Left = new Node(Convert.ToInt32(splitLineByEveryParent[indexInPreviousSplitLine]));
                            parents[i].Right = new Node(Convert.ToInt32(splitLineByEveryParent[indexInPreviousSplitLine + 1]));
                            repeatingParentsDictionary.Add(Convert.ToInt32(parents[i].Value), new int[]
                            {
                                Convert.ToInt32(splitLineByEveryParent[indexInPreviousSplitLine]), Convert.ToInt32(splitLineByEveryParent[indexInPreviousSplitLine + 1])
                            }); 
                        }
                    }
                    tempCollectParents.Add(parents[i].Left);
                    tempCollectParents.Add(parents[i].Right);
                }
            parents = new List<Node>(tempCollectParents);
            }
            return bTree;
        }
    }
}
