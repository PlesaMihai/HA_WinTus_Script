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

            this.CollTop = new Rectangle(new Point(this.Size.Width / 2 - 10, 0), new Size(20, 20));
            this.CollRight = new Rectangle(new Point(this.Size.Width - 20, this.Size.Height / 2 - 10), new Size(20, 20));
            this.CollBot = new Rectangle(new Point(this.Size.Width / 2 - 10, this.Size.Height - 20), new Size(20, 20));
            this.CollLeft = new Rectangle(new Point(0, this.Size.Height / 2 - 10), new Size(20, 20));
        }

        #region <----------- Public Methods ----------->
        // Default paint function
        public virtual void RenderPanel(object sender, PaintEventArgs e) 
        {
            e.Graphics.FillRectangle(defaultBrush, new Rectangle(new Point(0,0), this.Size));
            this.DrawCollBoxes(e.Graphics);
            e.Graphics.DrawString($"X: {this.Left} Y: {this.Top}", new Font("Arial", 10), new SolidBrush(Color.Black), 10, 10);

        }
        public virtual void AddChild() { }
        
        public virtual void CombinePanels()
        {

        }

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

        private void DrawCollBoxes(Graphics gObj)
        {
            if(CollTop != null) gObj.FillRectangle(collBoxBrush, CollTop);
            if (CollRight != null) gObj.FillRectangle(collBoxBrush, CollRight);
            if (CollBot != null) gObj.FillRectangle(collBoxBrush, CollBot);
            if (CollLeft != null) gObj.FillRectangle(collBoxBrush, CollLeft);
        }
        #endregion

        // Variables
        public static Color defaultColor = Color.Gray;
        public static Size defaultSize = new Size(100, 300);

        public Rectangle CollTop;
        public Rectangle CollBot;
        public Rectangle CollInside;
        public Rectangle CollRight;
        public Rectangle CollLeft;

        public bool movable = false;
        public bool acceptChildren = false;
        public SolidBrush defaultBrush = new SolidBrush(defaultColor);
        public SolidBrush collBoxBrush = new SolidBrush(Color.Red);

        private Point mouseLocation;
    }
}
