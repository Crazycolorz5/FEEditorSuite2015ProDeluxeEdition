namespace Modules
{
    partial class DebugModule
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
            this.reqData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataOffset = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataLength = new System.Windows.Forms.TextBox();
            this.display = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.writeOffset = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.writeHex = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.reqWrite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reqData
            // 
            this.reqData.Location = new System.Drawing.Point(150, 13);
            this.reqData.Name = "reqData";
            this.reqData.Size = new System.Drawing.Size(75, 23);
            this.reqData.TabIndex = 0;
            this.reqData.Text = "requestData";
            this.reqData.UseVisualStyleBackColor = true;
            this.reqData.Click += new System.EventHandler(this.getData);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "offset";
            // 
            // dataOffset
            // 
            this.dataOffset.Location = new System.Drawing.Point(44, 5);
            this.dataOffset.Name = "dataOffset";
            this.dataOffset.Size = new System.Drawing.Size(100, 20);
            this.dataOffset.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "length";
            // 
            // dataLength
            // 
            this.dataLength.Location = new System.Drawing.Point(44, 30);
            this.dataLength.Name = "dataLength";
            this.dataLength.Size = new System.Drawing.Size(100, 20);
            this.dataLength.TabIndex = 4;
            // 
            // display
            // 
            this.display.AcceptsReturn = true;
            this.display.AcceptsTab = true;
            this.display.Cursor = System.Windows.Forms.Cursors.Default;
            this.display.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.display.Location = new System.Drawing.Point(13, 228);
            this.display.Multiline = true;
            this.display.Name = "display";
            this.display.ReadOnly = true;
            this.display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.display.Size = new System.Drawing.Size(363, 228);
            this.display.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Accessed Data (automatically updates)";
            // 
            // writeOffset
            // 
            this.writeOffset.Location = new System.Drawing.Point(44, 97);
            this.writeOffset.Name = "writeOffset";
            this.writeOffset.Size = new System.Drawing.Size(100, 20);
            this.writeOffset.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "offset";
            // 
            // writeHex
            // 
            this.writeHex.Location = new System.Drawing.Point(44, 124);
            this.writeHex.Multiline = true;
            this.writeHex.Name = "writeHex";
            this.writeHex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.writeHex.Size = new System.Drawing.Size(332, 85);
            this.writeHex.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "hex";
            // 
            // reqWrite
            // 
            this.reqWrite.Location = new System.Drawing.Point(150, 95);
            this.reqWrite.Name = "reqWrite";
            this.reqWrite.Size = new System.Drawing.Size(75, 23);
            this.reqWrite.TabIndex = 11;
            this.reqWrite.Text = "requestWrite";
            this.reqWrite.UseVisualStyleBackColor = true;
            this.reqWrite.Click += new System.EventHandler(this.writeData);
            // 
            // DebugModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 468);
            this.Controls.Add(this.reqWrite);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.writeHex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.writeOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.display);
            this.Controls.Add(this.dataLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataOffset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reqData);
            this.Name = "DebugModule";
            this.Text = "DebugModule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button reqData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dataOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dataLength;
        private System.Windows.Forms.TextBox display;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox writeOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox writeHex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button reqWrite;
    }
}