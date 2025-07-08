namespace PokeViewer.NET.SubForms
{
    partial class ControllerView
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
            button1 = new Button();
            checkBox1 = new CheckBox();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            button2 = new Button();
            numericUpDown1 = new NumericUpDown();
            groupBox1 = new GroupBox();
            Ycoord = new Label();
            YcoordNum = new NumericUpDown();
            Xcoord = new Label();
            XcoordNum = new NumericUpDown();
            groupBox2 = new GroupBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)YcoordNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)XcoordNum).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(107, 94);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Go!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(18, 98);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(83, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Stop Turbo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "A", "B", "X", "Y", "RSTICK", "LSTICK", "L", "R", "ZL", "ZR", "PLUS", "MINUS", "DUP", "DDOWN", "DLEFT", "DRIGHT", "HOME", "CAPTURE" });
            comboBox1.Location = new Point(18, 32);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(165, 23);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "A";
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(188, 23);
            textBox1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(56, 79);
            button2.Name = "button2";
            button2.Size = new Size(92, 23);
            button2.TabIndex = 4;
            button2.Text = "Perform Clicks";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            numericUpDown1.Location = new Point(124, 50);
            numericUpDown1.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 500, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(70, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Ycoord);
            groupBox1.Controls.Add(YcoordNum);
            groupBox1.Controls.Add(Xcoord);
            groupBox1.Controls.Add(XcoordNum);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(34, 118);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(229, 137);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Turbo";
            // 
            // Ycoord
            // 
            Ycoord.AutoSize = true;
            Ycoord.Location = new Point(121, 63);
            Ycoord.Name = "Ycoord";
            Ycoord.Size = new Size(14, 15);
            Ycoord.TabIndex = 9;
            Ycoord.Text = "Y";
            // 
            // YcoordNum
            // 
            YcoordNum.Enabled = false;
            YcoordNum.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            YcoordNum.Location = new Point(141, 61);
            YcoordNum.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            YcoordNum.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            YcoordNum.Name = "YcoordNum";
            YcoordNum.Size = new Size(70, 23);
            YcoordNum.TabIndex = 8;
            // 
            // Xcoord
            // 
            Xcoord.AutoSize = true;
            Xcoord.Location = new Point(18, 63);
            Xcoord.Name = "Xcoord";
            Xcoord.Size = new Size(14, 15);
            Xcoord.TabIndex = 7;
            Xcoord.Text = "X";
            // 
            // XcoordNum
            // 
            XcoordNum.Enabled = false;
            XcoordNum.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            XcoordNum.Location = new Point(38, 61);
            XcoordNum.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            XcoordNum.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            XcoordNum.Name = "XcoordNum";
            XcoordNum.Size = new Size(70, 23);
            XcoordNum.TabIndex = 7;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(button2);
            groupBox2.Location = new Point(34, 7);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 108);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sequence";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 53);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 6;
            label1.Text = "Delay Between Clicks";
            // 
            // ControllerView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 267);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ControllerView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ControllerView";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)YcoordNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)XcoordNum).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Button button1;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Button button2;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label Ycoord;
        private NumericUpDown YcoordNum;
        private Label Xcoord;
        private NumericUpDown XcoordNum;
    }
}