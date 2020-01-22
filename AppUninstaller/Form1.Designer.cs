namespace AppUninstaller
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonServerStart = new System.Windows.Forms.Button();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.objectListViewPackages = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPath = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnIsSystemApp = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelFilter = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.backgroundWorkerStartServer = new System.ComponentModel.BackgroundWorker();
            this.labelDeviceName = new System.Windows.Forms.Label();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.buttonServerStop = new System.Windows.Forms.Button();
            this.groupBoxDevice = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPackages)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonServerStart
            // 
            this.buttonServerStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonServerStart.Location = new System.Drawing.Point(7, 24);
            this.buttonServerStart.Name = "buttonServerStart";
            this.buttonServerStart.Size = new System.Drawing.Size(70, 28);
            this.buttonServerStart.TabIndex = 0;
            this.buttonServerStart.Text = "Start";
            this.buttonServerStart.Click += new System.EventHandler(this.buttonServerStart_Click);
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderMessage});
            this.listViewLog.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.GridLines = true;
            this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLog.Location = new System.Drawing.Point(17, 404);
            this.listViewLog.Margin = new System.Windows.Forms.Padding(4);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(770, 134);
            this.listViewLog.TabIndex = 1;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.Width = 163;
            // 
            // columnHeaderMessage
            // 
            this.columnHeaderMessage.Text = "Message";
            this.columnHeaderMessage.Width = 380;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.objectListViewPackages);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(17, 79);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 317);
            this.panel1.TabIndex = 2;
            // 
            // objectListViewPackages
            // 
            this.objectListViewPackages.AllColumns.Add(this.olvColumnName);
            this.objectListViewPackages.AllColumns.Add(this.olvColumnPath);
            this.objectListViewPackages.AllColumns.Add(this.olvColumnIsSystemApp);
            this.objectListViewPackages.CellEditUseWholeCell = false;
            this.objectListViewPackages.CheckBoxes = true;
            this.objectListViewPackages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnPath,
            this.olvColumnIsSystemApp});
            this.objectListViewPackages.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewPackages.EmptyListMsg = "Update package list";
            this.objectListViewPackages.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.objectListViewPackages.FullRowSelect = true;
            this.objectListViewPackages.GridLines = true;
            this.objectListViewPackages.Location = new System.Drawing.Point(0, 27);
            this.objectListViewPackages.Name = "objectListViewPackages";
            this.objectListViewPackages.ShowGroups = false;
            this.objectListViewPackages.Size = new System.Drawing.Size(771, 290);
            this.objectListViewPackages.TabIndex = 1;
            this.objectListViewPackages.UseCompatibleStateImageBehavior = false;
            this.objectListViewPackages.UseFilterIndicator = true;
            this.objectListViewPackages.UseFiltering = true;
            this.objectListViewPackages.UseHotItem = true;
            this.objectListViewPackages.UseTranslucentHotItem = true;
            this.objectListViewPackages.UseTranslucentSelection = true;
            this.objectListViewPackages.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.Text = "Name";
            // 
            // olvColumnPath
            // 
            this.olvColumnPath.AspectName = "Path";
            this.olvColumnPath.Text = "Path";
            // 
            // olvColumnIsSystemApp
            // 
            this.olvColumnIsSystemApp.AspectName = "IsSystemApp";
            this.olvColumnIsSystemApp.Text = "System app";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripTextBoxFilter,
            this.toolStripLabelFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(771, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::AppUninstaller.Properties.Resources.round_arrow;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(82, 24);
            this.toolStripButton1.Text = "Update";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::AppUninstaller.Properties.Resources.rubbish_bin;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(90, 24);
            this.toolStripButton2.Text = "Uninstall";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripTextBoxFilter
            // 
            this.toolStripTextBoxFilter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBoxFilter.Name = "toolStripTextBoxFilter";
            this.toolStripTextBoxFilter.Size = new System.Drawing.Size(200, 27);
            this.toolStripTextBoxFilter.TextChanged += new System.EventHandler(this.toolStripTextBoxFilter_TextChanged);
            // 
            // toolStripLabelFilter
            // 
            this.toolStripLabelFilter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelFilter.Name = "toolStripLabelFilter";
            this.toolStripLabelFilter.Size = new System.Drawing.Size(45, 24);
            this.toolStripLabelFilter.Text = "Filter:";
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxDevices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(6, 24);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(206, 28);
            this.comboBoxDevices.TabIndex = 3;
            // 
            // backgroundWorkerStartServer
            // 
            this.backgroundWorkerStartServer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStartServer_DoWork);
            this.backgroundWorkerStartServer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerStartServer_RunWorkerCompleted);
            // 
            // labelDeviceName
            // 
            this.labelDeviceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDeviceName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDeviceName.Location = new System.Drawing.Point(218, 24);
            this.labelDeviceName.Name = "labelDeviceName";
            this.labelDeviceName.Size = new System.Drawing.Size(386, 28);
            this.labelDeviceName.TabIndex = 4;
            this.labelDeviceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.buttonServerStop);
            this.groupBoxServer.Controls.Add(this.buttonServerStart);
            this.groupBoxServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(160, 60);
            this.groupBoxServer.TabIndex = 5;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server";
            // 
            // buttonServerStop
            // 
            this.buttonServerStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonServerStop.Location = new System.Drawing.Point(83, 24);
            this.buttonServerStop.Name = "buttonServerStop";
            this.buttonServerStop.Size = new System.Drawing.Size(70, 28);
            this.buttonServerStop.TabIndex = 1;
            this.buttonServerStop.Text = "Stop";
            this.buttonServerStop.Click += new System.EventHandler(this.buttonServerStop_Click);
            // 
            // groupBoxDevice
            // 
            this.groupBoxDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDevice.Controls.Add(this.comboBoxDevices);
            this.groupBoxDevice.Controls.Add(this.labelDeviceName);
            this.groupBoxDevice.Location = new System.Drawing.Point(178, 12);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Size = new System.Drawing.Size(610, 60);
            this.groupBoxDevice.TabIndex = 6;
            this.groupBoxDevice.TabStop = false;
            this.groupBoxDevice.Text = "Device";
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(805, 554);
            this.Controls.Add(this.groupBoxDevice);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listViewLog);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "AppUninstaller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPackages)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxDevice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonServerStart;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStartServer;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFilter;
        private BrightIdeasSoftware.ObjectListView objectListViewPackages;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnPath;
        private BrightIdeasSoftware.OLVColumn olvColumnIsSystemApp;
        private System.Windows.Forms.Label labelDeviceName;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.Button buttonServerStop;
        private System.Windows.Forms.GroupBox groupBoxDevice;
    }
}

