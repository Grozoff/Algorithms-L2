using System;

namespace Task1
{
    class Program
    {
        public class TestCase
        {
            public int Node { get; set; }
            public int AddAfter { get; set; }
            public int Find { get; set; }
            public int Index { get; set; }
            public int Count { get; set; }
        }

        static void Main(string[] args)
        {
            var list = new LinkedList();

            // Тест на добавление и поиск ноды
            var testData = new TestCase[5];
            testData[0] = new TestCase()
            {
                Node = 72,
                Find = 72,
                Index = 0
            };
            testData[1] = new TestCase()
            {
                Node = 37,
                Find = 37,
                Index = 1
            };
            testData[2] = new TestCase()
            {
                Node = 48,
                Find = 48,
                Index = 2
            };
            testData[3] = new TestCase()
            {
                Node = 25,
                Find = 25,
                Index = 3
            };
            testData[4] = new TestCase()
            {
                Node = 51,
                Find = 51,
                Index = 4
            };

            foreach (var testCase in testData)
            {
                list.AddNode(testCase.Node);
                var resultAdd = list.FindNode(testCase.Find);
                var expected = list.GetNodeByIndex(testCase.Index);
                if (resultAdd == expected)
                {
                    Console.WriteLine("Test passed");
                }
                else
                {
                    Console.WriteLine("Test failed");
                }
            }

            // Тест на добавление ноды после определенного элемента и тест количества элементов в списке
            var testData2 = new TestCase[2];
            testData2[0] = new TestCase()
            {
                Node = 98,
                AddAfter = 72,
                Find = 98,
                Index = 1,
                Count = 6
            };
            testData2[1] = new TestCase()
            {
                Node = 60,
                AddAfter = 25,
                Find = 60,
                Index = 5,
                Count = 7
            };

            foreach (var testCase2 in testData2)
            {
                var addAfter = list.FindNode(testCase2.AddAfter);
                list.AddNodeAfter(addAfter, testCase2.Node);
                var resultAdd = list.FindNode(testCase2.Find);
                var expected = list.GetNodeByIndex(testCase2.Index);
                var cnt = list.GetCount();
                if ((resultAdd == expected) && (testCase2.Count == cnt))
                {
                    Console.WriteLine("Test passed");
                }
                else
                {
                    Console.WriteLine("Test failed");
                }
            }

            // Тест на удаление ноды по порядковому номеру и значению
            var findNode = list.FindNode(51);
            list.RemoveNode(2);
            list.RemoveNode(findNode);

            if ((list.FindNode(51) == null) && (list.FindNode(2) == null))
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }

        public class Node
        {
            public int Value { get; set; }
            public Node NextNode { get; set; }
            public Node PrevNode { get; set; }
        }

        //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
        public interface ILinkedList
        {
            int GetCount(); // возвращает количество элементов в списке
            void AddNode(int value);  // добавляет новый элемент списка
            void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
            void RemoveNode(int index); // удаляет элемент по порядковому номеру
            void RemoveNode(Node node); // удаляет указанный элемент
            Node FindNode(int searchValue); // ищет элемент по его значению
        }

        private class LinkedList : ILinkedList
        {
            private int count = 0;
            private Node FirstNode;
            private Node LastNode;

            public int GetCount() => count;

            public void AddNode(int value)
            {
                if (FirstNode == null)
                {
                    FirstNode = LastNode = new Node() { Value = value };
                }
                else
                {
                    var newItem = new Node() { Value = value, PrevNode = LastNode };
                    LastNode.NextNode = newItem;
                    LastNode = newItem;
                }
                count++;
            }

            public Node GetNodeByIndex(int nodeIndex)
            {
                Node node;

                if (count - 1 >= nodeIndex)
                {
                    node = FirstNode;
                    for (var i = 1; i <= nodeIndex; i++)
                    {
                        node = node.NextNode;
                    }
                }
                else
                {
                    node = LastNode;
                    for (var i = count - 1; i > nodeIndex; i--)
                    {
                        node = node.PrevNode;
                    }
                }
                return node;
            }

            public void AddNodeAfter(Node node, int value)
            {
                var next = node.NextNode;
                var newItem = new Node() { Value = value, NextNode = next, PrevNode = node };
                node.NextNode = newItem;
                if (next != null)
                {
                    next.PrevNode = newItem;
                }
                count++;
            }

            public void RemoveNode(int index)
            {
                if (index == 0)
                {
                    if (FirstNode.NextNode is { } node)
                    {
                        FirstNode = node;
                        node.PrevNode = null;
                    }
                    else
                    {
                        FirstNode = LastNode = null;
                    }
                }
                else
                {
                    var del = GetNodeByIndex(index);

                    if (del.NextNode != null)
                    {
                        del.NextNode.PrevNode = del.PrevNode;
                    }
                    del.PrevNode.NextNode = del.NextNode;
                    del.NextNode = del.PrevNode = null;
                }
                count--;
            }

            public void RemoveNode(Node node)
            {
                if (FirstNode == node)
                {
                    if (FirstNode == LastNode)
                    {
                        FirstNode = LastNode = null;
                    }
                    else
                    {
                        FirstNode = node.NextNode;
                        node.NextNode = null;
                        FirstNode.PrevNode = null;
                    }
                }
                else
                {
                    if (node.NextNode != null)
                    {
                        node.NextNode.PrevNode = node.PrevNode;
                    }
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode = node.PrevNode = null;
                }
                count--;
            }

            public Node FindNode(int searchValue)
            {
                var current = FirstNode;
                while (current != null)
                {
                    if (current.Value == searchValue)
                    {
                        return current;
                    }
                    current = current.NextNode;
                }
                return null;
            }
        }

    }
}
