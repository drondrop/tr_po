﻿using AForge.Imaging.Filters;
using Proj.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Command
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


   

}
