using System;
using System.Collections.Generic;
using System.Text;

namespace StackGeneric
{
    public class Stack<T>
    {
        readonly int _Size;
        int _StackPointer = 0;
        T[] _Items;

        public Stack()
            : this(100)
        { }
        public Stack(int size)
        {
            _Size = size;
            _Items = new T[_Size];
        }
        public void Push(T item)
        {
            if (_StackPointer >= _Size)
                throw new StackOverflowException();
            _Items[_StackPointer] = item;
            _StackPointer++;
        }
        public T Pop()
        {
            _StackPointer--;
            if (_StackPointer >= 0)
            {
                return _Items[_StackPointer];
            }
            else
            {
                _StackPointer = 0;
                throw new InvalidOperationException(Resources.Common.EmptyStack);
            }
        }
    }
}
