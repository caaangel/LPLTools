using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace LPLTools.FullScreen
{
    public class FullScreen : IFullScreen
    {
        private Form _form;
        private bool _fullscreen = false;
        private int _left;
        private int _top;
        private int _width;
        private int _height;

        public FullScreen(Form form)
        {
            _form = form;
            _fullscreen = false;

            _left = form.Left;
            _top = form.Top;
            _width = form.Width;
            _height = form.Height;
        }
        public static IFullScreen Create(Form form)
        {
            IFullScreen fs = new FullScreen(form);
            return fs;
        }


        private void SetWindow()
        {
            _form.FormBorderStyle = FormBorderStyle.Sizable;
            _form.Left = _left;
            _form.Top = _top;
            _form.Width = _width;
            _form.Height = _height;
            _form.BackColor = Color.FromArgb(35, 35, 35);
        }
        private void SetFullScreen()
        {
            _form.FormBorderStyle = FormBorderStyle.None;
            _form.Left = 0;
            _form.Top = 0;
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            _form.Width = resolution.Width;
            _form.Height = resolution.Height;
            _form.BackColor = Color.Black;
        }

        public bool IsFullScreen
        {
            get { return _fullscreen; }
            set
            {
                _fullscreen = !_fullscreen;
                if (_fullscreen)
                {
                    SetFullScreen();
                }
                else
                {
                    SetWindow();
                }
            }
        }
        public void Toogle()
        {
            IsFullScreen = !IsFullScreen;
        }
    }
}
