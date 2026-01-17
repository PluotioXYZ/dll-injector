using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace DLLInjector
{
    public partial class MainForm : Form
    {
        private string? selectedDllPath;
        private List<string> allProcesses = new List<string>();
        private Dictionary<int, bool> injectedProcesses = new Dictionary<int, bool>();

        public MainForm()
        {
            InitializeComponent();
            autoInjectTimer = new System.Windows.Forms.Timer();
            autoInjectTimer.Interval = 1000; // Check every second
            autoInjectTimer.Tick += AutoInjectTimer_Tick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void RefreshProcessList()
        {
            processListBox.Items.Clear();
            allProcesses.Clear();
            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    if (!string.IsNullOrEmpty(process.ProcessName))
                    {
                        string processEntry = $"{process.ProcessName} (PID: {process.Id})";
                        allProcesses.Add(processEntry);
                        processListBox.Items.Add(processEntry);
                    }
                }
                catch { }
            }

            txtSearch.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterProcesses();
        }

        private void FilterProcesses()
        {
            string searchText = txtSearch.Text.ToLower();
            processListBox.Items.Clear();

            foreach (var process in allProcesses)
            {
                if (process.ToLower().Contains(searchText))
                {
                    processListBox.Items.Add(process);
                }
            }
        }

        private void chkAutoInject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoInject.Checked)
            {
                if (string.IsNullOrEmpty(selectedDllPath) || processListBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select both a process and a DLL file before enabling auto-inject.", 
                        "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkAutoInject.Checked = false;
                    return;
                }
                autoInjectTimer.Start();
            }
            else
            {
                autoInjectTimer.Stop();
                injectedProcesses.Clear();
            }
        }

        private void AutoInjectTimer_Tick(object? sender, EventArgs e)
        {
            if (processListBox.SelectedItem == null || string.IsNullOrEmpty(selectedDllPath))
            {
                autoInjectTimer.Stop();
                chkAutoInject.Checked = false;
                return;
            }

            string selectedItem = processListBox.SelectedItem.ToString()!;
            int pidStart = selectedItem.LastIndexOf("(PID: ") + 6;
            int pidEnd = selectedItem.LastIndexOf(")");
            string pidStr = selectedItem.Substring(pidStart, pidEnd - pidStart);

            if (!int.TryParse(pidStr, out int processId))
                return;

            // Check if we've already injected this process
            if (injectedProcesses.ContainsKey(processId) && injectedProcesses[processId])
                return;

            try
            {
                Process targetProcess = Process.GetProcessById(processId);
                int delaySeconds = (int)numDelay.Value;

                InjectDll(targetProcess, selectedDllPath);
                injectedProcesses[processId] = true;

                // Disable auto-inject after successful injection
                autoInjectTimer.Stop();
                chkAutoInject.Checked = false;
                
                MessageBox.Show($"DLL auto-injected successfully into {targetProcess.ProcessName}!", 
                    "Auto-Inject Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                // Process might not exist anymore, just wait
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void btnSelectDll_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*";
                openFileDialog.Title = "Select DLL to Inject";
                openFileDialog.CheckFileExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedDllPath = openFileDialog.FileName;
                    lblSelectedDll.Text = $"Selected: {Path.GetFileName(selectedDllPath)}";
                    lblSelectedDllPath.Text = selectedDllPath;
                }
            }
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            if (processListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedDllPath))
            {
                MessageBox.Show("Please select a DLL file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(selectedDllPath))
            {
                MessageBox.Show("DLL file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedItem = processListBox.SelectedItem.ToString()!;
            int pidStart = selectedItem.LastIndexOf("(PID: ") + 6;
            int pidEnd = selectedItem.LastIndexOf(")");
            string pidStr = selectedItem.Substring(pidStart, pidEnd - pidStart);

            if (int.TryParse(pidStr, out int processId))
            {
                try
                {
                    Process targetProcess = Process.GetProcessById(processId);
                    InjectDll(targetProcess, selectedDllPath);
                    MessageBox.Show($"DLL injected successfully into {targetProcess.ProcessName}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Injection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InjectDll(Process targetProcess, string dllPath)
        {
            IntPtr procHandle = Interop.OpenProcess(Interop.ProcessAccessFlags.All, false, targetProcess.Id);
            if (procHandle == IntPtr.Zero)
                throw new Exception("Failed to open process.");

            try
            {
                IntPtr loadLibraryAddr = Interop.GetProcAddress(Interop.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                if (loadLibraryAddr == IntPtr.Zero)
                    throw new Exception("Failed to get LoadLibraryA address.");

                IntPtr allocMemAddress = Interop.VirtualAllocEx(procHandle, IntPtr.Zero, (uint)(dllPath.Length + 1), Interop.AllocationType.Commit, Interop.MemoryProtection.ExecuteReadWrite);
                if (allocMemAddress == IntPtr.Zero)
                    throw new Exception("Failed to allocate memory in target process.");

                byte[] dllPathBytes = Encoding.ASCII.GetBytes(dllPath);
                if (!Interop.WriteProcessMemory(procHandle, allocMemAddress, dllPathBytes, (uint)dllPathBytes.Length, out _))
                    throw new Exception("Failed to write DLL path to target process memory.");

                IntPtr remoteThreadHandle = Interop.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, out _);
                if (remoteThreadHandle == IntPtr.Zero)
                    throw new Exception("Failed to create remote thread.");

                Interop.WaitForSingleObject(remoteThreadHandle, Interop.INFINITE);
                Interop.CloseHandle(remoteThreadHandle);
                Interop.VirtualFreeEx(procHandle, allocMemAddress, 0, Interop.FreeType.Release);
            }
            finally
            {
                Interop.CloseHandle(procHandle);
            }
        }
    }
}
