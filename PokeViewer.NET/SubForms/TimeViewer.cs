using Newtonsoft.Json.Linq;
using Octokit;
using SysBot.Base;
using System.Text;
using static SysBot.Base.Decoder;

namespace PokeViewer.NET.SubForms
{
    public partial class TimeViewer : Form
    {
        private readonly ViewerState Executor;
        public TimeViewer(ViewerState executor)
        {
            InitializeComponent();
            Executor = executor;
        }

        private async void Backward_Click(object sender, EventArgs e)
        {
            DisableOptins();

            for (int i = 0; i < HourNum.Value; i++)
                await Executor.SwitchConnection.SendAsync(SwitchCommand.TimeSkipBack(Executor.UseCRLF), CancellationToken.None).ConfigureAwait(false);
            EnableOptions();
            string fal = HourNum.Value == 1 ? "hour" : "hours";
            MessageBox.Show($"Done. We skipped {HourNum.Value} {fal} backward.");
        }
        private async void ReadClock_Click(object sender, EventArgs e)
        {
            DisableOptins();
            long unixTime = await GetUnixTime(CancellationToken.None).ConfigureAwait(false);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime currentTime = epoch.AddSeconds(unixTime);
            MessageBox.Show($"Current Time{Environment.NewLine}{currentTime.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss")}", "Current Switch SystemClock");
            EnableOptions();
        }

        private async void Forward_Click(object sender, EventArgs e)
        {
            DisableOptins();

            for (int i = 0; i < HourNum.Value; i++)
                await Executor.SwitchConnection.SendAsync(SwitchCommand.TimeSkipForward(Executor.UseCRLF), CancellationToken.None).ConfigureAwait(false);
            EnableOptions();
            string fal = HourNum.Value == 1 ? "hour" : "hours";
            MessageBox.Show($"Done. We skipped {HourNum.Value} {fal} forward.");
        }
        private async void BackwardMinute_Click(object sender, EventArgs e)
        {
            DisableOptins();

            var command = SwitchCommand.Encode($"timeSkipBackMinute", Executor.UseCRLF);

            for (int i = 0; i < MinutesNum.Value; i++)
                await Executor.SwitchConnection.SendAsync(command, CancellationToken.None).ConfigureAwait(false);
            EnableOptions();
            string fal = HourNum.Value == 1 ? "minute" : "minutes";
            MessageBox.Show($"Done. We skipped {MinutesNum.Value} {fal} backward.");
        }

        private async void FowardMinute_Click(object sender, EventArgs e)
        {
            DisableOptins();

            var command = SwitchCommand.Encode($"timeSkipForwardMinute", Executor.UseCRLF);

            for (int i = 0; i < MinutesNum.Value; i++)
                await Executor.SwitchConnection.SendAsync(command, CancellationToken.None).ConfigureAwait(false);
            EnableOptions();
            string fal = HourNum.Value == 1 ? "minute" : "minutes";
            MessageBox.Show($"Done. We skipped {MinutesNum.Value} {fal} forward.");
        }

        // unixTime currently fails, hide assets for now.
        private async void SetTime_Click(object sender, EventArgs e)
        {
            DisableOptins();
            long NTPTime = await ResetNTPTime(CancellationToken.None).ConfigureAwait(false);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime resetTime = epoch.AddSeconds(NTPTime);
            MessageBox.Show($"Reset Time{Environment.NewLine}{resetTime.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss")}", "Switch NTP SystemClock");
            EnableOptions();
        }


        private async void Reset_Click(object sender, EventArgs e)
        {
            DisableOptins();
            await Executor.SwitchConnection.SendAsync(SwitchCommand.ResetTime(Executor.UseCRLF), CancellationToken.None).ConfigureAwait(false);
            MessageBox.Show("Done. Time has been reset.");
            EnableOptions();
        }
        private void SetCurrentTime_Click(object sender, EventArgs e)
        {
            DisableOptins();
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = DateTime.UtcNow;
            ulong setTime = (ulong)(date - epoch).TotalSeconds;
            var command = SwitchCommand.Encode($"setcurrentTime {setTime}", Executor.UseCRLF);
            Task.Run(async () => await Executor.SwitchConnection.SendAsync(command, CancellationToken.None).ConfigureAwait(false));
            EnableOptions();
        }
        public async Task<long> GetUnixTime(CancellationToken token) => await Executor.SwitchConnection.GetCurrentTime(token).ConfigureAwait(false);
        public async Task<long> ResetNTPTime(CancellationToken token) => await Executor.SwitchConnection.ResetTimeNTP(token).ConfigureAwait(false);

        private void DisableOptins()
        {
            BackwardButton.Enabled = false;
            ForwordButton.Enabled = false;
            ResetButton.Enabled = false;
            FowardMinute.Enabled = false;
            BackwardMinute.Enabled = false;
            HourNum.Enabled = false;
            MinutesNum.Enabled = false;
            ReadClock.Enabled = false;
            SetNTPTime.Enabled = false;
            SetCurrentTime.Enabled = false;
        }

        private void EnableOptions()
        {
            BackwardButton.Enabled = true;
            ForwordButton.Enabled = true;
            ResetButton.Enabled = true;
            FowardMinute.Enabled = true;
            BackwardMinute.Enabled = true;
            HourNum.Enabled = true;
            MinutesNum.Enabled = true;
            ReadClock.Enabled = true;
            SetNTPTime.Enabled = true;
            SetCurrentTime.Enabled = true;
        }
    }
}
