
namespace TriadNSim.Modifications.Forms
{
    partial class Translation
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.drawingPanel1.Location = new System.Drawing.Point(12, 35);
            this.drawingPanel1.Name = "drawingPanel1";
            this.drawingPanel1.Size = new System.Drawing.Size(837, 392);
            this.drawingPanel1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.drawingPanel1.StickToGrid = false;
            this.drawingPanel1.TabIndex = 0;
            this.drawingPanel1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.drawingPanel1.Zoom = 1F;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(269, 443);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(580, 130);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Транслировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(251, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Выбрать правила";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(861, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Translation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 585);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.drawingPanel1);
            this.Name = "Translation";
            this.Text = "Translation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DrawingPanel.DrawingPanel drawingPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}