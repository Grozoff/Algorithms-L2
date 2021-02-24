using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new LinkedList();
            list.AddNode(72);
            list.AddNode(37);
            list.AddNode(48);
            list.AddNode(25);
            var findNode = list.FindNode(25);
            list.AddNodeAfter(findNode, 10);
            list.RemoveNode(2);
            list.RemoveNode(findNode);
            list.GetCount();
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

            private Node GetNodeByIndex(int nodeIndex)
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
