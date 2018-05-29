using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpAdbClient;
using SharpAdbClient.DeviceCommands;

namespace AppUninstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            devices = new BindingList<DeviceData>();
            comboBoxDevices.DataSource = devices;
            comboBoxDevices.DisplayMember = "Serial";
        }

        private BindingList<DeviceData> devices;
        private bool adbServerIsRunning = false;
        private DeviceMonitor deviceMonitor;
        private bool ServerIsRunning
        {
            get { return adbServerIsRunning; }
            set
            {
                adbServerIsRunning = value;
                if (value)
                {
                    buttonServer.BackColor = Color.Green;
                    buttonServer.Text = "Stop";
                }
                else
                {
                    buttonServer.BackColor = Color.Red;
                    buttonServer.Text = "Start";
                }
            }
        }
        
        private void backgroundWorkerStartServer_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = AdbServer.Instance.StartServer(Application.StartupPath + @"\tools\adb.exe", false);
        }

        private void backgroundWorkerStartServer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (StartServerResult)e.Result;
            if (AdbServer.Instance.GetStatus().IsRunning)
            {
                this.ServerIsRunning = true;
                deviceMonitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
                deviceMonitor.DeviceConnected += OnDeviceConnected;
                deviceMonitor.DeviceDisconnected += OnDeviceDisconnected;
                deviceMonitor.Start();
                buttonServer.Enabled = true;
                WriteLog($"Server started with result: {result.ToString()}");
                WriteLog($"ADB version: {AdbServer.Instance.GetStatus().Version}");
            }
            else
                WriteLog($"Server NOT started with result: {result.ToString()}");
        }

        private void StopServer()
        {
            deviceMonitor.Dispose();
            AdbClient.Instance.KillAdb();
            ServerIsRunning = false;
            devices.Clear();
            listViewPackage.Items.Clear();
        }
        
        private DeviceData GetSelecetedDevice()
        {
            if (comboBoxDevices.InvokeRequired)
                return (DeviceData)comboBoxDevices.Invoke(new Func<DeviceData>(GetSelecetedDevice));
            else
                return (DeviceData)comboBoxDevices.SelectedItem;
        }

        private void WriteLog(string message)
        {
            if (listView1.InvokeRequired)
            {
                listView1.BeginInvoke(new MethodInvoker(() => listView1.Items.Add(DateTime.Now.ToString(@"HH:mm:ss.ffff")).SubItems.Add(message)));
                listView1.BeginInvoke(new MethodInvoker(() => listView1.EnsureVisible(listView1.Items.Count - 1)));
            }
            else
            {
                listView1.Items.Add(DateTime.Now.ToString(@"HH:mm:ss.ffff")).SubItems.Add(message);
                listView1.EnsureVisible(listView1.Items.Count - 1);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerIsRunning)
            {
                var result = MessageBox.Show("ADB server is running. Do you want to kill server and exit", "ADB Uninstaller", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    StopServer();
                else
                {
                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            if (adbServerIsRunning)
            {
                StopServer();
                WriteLog("Server stopped.");
            }
            else
            {
                buttonServer.Enabled = false;
                buttonServer.Text = "Starting...";
                backgroundWorkerStartServer.RunWorkerAsync();
            }
        }

        private void OnDeviceConnected(object sender, DeviceDataEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() => this.devices.Add(e.Device)));
            WriteLog($"The device {e.Device.Name} {e.Device.Serial} has connected to this PC");
        }

        private void OnDeviceDisconnected(object sender, DeviceDataEventArgs e)
        {
            if (e.Device.Equals(GetSelecetedDevice()))
                listViewPackage.BeginInvoke(new MethodInvoker(() => listViewPackage.Items.Clear()));
            this.BeginInvoke(new MethodInvoker(() => this.devices.Remove(e.Device)));
            WriteLog($"The device {e.Device.Name} {e.Device.Serial} has disconnected to this PC");
        }

        private void FillPakageList()
        {
            if (ServerIsRunning && devices.Count > 0)
            {
                PackageManager pm = new PackageManager(GetSelecetedDevice());
                listViewPackage.BeginUpdate();
                listViewPackage.Items.Clear();
                foreach (var package in pm.Packages)
                {
                    listViewPackage.Items.Add(package.Key).SubItems.Add(package.Value);
                }
                listViewPackage.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewPackage.EndUpdate();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FillPakageList();
        }
    }
}
