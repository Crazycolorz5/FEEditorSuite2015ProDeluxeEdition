namespace Modules
{
    partial class FreespaceModule
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
            this.freespaceListing = new System.Windows.Forms.TextBox();
            this.markFree = new System.Windows.Forms.Button();
            this.sortOffset = new System.Windows.Forms.Button();
            this.sortLength = new System.Windows.Forms.Button();
            this.markAllocated = new System.Windows.Forms.Button();
            this.startOffset = new System.Windows.Forms.Label();
            this.startBox = new System.Windows.Forms.TextBox();
            this.endBox = new System.Windows.Forms.TextBox();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.useEnd = new System.Windows.Forms.RadioButton();
            this.useLength = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // freespaceListing
            // 
            this.freespaceListing.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.freespaceListing.Location = new System.Drawing.Point(12, 120);
            this.freespaceListing.Multiline = true;
            this.freespaceListing.Name = "freespaceListing";
            this.freespaceListing.ReadOnly = true;
            this.freespaceListing.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.freespaceListing.Size = new System.Drawing.Size(313, 241);
            this.freespaceListing.TabIndex = 0;
            // 
            // markFree
            // 
            this.markFree.Location = new System.Drawing.Point(218, 12);
            this.markFree.Name = "markFree";
            this.markFree.Size = new System.Drawing.Size(118, 30);
            this.markFree.TabIndex = 1;
            this.markFree.Text = "Mark as Freespace";
            this.markFree.UseVisualStyleBackColor = true;
            this.markFree.Click += new System.EventHandler(this.markAsFreespace);
            // 
            // sortOffset
            // 
            this.sortOffset.Location = new System.Drawing.Point(12, 367);
            this.sortOffset.Name = "sortOffset";
            this.sortOffset.Size = new System.Drawing.Size(150, 23);
            this.sortOffset.TabIndex = 2;
            this.sortOffset.Text = "Sort by Offset";
            this.sortOffset.UseVisualStyleBackColor = true;
            this.sortOffset.Click += new System.EventHandler(this.sortByOffset);
            // 
            // sortLength
            // 
            this.sortLength.Location = new System.Drawing.Point(175, 367);
            this.sortLength.Name = "sortLength";
            this.sortLength.Size = new System.Drawing.Size(150, 23);
            this.sortLength.TabIndex = 3;
            this.sortLength.Text = "Sort by Length";
            this.sortLength.UseVisualStyleBackColor = true;
            this.sortLength.Click += new System.EventHandler(this.sortByLength);
            // 
            // markAllocated
            // 
            this.markAllocated.Location = new System.Drawing.Point(218, 48);
            this.markAllocated.Name = "markAllocated";
            this.markAllocated.Size = new System.Drawing.Size(118, 30);
            this.markAllocated.TabIndex = 4;
            this.markAllocated.Text = "Mark as Allocated";
            this.markAllocated.UseVisualStyleBackColor = true;
            this.markAllocated.Click += new System.EventHandler(this.markAsAllocated);
            // 
            // startOffset
            // 
            this.startOffset.AutoSize = true;
            this.startOffset.Location = new System.Drawing.Point(27, 15);
            this.startOffset.Name = "startOffset";
            this.startOffset.Size = new System.Drawing.Size(32, 13);
            this.startOffset.TabIndex = 5;
            this.startOffset.Text = "Start:";
            // 
            // startBox
            // 
            this.startBox.Location = new System.Drawing.Point(79, 12);
            this.startBox.Name = "startBox";
            this.startBox.Size = new System.Drawing.Size(119, 20);
            this.startBox.TabIndex = 6;
            // 
            // endBox
            // 
            this.endBox.Location = new System.Drawing.Point(79, 38);
            this.endBox.Name = "endBox";
            this.endBox.Size = new System.Drawing.Size(119, 20);
            this.endBox.TabIndex = 8;
            // 
            // lengthBox
            // 
            this.lengthBox.Location = new System.Drawing.Point(79, 93);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.ReadOnly = true;
            this.lengthBox.Size = new System.Drawing.Size(119, 20);
            this.lengthBox.TabIndex = 11;
            // 
            // useEnd
            // 
            this.useEnd.AutoSize = true;
            this.useEnd.Checked = true;
            this.useEnd.Location = new System.Drawing.Point(12, 41);
            this.useEnd.Name = "useEnd";
            this.useEnd.Size = new System.Drawing.Size(47, 17);
            this.useEnd.TabIndex = 12;
            this.useEnd.TabStop = true;
            this.useEnd.Text = "End:";
            this.useEnd.UseVisualStyleBackColor = true;
            this.useEnd.Click += new System.EventHandler(this.useEndChecked);
            // 
            // useLength
            // 
            this.useLength.AutoSize = true;
            this.useLength.Location = new System.Drawing.Point(12, 93);
            this.useLength.Name = "useLength";
            this.useLength.Size = new System.Drawing.Size(61, 17);
            this.useLength.TabIndex = 13;
            this.useLength.Text = "Length:";
            this.useLength.UseVisualStyleBackColor = true;
            this.useLength.Click += new System.EventHandler(this.useLengthChecked);
            // 
            // FreespaceModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 402);
            this.Controls.Add(this.useLength);
            this.Controls.Add(this.useEnd);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.endBox);
            this.Controls.Add(this.startBox);
            this.Controls.Add(this.startOffset);
            this.Controls.Add(this.markAllocated);
            this.Controls.Add(this.sortLength);
            this.Controls.Add(this.sortOffset);
            this.Controls.Add(this.markFree);
            this.Controls.Add(this.freespaceListing);
            this.Name = "FreespaceModule";
            this.Text = "Freespace Module";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox freespaceListing;
        private System.Windows.Forms.Button markFree;
        private System.Windows.Forms.Button sortOffset;
        private System.Windows.Forms.Button sortLength;
        private System.Windows.Forms.Button markAllocated;
        private System.Windows.Forms.Label startOffset;
        private System.Windows.Forms.TextBox startBox;
        private System.Windows.Forms.TextBox endBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.RadioButton useEnd;
        private System.Windows.Forms.RadioButton useLength;
    }
}