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
using BrightIdeasSoftware;
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
            objectListViewPackages.ClearObjects();
            //label1.Text = string.Empty;
            SetProduct(String.Empty);
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

        private void SetProduct(string product)
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke(new MethodInvoker(() => label1.Text = product));
            }
            else
                label1.Text = product;
        }

        private void FillPakageList()
        {
            if (ServerIsRunning && devices.Count > 0)
            {
                toolStripTextBoxFilter.Text = String.Empty;
                PackageManager pmSystemOnly = new PackageManager(GetSelecetedDevice(), PackageManager.AppListType.SystemOnly);
                PackageManager pmThirdParty = new PackageManager(GetSelecetedDevice(), PackageManager.AppListType.ThirdPartyOnly);
                List<PackageData> packagesSystemOnly = new List<PackageData>();
                List<PackageData> packagesThirdParty = new List<PackageData>();

                foreach (var package in pmSystemOnly.Packages)
                    packagesSystemOnly.Add(new PackageData(package.Key, package.Value, true));
                foreach (var package in pmThirdParty.Packages)
                    packagesThirdParty.Add(new PackageData(package.Key, package.Value, false));

                objectListViewPackages.AllColumns[2].GroupKeyGetter = delegate (object x)
                {
                    return ((PackageData)x).IsSystemApp;
                };

                objectListViewPackages.SetObjects(packagesSystemOnly);
                objectListViewPackages.AddObjects(packagesThirdParty);

                

                objectListViewPackages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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
            var devProperties = e.Device.GetProperties();
            SetProduct($"{devProperties["ro.product.brand"]} {devProperties["ro.product.model"]}");
        }

        private void OnDeviceDisconnected(object sender, DeviceDataEventArgs e)
        {
            if (e.Device.Equals(GetSelecetedDevice()))
                objectListViewPackages.ClearObjects();
            this.BeginInvoke(new MethodInvoker(() => this.devices.Remove(e.Device)));
            SetProduct(String.Empty);
            WriteLog($"The device {e.Device.Name} {e.Device.Serial} has disconnected to this PC"); 
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FillPakageList();
        }
        
        private void toolStripTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            TextMatchFilter filter = null;
            string txt = ((ToolStripTextBox)sender).Text;
            ObjectListView olv = objectListViewPackages;
            if (!String.IsNullOrEmpty(txt))
                filter = TextMatchFilter.Contains(olv, txt);

            // Text highlighting requires at least a default renderer
            if (olv.DefaultRenderer == null)
                olv.DefaultRenderer = new HighlightTextRenderer(filter);

            olv.AdditionalFilter = filter;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(objectListViewPackages.CheckedObjects.Count > 0 &&
                MessageBox.Show("Do you realy want to delete selected application?",Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PackageManager pm = new PackageManager(GetSelecetedDevice());
                foreach(PackageData package in objectListViewPackages.CheckedObjects)
                {
                    if (package.IsSystemApp)
                    {
                        pm.UninstallSystemPackage(package.Name);
                        WriteLog($"System application '{package.Name}' successfully removed.");
                    }
                    else
                    {
                        pm.UninstallPackage(package.Name);
                        WriteLog($"Application '{package.Name}' successfully removed.");
                    }
                }
                FillPakageList();
            }
        }
    }
}
