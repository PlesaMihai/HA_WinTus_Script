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
            this.Size = defaultSize;
        }

        #region <----------- Public Methods ----------->
        // Default paint function
        public virtual void RenderPanel(object sender, PaintEventArgs e) 
        {
            e.Graphics.FillRectangle(defaultBrush, new Rectangle(new Point(0,0), this.Size));
            e.Graphics.DrawString($"X: {this.Left} Y: {this.Top}", new Font("Arial", 10), new SolidBrush(Color.Black), 10, 10);
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
                Left += e.X - mouseLocation.X;
                Top += e.Y - mouseLocation.Y;

                this.Invalidate();
            }
        }

        private void Release(object sender, MouseEventArgs e)
        {
            movable = false;
        }
        #endregion



        // Variables
        public static Color defaultColor = Color.Gray;
        public static Size defaultSize = new Size(100, 300);

        public bool movable = false;
        public bool acceptChildren = false;
        public SolidBrush defaultBrush = new SolidBrush(defaultColor);

        private Point mouseLocation;
    }
}
