namespace WindowsFormsApplication1
{
  sealed partial class FormTachometer
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.button1 = new System.Windows.Forms.Button();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.listBox1 = new System.Windows.Forms.ListBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(13, 13);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(300, 300);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(319, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(98, 56);
      this.button1.TabIndex = 1;
      this.button1.Text = "direct Gas";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
      this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
      // 
      // trackBar1
      // 
      this.trackBar1.LargeChange = 10;
      this.trackBar1.Location = new System.Drawing.Point(319, 74);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.trackBar1.Size = new System.Drawing.Size(45, 423);
      this.trackBar1.TabIndex = 2;
      this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      // 
      // listBox1
      // 
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Items.AddRange(new object[] {
            "750",
            "1000",
            "1500",
            "2000",
            "2500",
            "3000",
            "3500",
            "4000",
            "4500",
            "5000",
            "5500",
            "6000",
            "6500",
            "7000",
            "7500",
            "8000",
            "8500"});
      this.listBox1.Location = new System.Drawing.Point(424, 12);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(120, 485);
      this.listBox1.TabIndex = 3;
      this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
      // 
      // FormTachometer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(973, 510);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.trackBar1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.pictureBox1);
      this.Name = "FormTachometer";
      this.Text = "FormTachometer";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.ListBox listBox1;
  }
}

