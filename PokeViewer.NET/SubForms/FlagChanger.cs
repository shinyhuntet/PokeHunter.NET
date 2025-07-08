using Newtonsoft.Json.Linq;
using Octokit;
using PKHeX.Core;
using SysBot.Base;
using System.Collections.Immutable;
using System.Text;
using static SysBot.Base.SwitchButton;
using static System.Buffers.Binary.BinaryPrimitives;
using PKHeX;

namespace PokeViewer.NET.SubForms
{
    public partial class FlagChanger : Form
    {
        private ViewerOffsets Offsets = new();
        private static ulong BaseBlockKeyPointer = 0;
        private readonly ViewerState Executor;
        private SAV9SV SAV = new();
        private SCBlockMetadata Metadata;
        public FlagChanger(ViewerState executor)
        {
            InitializeComponent();
            Metadata = new SCBlockMetadata(SAV.Accessor, new List<string>(), new List<string>().ToArray());
            BoolCombo.DataSource = Metadata.GetSortedBlockKeyList().ToArray();
            ByteCombo.DataSource = SAV.Accessor.BlockInfo.Where(z => z.Data.Length == 1).Select(z => $"0x{z.Key:X}").ToArray();
            IntCombo.DataSource = SAV.Accessor.BlockInfo.Where(z => z.Data.Length is 2 or 4 or 8).Select(z => $"0x{z.Key:X}").ToArray();
            UIntCombo.DataSource = SAV.Accessor.BlockInfo.Where(z => z.Data.Length is 2 or 4 or 8).Select(z => $"0x{z.Key:X}").ToArray();
            BoolCombo.SelectedIndex = -1;
            ByteCombo.SelectedIndex = -1;
            IntCombo.SelectedIndex = -1;
            UIntCombo.SelectedIndex = -1;
            Executor = executor;
            
        }
        private async void Readbutton_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            DisableOptins();
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if(UIntCombo.SelectedIndex >= 0)
            {
                if (SAV.Accessor.TryGetBlock(Convert.ToUInt32(UIntCombo.Items[UIntCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                {
                    DataBlock readblock = new DataBlock()
                    {
                        Key = block.Key,
                        Size = block.Data.Length,
                        Type = SCTypeCode.UInt32,
                        IsEncrypted = true,
                    };
                    UIntValue.Value = (await ReadEncryptedBlockUint(readblock, 0, token).ConfigureAwait(false)).Item1;
                }
            }
            if(IntCombo.SelectedIndex >= 0)
            {
                if (SAV.Accessor.TryGetBlock(Convert.ToUInt32(IntCombo.Items[IntCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                {
                    DataBlock readblock = new DataBlock()
                    {
                        Key = block.Key,
                        Size = block.Data.Length,
                        Type = SCTypeCode.Int32,
                        IsEncrypted = true,
                    };
                    IntValue.Value = (await ReadEncryptedBlockInt32(readblock, 0, token).ConfigureAwait(false)).Item1;
                }
            }
            if(ByteCombo.SelectedIndex >= 0)
            {
                if (SAV.Accessor.TryGetBlock(Convert.ToUInt32(ByteCombo.Items[ByteCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                {
                    DataBlock readblock = new DataBlock()
                    {
                        Key = block.Key,
                        Size = block.Data.Length,
                        Type = SCTypeCode.Byte,
                        IsEncrypted = true,
                    };
                    ByteValue.Value = (await ReadEncryptedBlockByte(readblock, 0, token).ConfigureAwait(false)).Item1;
                }
            }
            if(BoolCombo.SelectedIndex >= 0)
            {
                if (SAV.Accessor.TryGetBlock(Convert.ToUInt32(BoolCombo.Items[BoolCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                {
                    DataBlock readblock = new DataBlock()
                    {
                        Key = block.Key,
                        Size = block.Data.Length,
                        Type = SCTypeCode.Bool1,
                        IsEncrypted = true,
                    };
                    BoolValue.SelectedIndex = await ReadEncryptedBlockBool(readblock ,token).ConfigureAwait(false) ? 1 : 0;
                }
            }
            EnableOptions();
        }
        private async void SetBoolButton_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if (BoolCombo.SelectedIndex < 0)
                return;
            if (!SAV.Accessor.TryGetBlock(Convert.ToUInt32(BoolCombo.Items[BoolCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                return;
            DisableOptins();
            DataBlock dataBlock = new DataBlock()
            {
                Key = block.Key,
                Size = block.Data.Length,
                Type = SCTypeCode.Bool2,
                IsEncrypted = true,
            };
            await ResetGame(token).ConfigureAwait(false);
            var previous = await ReadEncryptedBlockBool(dataBlock, token).ConfigureAwait(false);
            var success = await WriteBlock((BoolValue.SelectedIndex is 0 ? false : true), dataBlock, token, previous).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            while (!await IsOnOverworldTitle(token).ConfigureAwait(false))
                await Click(A, 6_000, token).ConfigureAwait(false);
            EnableOptions();
            if(success)
                MessageBox.Show("Finish writing value to Blocks!", $"Block Key: 0x{block.Key:X}");
        }
        private async void SetByteButton_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if (ByteCombo.SelectedIndex < 0)
                return;
            if (!SAV.Accessor.TryGetBlock(Convert.ToUInt32(ByteCombo.Items[ByteCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                return;
            DisableOptins();
            DataBlock dataBlock = new DataBlock()
            {
                Key = block.Key,
                Size = block.Data.Length,
                Type = SCTypeCode.Byte,
                IsEncrypted = true,
            };
            await ResetGame(token).ConfigureAwait(false);
            var previous = (await ReadEncryptedBlockByte(dataBlock, 0, token).ConfigureAwait(false)).Item1;
            var success = await WriteBlock((byte)ByteValue.Value, dataBlock, token, previous).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            while (!await IsOnOverworldTitle(token).ConfigureAwait(false))
                await Click(A, 6_000, token).ConfigureAwait(false);
            EnableOptions();
            if (success)
                MessageBox.Show("Finish writing value to Blocks!", $"Block Key: 0x{block.Key:X}");
        }

        private async void SetUIntButton_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if (UIntCombo.SelectedIndex < 0)
                return;
            if (!SAV.Accessor.TryGetBlock(Convert.ToUInt32(UIntCombo.Items[UIntCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                return;
            DisableOptins();
            DataBlock dataBlock = new DataBlock()
            {
                Key = block.Key,
                Size = block.Data.Length,
                Type = SCTypeCode.UInt32,
                IsEncrypted = true,
            };
            await ResetGame(token).ConfigureAwait(false);
            var previous = (await ReadEncryptedBlockUint(dataBlock, 0, token).ConfigureAwait(false)).Item1;
            var success = await WriteBlock((uint)UIntValue.Value, dataBlock, token, previous).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            while (!await IsOnOverworldTitle(token).ConfigureAwait(false))
                await Click(A, 6_000, token).ConfigureAwait(false);
            EnableOptions();
            if (success)
                MessageBox.Show("Finish writing value to Blocks!", $"Block Key: 0x{block.Key:X}");
        }

        private async void SetIntButton_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if (IntCombo.SelectedIndex < 0)
                return;
            if (!SAV.Accessor.TryGetBlock(Convert.ToUInt32(IntCombo.Items[IntCombo.SelectedIndex]!.ToString(), 16), out SCBlock? block))
                return;
            DisableOptins();
            DataBlock dataBlock = new DataBlock()
            {
                Key = block.Key,
                Size = block.Data.Length,
                Type = SCTypeCode.Int32,
                IsEncrypted = true,
            };
            await ResetGame(token).ConfigureAwait(false);
            var previous = (await ReadEncryptedBlockInt32(dataBlock, 0, token).ConfigureAwait(false)).Item1;
            var success = await WriteBlock((int)IntValue.Value, dataBlock, token, previous).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            while (!await IsOnOverworldTitle(token).ConfigureAwait(false))
                await Click(A, 6_000, token).ConfigureAwait(false);
            EnableOptions();
            if (success)
                MessageBox.Show("Finish writing value to Blocks!", $"Block Key: 0x{block.Key:X}");
        }

        #region saveblocktasks
        public static byte[] DecryptBlock(uint key, byte[] block)
        {
            var rng = new SCXorShift32(key);
            for (int i = 0; i < block.Length; i++)
                block[i] = (byte)(block[i] ^ rng.Next());
            return block;
        }

        private async Task<(byte, ulong)> ReadEncryptedBlockByte(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (header[1], address);
        }

        private async Task<(byte[], ulong)> ReadEncryptedBlockHeader(DataBlock block, ulong init, CancellationToken token)
        {
            if (init == 0)
            {
                var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
                init = address;
            }

            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(init, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);

            return (header, init);
        }

        private async Task<(uint, ulong)> ReadEncryptedBlockUint(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (ReadUInt32LittleEndian(header.AsSpan()[1..]), address);
        }

        private async Task<(int, ulong)> ReadEncryptedBlockInt32(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (ReadInt32LittleEndian(header.AsSpan()[1..]), address);
        }

        private async Task<bool> ReadEncryptedBlockBool(DataBlock block, CancellationToken token)
        {
            var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
            address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, block.Size, token).ConfigureAwait(false);
            var res = DecryptBlock(block.Key, data);
            return res[0] == 2;
        }

        public async Task<bool> WriteBlock(object data, DataBlock block, CancellationToken token, object? toExpect = default)
        {
            if (block.IsEncrypted)
                return await WriteEncryptedBlockSafe(block, toExpect, data, token).ConfigureAwait(false);
            else
                return await WriteDecryptedBlock((byte[])data!, block, token).ConfigureAwait(false);
        }

        private async Task<bool> WriteDecryptedBlock(byte[] data, DataBlock block, CancellationToken token)
        {
            await Executor.SwitchConnection.PointerPoke(data, block.Pointer!, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockSafe(DataBlock block, object? toExpect, object toWrite, CancellationToken token)
        {
            if (toExpect == default || toWrite == default)
                return false;

            return block.Type switch
            {
                SCTypeCode.Bool1 or SCTypeCode.Bool2 or SCTypeCode.Bool3 => await WriteEncryptedBlockBool(block, (bool)toExpect, (bool)toWrite, token).ConfigureAwait(false),
                SCTypeCode.Byte or SCTypeCode.SByte => await WriteEncryptedBlockByte(block, (byte)toExpect, (byte)toWrite, token).ConfigureAwait(false),
                SCTypeCode.UInt32 or SCTypeCode.UInt64 => await WriteEncryptedBlockUint(block, (uint)toExpect, (uint)toWrite, token).ConfigureAwait(false),
                SCTypeCode.Int32 => await WriteEncryptedBlockInt32(block, (int)toExpect, (int)toWrite, token).ConfigureAwait(false),
                _ => throw new NotSupportedException($"Block {block.Name} (Type {block.Type}) is currently not supported.")
            };
        }

        private async Task<bool> WriteEncryptedBlockUint(DataBlock block, uint valueToExpect, uint valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = ReadUInt32LittleEndian(header.AsSpan()[1..]);
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            WriteUInt32LittleEndian(header.AsSpan()[1..], valueToInject);
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockInt32(DataBlock block, int valueToExpect, int valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = ReadInt32LittleEndian(header.AsSpan()[1..]);
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            WriteInt32LittleEndian(header.AsSpan()[1..], valueToInject);
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockByte(DataBlock block, byte valueToExpect, byte valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = header[1];
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            header[1] = valueToInject;
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockBool(DataBlock block, bool valueToExpect, bool valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, block.Size, token).ConfigureAwait(false);
            data = DecryptBlock(block.Key, data);
            //Validate ram data
            var ram = data[0] == 2;
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            data[0] = valueToInject ? (byte)2 : (byte)1;
            data = EncryptBlock(block.Key, data);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(data, address, token).ConfigureAwait(false);

            return true;
        }

        public static byte[] EncryptBlock(uint key, byte[] block) => DecryptBlock(key, block);

        public async Task<ulong> SearchSaveKey(uint key, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(BaseBlockKeyPointer + 8, 16, token).ConfigureAwait(false);
            var start = BitConverter.ToUInt64(data.AsSpan()[..8]);
            var end = BitConverter.ToUInt64(data.AsSpan()[8..]);

            while (start < end)
            {
                var block_ct = (end - start) / 48;
                var mid = start + (block_ct >> 1) * 48;

                data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(mid, 4, token).ConfigureAwait(false);
                var found = BitConverter.ToUInt32(data);
                if (found == key)
                    return mid;

                if (found >= key)
                    end = mid;
                else start = mid + 48;
            }
            return start;
        }
        #endregion

        private async Task SVSaveGameOverworld(CancellationToken token)
        {
            await Click(X, 1_000, token).ConfigureAwait(false);
            await Click(R, 1_000, token).ConfigureAwait(false);
            await Click(A, 3_000, token).ConfigureAwait(false);
            for (int i = 0; i < 3; i++) // Mash B
                await Click(B, 0_500, token).ConfigureAwait(false);
        }
        private async Task ResetGame(CancellationToken token)
        {
            await CloseGame(token).ConfigureAwait(false);
            await StartGameScreen(token).ConfigureAwait(false);
        }
        private async Task<bool> IsOnOverworldTitle(CancellationToken token)
        {
            var offset = await Executor.SwitchConnection.PointerAll(Offsets.OverworldPointer, token).ConfigureAwait(false);
            if (offset == 0)
                return false;
            return await IsOnOverworld(offset, token).ConfigureAwait(false);
        }
        private async Task<bool> IsOnOverworld(ulong offset, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(offset, 1, token).ConfigureAwait(false);
            return data[0] == 0x11;
        }
        public async Task CloseGame(CancellationToken token)
        {
            // Close out of the game
            await Click(B, 0_500, token).ConfigureAwait(false);
            await Click(HOME, 2_000, token).ConfigureAwait(false);
            await Click(X, 1_000, token).ConfigureAwait(false);
            await Click(A, 5_000, token).ConfigureAwait(false);
        }

        public async Task StartGameScreen(CancellationToken token)
        {
            // Open game.
            await Click(A, 1_000, token).ConfigureAwait(false);

            // Menus here can go in the order: Update Prompt -> Profile -> DLC check -> Unable to use DLC.
            //  The user can optionally turn on the setting if they know of a breaking system update incoming.
            await Click(DUP, 0_600, token).ConfigureAwait(false);
            await Click(A, 1_000, token).ConfigureAwait(false);

            await Click(A, 1_000, token).ConfigureAwait(false);
            // If they have DLC on the system and can't use it, requires an UP + A to start the game.
            // Should be harmless otherwise since they'll be in loading screen.
            await Click(DUP, 0_600, token).ConfigureAwait(false);
            await Click(A, 0_600, token).ConfigureAwait(false);


            // Switch Logo and game load screen
            await Task.Delay(19_000, token).ConfigureAwait(false);
        }
        public new async Task Click(SwitchButton b, int delay, CancellationToken token)
        {
            await Executor.Connection.SendAsync(SwitchCommand.Click(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }
        private void DisableOptins()
        {
            Readbutton.Enabled = false;
            SetBoolButton.Enabled = false;
            SetByteButton.Enabled = false;
            SetIntButton.Enabled = false;
            SetUIntButton.Enabled = false;
            BoolValue.Enabled = false;
            ByteValue.Enabled = false;
            IntValue.Enabled = false;
            UIntValue.Enabled = false;
            BoolCombo.Enabled = false;
            ByteCombo.Enabled = false;
            IntCombo.Enabled = false;
            UIntCombo.Enabled = false;
        }

        private void EnableOptions()
        {
            Readbutton.Enabled = true;
            SetBoolButton.Enabled = true;
            SetByteButton.Enabled = true;
            SetIntButton.Enabled = true;
            SetUIntButton.Enabled = true;
            BoolValue.Enabled = true;
            ByteValue.Enabled = true;
            IntValue.Enabled = true;
            UIntValue.Enabled = true;
            BoolCombo.Enabled = true;
            ByteCombo.Enabled = true;
            IntCombo.Enabled = true;
            UIntCombo.Enabled = true;
        }
    }
}
