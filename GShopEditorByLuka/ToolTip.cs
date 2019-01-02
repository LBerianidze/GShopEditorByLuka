using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GShopEditorByLuka
{
    public partial class ToolTip : Form
    {
        public ToolTip()
        {
            InitializeComponent();
        }
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
        public void ShowToolTip(IntPtr WindowHandle, string Text)
        {
            this.ShowToolTip(WindowHandle, Text, 0, -1.0, -1.0);
        }
        public void SetText(string Text)
        {
            try
            {
                string output;
                output = Text.Replace("\\r", "\n");
                output = output.Replace("\\", "\n");
                List<string> colors = new List<string>();
                List<int> Symbol = new List<int>();
                for (int Index = 0; Index < output.Length; ++Index)
                {
                    int b = output.IndexOf("^", Index);
                    if (b >= 0)
                    {
                        colors.Add(output.Substring(b + 1, 6));
                        output = output.Remove(b, 7);
                        Symbol.Add(b);
                    }
                }
                richTextBox1.Text = output;
                for (int b = 0; b < Symbol.Count; ++b)
                {
                    richTextBox1.Select(Symbol[b], richTextBox1.Text.Length);
                    Color col = ColorTranslator.FromHtml("#" + colors[b]);
                    richTextBox1.SelectionColor = col;
                }

            }
            catch
            {
                richTextBox1.Text = "Text parse error";
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point p);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        bool IsItemToolTip;
        public void ShowToolTip(IntPtr WindowHandle, string Text, int TimeOut, double LineHeihgt, double WordsMultiplier)
        {
            if ((Text == null) || (Text == ""))
            {
                return;
            }
            GetCursorPos(out Point gpoint);
            if (WindowFromPoint(gpoint) != WindowHandle)
            {
                return;
            }
            tmrHideMe.Enabled = false;
            Point position = Cursor.Position;
            SetText(Text);
            Size sz = TextRenderer.MeasureText(richTextBox1.Text, richTextBox1.Font);
            Width = sz.Width + 10;
            Height = sz.Height + 15;
            Cursor cursor2 = Cursor;
            int WWidth = Cursor.Position.X + 25;
            Cursor cursor3 = Cursor;
            int HHeight = Cursor.Position.Y + 25;
            bool flag = false;
            if (WWidth + Width > SystemInformation.VirtualScreen.Width)
            {
                WWidth = SystemInformation.VirtualScreen.Width - Width;
                flag = true;
            }
            Rectangle rectangle2 = SystemInformation.VirtualScreen;
            if ((HHeight + Height) > rectangle2.Height)
            {
                HHeight = SystemInformation.VirtualScreen.Height - Height;
            }
            else if (!flag)
            {
                goto Label_01DC;
            }
            Cursor cursor4 = Cursor;
            if (Cursor.Position.X >= WWidth)
            {
                Cursor cursor5 = Cursor;
                if (Cursor.Position.Y >= HHeight)
                {
                    Cursor cursor6 = Cursor;
                    WWidth = Cursor.Position.X + 20;
                    Cursor cursor7 = Cursor;
                    HHeight = Cursor.Position.Y + 15;
                }
            }
            Label_01DC:
            Left = WWidth;
            Top = HHeight;
            if (!Visible)
            {
                Left = 0x1388;
                Top = 0x1388;
                SetWindowPos(Handle, 1, 0, 0, 0, 0, 0x13);
                Show();
                Application.DoEvents();
                Left = WWidth;
                Top = HHeight;
                Application.DoEvents();
                SetWindowPos(Handle, -1, 0, 0, 0, 0, 0x13);
            }
            IsItemToolTip = false;
            if (TimeOut > 0)
            {
                tmrHideMe.Interval = TimeOut;
                tmrHideMe.Enabled = true;
            }
        }

        private void HideWindow(object sender, EventArgs e)
        {
            if (!IsItemToolTip)
            {
                Hide();
            }
            tmrHideMe.Enabled = false;
        }
    }
}
