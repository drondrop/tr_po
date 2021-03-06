﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Command
{

    
    public class UndoRedoKeper<T>
    {
        private LimitedSizeStack<ICommand<T>> _Undo;
        private LimitedSizeStack<ICommand<T>> _Redo;

        public int UndoCount
        {
            get
            {
                return _Undo.Count;
            }
        }
        public int RedoCount
        {
            get
            {
                return _Redo.Count;
            }
        }

        public T doAll(T input)
        {
            foreach (var cmd in _Undo)
            {
                input=cmd.DoPreView(input);
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
           
            return input;
        }
        public UndoRedoKeper()
        {
            Reset();

        }
        public void Reset()
        {
            _Undo = new LimitedSizeStack<ICommand<T>>(10);
            _Redo = new LimitedSizeStack<ICommand<T>>(10);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public T Do(ICommand<T> cmd, T input)
        {
            T output = cmd.Do(input);
            _Undo.Push(cmd);
            _Redo.Clear(); // Once we issue a new command, the redo stack clears
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public T DoPreView(ICommand<T> cmd, T input)
        {
            T output = cmd.DoPreView(input);
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public T Undo(T input)
        {
            if (_Undo.Count > 0)
            {
                ICommand<T> cmd = _Undo.Pop();
                T output = cmd.Undo(input);
                _Redo.Push(cmd);
                return output;
            }
            else
            {
                return input;
            }
        }
        /// <summary>
       /// 
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public T Redo(T input)
        {
            if (_Redo.Count > 0)
            {
                ICommand<T> cmd = _Redo.Pop();
                T output = cmd.Do(input);
                _Undo.Push(cmd);
                return output;
            }
            else
            {
                return input;
            }
        }

    }
}
