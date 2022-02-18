using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HA_WinTus_Script
{
    public partial class MainWindow : Form
    {


        public MainWindow()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            timer1.Enabled = true;
        }
        #region <------------------- Window control ------------------->

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void bntMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != 0)
                        this.Padding = new Padding(0, 0, 0, 0);
                    break;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void btnLoops_Click(object sender, EventArgs e)
        {
            WorkspacePanel.Controls.Add(new Block(new Point(10,10)));
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            foreach(Block block in WorkspacePanel.Controls.OfType<Block>().ToList())
            {
                WorkspacePanel.Controls.Remove(block);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            foreach (Block thisBlock in WorkspacePanel.Controls.OfType<Block>().ToList())
            {
                if(thisBlock.movable == true)
                {
                    foreach (Block otherBlock in WorkspacePanel.Controls.OfType<Block>().ToList())
                    {
                        if(thisBlock != otherBlock && thisBlock.Bounds.IntersectsWith(otherBlock.Bounds))
                        {
                            thisBlock.CheckCollission(otherBlock);
                        }
                    }
                }
            }
        }
    }
}
