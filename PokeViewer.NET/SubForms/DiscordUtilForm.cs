﻿using PokeViewer.NET.Properties;

namespace PokeViewer.NET.SubForms
{
    public partial class DiscordUtilForm : Form
    {
        private string BotPrefix = string.Empty;
        public DiscordUtilForm((Color, Color) color)
        {
            InitializeComponent();
            TimeZoneCombo.DataSource = TimeZoneInfo.GetSystemTimeZones();
            TimeZoneCombo.DisplayMember = "DisplayName";
            TimeZoneCombo.ValueMember = "Id";
            SetColors(color);
            SetBotPrefix(PrefixBox.SelectedIndex);
            LoadSavedSettings();
        }

        private void SetColors((Color, Color) color)
        {
            var back = color.Item1;
            var fore = color.Item2;
            BackColor = back;
            ForeColor = fore;
            WebHookURL.BackColor = back;
            WebHookURL.ForeColor = fore;
            DiscordID.BackColor = back;
            DiscordID.ForeColor = fore;
            DiscordName.BackColor = back;
            DiscordName.ForeColor = fore;
            PingLabel.BackColor = back;
            PingLabel.ForeColor = fore;
            WebhookURLText.BackColor = back;
            WebhookURLText.ForeColor = fore;
            DiscordIDText.BackColor = back;
            DiscordIDText.ForeColor = fore;
            DiscordNameText.BackColor = back;
            DiscordNameText.ForeColor = fore;
            TimeZoneCombo.BackColor = back;
            TimeZoneCombo.ForeColor = fore;
            TimeZoneLabel.BackColor = back;
            TimeZoneLabel.ForeColor = fore;
            MessageText.BackColor = back;
            MessageText.ForeColor = fore;
            BotToken.BackColor = back;
            BotToken.ForeColor = fore;
            BotTokenLabel.BackColor = back;
            BotTokenLabel.ForeColor = fore;
            PrefixBox.BackColor = back;
            PrefixBox.ForeColor = fore;
            BotPrefixLabel.BackColor = back;
            BotPrefixLabel.ForeColor = fore;
            UseWiFiProtocol.BackColor = back;
            UseWiFiProtocol.ForeColor = fore;
            SaveButton.BackColor = back;
            SaveButton.ForeColor = fore;
        }

        private void LoadSavedSettings()
        {
            WebhookURLText.Text = Settings.Default.WebHook;
            DiscordIDText.Text = Settings.Default.UserDiscordID;
            DiscordNameText.Text = Settings.Default.DiscordUserName;
            TimeZoneCombo.SelectedIndex = string.IsNullOrEmpty(Settings.Default.DiscordTimeZone) ? -1 : TimeZoneCombo.Items.Cast<TimeZoneInfo>().ToList().FindIndex(tz => tz.Id == Settings.Default.DiscordTimeZone);
            MessageText.Text = Settings.Default.PingMessage;
            BotToken.Text = Settings.Default.BotToken;
            PrefixBox.SelectedIndex = Settings.Default.BotPrefix;
            UseWiFiProtocol.Checked = !Settings.Default.UseWiFiProtocol;
        }

        private string SetBotPrefix(int value)
        {
            switch (value)
            {
                case 0: BotPrefix = "$"; break;
                case 1: BotPrefix = "!"; break;
                case 2: BotPrefix = "%"; break;
                case 3: BotPrefix = "^"; break;
                case 4: BotPrefix = "&"; break;
                case 5: BotPrefix = "*"; break;
                case 6: BotPrefix = "."; break;
                case 7: BotPrefix = ","; break;
                case 8: BotPrefix = ";"; break;
                case 9: BotPrefix = "-"; break;
                case 10: BotPrefix = "_"; break;
            }
            return BotPrefix;
        }

        private void Prefix_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.BotPrefix = PrefixBox.SelectedIndex;
            Settings.Default.BotPrefixString = SetBotPrefix(Settings.Default.BotPrefix);
            Settings.Default.Save();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WebhookURLText.Text))
                Settings.Default.WebHook = WebhookURLText.Text;

            if (!string.IsNullOrEmpty(DiscordIDText.Text))
                Settings.Default.UserDiscordID = DiscordIDText.Text;
            
            if (!string.IsNullOrEmpty(DiscordNameText.Text))
                Settings.Default.DiscordUserName = DiscordNameText.Text;

            if (TimeZoneCombo.SelectedIndex >= 0)
                Settings.Default.DiscordTimeZone = TimeZoneCombo.Items.Cast<TimeZoneInfo>().ToList()[TimeZoneCombo.SelectedIndex].Id;

            if (!string.IsNullOrEmpty(MessageText.Text))
                Settings.Default.PingMessage = MessageText.Text;

            if (!string.IsNullOrEmpty(BotToken.Text))
                Settings.Default.BotToken = BotToken.Text;

            Settings.Default.BotPrefix = PrefixBox.SelectedIndex;
            Settings.Default.BotPrefixString = SetBotPrefix(Settings.Default.BotPrefix);
            Settings.Default.UseWiFiProtocol = !UseWiFiProtocol.Checked;
            Settings.Default.Save();
            this.Close();
        }
    }
}
