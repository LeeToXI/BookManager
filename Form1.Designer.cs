namespace _012;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        button1 = new Button();
        label1 = new Label();
        pictureBox2 = new PictureBox();
        button2 = new Button();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        textBox1 = new AntdUI.Input();
        textBox2 = new AntdUI.Input();
        pictureBox1 = new AntdUI.Avatar();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Font = new Font("黑体", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
        button1.Location = new Point(204, 222);
        button1.Name = "button1";
        button1.Size = new Size(62, 31);
        button1.TabIndex = 0;
        button1.Text = "登录";
        button1.UseVisualStyleBackColor = true;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.BackColor = Color.Transparent;
        label1.ForeColor = SystemColors.Control;
        label1.Location = new Point(235, 321);
        label1.Name = "label1";
        label1.Size = new Size(112, 17);
        label1.TabIndex = 8;
        label1.Text = "嶳鬱™科技 © DiYu";
        // 
        // pictureBox2
        // 
        pictureBox2.BackColor = Color.Transparent;
        pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
        pictureBox2.Location = new Point(412, 171);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(23, 22);
        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox2.TabIndex = 9;
        pictureBox2.TabStop = false;
        // 
        // button2
        // 
        button2.Font = new Font("黑体", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
        button2.Location = new Point(314, 222);
        button2.Name = "button2";
        button2.Size = new Size(62, 31);
        button2.TabIndex = 0;
        button2.Text = "注册";
        button2.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.ForeColor = SystemColors.ControlDarkDark;
        label2.Location = new Point(270, 270);
        label2.Name = "label2";
        label2.Size = new Size(62, 17);
        label2.TabIndex = 10;
        label2.Text = "忘记密码?";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.BackColor = Color.Transparent;
        label3.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
        label3.ForeColor = SystemColors.ControlLight;
        label3.Location = new Point(174, 135);
        label3.Name = "label3";
        label3.Size = new Size(39, 19);
        label3.TabIndex = 11;
        label3.Text = "账号:";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.BackColor = Color.Transparent;
        label4.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
        label4.ForeColor = SystemColors.Control;
        label4.Location = new Point(174, 171);
        label4.Name = "label4";
        label4.Size = new Size(39, 19);
        label4.TabIndex = 12;
        label4.Text = "密码:";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.BackColor = Color.Transparent;
        label5.ForeColor = SystemColors.ControlDarkDark;
        label5.Location = new Point(344, 196);
        label5.Name = "label5";
        label5.Size = new Size(62, 17);
        label5.TabIndex = 13;
        label5.Text = "忘记密码?";
        // 
        // textBox1
        // 
        textBox1.IconRatio = 0.9F;
        textBox1.Location = new Point(219, 135);
        textBox1.Name = "textBox1";
        textBox1.SelectionColor = Color.FromArgb(224, 224, 224);
        textBox1.Size = new Size(187, 23);
        textBox1.TabIndex = 14;
        // 
        // textBox2
        // 
        textBox2.IconRatio = 0.9F;
        textBox2.Location = new Point(219, 171);
        textBox2.Name = "textBox2";
        textBox2.PasswordChar = '^';
        textBox2.SelectionColor = Color.FromArgb(224, 224, 224);
        textBox2.Size = new Size(187, 23);
        textBox2.TabIndex = 14;
        // 
        // pictureBox1
        // 
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(174, 60);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Radius = 10;
        pictureBox1.Size = new Size(65, 65);
        pictureBox1.TabIndex = 16;
        pictureBox1.Text = "a";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
        BackgroundImageLayout = ImageLayout.Stretch;
        ClientSize = new Size(598, 369);
        Controls.Add(pictureBox1);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(pictureBox2);
        Controls.Add(label1);
        Controls.Add(button2);
        Controls.Add(button1);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "Form1";
        Text = "嶳鬱™-登录页";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button button1;
    private Label label1;
    private PictureBox pictureBox2;
    private Button button2;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private AntdUI.Input textBox1;
    private AntdUI.Input textBox2;
    private AntdUI.Avatar pictureBox1;
}