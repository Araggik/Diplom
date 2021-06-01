
namespace TriadNSim.Modifications.Forms
{
    partial class CreateRule
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
            this.drawingPanel1 = new DrawingPanel.DrawingPanel();
            this.drawingPanel2 = new DrawingPanel.DrawingPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // drawingPanel1
            // 
            this.drawingPanel1.A4 = true;
            this.drawingPanel1.AllowDrop = true;
            this.drawingPanel1.AutoScroll = true;
            this.drawingPanel1.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.drawingPanel1.BackColor = System.Drawing.Color.White;
            this.drawingPanel1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.drawingPanel1.dx = 0;
            this.drawingPanel1.dy = 0;
            this.drawingPanel1.gridSize = 20;
            this.drawingPanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.drawingPanel1.Location = new System.Drawing.Point(12, 28);
            this.drawingPanel1.Name = "drawingPanel1";
            this.drawingPanel1.Size = new System.Drawing.Size(506, 363);
            this.drawingPanel1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.drawingPanel1.StickToGrid = false;
            this.drawingPanel1.TabIndex = 0;
            this.drawingPanel1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.drawingPanel1.Zoom = 1F;
            // 
            // drawingPanel2
            // 
            this.drawingPanel2.A4 = true;
            this.drawingPanel2.AllowDrop = true;
            this.drawingPanel2.AutoScroll = true;
            this.drawingPanel2.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.drawingPanel2.BackColor = System.Drawing.Color.White;
            this.drawingPanel2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.drawingPanel2.dx = 0;
            this.drawingPanel2.dy = 0;
            this.drawingPanel2.gridSize = 20;
            this.drawingPanel2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.drawingPanel2.Location = new System.Drawing.Point(534, 28);
            this.drawingPanel2.Name = "drawingPanel2";
            this.drawingPanel2.Size = new System.Drawing.Size(506, 363);
            this.drawingPanel2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.drawingPanel2.StickToGrid = false;
            this.drawingPanel2.TabIndex = 1;
            this.drawingPanel2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.drawingPanel2.Zoom = 1F;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1052, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 452);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(220, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Имя правила";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(302, 397);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(729, 126);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // CreateRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 535);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.drawingPanel2);
            this.Controls.Add(this.drawingPanel1);
            this.Name = "CreateRule";
            this.Text = "CreateRule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DrawingPanel.DrawingPanel drawingPanel1;
        private DrawingPanel.DrawingPanel drawingPanel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
    }
}