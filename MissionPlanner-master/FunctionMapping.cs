using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using MissionPlanner.Utilities;
using System.Speech.Synthesis;

namespace MissionPlanner
{
    public partial class MainV2
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();

        private void FunctionCaller(object sender, EventArgs e)
        {
            string XmlFilePalth = "ASR.xml";

            try
            {
                if (File.Exists(XmlFilePalth))
                {
                    XmlTextReader reader = new XmlTextReader(XmlFilePalth);

                    int Number = 0;
                    string Parameter, unit;
                    bool XmlUpdated = false;

                    while (!XmlUpdated)
                    {
                        DateTime dt = File.GetLastWriteTime(XmlFilePalth);

                        System.TimeSpan result = DateTime.Now - dt;
                        if (result.Seconds < 3 && !IsFileLocked(new FileInfo(XmlFilePalth)))
                        {
                            XmlUpdated = true;
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Keyword")
                                {
                                    string Keyword = reader.GetAttribute("word");

                                    while (reader.Read())
                                    {
                                        if (reader.Name == "NUM")
                                        {
                                            if (reader.Read() && reader.HasValue)
                                            {
                                                Number = reader.ReadContentAsInt();
                                                continue;
                                            }
                                        }
                                        if (reader.Name == "PAR")
                                        {
                                            if (reader.Read() && reader.HasValue)
                                            {
                                                Parameter = reader.Value;
                                                continue;
                                            }
                                        }
                                        if (reader.Name == "UNIT")
                                        {
                                            if (reader.Read() && reader.HasValue)
                                            {
                                                unit = reader.Value;
                                                continue;
                                            }
                                        }
                                    }

                                    Keyword = Keyword.Trim();
                                    switch (Keyword)
                                    {
                                        case "ACTIONS":
                                            SubTabCall(1, "Actions");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "CONFIG":
                                            MainTabCall(3, "SWConfig", "Config");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "TUNING":
                                            MainTabCall(3, "SWConfig", "Tuning");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "DATA FLASH LOGS":
                                            SubTabCall(6, "DataFlashLogs");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "FLIGHT DATA":
                                            MainTabCall(0, "FlightData", "FlightData");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "FLIGHT PLAN":
                                            MainTabCall(1, "FlightPlanner", "FlightPlan");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "GAUGES":
                                            SubTabCall(2, "Gauges");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "INITIAL SETUP":
                                            MainTabCall(2, "HWConfig", "InitialSetup");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "MESSAGES":
                                            SubTabCall(8, "Messages");
                                            break;

                                        ///////////////////////////////////////////////////////////////////////////////

                                        case "SCRIPTS":
                                            SubTabCall(7, "Scripts");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "SERVO":
                                            SubTabCall(4, "Servo");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "SIMULATION":
                                            MainTabCall(4, "Simulation", "Simulation");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "STATUS":
                                            SubTabCall(3, "Status");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "TELEMETRY LOGS":
                                            SubTabCall(5, "TelemetryLogs");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "TERMINAL":
                                            MainTabCall(5, "Terminal", "Terminal");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "RETURN TO LAUNCH":
                                            SetModeClick("RTL");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "RTL":
                                            SetModeClick("RTL");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "GUIDED":
                                            SetModeClick("Guided");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "AUTO":
                                            AutoBtnClick();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "LAND":
                                            SetModeClick("Land");
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "RESTART MISSION":
                                            ss.SpeakAsync("RESTART MISSION");
                                            try
                                            {
                                                MainV2.comPort.setWPCurrent(0); // set nav to
                                            }
                                            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "RAW SENSOR VIEW":
                                            RawSensorViewClick();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "CLEAR TRACK":
                                            ClearTrackClick();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "GOTO":
                                            SetWPClick(Number);
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "START MISSION":
                                            StartMission_Click();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "ARM":
                                            ss.SpeakAsync("ARM THE MOTORS");
                                            ARM_DISARM();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "DISARM":
                                            ss.SpeakAsync("DISARM THE MOTORS");
                                            ARM_DISARM();
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////

                                        case "TAKEOFF":
                                            if (Number == 0)
                                                Number = 10;
                                            TakeoffSpeech(Number.ToString());
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////
                                        case "":
                                            break;

                                        //////////////////////////////////////////////////////////////////////////////
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                else throw new FileNotFoundException("ASR.xml is not found in the assembly folder");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.GetType().Name);
                Console.WriteLine(ex.Message);
            }
        }

        private void ARM_DISARM()
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
                return;

            // arm the MAV
            try
            {
                if (MainV2.comPort.MAV.cs.armed)
                    if (CustomMessageBox.Show("Are you sure you want to Disarm?", "Disarm?", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                SetModeClick("Guided");
                bool ans = MainV2.comPort.doARM(!MainV2.comPort.MAV.cs.armed);
                if (ans == false)
                    CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
            }
            catch { CustomMessageBox.Show(Strings.ErrorNoResponce, Strings.ERROR); }
        }

        TabControl tabServo;

        public void SubTabCall(int SubTabIndex, string pronounce = null)
        {
            if (pronounce != null)
                ss.SpeakAsync(pronounce);

            Control[] c = FlightData.Controls.Find("tabControlactions", true);

            try
            {
                if (c.Length > 0)
                {
                    tabServo = (TabControl)c[0];
                    tabServo.SelectedIndex = SubTabIndex;
                }
            }
            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }

            MainTabCall(0, "FlightData");
        }

        public void MainTabCall(int MainTabIndex, string Keyword, string pronounce = null)
        {
            if (pronounce != null)
                ss.SpeakAsync(pronounce);

            if (!MyView.current.Name.Equals(Keyword))
            {
                MyView.ShowScreen(Keyword);
            }
        }

        public void AutoBtnClick()
        {
            ss.SpeakAsync("AUTO MODE");
            FlightData.BUT_quickauto_Click(new Button(), new EventArgs());
            MainTabCall(0, "FlightData");
        }

        public void SetWPClick(int num)
        {
            ushort WPnum = Convert.ToUInt16(num);
            ss.SpeakAsync("GOTO WAYPOINT" + num.ToString());

            try
            {
                MainV2.comPort.setWPCurrent(WPnum); // set nav to
            }
            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }

            MainTabCall(0, "FlightData");
        }

        public void RawSensorViewClick()
        {
            ss.SpeakAsync("RAW SENSOR VIEW");
            try
            {
                Form temp = new RAW_Sensor();
                ThemeManager.ApplyThemeTo(temp);
                temp.Show();
            }
            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }
        }

        public void RestartMissionClick()
        {
            ss.SpeakAsync("RESTART MISSION");
            try
            {
                MainV2.comPort.setWPCurrent(0); // set nav to
            }
            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }

            MainTabCall(0, "FlightData");
        }

        public void ClearTrackClick()
        {
            ss.SpeakAsync("CLEAR TRACK");
        }

        public void StartMission_Click()
        {
            ss.SpeakAsync("START THE MISSION");
            try
            {
                MainV2.comPort.doCommand((MAVLink.MAV_CMD)Enum.Parse(typeof(MAVLink.MAV_CMD), "MISSION_START"), 0, 0, 1, 0, 0, 0, 0);
            }
            catch { CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR); }

            MainTabCall(0, "FlightData");
        }

        public void SetModeClick(string mode, string pronounce = null)
        {
            if (pronounce != null)
                ss.SpeakAsync(mode.ToString() + "MODE");
            comPort.setMode(mode);
            MainTabCall(0, "FlightData");
        }

        MissionPlanner.GCSViews.FlightData FD_Mapping = new GCSViews.FlightData();

        public void TakeoffSpeech(string alt)
        {
            if (MainV2.comPort.BaseStream.IsOpen)
            {

                //string alt = "100";

                //if (MainV2.comPort.MAV.cs.firmware == MainV2.Firmwares.ArduCopter2)
                //{
                //    alt = (10 * CurrentState.multiplierdist).ToString("0");
                //}
                //else
                //{
                //    alt = (100 * CurrentState.multiplierdist).ToString("0");
                //}

                //if (MainV2.config.ContainsKey("guided_alt"))
                //    alt = MainV2.config["guided_alt"].ToString();

                MainV2.config["guided_alt"] = alt;

                //int intalt = (int)(100 * CurrentState.multiplierdist);
                int intalt;
                if (!int.TryParse(alt, out intalt))
                {
                    CustomMessageBox.Show("Bad Alt");
                    return;
                }

                MainV2.comPort.MAV.GuidedMode.z = intalt / CurrentState.multiplierdist;

                if (MainV2.comPort.MAV.cs.mode == "Guided")
                {
                    MainV2.comPort.setGuidedModeWP(new Locationwp() { alt = (float)MainV2.comPort.MAV.GuidedMode.z, lat = MainV2.comPort.MAV.GuidedMode.x, lng = MainV2.comPort.MAV.GuidedMode.y });
                }

                MainV2.comPort.setMode("GUIDED");

                try
                {
                    MainV2.comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, MainV2.comPort.MAV.GuidedMode.z);
                }
                catch
                {
                    CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
                }
            }
        }

        private void MovementCmnd()
        {
            Locationwp gotohere = new Locationwp();

            try
            {
                comPort.setGuidedModeWP(gotohere);
            }
            catch (Exception ex) { MainV2.comPort.giveComport = false; CustomMessageBox.Show(Strings.CommandFailed + ex.Message, Strings.ERROR); }
        }


        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}