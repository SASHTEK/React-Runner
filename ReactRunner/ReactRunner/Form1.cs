using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReactRunner
{
    public partial class frmRR : Form
    {
        private string projectPath = "";
        private Process devProcess;
        private bool browserOpened = false; // Prevents opening multiple tabs on hot-reloads

        public frmRR()
        {
            InitializeComponent();
            pnlArea.AllowDrop = true;
            pnlArea.DragEnter += pnlArea_DragEnter;
            //pnlArea.DragDrop += pnlArea_DragDrop;
            //btnRun.Click += btnRun_Click;
            //btnStop.Click += btnStop_Click;
            btnStop.Enabled = false;
        }

        private void frmRR_Load(object sender, EventArgs e)
        {
        }

        private void pnlArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void pnlArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0 && Directory.Exists(files[0]))
            {
                string droppedPath = files[0];
                string packageJsonPath = Path.Combine(droppedPath, "package.json");
                // 1. Check if the folder is actually a React/Node project
                if (!File.Exists(packageJsonPath))
                {
                    MessageBox.Show("This doesn't look like a valid React project.\n\nPlease drag a folder that contains a 'package.json' file.",
                                    "Invalid Folder",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    // Reset UI to show it was rejected
                    projectPath = "";
                    pnlArea.BackColor = System.Drawing.Color.LightCoral;
                    lblInfo.ForeColor = System.Drawing.Color.White;
                    lblInfo.Text = "Invalid folder. Try again.";
                    return;
                }
                // 2. If it is valid, accept it!
                projectPath = droppedPath;
                pnlArea.BackColor = System.Drawing.Color.LightGreen;
                lblInfo.ForeColor = System.Drawing.Color.Black;
                lblInfo.Text = "Ready";
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(projectPath))
            {
                MessageBox.Show("Please drag a project folder first.");
                return;
            }
            pnlArea.BackColor = System.Drawing.Color.LightGreen;
            lblInfo.Text = "Starting server...";
            browserOpened = false;
            btnRun.Enabled = false;
            btnStop.Enabled = true;
            devProcess = new Process();
            devProcess.StartInfo.FileName = "cmd.exe";
            devProcess.StartInfo.WorkingDirectory = projectPath;
            devProcess.StartInfo.Arguments = "/c npm run dev";
            devProcess.StartInfo.UseShellExecute = false;
            devProcess.StartInfo.RedirectStandardOutput = true;
            devProcess.StartInfo.RedirectStandardError = true;
            devProcess.StartInfo.CreateNoWindow = true;
            // Force UTF-8 Encoding so the '➜' arrows don't turn into garbled 'âžœ' characters!
            devProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            devProcess.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
            devProcess.OutputDataReceived += (s, ev) => ParseOutput(ev.Data);
            devProcess.ErrorDataReceived += (s, ev) => ParseOutput(ev.Data);
            try
            {
                devProcess.Start();
                devProcess.BeginOutputReadLine();
                devProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting process: " + ex.Message);
                btnRun.Enabled = true;
                btnStop.Enabled = false;
            }
        }


        private void ParseOutput(string data)
        {
            if (string.IsNullOrEmpty(data)) return;
            // Strip out all hidden terminal color codes (ANSI sequences) 
            // This cleans the text so the Regex can easily find the URL, and makes your Label look nice!
            string cleanData = Regex.Replace(data, @"\x1B\[[0-9;]*[a-zA-Z]", "");
            this.Invoke(new Action(() => {
                if (!browserOpened) lblInfo.Text = cleanData.Trim();
            }));
            // Ultra-forgiving Regex to catch the URL
            var match = Regex.Match(cleanData, @"http://[a-zA-Z0-9\.\-]+:\d+", RegexOptions.IgnoreCase);
            if (match.Success && !browserOpened)
            {
                browserOpened = true;
                string url = match.Value;
                this.Invoke(new Action(() => {
                    lblInfo.Text = "Running on " + url;
                }));
                try
                {
                    // Launch Edge in private mode
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "msedge.exe",
                        Arguments = $"--inprivate {url}",
                        UseShellExecute = true
                    });
                }
                catch
                {
                    // If Edge is missing, fallback to the system default browser
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Forcefully hunt down and kill all Node.js servers
                // This completely bypasses the detached-process bug and guarantees the port is freed.
                foreach (var process in Process.GetProcessesByName("node"))
                {
                    try { process.Kill(); } catch { } // Catch prevents crashing if a process is locked
                }
                // 2. Do the same for esbuild (Vite's background bundler) just in case
                foreach (var process in Process.GetProcessesByName("esbuild"))
                {
                    try { process.Kill(); } catch { }
                }
                // 3. Clean up the original invisible CMD process we started
                if (devProcess != null && !devProcess.HasExited)
                {
                    try { devProcess.Kill(); } catch { }
                }
                lblInfo.Text = "Stopped";
                pnlArea.BackColor = System.Drawing.Color.LightGray;
                btnRun.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping process: " + ex.Message);
            }
        }

        private void frmRR_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStop.Enabled)
            {
                DialogResult result = MessageBox.Show(
                    "The server is still running! \n\nWould you like to stop the server and exit?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnStop_Click(null, null);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void pnlArea_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
