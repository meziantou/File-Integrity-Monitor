using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileIntegrityMonitor.Notifier
{
    public partial class MainForm : Form
    {
        private Client _client;

        public MainForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Closing += Form1_Closing;
        }

        void Form1_Closing(object sender, CancelEventArgs e)
        {
            _client.Dispose();
            _client = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _client = new Client();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
