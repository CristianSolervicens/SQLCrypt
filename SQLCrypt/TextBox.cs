using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{

    public class MyButton : Button
    {
        public int ini_Left { get; set; }
        public int ini_Top { get; set; }

        public void LoadInitialPosition()
        {
            this.ini_Left = this.Left;
            this.ini_Top = this.Top;
        }

        public void SetInitialPosition()
        {
            this.Left = this.ini_Left;
            this.Top = this.ini_Top;
        }

        public void SetLocationDifference(int Diff_Left, int Diff_Top)
        {
            this.Top = this.ini_Top + Diff_Top;
            this.Left = this.ini_Left + Diff_Left;
        }
    }


    public class MyTextBox : TextBox
    {
        public int ini_Left { get; set; }
        public int ini_Top { get; set; }

        public void LoadInitialPosition()
        {
            this.ini_Left = this.Left;
            this.ini_Top = this.Top;
        }

        public void SetInitialPosition()
        {
            this.Left = this.ini_Left;
            this.Top = this.ini_Top;
        }

        public void SetLocationDifference(int Diff_Left, int Diff_Top)
        {
            this.Top = this.ini_Top + Diff_Top;
            this.Left = this.ini_Left + Diff_Left;
        }


    }


    public class MyLabel : Label
    {
        public int ini_Left { get; set; }
        public int ini_Top { get; set; }

        public void LoadInitialPosition()
        {
            this.ini_Left = this.Left;
            this.ini_Top = this.Top;
        }

        public void SetInitialPosition()
        {
            this.Left = this.ini_Left;
            this.Top = this.ini_Top;
        }

        public void SetLocationDifference(int Diff_Left, int Diff_Top)
        {
            this.Top = this.ini_Top + Diff_Top;
            this.Left = this.ini_Left + Diff_Left;
        }
    }


    public class LabelToolTip : Label
    {
        public ToolTip MyToolTip { get; set; }
        
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (this.MyToolTip != null)
                    this.MyToolTip.SetToolTip(this, value);
                base.Text = value;
            }
        }
    }
}
