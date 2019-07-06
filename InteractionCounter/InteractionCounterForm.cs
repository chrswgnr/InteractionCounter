using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractionCounter
{
    public partial class InteractionCounterForm : Form
    {
        
        private ulong keyCount = 0;
        private ulong mouseCount = 0;
        private Stopwatch stopwatch = new Stopwatch();
        private Timer uiUpdateTimer = new Timer();

        private readonly KeyInterceptor keyInterceptor;
        private readonly MouseInterceptor mouseInterceptor;

        public InteractionCounterForm(KeyInterceptor keyInterceptor, MouseInterceptor mouseInterceptor)
        {
            InitializeComponent();

            this.keyInterceptor = keyInterceptor;
            this.mouseInterceptor = mouseInterceptor;

            uiUpdateTimer.Interval = 100;
            uiUpdateTimer.Tick += UiUpdateTimer_Tick;
        }

        private void UiUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keyInterceptor.CallBacks += KeyInterceptor_CallBack;
            mouseInterceptor.CallBacks += MouseInterceptor_CallBacks;
            stopwatch.Start();
            uiUpdateTimer.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void MouseInterceptor_CallBacks(MouseButtons obj)
        {
            mouseCount++;
        }

        private void KeyInterceptor_CallBack(Keys keys)
        {
            keyCount++;
        }

        private void UpdateView()
        {
            lblKeyCount.Text = keyCount.ToString();
            lblMouseCount.Text = mouseCount.ToString();
            lblTime.Text = stopwatch.Elapsed.ToString("mm\\:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keyInterceptor.CallBacks -= KeyInterceptor_CallBack;
            mouseInterceptor.CallBacks -= MouseInterceptor_CallBacks;
            stopwatch.Stop();
            uiUpdateTimer.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            keyCount = 0;
            mouseCount = 0;
            stopwatch.Reset();
            if (uiUpdateTimer.Enabled)
            {
                stopwatch.Start();
            }
            else
            {
                UpdateView();
            }
        }
    }
}
