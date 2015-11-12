using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace HighLowGame
{
    public partial class Form1 : Form
    {
        //Random class and global variables
        Random rnd = new Random();
        int randomnum;
        int wGuess;
        int seconds;

        public Form1()
        {
            //Initialize the game
            InitializeComponent();
            
            //Labels text
            label1.Text = "Guess the number!";
            label2.Text = "Choose your range.";
            label3.Text = "Wrong guesses: " + wGuess;
            label5.Text = "0:" + seconds;

            //Buttons properties
            button1.Enabled = false;
            button2.Visible = false;   
        }

        //Perform the following when the user clicks on button1
        private void button1_Click(object sender, EventArgs e)
        {
            //If the random number is the same as user input
            if (Convert.ToInt32(textBox1.Text) == randomnum)
            {
                label1.Text = "Correct!";
                button1.Visible = false;
                textBox1.Enabled = false;
                label1.Visible = false;
                Thread.Sleep(50);
                label1.Visible = true;

                button2.Visible = true;

                SoundPlayer applause = new SoundPlayer(@"C:\Users\Duha\Music\applause-8.wav");
                applause.Play();

                this.BackColor = System.Drawing.Color.Green;

                timer1.Stop();
            }
            //Else if the random number is LOWER than the user input
            else if(Convert.ToInt32(textBox1.Text) > randomnum)
            {
                label1.Text = "Lower";
                label1.Visible = false;
                Thread.Sleep(50);
                label1.Visible = true;
                wGuess++;
                label3.Text = "Wrong guesses: " + wGuess;

                SoundPlayer low = new SoundPlayer(@"C:\Users\Duha\Music\beep4.wav");
                low.Play();

                this.BackColor = System.Drawing.Color.Red;
            }
            //Else if the random number is HIGHER than the user input
            else if (Convert.ToInt32(textBox1.Text) < randomnum)
            {
                label1.Text = "Higher";
                label1.Visible = false;
                Thread.Sleep(50);
                label1.Visible = true;
                wGuess++;
                label3.Text = "Wrong guesses: " + wGuess;

                SoundPlayer high = new SoundPlayer(@"C:\Users\Duha\Music\beep1.wav");
                high.Play();

                this.BackColor = System.Drawing.Color.Blue;
            }
            //If the user enters 420 (Easter egg)
            if (Convert.ToInt32(textBox1.Text) == 420)
            {
                SoundPlayer high = new SoundPlayer(@"C:\Users\Duha\Music\hitsound.wav");
                high.Play();
            }
        }

        //This method makes sure the user can press enter and cannot enter
        //Anything other than numbers
        //The user will not be able to enter more than 4 numbers
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.MaxLength = 4;
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                button1.PerformClick();
            }
        }
        //When the user clicks on button2, the application will restart
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //The user can choose the range between 10/100/1000
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the user chooses 10, the random number will be between 1 and 10
            //The user will get 10 seconds to guess the random number
            if (comboBox1.SelectedItem.ToString() == "10")
            {
                randomnum = rnd.Next(1, 10);
                button1.Enabled = true;

                seconds = 10;
                timer1.Start();
                label5.Text = "0:" + seconds;
            }

            //If the user chooses 100, the random number will be between 1 and 100
            //The user will get 15 seconds to guess the random number
            if (comboBox1.SelectedItem.ToString() == "100")
            {
                randomnum = rnd.Next(1, 100);
                button1.Enabled = true;

                seconds = 15;
                timer1.Start();
                label5.Text = "0:" + seconds;
            }
            //If the user chooses 1000, the random number will be between 1 and 1000
            // The user will get 20 seconds to guess the random number
            if (comboBox1.SelectedItem.ToString() == "1000")
            {
                randomnum = rnd.Next(1, 1000);
                button1.Enabled = true;

                seconds = 20;
                timer1.Start();
                label5.Text = "0:" + seconds;
            }
        }

        //The timer counts down and when it hits 0, a messagebox will show and 
        //the game will restart.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--;
                label5.Text = "0:" + seconds;
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                Application.Restart();
            }
        }
    }
}