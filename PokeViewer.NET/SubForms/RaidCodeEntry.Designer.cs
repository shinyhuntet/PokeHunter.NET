namespace PokeViewer.NET.SubForms
{
    partial class RaidCodeEntry
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
            FCETextBox = new TextBox();
            EnterButton = new Button();
            AutoPaste = new Button();
            button1 = new Button();
            SeedToPokemonGroup = new GroupBox();
            Screenshot = new Button();
            RaidSensCheck = new CheckBox();
            TeraIcon = new PictureBox();
            RaidIcon = new PictureBox();
            Results = new TextBox();
            RaidNumeric = new NumericUpDown();
            GoButton = new Button();
            SeedLabel = new Label();
            SeedToPokemonGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TeraIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RaidIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RaidNumeric).BeginInit();
            SuspendLayout();
            // 
            // FCETextBox
            // 
            FCETextBox.Location = new Point(33, 16);
            FCETextBox.Margin = new Padding(3, 4, 3, 4);
            FCETextBox.MaxLength = 6;
            FCETextBox.Multiline = true;
            FCETextBox.Name = "FCETextBox";
            FCETextBox.Size = new Size(228, 99);
            FCETextBox.TabIndex = 0;
            FCETextBox.DoubleClick += textBox1_DoubleClicked;
            // 
            // EnterButton
            // 
            EnterButton.BackColor = Color.Transparent;
            EnterButton.Location = new Point(33, 120);
            EnterButton.Margin = new Padding(3, 4, 3, 4);
            EnterButton.Name = "EnterButton";
            EnterButton.Size = new Size(106, 31);
            EnterButton.TabIndex = 1;
            EnterButton.Text = "Enter";
            EnterButton.UseVisualStyleBackColor = false;
            EnterButton.Click += button1_Click;
            // 
            // AutoPaste
            // 
            AutoPaste.BackColor = Color.Transparent;
            AutoPaste.Location = new Point(155, 120);
            AutoPaste.Margin = new Padding(3, 4, 3, 4);
            AutoPaste.Name = "AutoPaste";
            AutoPaste.Size = new Size(106, 31);
            AutoPaste.TabIndex = 3;
            AutoPaste.Text = "Auto Paste";
            AutoPaste.UseVisualStyleBackColor = false;
            AutoPaste.Click += AutoPaste_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.Location = new Point(104, 159);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(86, 31);
            button1.TabIndex = 4;
            button1.Text = "Clear All";
            button1.UseVisualStyleBackColor = false;
            button1.Click += ClearFCE_Click;
            // 
            // SeedToPokemonGroup
            // 
            SeedToPokemonGroup.Controls.Add(Screenshot);
            SeedToPokemonGroup.Controls.Add(RaidSensCheck);
            SeedToPokemonGroup.Controls.Add(TeraIcon);
            SeedToPokemonGroup.Controls.Add(RaidIcon);
            SeedToPokemonGroup.Controls.Add(Results);
            SeedToPokemonGroup.Controls.Add(RaidNumeric);
            SeedToPokemonGroup.Controls.Add(GoButton);
            SeedToPokemonGroup.Controls.Add(SeedLabel);
            SeedToPokemonGroup.Location = new Point(15, 197);
            SeedToPokemonGroup.Margin = new Padding(3, 4, 3, 4);
            SeedToPokemonGroup.Name = "SeedToPokemonGroup";
            SeedToPokemonGroup.Padding = new Padding(3, 4, 3, 4);
            SeedToPokemonGroup.Size = new Size(266, 257);
            SeedToPokemonGroup.TabIndex = 7;
            SeedToPokemonGroup.TabStop = false;
            SeedToPokemonGroup.Text = "Raid Viewer";
            // 
            // Screenshot
            // 
            Screenshot.BackColor = Color.Transparent;
            Screenshot.Location = new Point(163, 219);
            Screenshot.Margin = new Padding(3, 4, 3, 4);
            Screenshot.Name = "Screenshot";
            Screenshot.Size = new Size(98, 31);
            Screenshot.TabIndex = 13;
            Screenshot.Text = "Screenshot";
            Screenshot.UseVisualStyleBackColor = false;
            Screenshot.Click += Screenshot_Click;
            // 
            // RaidSensCheck
            // 
            RaidSensCheck.AutoSize = true;
            RaidSensCheck.Location = new Point(7, 223);
            RaidSensCheck.Margin = new Padding(3, 4, 3, 4);
            RaidSensCheck.Name = "RaidSensCheck";
            RaidSensCheck.Size = new Size(162, 24);
            RaidSensCheck.TabIndex = 12;
            RaidSensCheck.Text = "Hide Sensitive Info?";
            RaidSensCheck.UseVisualStyleBackColor = true;
            // 
            // TeraIcon
            // 
            TeraIcon.BackColor = SystemColors.Control;
            TeraIcon.Location = new Point(9, 67);
            TeraIcon.Margin = new Padding(3, 4, 3, 4);
            TeraIcon.Name = "TeraIcon";
            TeraIcon.Size = new Size(64, 65);
            TeraIcon.SizeMode = PictureBoxSizeMode.Zoom;
            TeraIcon.TabIndex = 11;
            TeraIcon.TabStop = false;
            // 
            // RaidIcon
            // 
            RaidIcon.BackColor = SystemColors.Control;
            RaidIcon.BackgroundImageLayout = ImageLayout.None;
            RaidIcon.Location = new Point(192, 67);
            RaidIcon.Margin = new Padding(3, 4, 3, 4);
            RaidIcon.Name = "RaidIcon";
            RaidIcon.Size = new Size(65, 65);
            RaidIcon.SizeMode = PictureBoxSizeMode.Zoom;
            RaidIcon.TabIndex = 10;
            RaidIcon.TabStop = false;
            // 
            // Results
            // 
            Results.Location = new Point(7, 64);
            Results.Margin = new Padding(3, 4, 3, 4);
            Results.Multiline = true;
            Results.Name = "Results";
            Results.ReadOnly = true;
            Results.Size = new Size(252, 151);
            Results.TabIndex = 7;
            Results.TextAlign = HorizontalAlignment.Center;
            // 
            // RaidNumeric
            // 
            RaidNumeric.Location = new Point(57, 23);
            RaidNumeric.Margin = new Padding(3, 4, 3, 4);
            RaidNumeric.Maximum = new decimal(new int[] { 95, 0, 0, 0 });
            RaidNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RaidNumeric.Name = "RaidNumeric";
            RaidNumeric.Size = new Size(119, 27);
            RaidNumeric.TabIndex = 9;
            RaidNumeric.TextAlign = HorizontalAlignment.Right;
            RaidNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            RaidNumeric.ValueChanged += NumericValue_Changed;
            // 
            // GoButton
            // 
            GoButton.BackColor = Color.Transparent;
            GoButton.Location = new Point(183, 20);
            GoButton.Margin = new Padding(3, 4, 3, 4);
            GoButton.Name = "GoButton";
            GoButton.Size = new Size(77, 33);
            GoButton.TabIndex = 8;
            GoButton.Text = "Go";
            GoButton.UseVisualStyleBackColor = false;
            GoButton.Click += GoButton_Click;
            // 
            // SeedLabel
            // 
            SeedLabel.AutoSize = true;
            SeedLabel.Location = new Point(7, 28);
            SeedLabel.Name = "SeedLabel";
            SeedLabel.Size = new Size(49, 20);
            SeedLabel.TabIndex = 6;
            SeedLabel.Text = "Den #";
            // 
            // RaidCodeEntry
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(295, 460);
            Controls.Add(SeedToPokemonGroup);
            Controls.Add(button1);
            Controls.Add(EnterButton);
            Controls.Add(AutoPaste);
            Controls.Add(FCETextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "RaidCodeEntry";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RaidUtil - SV";
            SeedToPokemonGroup.ResumeLayout(false);
            SeedToPokemonGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TeraIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)RaidIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)RaidNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox FCETextBox;
        private Button EnterButton;
        private Button AutoPaste;
        private Button button1;
        private GroupBox SeedToPokemonGroup;
        private TextBox Results;
        private Label SeedLabel;
        private Button GoButton;
        private NumericUpDown RaidNumeric;
        private PictureBox RaidIcon;
        private PictureBox TeraIcon;
        private Button Screenshot;
        private CheckBox RaidSensCheck;
    }
}