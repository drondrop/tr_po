﻿using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    public interface ICommand<T>
    {
        T Do(T input);
        T Undo(T input);
        T DoPreView(T input);
    }


   
    public class Filter_Command : ICommand<Bitmap>
    {
       
        private Bitmap _undoValue;
        private ICFilter _filter;
        public Filter_Command(ICFilter filter)
        {
           // _value = value;
            _filter = filter;
        }
        public Bitmap Do(Bitmap input)
        {
            _undoValue = input;
           // BrightnessCorrection filter = new BrightnessCorrection(_value);
            
            return _filter.Apply(input);
        }

        public Bitmap Undo(Bitmap input)
        {
            return _undoValue;
        }
        public Bitmap DoPreView(Bitmap input)
        {
            return _filter.Apply(input);
        }
        
    }
    public class Filter_Command_ : ICommand<Bitmap>
    {

        private Bitmap _undoValue;
        private IFilter _filter;
        public Filter_Command_(IFilter filter)
        {
            // _value = value;
            _filter = filter;
        }
        public Bitmap Do(Bitmap input)
        {
            _undoValue = input;
            // BrightnessCorrection filter = new BrightnessCorrection(_value);

            return _filter.Apply(input);
        }

        public Bitmap Undo(Bitmap input)
        {
            return _undoValue;
        }
        public Bitmap DoPreView(Bitmap input)
        {
            return _filter.Apply(input);
        }

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
        public T DoPreView(ICommand<T> cmd, T input)
        {
            T output = cmd.DoPreView(input);
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
