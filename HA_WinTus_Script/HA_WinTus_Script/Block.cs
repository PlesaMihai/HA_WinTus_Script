using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HA_WinTus_Script
{
    public class Block : Panel
    {
        // Constructors
        public Block(Point point)
        {
            this.Location = point; ;
            this.MouseDown += new MouseEventHandler(this.Click);
            this.MouseMove += new MouseEventHandler(this.Move);
            this.MouseUp += new MouseEventHandler(this.Release);
            this.Paint += new PaintEventHandler(this.RenderPanel);
        }

        #region <----------- Public Methods ----------->
        // Default paint function
        public virtual void RenderPanel(object sender, PaintEventArgs e) 
        {
            e.Graphics.FillRectangle(defaultBrush, new Rectangle(this.Location, this.Size));
        }
        public virtual void AddChild() { }
        #endregion

        #region <----------- Private Methods ----------->
        private new void Click(object sender, MouseEventArgs e)
        {
            this.mouseLocation = e.Location;
            this.movable = true;
            this.BringToFront();
        }

        private new void Move(object sender, MouseEventArgs e)
        {
            if (this.movable == true)
            {
                this.Left += e.X - mouseLocation.X;
                this.Top += e.Y - mouseLocation.Y;

                this.Invalidate();
            }
        }

        private void Release(object sender, MouseEventArgs e)
        {
            movable = false;
        }
        #endregion



        // Variables
        public bool movable = false;
        public bool acceptChildren = false;
        public static Color defaultColor = Color.Gray;
        public SolidBrush defaultBrush = new SolidBrush(defaultColor);

        private Point mouseLocation;
    }
}
