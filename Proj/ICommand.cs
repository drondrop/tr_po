using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    public interface ICommand<T>
    {
        T Do(T input);
        T Undo(T input);
    }

    public class UndoRedoFactory<T>
    {
        private Stack<ICommand<T>> _Undo;
        private Stack<ICommand<T>> _Redo;

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

        public UndoRedoFactory()
        {
            Reset();
        }
        public void Reset()
        {
            _Undo = new Stack<ICommand<T>>();
            _Redo = new Stack<ICommand<T>>();
        }

        public T Do(ICommand<T> cmd, T input)
        {
            T output = cmd.Do(input);
            _Undo.Push(cmd);
            _Redo.Clear(); // Once we issue a new command, the redo stack clears
            return output;
        }
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
