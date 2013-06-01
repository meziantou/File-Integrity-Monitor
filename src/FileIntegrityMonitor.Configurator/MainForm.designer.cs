using System.Windows.Forms;
using FileIntegrityMonitor.Common;

namespace FileIntegrityMonitor.Configurator
{
    partial class MainForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._installButton = new System.Windows.Forms.Button();
            this._uninstallButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._addFileButton = new System.Windows.Forms.Button();
            this._addFolderButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.watcherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._configurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this._serviceStateLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._saveConfigurationButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._notifierComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this._certificateLabel = new System.Windows.Forms.Label();
            this._selectCertificateButton = new System.Windows.Forms.Button();
            this._clearCertificateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._configurationBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _installButton
            // 
            this._installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._installButton.Location = new System.Drawing.Point(470, 434);
            this._installButton.Name = "_installButton";
            this._installButton.Size = new System.Drawing.Size(75, 23);
            this._installButton.TabIndex = 0;
            this._installButton.Text = "Install";
            this._installButton.UseVisualStyleBackColor = true;
            this._installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // _uninstallButton
            // 
            this._uninstallButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._uninstallButton.Location = new System.Drawing.Point(389, 434);
            this._uninstallButton.Name = "_uninstallButton";
            this._uninstallButton.Size = new System.Drawing.Size(75, 23);
            this._uninstallButton.TabIndex = 0;
            this._uninstallButton.Text = "Uninstall";
            this._uninstallButton.UseVisualStyleBackColor = true;
            this._uninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paths to check";
            // 
            // _addFileButton
            // 
            this._addFileButton.Location = new System.Drawing.Point(91, 6);
            this._addFileButton.Name = "_addFileButton";
            this._addFileButton.Size = new System.Drawing.Size(75, 23);
            this._addFileButton.TabIndex = 2;
            this._addFileButton.Text = "Add File";
            this._addFileButton.UseVisualStyleBackColor = true;
            this._addFileButton.Click += new System.EventHandler(this.AddFileButton_Click);
            // 
            // _addFolderButton
            // 
            this._addFolderButton.Location = new System.Drawing.Point(172, 6);
            this._addFolderButton.Name = "_addFolderButton";
            this._addFolderButton.Size = new System.Drawing.Size(75, 23);
            this._addFolderButton.TabIndex = 2;
            this._addFolderButton.Text = "Add Folder";
            this._addFolderButton.UseVisualStyleBackColor = true;
            this._addFolderButton.Click += new System.EventHandler(this.AddFolderButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pathDataGridViewTextBoxColumn,
            this.filterDataGridViewTextBoxColumn,
            this.dataGridViewCheckBoxColumn1});
            this.dataGridView1.DataSource = this.watcherBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(537, 357);
            this.dataGridView1.TabIndex = 3;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Path";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            // 
            // filterDataGridViewTextBoxColumn
            // 
            this.filterDataGridViewTextBoxColumn.DataPropertyName = "Filter";
            this.filterDataGridViewTextBoxColumn.HeaderText = "Filter";
            this.filterDataGridViewTextBoxColumn.Name = "filterDataGridViewTextBoxColumn";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IncludeSubdirectories";
            this.dataGridViewCheckBoxColumn1.HeaderText = "IncludeSubdirectories";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // watcherBindingSource
            // 
            this.watcherBindingSource.DataMember = "Watchers";
            this.watcherBindingSource.DataSource = this._configurationBindingSource;
            // 
            // _configurationBindingSource
            // 
            this._configurationBindingSource.DataSource = typeof(FileIntegrityMonitor.Common.Configuration);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Service state: ";
            // 
            // _serviceStateLabel
            // 
            this._serviceStateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._serviceStateLabel.AutoSize = true;
            this._serviceStateLabel.Location = new System.Drawing.Point(84, 439);
            this._serviceStateLabel.Name = "_serviceStateLabel";
            this._serviceStateLabel.Size = new System.Drawing.Size(51, 13);
            this._serviceStateLabel.TabIndex = 5;
            this._serviceStateLabel.Text = "unknown";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _saveConfigurationButton
            // 
            this._saveConfigurationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._saveConfigurationButton.Location = new System.Drawing.Point(308, 434);
            this._saveConfigurationButton.Name = "_saveConfigurationButton";
            this._saveConfigurationButton.Size = new System.Drawing.Size(75, 23);
            this._saveConfigurationButton.TabIndex = 0;
            this._saveConfigurationButton.Text = "Save Conf";
            this._saveConfigurationButton.UseVisualStyleBackColor = true;
            this._saveConfigurationButton.Click += new System.EventHandler(this.SaveConfigurationButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(557, 424);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this._addFileButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this._addFolderButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Paths";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._notifierComboBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(533, 53);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notifier";
            // 
            // _notifierComboBox
            // 
            this._notifierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._notifierComboBox.FormattingEnabled = true;
            this._notifierComboBox.Items.AddRange(new object[] {
            "None",
            "Current User",
            "All Users"});
            this._notifierComboBox.Location = new System.Drawing.Point(102, 17);
            this._notifierComboBox.Name = "_notifierComboBox";
            this._notifierComboBox.Size = new System.Drawing.Size(121, 21);
            this._notifierComboBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Install Notifier to : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._certificateLabel);
            this.groupBox1.Controls.Add(this._selectCertificateButton);
            this.groupBox1.Controls.Add(this._clearCertificateButton);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration integrity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Digital Signature: ";
            // 
            // _certificateLabel
            // 
            this._certificateLabel.AutoSize = true;
            this._certificateLabel.Location = new System.Drawing.Point(102, 52);
            this._certificateLabel.Name = "_certificateLabel";
            this._certificateLabel.Size = new System.Drawing.Size(91, 13);
            this._certificateLabel.TabIndex = 2;
            this._certificateLabel.Text = "Current Certificate";
            // 
            // _selectCertificateButton
            // 
            this._selectCertificateButton.Location = new System.Drawing.Point(102, 22);
            this._selectCertificateButton.Name = "_selectCertificateButton";
            this._selectCertificateButton.Size = new System.Drawing.Size(97, 23);
            this._selectCertificateButton.TabIndex = 1;
            this._selectCertificateButton.Text = "Select Certificate";
            this._selectCertificateButton.UseVisualStyleBackColor = true;
            this._selectCertificateButton.Click += new System.EventHandler(this._selectCertificateButton_Click);
            // 
            // _clearCertificateButton
            // 
            this._clearCertificateButton.Location = new System.Drawing.Point(205, 22);
            this._clearCertificateButton.Name = "_clearCertificateButton";
            this._clearCertificateButton.Size = new System.Drawing.Size(97, 23);
            this._clearCertificateButton.TabIndex = 1;
            this._clearCertificateButton.Text = "Clear Certificate";
            this._clearCertificateButton.UseVisualStyleBackColor = true;
            this._clearCertificateButton.Click += new System.EventHandler(this._clearCertificateButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 469);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._serviceStateLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._saveConfigurationButton);
            this.Controls.Add(this._uninstallButton);
            this.Controls.Add(this._installButton);
            this.Name = "MainForm";
            this.Text = "Configurator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._configurationBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _installButton;
        private System.Windows.Forms.Button _uninstallButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _addFileButton;
        private System.Windows.Forms.Button _addFolderButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn includeSubdirectoriesDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource watcherBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Label label2;
        private Label _serviceStateLabel;
        private Timer timer1;
        private Button _saveConfigurationButton;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label3;
        private Button _selectCertificateButton;
        private BindingSource _configurationBindingSource;
        private DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn filterDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private Button _clearCertificateButton;
        private Label _certificateLabel;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private ComboBox _notifierComboBox;
        private Label label4;
    }
}

