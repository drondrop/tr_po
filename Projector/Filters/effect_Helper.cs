﻿using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    public static class effect_Helper
    {
        static void PaintMask(Graphics g, Rectangle bounds, Color color)
        {
            Rectangle ellipsebounds = bounds;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(ellipsebounds);//AddEllipse(ellipsebounds);
                using (SolidBrush brush = new SolidBrush(color))
                {
                    //brush.WrapMode = WrapMode.Tile;
                    //brush.CenterColor = Color.FromArgb(0, 0, 0, 0);
                    //brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 0, 0) };
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0F };
                    blend.Factors = new float[] { 0.0f, 0.5f, 1f, 1f, 1.0f, 1.0f };
                    //brush.Blend = blend;
                    Region oldClip = g.Clip;
                    g.Clip = new Region(bounds);
                    g.FillRectangle(brush, ellipsebounds);
                    g.Clip = oldClip;
                }
            }
        }

        public static void PaintMask(ref Bitmap final, Color color)
        {
                
                
           // Bitmap final = new Bitmap(b);
            
                using (Graphics g = Graphics.FromImage(final))
                {
                    PaintMask(g, new Rectangle(0, 0, final.Width, final.Height), color);
                   // b = (Bitmap)final.Clone();

                   // final.Dispose(); final = null;
                
            }
        }
        static void PaintVignette(Graphics g, Rectangle bounds)
        {
            Rectangle ellipsebounds = bounds;
            ellipsebounds.Offset(-ellipsebounds.X, -ellipsebounds.Y);
            int x = ellipsebounds.Width - (int)Math.Round(.70712 * ellipsebounds.Width);
            int y = ellipsebounds.Height - (int)Math.Round(.70712 * ellipsebounds.Height);
            ellipsebounds.Inflate(x, y);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ellipsebounds);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.WrapMode = WrapMode.Tile;
                    brush.CenterColor = Color.FromArgb(0, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 0, 0) };
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0F };
                    blend.Factors = new float[] { 0.0f, 0.5f, 1f, 1f, 1.0f, 1.0f };
                    brush.Blend = blend;
                    Region oldClip = g.Clip;
                    g.Clip = new Region(bounds);
                    g.FillRectangle(brush, ellipsebounds);
                    g.Clip = oldClip;
                }
            }
        }

        public static void  Vignette(ref Bitmap final)
        {
            //Bitmap final = new Bitmap(b);
            
                using (Graphics g = Graphics.FromImage(final))
                {
                    PaintVignette(g, new Rectangle(0, 0, final.Width, final.Height));
                   // b = (Bitmap)final.Clone();
                    //final.Dispose(); final = null;
                }
        
    }

        public static Bitmap  Resize( Bitmap input,int W, int H)
        {
           
           var _filter = new ResizeBicubic(W, H);
           return _filter.Apply(input);
        }
        
    }
}
