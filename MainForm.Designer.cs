namespace DLLInjector
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox processListBox;
        private Button btnRefresh;
        private Button btnSelectDll;
        private Button btnInject;
        private Label lblProcesses;
        private Label lblSelectedDll;
        private Label lblSelectedDllPath;
        private TextBox txtSearch;
        private Label lblSearch;
        private CheckBox chkAutoInject;
        private Label lblDelay;
        private NumericUpDown numDelay;
        private System.Windows.Forms.Timer autoInjectTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.processListBox = new ListBox();
            this.btnRefresh = new Button();
            this.btnSelectDll = new Button();
            this.btnInject = new Button();
            this.lblProcesses = new Label();
            this.lblSelectedDll = new Label();
            this.lblSelectedDllPath = new Label();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.chkAutoInject = new CheckBox();
            this.lblDelay = new Label();
            this.numDelay = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();

            this.SuspendLayout();

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(12, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(45, 15);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Search:";
            this.lblSearch.ForeColor = Color.FromArgb(220, 220, 220);

            // txtSearch
            this.txtSearch.Location = new Point(60, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(355, 23);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.BackColor = Color.FromArgb(45, 45, 48);
            this.txtSearch.ForeColor = Color.FromArgb(220, 220, 220);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // lblProcesses
            this.lblProcesses.AutoSize = true;
            this.lblProcesses.Location = new Point(12, 35);
            this.lblProcesses.Name = "lblProcesses";
            this.lblProcesses.Size = new Size(63, 15);
            this.lblProcesses.TabIndex = 2;
            this.lblProcesses.Text = "Processes:";
            this.lblProcesses.ForeColor = Color.FromArgb(220, 220, 220);

            // processListBox
            this.processListBox.FormattingEnabled = true;
            this.processListBox.ItemHeight = 15;
            this.processListBox.Location = new Point(12, 55);
            this.processListBox.Name = "processListBox";
            this.processListBox.Size = new Size(400, 270);
            this.processListBox.TabIndex = 0;
            this.processListBox.BackColor = Color.FromArgb(45, 45, 48);
            this.processListBox.ForeColor = Color.FromArgb(220, 220, 220);

            // btnRefresh
            this.btnRefresh.Location = new Point(418, 55);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.BackColor = Color.FromArgb(60, 60, 65);
            this.btnRefresh.ForeColor = Color.FromArgb(220, 220, 220);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // btnSelectDll
            this.btnSelectDll.Location = new Point(12, 343);
            this.btnSelectDll.Name = "btnSelectDll";
            this.btnSelectDll.Size = new Size(100, 23);
            this.btnSelectDll.TabIndex = 3;
            this.btnSelectDll.Text = "Select DLL";
            this.btnSelectDll.BackColor = Color.FromArgb(60, 60, 65);
            this.btnSelectDll.ForeColor = Color.FromArgb(220, 220, 220);
            this.btnSelectDll.FlatStyle = FlatStyle.Flat;
            this.btnSelectDll.Click += new EventHandler(this.btnSelectDll_Click);

            // lblSelectedDll
            this.lblSelectedDll.AutoSize = true;
            this.lblSelectedDll.Location = new Point(12, 369);
            this.lblSelectedDll.Name = "lblSelectedDll";
            this.lblSelectedDll.Size = new Size(83, 15);
            this.lblSelectedDll.TabIndex = 4;
            this.lblSelectedDll.Text = "Selected: None";
            this.lblSelectedDll.ForeColor = Color.FromArgb(220, 220, 220);

            // lblSelectedDllPath
            this.lblSelectedDllPath.AutoSize = true;
            this.lblSelectedDllPath.Location = new Point(12, 390);
            this.lblSelectedDllPath.Name = "lblSelectedDllPath";
            this.lblSelectedDllPath.Size = new Size(0, 15);
            this.lblSelectedDllPath.TabIndex = 5;
            this.lblSelectedDllPath.ForeColor = Color.FromArgb(150, 150, 150);

            // btnInject
            this.btnInject.Location = new Point(118, 343);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new Size(100, 23);
            this.btnInject.TabIndex = 6;
            this.btnInject.Text = "Inject DLL";
            this.btnInject.BackColor = Color.FromArgb(60, 60, 65);
            this.btnInject.ForeColor = Color.FromArgb(220, 220, 220);
            this.btnInject.FlatStyle = FlatStyle.Flat;
            this.btnInject.Click += new EventHandler(this.btnInject_Click);

            // chkAutoInject
            this.chkAutoInject.AutoSize = true;
            this.chkAutoInject.Location = new Point(12, 370);
            this.chkAutoInject.Name = "chkAutoInject";
            this.chkAutoInject.Size = new Size(91, 19);
            this.chkAutoInject.TabIndex = 9;
            this.chkAutoInject.Text = "Auto Inject";
            this.chkAutoInject.ForeColor = Color.FromArgb(220, 220, 220);
            this.chkAutoInject.BackColor = Color.FromArgb(30, 30, 30);
            this.chkAutoInject.CheckedChanged += new EventHandler(this.chkAutoInject_CheckedChanged);

            // lblDelay
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new Point(110, 370);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new Size(47, 15);
            this.lblDelay.TabIndex = 10;
            this.lblDelay.Text = "Delay (s):";
            this.lblDelay.ForeColor = Color.FromArgb(220, 220, 220);

            // numDelay
            this.numDelay.Location = new Point(160, 368);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new Size(60, 23);
            this.numDelay.TabIndex = 11;
            this.numDelay.Value = 5;
            this.numDelay.Minimum = 1;
            this.numDelay.Maximum = 300;
            this.numDelay.BackColor = Color.FromArgb(45, 45, 48);
            this.numDelay.ForeColor = Color.FromArgb(220, 220, 220);

            // MainForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 420);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.Controls.Add(this.numDelay);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.chkAutoInject);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.lblSelectedDllPath);
            this.Controls.Add(this.lblSelectedDll);
            this.Controls.Add(this.btnSelectDll);
            this.Controls.Add(this.lblProcesses);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.processListBox);
            this.Name = "MainForm";
            this.Text = "DLL Injector";
            this.Load += new EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
