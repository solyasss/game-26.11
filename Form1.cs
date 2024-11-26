using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace hw
{
    public partial class Form1 : Form
    {
        private List<int> numbers;
        private int current_number;
        private Timer game_timer;
        private int time_left;
        private Button[] b;

        public Form1()
        {
            initialize_comp();
            intialize_buttons();
            initialize_game();
        }

        private void intialize_buttons()
        {
            b = new Button[]
            {
                b0, b1, b2, b3,
                b4, b5, b6, b7,
                b8, b9, b10, b11,
                b12, b13, b14, b15
            };

            foreach (Button button in b)
            {
                button.Click += new EventHandler(Button_Click);
            }
        }

        private void initialize_game()
        {
            numbers = new List<int>();
            current_number = int.MinValue;
            time_left = 60;

            game_timer = new Timer();
            game_timer.Interval = 1000;
            game_timer.Tick += GameTimer_Tick;

            set_button();
        }

        private void set_button()
        {
            Random rnd = new Random();
            numbers = Enumerable.Range(0, 101).OrderBy(x => rnd.Next()).Take(16).ToList();

            int index = 0;
            foreach (Button button in b)
            {
                button.Text = numbers[index].ToString();
                button.Enabled = true;
                index++;
            }

            current_number = numbers.Min();
        }

        private void start_game()
        {
            nl.Items.Clear();
            current_number = numbers.Min();
            time_left = 60;
            progress_bar.Value = 0;
            label.Text = $"Time: {time_left} sec";

            game_timer.Start();
            set_button();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            time_left--;
            progress_bar.Value = progress_bar.Maximum - time_left;
            label.Text = $"Time: {time_left} sec";

            if (time_left <= 0)
            {
                game_timer.Stop();
                MessageBox.Show("Time is up! You lose!", "Game ended");
                disable_buttons();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int number = int.Parse(button.Text);

            if (number == current_number)
            {
                nl.Items.Add(number);
                button.Enabled = false;

                numbers.Remove(number);
                if (numbers.Count > 0)
                {
                    current_number = numbers.Min();
                }
                else
                {
                    game_timer.Stop();
                    MessageBox.Show("Congratulations! You win!", "Game ended");
                    disable_buttons();
                }
            }
        }

        private void disable_buttons()
        {
            foreach (Button button in b)
            {
                button.Enabled = false;
            }
        }

        private void button_game_Click(object sender, EventArgs e)
        {
            start_game();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}