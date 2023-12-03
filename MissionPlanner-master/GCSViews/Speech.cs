using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;

namespace MissionPlanner.GCSViews
{
    public partial class Speech : UserControl,IActivate
    {
        public Speech()
        {
            InitializeComponent();
        }

        private SpeechRecognitionEngine _recognizer;
        SpeechSynthesizer ss = new SpeechSynthesizer();

        public void Activate()
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            if (myButton1.Enabled == true)
            {
                _recognizer = new SpeechRecognitionEngine();
                _recognizer.SetInputToDefaultAudioDevice();

                Choices choices = new Choices("FlightData", "FlightPlan", "Config", "InitialSetup");

                GrammarBuilder grBuilder = new GrammarBuilder(choices);
                Grammar grammar = new Grammar(grBuilder);

                _recognizer.LoadGrammar(grammar);
                _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit word in e.Result.Words)
            {

                switch (word.Text)
                {
                    case "FlightData":
                        ss.SpeakAsync("FlightData");
               //  if (word.Text == "FlightData")
                  //     {
                  //   MenuFlightData.Enabled=true;
                        //    button1.Enabled = true;
                       //     button2.Enabled = false;
                        //    button3.Enabled = false;
                       //     button4.Enabled = false;
                       //     button5.Enabled = false;
                  //      }
                        //  MoveLeft();

                        break;

                    case "FlightPlan":
                        ss.SpeakAsync("FlightPlan");
                     //   if (word.Text == "FlightPlan")
                     //   {
                    //        button2.Enabled = true;
                   //         button1.Enabled = false;
                      //      button3.Enabled = false;
                        //    button4.Enabled = false;
                      //      button5.Enabled = false;
                      //  }
                        // MoveRight();
                        break;
                    case "Config":
                        ss.SpeakAsync("Config");
                      //  if (word.Text == "Config")
                      //  {
                      //      button4.Enabled = true;
                      //      button1.Enabled = false;
                      //      button2.Enabled = false;
                        //    button3.Enabled = false;
                          //  button5.Enabled = false;
                      //  }
                        //MoveTop();
                        break;

                    case "InitialSetup":
                        ss.SpeakAsync("InitialSetup");
                     //   if (word.Text == "InitialSetup")
                    //    {
                    //        button5.Enabled = true;
                    //        button1.Enabled = false;
                    //        button2.Enabled = false;
                   //         button3.Enabled = false;
                     //       button4.Enabled = false;
                     //   }
                        // MoveDown();
                        break;

                    default:
                        break;
                }
                richTextBox1.Text += word.Text + Environment.NewLine;
            }
        }

        private void Speech_Load(object sender, EventArgs e)
        {

        }
    }
}
