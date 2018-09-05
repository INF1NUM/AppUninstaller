using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SharpAdbClient;
using SharpAdbClient.DeviceCommands;

namespace AppUninstaller
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            buttonServerStop.Enabled = false;
            devices = new BindingList<DeviceData>();
            comboBoxDevices.DataSource = devices;
            comboBoxDevices.DisplayMember = "Serial";
        }

        private BindingList<DeviceData> devices;
        private DeviceMonitor deviceMonitor;

        private void ControlInvoke(Control control, MethodInvoker methodInvoker)
        {
            if (control.InvokeRequired)
                control.BeginInvoke(methodInvoker);
            else
                methodInvoker();
        }

        private void WriteLog(string text)
        {
            ControlInvoke(listViewLog, (MethodInvoker)delegate ()
            {
                listViewLog.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(@"HH:mm:ss.ffff"), text }));
                listViewLog.Items[listViewLog.Items.Count - 1].EnsureVisible();
            });

        }

        private void ServerStop()
        {
            deviceMonitor.Dispose();
            AdbClient.Instance.KillAdb();
            devices.Clear();
            objectListViewPackages.ClearObjects();
            SetProduct(String.Empty);
            buttonServerStop.Enabled = false;
            buttonServerStart.Enabled = true;
        }

        private DeviceData GetSelecetedDevice()
        {
            if (comboBoxDevices.InvokeRequired)
                return (DeviceData)comboBoxDevices.Invoke(new Func<DeviceData>(GetSelecetedDevice));
            else
                return (DeviceData)comboBoxDevices.SelectedItem;
        }

        private void SetProduct(DeviceData deviceData)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                var properties = DeviceExtensions.GetProperties(deviceData);

                if (properties.Count > 0)
                {
                    ControlInvoke(labelDeviceName, (MethodInvoker)delegate ()
                    {
                        labelDeviceName.Text = properties["ro.product.brand"] + " " + properties["ro.product.model"];
                    });
                }
            });
        }

        private void SetProduct(string text)
        {
            ControlInvoke(labelDeviceName, (MethodInvoker)delegate ()
            {
                labelDeviceName.Text = text;
            });
        }

        private void FillPakageList()
        {
            if (AdbServer.Instance.GetStatus().IsRunning && devices.Count > 0)
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

        private void backgroundWorkerStartServer_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = AdbServer.Instance.StartServer(Application.StartupPath + @"\tools\adb.exe", true);
        }

        private void backgroundWorkerStartServer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (StartServerResult)e.Result;
            if (AdbServer.Instance.GetStatus().IsRunning)
            {
                deviceMonitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
                deviceMonitor.DeviceConnected += OnDeviceConnected;
                deviceMonitor.DeviceDisconnected += OnDeviceDisconnected;
                deviceMonitor.Start();
                buttonServerStop.Enabled = true;
                WriteLog($"Server started with result: {result.ToString()}");
                WriteLog($"ADB version: {AdbServer.Instance.GetStatus().Version}");
            }
            else
            {
                WriteLog($"Server NOT started with result: {result.ToString()}");
                buttonServerStart.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AdbServer.Instance.GetStatus().IsRunning)
            {
                var result = MessageBox.Show("ADB server is running. Do you want to kill server and exit", "ADB Uninstaller", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    ServerStop();
                else
                {
                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void buttonServerStart_Click(object sender, EventArgs e)
        {
            buttonServerStart.Enabled = false;
            backgroundWorkerStartServer.RunWorkerAsync();
        }

        private void OnDeviceConnected(object sender, DeviceDataEventArgs e)
        {
            ControlInvoke(this, (MethodInvoker)delegate () { this.devices.Add(e.Device); });
            WriteLog($"The device {e.Device.Name} {e.Device.Serial} has connected to this PC");
            SetProduct(e.Device);
        }

        private void OnDeviceDisconnected(object sender, DeviceDataEventArgs e)
        {
            if (e.Device.Equals(GetSelecetedDevice()))
                objectListViewPackages.ClearObjects();
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate () { this.devices.Remove(e.Device); });
            else
                this.devices.Remove(e.Device);
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
            if (objectListViewPackages.CheckedObjects.Count > 0 &&
                MessageBox.Show("Do you realy want to delete selected application?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PackageManager pm = new PackageManager(GetSelecetedDevice());

                foreach (PackageData package in objectListViewPackages.CheckedObjects)
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

        private void buttonServerStop_Click(object sender, EventArgs e)
        {
            ServerStop();
        }
    }
}
