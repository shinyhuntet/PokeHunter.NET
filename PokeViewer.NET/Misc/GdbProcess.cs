using PKHeX.Core;
using System.IO;
using System.Diagnostics;

namespace PokeViewer.NET.Misc
{
    public class GdbProcess
    {
        public GdbProcess(string ip, List<BreakPoint> breakPoints, bool wait = false, float TimeToCheck = 60)
        {
            IP = ip;
            BreakPoints = breakPoints;
            wait_for_application = wait;
            TimeToCheckForAddtionalOutputSec = TimeToCheck;
            StartInfo = new();
            StartInfo.FileName = PathToGdb;
            StartInfo.Arguments = "";
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.UseShellExecute = false;
            Gdb = Process.Start(StartInfo) ?? new();
        }

        public string IP;
        public Process Gdb;
        public ProcessStartInfo StartInfo;
        public List<BreakPoint> BreakPoints;
        public List<BreakPoint> ActiveBreakPoints = new();
        public bool wait_for_application = false;
        public string PathToGdb { get; } = "C:/devkitpro/devkitA64/bin/aarch64-none-elf-gdb.exe";
        public float TimeToCheckForAddtionalOutputSec = 60;
        public int MainBase;
        public int MainMax;
        public int HeapBase;
        public int HeapMax;
        public int StackBase;
        public int StackMax;
        public int BreakpointNumber = 1;
        public List<Dictionary<string, dynamic>> GetResponse(float timeoutsec, bool RaiseErrorTimeout)
        {
            List<Dictionary<string, dynamic>> responseList = new();
            string? response;
            string respose_out;
            DateTime TimeOut = DateTime.Now + TimeSpan.FromSeconds(timeoutsec);
            while (true)
            {
                response = Gdb.StandardOutput.ReadLine();
                if (response != null)
                {
                    respose_out = response.Replace("\r", "\n");
                }
                if (timeoutsec == 0)
                    break;
                else if (DateTime.Now > TimeOut)
                    break;
            }
            if (response == null && RaiseErrorTimeout)
            {
                MessageBox.Show($"Did not get response from gdb after {timeoutsec} seconds", "GDB Error");
            }
            return responseList;
        }
        public List<Dictionary<string, dynamic>> Write(string command, float timeoutsec, bool RaiseErrorTimeOut = true, bool readresponse = true)
        {
            if (timeoutsec < 0)
                timeoutsec = 0;

            if (!command.EndsWith("\n"))
                command += "\n";

            Gdb.StandardInput.Write(command);
            Gdb.StandardInput.Flush();

            if (readresponse)
                return GetResponse(timeoutsec, RaiseErrorTimeOut);
            else
                return new List<Dictionary<string, dynamic>>();
        }

        public void GetBases()
        {

        }


    }
    public class BreakPoint
    {
        public int address { get; set; }
        public string Name { get; set; } = string.Empty;
        public Func<GdbProcess, PK9>? OnBreak { get; set; }
        public bool active { get; set; } = true;
        public int breakpoint_number { get; set; }
    }
    public class WatchPoint : BreakPoint
    {
        public string Watch_Type { get; set; } = "awatch";
    }
}

