using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.IO;
using System.Xml; // config file
using System.Runtime.InteropServices; // dll imports
using log4net;
using ZedGraph; // Graphs
using MissionPlanner;
using System.Reflection;
using MissionPlanner.Controls;
using System.Drawing.Drawing2D;
using MissionPlanner.HIL;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using MissionPlanner.Utilities;



// Written by Michael Oborne
namespace MissionPlanner.GCSViews
{
    public partial class Speech1: MyUserControl
    {
       

        public Speech1()
        {
            InitializeComponent();
        }

        private void Speech1_Load(object sender, EventArgs e)
        {
            
        }
        private SpeechRecognitionEngine _recognizer;
        SpeechSynthesizer ss = new SpeechSynthesizer();

        public void Activate()
        {

        }

        ////private void myButton2_Click(object sender, EventArgs e)
        ////{
        ////  //  Form parentForm = (this.Parent as Form);

        ////    Control[] c = this.ParentForm.Controls.Find("MainMenu", true );
        ////    System.Windows.Forms.MenuStrip ms = (System.Windows.Forms.MenuStrip)c[0];

        ////    ms.Items[0].Enabled = false;
        ////    ms.Items[1].Enabled = false;
        ////    ms.Items[2].Enabled = false;
        ////    ms.Items[3].Enabled = false;
        ////    ms.Items[4].Enabled = false;
        ////    ms.Items[5].Enabled = false;
        ////    ms.Items[6].Enabled = false;
        ////   // ms.Items[7].Enabled = false;
        //// //   System.Windows.Forms.Control[] btn1 = this.Parent.Controls.Find("MenuFlightData", true);
        ////  //  if(btn1.Length>0)
        ////  //  {
        ////    //    btn1[0].Enabled = false;
        ////   // }
            
        ////    if (myButton2.Enabled == true)
        ////    {
        //        _recognizer = new SpeechRecognitionEngine();
        //        _recognizer.SetInputToDefaultAudioDevice();

        //        Choices choices = new Choices("FlightData", "FlightPlanner", "HWConfig","SWConfig","Simulation","Terminal","Help");

        //        GrammarBuilder grBuilder = new GrammarBuilder(choices);
        //        Grammar grammar = new Grammar(grBuilder);

        //        _recognizer.LoadGrammar(grammar);
        //        _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
        //        _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        ////    }
        ////}

        //void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    foreach (RecognizedWordUnit word in e.Result.Words)
        //    {

        //        switch (word.Text)
        //        {
        //            case "FlightData":
        //                ss.SpeakAsync("FlightData");
        //                //  if (word.Text == "FlightData")
        //                //     {
        //                //   MenuFlightData.Enabled=true;
        //                //    button1.Enabled = true;
        //                //     button2.Enabled = false;
        //                //    button3.Enabled = false;
        //                //     button4.Enabled = false;
        //                //     button5.Enabled = false;
        //                //      }
        //                //  MoveLeft();


        //                //MainV2.ControlCollection x;//= new Form.ControlCollection(MainV2);// MainV2.ControlCollection();
                        
        //                //Control[] c = MainV2.ActiveForm.ParentForm.Controls.Find("MainMenu",true); //this.ParentForm.Controls.Find("MainMenu", true );
                        
                        
        //   // System.Windows.Forms.MenuStrip ms = (System.Windows.Forms.MenuStrip)c[0];

        //    //ms.Items[0].Enabled = true;
        //    //ms.Items[1].Enabled = false;
        //    //ms.Items[2].Enabled = false;
        //    //ms.Items[3].Enabled = false;
        //    //ms.Items[4].Enabled = false;
        //    //ms.Items[5].Enabled = false;
        //    //ms.Items[6].Enabled = false;
        //    //MyView.ShowScreen("FlightData");
        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("FlightData");
        //                break;

        //            case "FlightPlanner":
        //                ss.SpeakAsync("FlightPlanner");
        //                 MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                 MainV2.instance.MainMenuStrip.Items[1].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("FlightPlanner");
        //                break;

        //            case "HWConfig":
        //                ss.SpeakAsync("HWConfig");
                       
        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("HWConfig");
        //                break;

        //            case "SWConfig":
        //                ss.SpeakAsync("SWConfig");
                        
        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                 MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("SWConfig");
        //                break;

        //            case "Simulation":
        //                ss.SpeakAsync("Simulation");

        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("Simulation");
        //                break;


        //            case "Terminal":
        //                ss.SpeakAsync("Terminal");

        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = true;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = false;
        //                MainV2.View.ShowScreen("Terminal");
        //                break;

        //            case "Help":
        //                ss.SpeakAsync("Help");

        //                MainV2.instance.MainMenuStrip.Items[0].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[1].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[2].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[3].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[4].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[5].Enabled = false;
        //                MainV2.instance.MainMenuStrip.Items[6].Enabled = true;
        //                MainV2.View.ShowScreen("Help");
        //                break;

        //            default:
        //                break;
        //        }
        //        myLabel2.Text += word.Text;
        //    }
        //}
      
    }
}
