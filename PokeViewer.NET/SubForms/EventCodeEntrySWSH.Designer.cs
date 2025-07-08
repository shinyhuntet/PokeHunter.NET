﻿namespace PokeViewer.NET.SubForms
{
    partial class EventCodeEntrySWSH
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
            Num = new NumericUpDown();
            RedeemCount = new Label();
            ResetMysteryGifts = new Button();
            RedeemButton = new Button();
            HardStop = new Button();
            GiftCode = new TextBox();
            ClearButton = new Button();
            label1 = new Label();
            OverShoot = new NumericUpDown();
            ItemGift = new CheckBox();
            StopConditions = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            NonCode = new CheckBox();
            textBox2 = new TextBox();
            LinkAccount = new CheckBox();
            filter = new CheckBox();
            AutoPaste = new Button();
            Repeatable = new CheckBox();
            GiftLabel = new Label();
            GiftIndex = new NumericUpDown();
            RateBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OverShoot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GiftIndex).BeginInit();
            SuspendLayout();
            // 
            // Num
            // 
            Num.Location = new Point(222, 539);
            Num.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Num.Name = "Num";
            Num.Size = new Size(50, 23);
            Num.TabIndex = 10;
            Num.TextAlign = HorizontalAlignment.Center;
            Num.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // RedeemCount
            // 
            RedeemCount.AutoSize = true;
            RedeemCount.Font = new Font("Segoe UI", 9.75F);
            RedeemCount.Location = new Point(99, 541);
            RedeemCount.Name = "RedeemCount";
            RedeemCount.Size = new Size(117, 17);
            RedeemCount.TabIndex = 11;
            RedeemCount.Text = "Redemption Count";
            // 
            // ResetMysteryGifts
            // 
            ResetMysteryGifts.Location = new Point(286, 512);
            ResetMysteryGifts.Name = "ResetMysteryGifts";
            ResetMysteryGifts.Size = new Size(130, 23);
            ResetMysteryGifts.TabIndex = 7;
            ResetMysteryGifts.Text = "ResetMysteryGifts";
            ResetMysteryGifts.UseVisualStyleBackColor = true;
            ResetMysteryGifts.Click += button4_Click;
            // 
            // RedeemButton
            // 
            RedeemButton.Location = new Point(15, 485);
            RedeemButton.Name = "RedeemButton";
            RedeemButton.Size = new Size(98, 23);
            RedeemButton.TabIndex = 0;
            RedeemButton.Text = "Redeem!";
            RedeemButton.UseVisualStyleBackColor = true;
            RedeemButton.Click += RedeemButton_Click;
            // 
            // HardStop
            // 
            HardStop.Location = new Point(18, 539);
            HardStop.Name = "HardStop";
            HardStop.Size = new Size(75, 23);
            HardStop.TabIndex = 5;
            HardStop.Text = "HardStop";
            HardStop.UseVisualStyleBackColor = true;
            HardStop.Click += button2_Click;
            // 
            // GiftCode
            // 
            GiftCode.Location = new Point(18, 441);
            GiftCode.MaxLength = 16;
            GiftCode.Multiline = true;
            GiftCode.Name = "GiftCode";
            GiftCode.Size = new Size(233, 36);
            GiftCode.TabIndex = 1;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(164, 485);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(98, 23);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Clear Text";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(119, 480);
            label1.Name = "label1";
            label1.Size = new Size(39, 28);
            label1.TabIndex = 3;
            label1.Text = "🎁";
            // 
            // OverShoot
            // 
            OverShoot.Location = new Point(286, 485);
            OverShoot.Maximum = new decimal(new int[] { 2500, 0, 0, 0 });
            OverShoot.Name = "OverShoot";
            OverShoot.Size = new Size(67, 23);
            OverShoot.TabIndex = 4;
            OverShoot.TextAlign = HorizontalAlignment.Center;
            // 
            // ItemGift
            // 
            ItemGift.AutoSize = true;
            ItemGift.Location = new Point(18, 514);
            ItemGift.Name = "ItemGift";
            ItemGift.Size = new Size(71, 19);
            ItemGift.TabIndex = 9;
            ItemGift.Text = "Item Gift";
            ItemGift.UseVisualStyleBackColor = true;
            ItemGift.CheckedChanged += ItemGift_CheckedChanged;
            // 
            // StopConditions
            // 
            StopConditions.Location = new Point(286, 541);
            StopConditions.Name = "StopConditions";
            StopConditions.Size = new Size(130, 23);
            StopConditions.TabIndex = 12;
            StopConditions.Text = "StopConditions";
            StopConditions.UseVisualStyleBackColor = true;
            StopConditions.Click += StopConditions_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(18, 13);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(195, 187);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(237, 88);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(96, 112);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 87;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(237, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(69, 57);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 88;
            pictureBox1.TabStop = false;
            // 
            // NonCode
            // 
            NonCode.AutoSize = true;
            NonCode.Location = new Point(106, 514);
            NonCode.Name = "NonCode";
            NonCode.Size = new Size(88, 19);
            NonCode.TabIndex = 89;
            NonCode.Text = "Online Gift?";
            NonCode.UseVisualStyleBackColor = true;
            NonCode.CheckedChanged += NonCode_CheckedChanged;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Yu Gothic UI", 14.25F);
            textBox2.Location = new Point(18, 207);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(315, 228);
            textBox2.TabIndex = 90;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // LinkAccount
            // 
            LinkAccount.AutoSize = true;
            LinkAccount.Location = new Point(110, 576);
            LinkAccount.Name = "LinkAccount";
            LinkAccount.Size = new Size(101, 19);
            LinkAccount.TabIndex = 93;
            LinkAccount.Text = "Nintendo Link";
            LinkAccount.UseVisualStyleBackColor = true;
            // 
            // filter
            // 
            filter.AutoSize = true;
            filter.Location = new Point(18, 576);
            filter.Name = "filter";
            filter.Size = new Size(86, 19);
            filter.TabIndex = 94;
            filter.Text = "Filter Mode";
            filter.UseVisualStyleBackColor = true;
            filter.CheckedChanged += Filter_CheckedChanged;
            // 
            // AutoPaste
            // 
            AutoPaste.Location = new Point(222, 571);
            AutoPaste.Name = "AutoPaste";
            AutoPaste.Size = new Size(75, 23);
            AutoPaste.TabIndex = 95;
            AutoPaste.Text = "AutoPaste";
            AutoPaste.UseVisualStyleBackColor = true;
            AutoPaste.Click += AutoPaste_Click;
            // 
            // Repeatable
            // 
            Repeatable.AutoSize = true;
            Repeatable.Location = new Point(303, 574);
            Repeatable.Name = "Repeatable";
            Repeatable.Size = new Size(95, 19);
            Repeatable.TabIndex = 96;
            Repeatable.Text = "Is Repeatable";
            Repeatable.UseVisualStyleBackColor = true;
            // 
            // GiftLabel
            // 
            GiftLabel.AutoSize = true;
            GiftLabel.Font = new Font("Segoe UI", 9.75F);
            GiftLabel.Location = new Point(257, 451);
            GiftLabel.Name = "GiftLabel";
            GiftLabel.Size = new Size(63, 17);
            GiftLabel.TabIndex = 97;
            GiftLabel.Text = "Gift Index";
            // 
            // GiftIndex
            // 
            GiftIndex.Location = new Point(326, 451);
            GiftIndex.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GiftIndex.Name = "GiftIndex";
            GiftIndex.Size = new Size(50, 23);
            GiftIndex.TabIndex = 98;
            GiftIndex.TextAlign = HorizontalAlignment.Center;
            GiftIndex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // RateBox
            // 
            RateBox.Font = new Font("Yu Gothic UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RateBox.Location = new Point(339, 344);
            RateBox.Multiline = true;
            RateBox.Name = "RateBox";
            RateBox.ReadOnly = true;
            RateBox.Size = new Size(96, 91);
            RateBox.TabIndex = 99;
            RateBox.TextAlign = HorizontalAlignment.Center;
            // 
            // EventCodeEntrySWSH
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 607);
            Controls.Add(RateBox);
            Controls.Add(GiftIndex);
            Controls.Add(GiftLabel);
            Controls.Add(Repeatable);
            Controls.Add(AutoPaste);
            Controls.Add(filter);
            Controls.Add(LinkAccount);
            Controls.Add(textBox2);
            Controls.Add(NonCode);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(StopConditions);
            Controls.Add(ClearButton);
            Controls.Add(GiftCode);
            Controls.Add(RedeemButton);
            Controls.Add(label1);
            Controls.Add(ItemGift);
            Controls.Add(OverShoot);
            Controls.Add(HardStop);
            Controls.Add(ResetMysteryGifts);
            Controls.Add(Num);
            Controls.Add(RedeemCount);
            MaximizeBox = false;
            Name = "EventCodeEntrySWSH";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Code Entry";
            ((System.ComponentModel.ISupportInitialize)Num).EndInit();
            ((System.ComponentModel.ISupportInitialize)OverShoot).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)GiftIndex).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Button RedeemButton;
        private TextBox GiftCode;
        private Button ClearButton;
        private Label label1;
        private NumericUpDown OverShoot;
        private CheckBox ItemGift;
        private Button HardStop;
        private Button ResetMysteryGifts;
        private NumericUpDown Num;
        private Label RedeemCount;
        private Button StopConditions;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private CheckBox NonCode;
        private TextBox textBox2;
        private CheckBox LinkAccount;
        private CheckBox filter;
        private Button AutoPaste;
        private CheckBox Repeatable;
        private Label GiftLabel;
        private NumericUpDown GiftIndex;
        private TextBox RateBox;
    }
}