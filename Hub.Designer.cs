namespace FE_Editor_Suite
{
    partial class Hub
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
            this.ROMNameStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.hubMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugFEEditingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setROMChangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HubTabs = new System.Windows.Forms.TabControl();
            this.ROM_Info = new System.Windows.Forms.TabPage();
            this.ROMDataTable = new System.Windows.Forms.DataGridView();
            this.Open_New_Editors = new System.Windows.Forms.TabPage();
            this.openDebug = new System.Windows.Forms.Button();
            this.List_Open_Editors = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openEditorList = new System.Windows.Forms.ListBox();
            this.openFreespace = new System.Windows.Forms.Button();
            this.ROMNameStrip.SuspendLayout();
            this.hubMenuStrip.SuspendLayout();
            this.HubTabs.SuspendLayout();
            this.ROM_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ROMDataTable)).BeginInit();
            this.Open_New_Editors.SuspendLayout();
            this.List_Open_Editors.SuspendLayout();
            this.SuspendLayout();
            // 
            // ROMNameStrip
            // 
            this.ROMNameStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.ROMNameStrip.Location = new System.Drawing.Point(0, 404);
            this.ROMNameStrip.Name = "ROMNameStrip";
            this.ROMNameStrip.Size = new System.Drawing.Size(551, 22);
            this.ROMNameStrip.TabIndex = 0;
            this.ROMNameStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel.Visible = false;
            // 
            // hubMenuStrip
            // 
            this.hubMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.debugFEEditingToolStripMenuItem});
            this.hubMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.hubMenuStrip.Name = "hubMenuStrip";
            this.hubMenuStrip.Size = new System.Drawing.Size(551, 24);
            this.hubMenuStrip.TabIndex = 1;
            this.hubMenuStrip.Text = "hubMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.autoSaveToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openROM);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToMetadata);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // autoSaveToolStripMenuItem
            // 
            this.autoSaveToolStripMenuItem.CheckOnClick = true;
            this.autoSaveToolStripMenuItem.Enabled = false;
            this.autoSaveToolStripMenuItem.Name = "autoSaveToolStripMenuItem";
            this.autoSaveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.autoSaveToolStripMenuItem.Text = "Auto Save";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeROM);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exit);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // debugFEEditingToolStripMenuItem
            // 
            this.debugFEEditingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setROMChangedToolStripMenuItem});
            this.debugFEEditingToolStripMenuItem.Name = "debugFEEditingToolStripMenuItem";
            this.debugFEEditingToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.debugFEEditingToolStripMenuItem.Text = "Debug FE Editing";
            // 
            // setROMChangedToolStripMenuItem
            // 
            this.setROMChangedToolStripMenuItem.Name = "setROMChangedToolStripMenuItem";
            this.setROMChangedToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.setROMChangedToolStripMenuItem.Text = "Set ROMChanged";
            this.setROMChangedToolStripMenuItem.Click += new System.EventHandler(this.Set_ROMChanged);
            // 
            // HubTabs
            // 
            this.HubTabs.Controls.Add(this.ROM_Info);
            this.HubTabs.Controls.Add(this.Open_New_Editors);
            this.HubTabs.Controls.Add(this.List_Open_Editors);
            this.HubTabs.Location = new System.Drawing.Point(0, 27);
            this.HubTabs.Name = "HubTabs";
            this.HubTabs.SelectedIndex = 0;
            this.HubTabs.Size = new System.Drawing.Size(551, 374);
            this.HubTabs.TabIndex = 2;
            this.HubTabs.Visible = false;
            // 
            // ROM_Info
            // 
            this.ROM_Info.Controls.Add(this.ROMDataTable);
            this.ROM_Info.Location = new System.Drawing.Point(4, 22);
            this.ROM_Info.Name = "ROM_Info";
            this.ROM_Info.Padding = new System.Windows.Forms.Padding(3);
            this.ROM_Info.Size = new System.Drawing.Size(543, 348);
            this.ROM_Info.TabIndex = 0;
            this.ROM_Info.Text = "ROM Info";
            this.ROM_Info.UseVisualStyleBackColor = true;
            // 
            // ROMDataTable
            // 
            this.ROMDataTable.AllowUserToAddRows = false;
            this.ROMDataTable.AllowUserToDeleteRows = false;
            this.ROMDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ROMDataTable.Location = new System.Drawing.Point(0, 0);
            this.ROMDataTable.Name = "ROMDataTable";
            this.ROMDataTable.Size = new System.Drawing.Size(543, 348);
            this.ROMDataTable.TabIndex = 0;
            // 
            // Open_New_Editors
            // 
            this.Open_New_Editors.Controls.Add(this.openFreespace);
            this.Open_New_Editors.Controls.Add(this.openDebug);
            this.Open_New_Editors.Location = new System.Drawing.Point(4, 22);
            this.Open_New_Editors.Name = "Open_New_Editors";
            this.Open_New_Editors.Padding = new System.Windows.Forms.Padding(3);
            this.Open_New_Editors.Size = new System.Drawing.Size(543, 348);
            this.Open_New_Editors.TabIndex = 1;
            this.Open_New_Editors.Text = "Open New Editors";
            this.Open_New_Editors.UseVisualStyleBackColor = true;
            // 
            // openDebug
            // 
            this.openDebug.Location = new System.Drawing.Point(409, 319);
            this.openDebug.Name = "openDebug";
            this.openDebug.Size = new System.Drawing.Size(126, 23);
            this.openDebug.TabIndex = 0;
            this.openDebug.Text = "Open Debug Module";
            this.openDebug.UseVisualStyleBackColor = true;
            this.openDebug.Click += new System.EventHandler(this.openDebugModule);
            // 
            // List_Open_Editors
            // 
            this.List_Open_Editors.Controls.Add(this.button1);
            this.List_Open_Editors.Controls.Add(this.label1);
            this.List_Open_Editors.Controls.Add(this.openEditorList);
            this.List_Open_Editors.Location = new System.Drawing.Point(4, 22);
            this.List_Open_Editors.Name = "List_Open_Editors";
            this.List_Open_Editors.Padding = new System.Windows.Forms.Padding(3);
            this.List_Open_Editors.Size = new System.Drawing.Size(543, 348);
            this.List_Open_Editors.TabIndex = 2;
            this.List_Open_Editors.Text = "List Open Editors";
            this.List_Open_Editors.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 319);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All Open Editors:";
            // 
            // openEditorList
            // 
            this.openEditorList.FormattingEnabled = true;
            this.openEditorList.Location = new System.Drawing.Point(6, 23);
            this.openEditorList.Name = "openEditorList";
            this.openEditorList.ScrollAlwaysVisible = true;
            this.openEditorList.Size = new System.Drawing.Size(527, 264);
            this.openEditorList.TabIndex = 0;
            // 
            // openFreespace
            // 
            this.openFreespace.Location = new System.Drawing.Point(9, 7);
            this.openFreespace.Name = "openFreespace";
            this.openFreespace.Size = new System.Drawing.Size(143, 23);
            this.openFreespace.TabIndex = 1;
            this.openFreespace.Text = "Open Freespace Manager";
            this.openFreespace.UseVisualStyleBackColor = true;
            this.openFreespace.Click += new System.EventHandler(this.openFreespaceManager);
            // 
            // Hub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 426);
            this.Controls.Add(this.HubTabs);
            this.Controls.Add(this.ROMNameStrip);
            this.Controls.Add(this.hubMenuStrip);
            this.Name = "Hub";
            this.Text = "FE Editing Hub";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.promptSaveIfChanged_void);
            this.ROMNameStrip.ResumeLayout(false);
            this.ROMNameStrip.PerformLayout();
            this.hubMenuStrip.ResumeLayout(false);
            this.hubMenuStrip.PerformLayout();
            this.HubTabs.ResumeLayout(false);
            this.ROM_Info.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ROMDataTable)).EndInit();
            this.Open_New_Editors.ResumeLayout(false);
            this.List_Open_Editors.ResumeLayout(false);
            this.List_Open_Editors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ROMNameStrip;
        private System.Windows.Forms.MenuStrip hubMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.TabControl HubTabs;
        private System.Windows.Forms.TabPage ROM_Info;
        private System.Windows.Forms.TabPage Open_New_Editors;
        private System.Windows.Forms.TabPage List_Open_Editors;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem debugFEEditingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setROMChangedToolStripMenuItem;
        private System.Windows.Forms.DataGridView ROMDataTable;
        private System.Windows.Forms.Button openDebug;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox openEditorList;
        private System.Windows.Forms.ToolStripMenuItem autoSaveToolStripMenuItem;
        private System.Windows.Forms.Button openFreespace;


    }
}