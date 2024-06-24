namespace _012
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            label3 = new Label();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label6 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            label5 = new Label();
            label4 = new Label();
            label8 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 14F);
            label1.Location = new Point(19, 30);
            label1.Name = "label1";
            label1.Size = new Size(149, 25);
            label1.TabIndex = 0;
            label1.Text = "输入用户名 ^-^";
            label1.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(41, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 320);
            panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(209, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(154, 204);
            label7.Name = "label7";
            label7.Size = new Size(92, 17);
            label7.TabIndex = 3;
            label7.Text = "这次要记住了哦";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(162, 134);
            label3.Name = "label3";
            label3.Size = new Size(77, 17);
            label3.TabIndex = 3;
            label3.Text = "600秒内有效";
            // 
            // button3
            // 
            button3.Location = new Point(19, 378);
            button3.Name = "button3";
            button3.Size = new Size(240, 23);
            button3.TabIndex = 2;
            button3.Text = "提交";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(19, 267);
            button2.Name = "button2";
            button2.Size = new Size(240, 23);
            button2.TabIndex = 2;
            button2.Text = "提交";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(19, 96);
            button1.Name = "button1";
            button1.Size = new Size(240, 23);
            button1.TabIndex = 2;
            button1.Text = "发送验证码";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(19, 232);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(240, 23);
            textBox3.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(19, 162);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(240, 23);
            textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(19, 65);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(240, 23);
            textBox1.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 14F);
            label6.Location = new Point(19, 197);
            label6.Name = "label6";
            label6.Size = new Size(88, 25);
            label6.TabIndex = 0;
            label6.Text = "修改密码";
            label6.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 14F);
            label2.Location = new Point(19, 127);
            label2.Name = "label2";
            label2.Size = new Size(107, 25);
            label2.TabIndex = 0;
            label2.Text = "输入验证码";
            label2.Click += label1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(385, 32);
            panel2.Name = "panel2";
            panel2.Size = new Size(237, 320);
            panel2.TabIndex = 2;
            // 
            // label5
            // 
            label5.Font = new Font("Microsoft YaHei UI", 7F);
            label5.Location = new Point(8, 43);
            label5.Name = "label5";
            label5.Size = new Size(222, 267);
            label5.TabIndex = 0;
            label5.Text = resources.GetString("label5.Text");
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label4.Location = new Point(8, 6);
            label4.Name = "label4";
            label4.Size = new Size(116, 37);
            label4.TabIndex = 0;
            label4.Text = "用户须知";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.ForeColor = SystemColors.Control;
            label8.Location = new Point(298, 366);
            label8.Name = "label8";
            label8.Size = new Size(112, 17);
            label8.TabIndex = 9;
            label8.Text = "嶳鬱™科技 © DiYu";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(671, 387);
            Controls.Add(label8);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Name = "Form3";
            RightToLeftLayout = true;
            Text = "Form3";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private TextBox textBox1;
        private Panel panel2;
        private Label label3;
        private Button button2;
        private Button button1;
        private TextBox textBox2;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label7;
        private Button button3;
        private TextBox textBox3;
        private Label label6;
        private PictureBox pictureBox1;
        private Label label8;
    }
}