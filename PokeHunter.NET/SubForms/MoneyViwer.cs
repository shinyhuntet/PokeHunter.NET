using Newtonsoft.Json.Linq;
using Octokit;
using SysBot.Base;
using System.Text;

namespace PokeViewer.NET.SubForms
{
    public partial class MoneyViewer : Form
    {
        private ViewerOffsets Offsets = new();
        private readonly ViewerState Executor;
        public MoneyViewer(ViewerState executor)
        {
            InitializeComponent();
            Executor = executor;
        }
        private async void Readbutton_Click(object sender, EventArgs e)
        {
            var money = BitConverter.ToUInt64(await Executor.SwitchConnection.PointerPeek(8, Offsets.MoneyPointer, CancellationToken.None).ConfigureAwait(false));
            if(money > MoneyValue.Maximum)
            {
                MessageBox.Show("Invalid money value.Maybe the Offset is shift.");
                return;
            }
            MoneyValue.Value = money;
            var LP = BitConverter.ToUInt64(await Executor.SwitchConnection.PointerPeek(8, Offsets.LPPointer, CancellationToken.None).ConfigureAwait(false));
            if (LP > LPValue.Maximum)
            {
                MessageBox.Show("Invalid LP value.Maybe the Offset is shift.");
                return;
            }
            LPValue.Value = LP;
            var BP = BitConverter.ToUInt64(await Executor.SwitchConnection.PointerPeek(8, Offsets.BlueberryPointPointer, CancellationToken.None).ConfigureAwait(false));
            if (BP > BPValue.Maximum)
            {
                MessageBox.Show("Invalid BP value.Maybe the Offset is shift.");
                return;
            }
            BPValue.Value = BP;
        }

        private async void LPbutton_Click(object sender, EventArgs e)
        {
            var pointer = await Executor.SwitchConnection.PointerAll(Offsets.LPPointer, CancellationToken.None).ConfigureAwait(false);
            while (pointer == 0)
            {
                await Task.Delay(0_050, CancellationToken.None).ConfigureAwait(false);
                pointer = await Executor.SwitchConnection.PointerAll(Offsets.LPPointer, CancellationToken.None).ConfigureAwait(false);
            }
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(BitConverter.GetBytes((ulong)LPValue.Value), pointer, CancellationToken.None).ConfigureAwait(false);
        }

        private async void Moneybutton_Click(object sender, EventArgs e)
        {
            var pointer = await Executor.SwitchConnection.PointerAll(Offsets.MoneyPointer, CancellationToken.None).ConfigureAwait(false);
            while (pointer == 0)
            {
                await Task.Delay(0_050, CancellationToken.None).ConfigureAwait(false);
                pointer = await Executor.SwitchConnection.PointerAll(Offsets.MoneyPointer, CancellationToken.None).ConfigureAwait(false);
            }
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(BitConverter.GetBytes((ulong)MoneyValue.Value), pointer, CancellationToken.None).ConfigureAwait(false);
        }

        private async void BPbutton_Click(object sender, EventArgs e)
        {
            var pointer = await Executor.SwitchConnection.PointerAll(Offsets.BlueberryPointPointer, CancellationToken.None).ConfigureAwait(false);
            while (pointer == 0)
            {
                await Task.Delay(0_050, CancellationToken.None).ConfigureAwait(false);
                pointer = await Executor.SwitchConnection.PointerAll(Offsets.BlueberryPointPointer, CancellationToken.None).ConfigureAwait(false);
            }
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(BitConverter.GetBytes((ulong)BPValue.Value), pointer, CancellationToken.None).ConfigureAwait(false);
        }

        private void DisableOptins()
        {
            Readbutton.Enabled = false;
            Moneybutton.Enabled = false;
            Readbutton.Enabled = false;
            LPbutton.Enabled = false;
            MoneyValue.Enabled = false;
            LPValue.Enabled = false;
            BPValue.Enabled = false;
        }

        private void EnableOptions()
        {
            Readbutton.Enabled = true;
            Moneybutton.Enabled = true;
            Readbutton.Enabled = true;
            LPbutton.Enabled = true;
            MoneyValue.Enabled = true;
            LPValue.Enabled = true;
            BPValue.Enabled = true;
        }
    }
}
