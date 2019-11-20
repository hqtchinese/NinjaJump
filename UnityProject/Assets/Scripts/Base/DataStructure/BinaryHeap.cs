using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Data
{
    public class BinaryHeapNode<T> where T : IComparable
    {
        public BinaryHeapNode<T> LeftChild => Childs[0];
        public BinaryHeapNode<T> RightChild => Childs[1];
        public BinaryHeapNode<T> Parent { get; set; }
        public T Value { get; set; }
        public int Index { get; set; }

        private BinaryHeapNode<T>[] Childs { get; set; }

        public BinaryHeapNode()
        {
            Childs = new BinaryHeapNode<T>[2]{null,null};
        }

        public void SetChild(BinaryHeapNode<T> childNode)
        {
            Childs[(childNode.Index & 1)] = childNode;
        }

        public void RemoveChild()
        {
            if (Childs[1] != null)
                Childs[1] = null;
            else
                Childs[0] = null;
        }
    }
    public class BinaryHeap<T> where T : class, IComparable
    {
        public BinaryHeapNode<T> Root { get; set; }
        public BinaryHeapNode<T> Last { get; set; }

        public List<BinaryHeapNode<T>> NodeList { get; set; }


        public BinaryHeap()
        {
            NodeList = new List<BinaryHeapNode<T>>();
            //填充一个空位作为根节点的父节点(value=null),目的是后面在添加新节点时方便为新节点寻找父节点
            //父节点index = 子节点index >> 1
            BinaryHeapNode<T> baseNode = new BinaryHeapNode<T>(){Index = 0};
            Root = baseNode;
            Last = baseNode;
            baseNode.Parent = baseNode;
            NodeList.Add(baseNode);
        }

        public void Add(T value)
        {
            AddNewChild();
            Last.Value = value;
            FloatLast();
        }

        public T Peek()
        {
            return Root.Value;
        }

        public T Pop()
        {
            T ret = Root.Value;
            if (ret == null)
                return ret;

            if (NodeList.Count == 2)
            {
                Last = Root = NodeList[0];
                NodeList.RemoveAt(1);
                return ret;
            }
            else
            {
                SwapValue(Root,Last);
                Last.Parent.RemoveChild();
                NodeList.RemoveAt(Last.Index);
                Last = NodeList[Last.Index - 1];
                Sink(Root);
                return ret;
            }
        }

        private void FloatLast()
        {
            BinaryHeapNode<T> temp = Last;
            while (temp.Parent.Value != null)
            {
                if (temp.Value.CompareTo(temp.Parent.Value) < 0)
                {
                    SwapValue(temp,temp.Parent);
                    temp = temp.Parent;
                }
                else
                {
                    break;
                }
            }
        }

        private void Sink(BinaryHeapNode<T> node)
        {
            if (node.LeftChild == null)
                return;
            
            if (node.RightChild == null)
            {
                if (node.Value.CompareTo(node.LeftChild.Value) > 0)
                {
                    SwapValue(node,node.LeftChild);
                    Sink(node.LeftChild);
                }
                
            }
            else
            {
                BinaryHeapNode<T> compareNode = node.LeftChild.Value.CompareTo(node.RightChild.Value) < 0 ? node.LeftChild : node.RightChild;
                if (node.Value.CompareTo(compareNode.Value) > 0)
                {
                    SwapValue(node,compareNode);
                    Sink(compareNode);
                }
                
            }

        }

        private void SwapValue(BinaryHeapNode<T> a, BinaryHeapNode<T> b)
        {
            T temp = a.Value;
            a.Value = b.Value;
            b.Value = temp;
        }

        private void AddNewChild()
        {
            BinaryHeapNode<T> newNode = new BinaryHeapNode<T>();
            newNode.Index = NodeList.Count;
            NodeList.Add(newNode);
            Last = newNode;
            newNode.Parent = NodeList[newNode.Index >> 1];

            if (newNode.Parent.Value != null)
                newNode.Parent.SetChild(newNode);
            else
                Root = newNode;
        }

    }

}
