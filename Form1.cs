using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Game
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string> {"!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z" };

        //to reference variables to keep track of two labels clicked 
        Label FirstClicked = null;
        Label Secondlicked = null;

        private void AssignIconsToLabels()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
             
                Label label = control as Label;
                if(label != null)
                {
                    // create random number which maximum or upper limit is the number of icons
                    int RandomNumber = random.Next(icons.Count);
                    //Shuffle the icons each time
                    label.Text = icons[RandomNumber];

                    //hide the icons from the user
                    label.ForeColor = label.BackColor;

                    //remove the icon u choose 
                    icons.RemoveAt(RandomNumber);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            AssignIconsToLabels();
        }

        private void label_click(object sender, EventArgs e)
        {

        }

        private void label_clicked(object sender, EventArgs e)
        {


            // if timer is running means that there are tw Un-matching pairs , so ignore any clicks
            if (timer1.Enabled == true)
                return;



            Label label = sender as Label;

            if (label != null)
            {
                if (label.ForeColor == Color.Black)
                    return;
                
                //means one of two things 
                //1- this is the first play in the game , so this is the first click 
                //2- there are two labels that matched each other , so make these variable null again
                if(FirstClicked == null)
                {
                    FirstClicked = label;
                    FirstClicked.ForeColor = Color.Black;
                    return; 
                }


                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // Set its color to black
                Secondlicked = label;
                Secondlicked.ForeColor = Color.Black;

                // Check to see if the player won
                CheckForWinner();

                // If the player clicked two matching icons, keep them 
                // black and reset firstClicked and secondClicked 
                // so the player can click another icon
                if (FirstClicked.Text == Secondlicked.Text)
                {
                    FirstClicked = null;
                    Secondlicked = null;
                    return;
                }


                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                timer1.Start();



            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this function executed every 750 ms if the two pairs are not matching  

            //make sure that the timer is not running 
            timer1.Stop();

            //hide two labels 
            FirstClicked.ForeColor = FirstClicked.BackColor;
            Secondlicked.ForeColor = Secondlicked.BackColor;

            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            FirstClicked = null;
            Secondlicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls )
            {
                Label icon = control as Label;
                
                if(icon != null)
                {
                    if (icon.ForeColor == icon.BackColor)
                        return;
                }


            }

            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
