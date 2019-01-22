using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;

/*
 
 Muse Music Generator
 
 Author:  Samuel Haws
 Version: 1.0
 Date: March 2017

 Muse Music Generator creates "sheet music" in the form of a list (possibly
 of user-defined length). 
 Each note is represented by an integer value corresponding to an interval
 in the given key. After the melody is created in a list, the program then
 traverses the list. Each note has a corresponding audio file that is played
 as the list is traversed. 

 Muse uses a 0-12 semitone interval system, with 0 representing the root
 note of the key, all the way up to 12, which represents an octave. 
 The probability of binding the next note being generated in the list to 
 a particular value is shown below.

 Integer: 0   1    2   3    4    5    6    7   8   9    10    11   12
 Note(C): C   C#   D   D#   E    F    F#   G   G#  A    A#    B    C

 Relative Probility* of Major Notes (for now)
 0: 2							7:  4
 1: 0        					8:  0
 2: 2							9:  2
 3: 0                           10: 0
 4: 3							11: 1
 5: 4							12: 2
 6: 0

 *note: As of version 1.0, the probability of creating a given note is independent
        of what note was justed played, i.e. 0->5 has the same probability
        as 12->5. I would consider making the creation dependent going forward.

 An example of a melody (C):
 {0, 2, 5, 4, 7}
  C  D  F  E  G

*/


namespace Muse
{
    class NoteGenerator
    {

        public static readonly Random majorrnd = new Random();


        public static int probab(string key) //generates a single note based off defined probability
        {
            if (key == "C major")
            {
                int noteNum; 
                int relpr = majorrnd.Next(0, 19); // relpr == relative probability, a way of assigning frequency of generation of particular note
                if (relpr >= 0 && relpr <= 1) { noteNum = 0; }
                else if (relpr >= 2 && relpr <= 3) { noteNum = 2; }
                else if (relpr >= 4 && relpr <= 6) { noteNum = 4; }
                else if (relpr >= 7 && relpr <= 10) { noteNum = 5; }
                else if (relpr >= 11 && relpr <= 14) { noteNum = 7; }
                else if (relpr >= 15 && relpr <= 16) { noteNum = 9; }
                else if (relpr == 17) { noteNum = 11; }
                else if (relpr >= 18 && relpr <= 19) { noteNum = 12; }
                else noteNum = 0;
                return noteNum;
            }
            else if (key == "C minor")
            {
                int noteNum; 
                int relpr = majorrnd.Next(0, 19); // relpr == relative probability, a way of assigning frequency of generation of particular note
                if (relpr >= 0 && relpr <= 1) { noteNum = 0; }
                else if (relpr >= 2 && relpr <= 3) { noteNum = 2; }
                else if (relpr >= 4 && relpr <= 6) { noteNum = 3; }
                else if (relpr >= 7 && relpr <= 10) { noteNum = 5; }
                else if (relpr >= 11 && relpr <= 14) { noteNum = 7; }
                else if (relpr >= 15 && relpr <= 16) { noteNum = 8; }
                else if (relpr == 17) { noteNum = 10; }
                else if (relpr >= 18 && relpr <= 19) { noteNum = 12; }
                else noteNum = 0;
                return noteNum;
            }
            else return 0;
        }
        /*
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0,0,1);
        dispatcherTimer.Start();

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {}
        */
        public static List<int> creator(string key,int num)
        {
            List<int> sheet = new List<int>();
            for (int i = 0; i < num; i++)
            {
                sheet.Add(probab(key));
            }
            return sheet;
        }


        public static void notePlayer(int note)
        {
            var path = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            Debug.WriteLine("Executing path: " + path);
            if (note == 0) { PlaySound(@"..\..\..\sounds\note_C_0.wav"); }
            else if (note == 1) { PlaySound(@"..\..\..\sounds\note_C#_1.wav"); }
            else if (note == 2) { PlaySound(@"..\..\..\sounds\note_D_2.wav"); }
            else if (note == 3) { PlaySound(@"..\..\..\sounds\note_D#_3.wav"); }
            else if (note == 4) { PlaySound(@"..\..\..\sounds\note_E_4.wav"); }
            else if (note == 5) { PlaySound(@"..\..\..\sounds\note_F_5.wav"); }
            else if (note == 6) { PlaySound(@"..\..\..\sounds\note_F#_6.wav"); }
            else if (note == 7) { PlaySound(@"..\..\..\sounds\note_G_7.wav"); }
            else if (note == 8) { PlaySound(@"..\..\..\sounds\note_G#_8.wav"); }
            else if (note == 9) { PlaySound(@"..\..\..\sounds\note_A_9.wav"); }
            else if (note == 10) { PlaySound(@"..\..\..\sounds\note_A#_10.wav"); }
            else if (note == 11) { PlaySound(@"..\..\..\sounds\note_B_11.wav"); }
            else if (note == 12) { PlaySound(@"..\..\..\sounds\note_C_12.wav"); }
        }
   
        private static void PlaySound(string path)
        {
            System.Media.SoundPlayer player =
                new System.Media.SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }
    }
}
