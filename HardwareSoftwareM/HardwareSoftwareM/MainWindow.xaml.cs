using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.Management;
using System.IO;
using System.Runtime.InteropServices;


namespace HardwareSoftwareM
{

    static public class MotherboardInfo
    {
        private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        private static ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");
        private static ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");


        static public string Termek
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in baseboardSearcher.Get())
                    {
                        return queryObj["Product"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        static public string Statusz
        {
            get
            {
                try
                {
                    foreach (ManagementObject querObj in baseboardSearcher.Get())
                    {
                        return querObj["Status"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        static public string Gyarto
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in baseboardSearcher.Get())
                    {
                        return queryObj["Manufacturer"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        static public string PrimaryBusType
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in motherboardSearcher.Get())
                    {
                        return queryObj["PrimaryBusType"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        static public string SecondaryBusType
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in motherboardSearcher.Get())
                    {
                        return queryObj["SecondaryBusType"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
        static public string Sorozatszam
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in baseboardSearcher.Get())
                    {
                        return queryObj["SerialNumber"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        static public string Nev
        {
            get
            {
                try
                {
                    foreach (ManagementObject queryObj in motherboardSearcher.Get())
                    {
                        return queryObj["SystemName"].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
    }
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
        public void GetInstalledApps()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        listbox1.Items.Add(sk.GetValue("DisplayName"));
                    }
                }
               
            }
        }

        private void torles_Click(object sender, RoutedEventArgs e)
        {
            
            
            box3.Text = "";
            box4.Text = "";
            box5.Text = "";
            box6.Text = "";
            box7.Text = "";
        }

        private void lekerdezes_Click(object sender, RoutedEventArgs e)
        {
            
            box3.Text = Environment.SystemDirectory;
            box4.Text = Environment.OSVersion.Platform.ToString();
            box5.Text = Environment.CurrentDirectory;
            box6.Text = Environment.MachineName;
            box7.Text = Environment.UserName;


        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            box11.Text = "";
            box22.Text = "";
            box33.Text = "";
            box44.Text = "";
            box55.Text = "";
            box66.Text = "";
            box77.Text = "";
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            box11.Text = MotherboardInfo.Gyarto;
            box22.Text = MotherboardInfo.PrimaryBusType;
            box33.Text = MotherboardInfo.SecondaryBusType;
            box44.Text = MotherboardInfo.Termek;
            box55.Text = MotherboardInfo.Sorozatszam;
            box66.Text = MotherboardInfo.Statusz;
            box77.Text = MotherboardInfo.Nev;
        }

        private void proci_Click(object sender, RoutedEventArgs e)
        {
            SelectQuery Sq = new SelectQuery("Win32_Processor");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            StringBuilder sb = new StringBuilder();
            foreach (ManagementObject mo in osDetailsCollection)
            {
                sb.AppendLine(string.Format("Név : {0}", (string)mo["Name"]));
                sb.AppendLine(string.Format("Magok száma : {0}", mo["NumberOfCores"]).ToString());
                sb.AppendLine(string.Format("Szálak száma : {0}", mo["NumberOfLogicalProcessors"]).ToString());
                sb.AppendLine(string.Format("Foglalat : {0}", mo["SocketDesignation"]).ToString());
                sb.AppendLine(string.Format("Gyártó: {0}", (string)mo["Manufacturer"]));
                sb.AppendLine(string.Format("Availability: {0}", (ushort)mo["Availability"]));
                sb.AppendLine(string.Format("Architecture: {0}", (ushort)mo["Architecture"]));
                sb.AppendLine(string.Format("Bitek: {0}", (ushort)mo["AddressWidth"]));
                sb.AppendLine(string.Format("Caption: {0}", (string)mo["Caption"]));
                sb.AppendLine(string.Format("CreationClassName : {0}", (string)mo["CreationClassName"]));
                sb.AppendLine(string.Format("CurrentClockSpeed : {0}", mo["CurrentClockSpeed"]).ToString());
                sb.AppendLine(string.Format("MaxClockSpeed : {0}", mo["MaxClockSpeed"]).ToString());
                sb.AppendLine(string.Format("DataWidth : {0}", (ushort)mo["DataWidth"]));
                sb.AppendLine(string.Format("Description: {0}", (string)mo["Description"]));
                sb.AppendLine(string.Format("L2CacheSize : {0}", mo["L2CacheSize"]).ToString());
                sb.AppendLine(string.Format("L3CacheSize : {0}", mo["L3CacheSize"]).ToString());
            }
            MessageBox.Show(sb.ToString());
        }
    }

}
