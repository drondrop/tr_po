﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Command
{
    public class LimitedSizeStack<T> : LinkedList<T>
    {
        private readonly int _maxSize;
        public LimitedSizeStack(int maxSize)
        {
            _maxSize = maxSize;
        }

        public void Push(T item)
        {
            this.AddFirst(item);

            if (this.Count > _maxSize)
                this.RemoveLast();
        }

        public T Pop()
        {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }
    }
}
