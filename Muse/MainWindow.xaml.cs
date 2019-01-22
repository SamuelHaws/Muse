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
using System.Windows.Threading;
using System.Timers;


namespace Muse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int j; //iteration var for sheet
        int numnotes; //user input: number of notes in sheet
        int millisec; //user input: number of milliseconds in timer delay (between notes played)
        bool halt = false; //set to true to stop the play
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        List<int> sheet = new List<int>(); //the list to be populated with notes




        public void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        public void play(List<int> sheet)
        {
            if (!halt)
            {

                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(millisec);
                dispatcherTimer.Start();
            }
            else return;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (j >= numnotes || halt)
            {
                dispatcherTimer.Stop();
                return;
            }
            else
            {
                NoteGenerator.notePlayer(sheet[j]);
                j++;
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            numnotes = Int32.Parse(textBox1.Text); //take in user input for numnotes from textBox1
            millisec = Int32.Parse(textBox1_Copy.Text); //take in user input for millisec from textBox1_Copy
            sheet = NoteGenerator.creator(comboBox.Text,numnotes); 
            j = 0; 
            textBox.Text = String.Join(Environment.NewLine, sheet);


            //this.button.Click += new RoutedEventHandler(button_Click);
            //read through sheet and play each note

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            halt = false;
            play(sheet);
            this.button1.Click += new RoutedEventHandler(button1_Click);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            halt = true;
            dispatcherTimer.Stop();
            this.button2.Click += new RoutedEventHandler(button2_Click);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
