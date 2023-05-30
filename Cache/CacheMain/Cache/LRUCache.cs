using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace Cache
{
    public class LRUCache
    {
        private Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> dict;
        private LinkedList<KeyValuePair<int, int>> linkedList;
        private int capacity;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            this.dict = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>();
            this.linkedList = new LinkedList<KeyValuePair<int, int>>();
        }

        public int Get(int key)
        {
            if (!dict.ContainsKey(key))
            {
                return -1;
            }

            this.MoveFirst(key, dict[key].Value.Value);
            return dict[key].Value.Value;
        }

        public void Put(int key, int value)
        {
            if (!dict.ContainsKey(key))
            {
                LinkedListNode<KeyValuePair<int, int>> newNode = this.linkedList.AddFirst(new KeyValuePair<int, int>(key, value));
                dict.Add(key, newNode);

                if (dict.Count > this.capacity)
                {
                    LinkedListNode<KeyValuePair<int, int>> node = this.linkedList.Last;
                    int leastKey = node.Value.Key;
                    this.linkedList.Remove(node);
                    this.dict.Remove(leastKey);
                }
            }
            else
            {
                this.MoveFirst(key, value);
            }
        }

        private void MoveFirst(int key, int value)
        {
            LinkedListNode<KeyValuePair<int, int>> node = dict[key];
            KeyValuePair<int, int> val = new KeyValuePair<int, int>(key, value);
            this.linkedList.Remove(node);
            LinkedListNode<KeyValuePair<int, int>> newNode = this.linkedList.AddFirst(val);
            dict[key] = newNode;
        }
    }
}
