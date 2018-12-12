using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viergewinnt
{
    public partial class Form1 : Form
    {

        bool state_blue;
        //erstelle Array
        int[,] gameboard = new int[7, 6];

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.KeyPreview = true;

            for (int x = 0; x <= 6; x++)
                {
                for (int y = 0; y <= 5; y++)
                    {
                    //bt_0_0.Enabled = false;
                    gameboard[x, y] = 0;
                    string buttonName = "bt_" + x + "_" + y;
                    Controls[buttonName].Text = "";

                    textBox1.BackColor = System.Drawing.Color.Blue;
                    textBox1.Text = "Blau ist an der Reihe.";

                    //bt_0_0.Text = gameboard[x, y].ToString();
                    //buttons[x,y].Text = x.ToString() + '_' + y.ToString();

                }
                }
            //erstelle globale Variable wer dran ist (blau / rot)
            state_blue = true;


        }

        void sortinarrayandsetbuttoncolor(int spalte)
        {
            //übernimm die Spalte und speichere die farbe im array slot (wenn voll disable button)
            //färbe den passenden Button
            //checkwin()
            int y;
            for (y = 0; y <= 5; y++)
                {
                if (gameboard[spalte, y] == 0 & y <= 5) break;
                }

            if (y == 5) //(wenn voll disable button)
            {
                string buttonName = "bt_s" + spalte;
                Controls[buttonName].Enabled = false;
                }

            if (state_blue == true)
                {
                gameboard[spalte, y] = 1;
                string buttonName = "bt_" + spalte + "_" + y;
                //Controls[buttonName].Text = "blau";
                Controls[buttonName].BackColor = System.Drawing.Color.Blue;
                }
            else
                {
                gameboard[spalte, y] = 2;
                string buttonName = "bt_" + spalte + "_" + y;
                //Controls[buttonName].Text = "rot";
                Controls[buttonName].BackColor = System.Drawing.Color.Red;
                }

            checkwin(spalte,y);

        }

        void checkwin(int x, int y)
        {
            //gehe aneinanerhängende farben horizontal diagonal und senkrecht durch ( x +- 3 | y +-3 | x&y +-3 | x&-y +- 3)
            // check patterns 1 -> 4
            
            bool win = false;

            int wincondi;
            if (state_blue == true)
                {
                wincondi = 1;
                }
            else
                {
                wincondi = 2;
                }
                

            win = pat1(x, y,wincondi);
            if (!win) { win = pat2(x, y, wincondi); }
            if (!win) { win = pat3(x, y, wincondi); }
            if (!win) { win = pat4(x, y, wincondi); }

            //wenn win dann popup fenster oder sonstwas     
            if (win == true)
            {
                string winner;
                string loser;
                if (state_blue == true)
                    {
                    winner = "Blau";
                    loser = "Rot";
                    }
                else
                    {
                    winner = "Rot";
                    loser = "Blau";
                    }

                this.KeyPreview = false;
                MessageBox.Show(winner + " hat gewonnen, " + loser + " ist ein Loooooooooooooser^^");

                for (int i = 0; i <= 6; i++)
                {
                    //bt_si.Enabled = false;
                    string buttonName = "bt_s" + i;
                    Controls[buttonName].Enabled = false;
                }

                }
            else //wenn nicht dann ändere spielfarbe
            {
                if (state_blue == true)
                    {
                    state_blue = false;
                    textBox1.BackColor = System.Drawing.Color.Red;
                    textBox1.Text = "Rot ist an der Reihe.";
                    }
                    else
                    {
                    state_blue = true;
                    textBox1.BackColor = System.Drawing.Color.Blue;
                    textBox1.Text = "Blau ist an der Reihe.";
                    }
            }
            

        }

        Boolean pat1(int x, int y, int wincondi)
        {
            bool win = false;
            int samecolorinarow = 1;

            int xtemp = x;
            int ytemp = y;
            //checkleft
            while (xtemp > 0)
                {
                    if(gameboard[xtemp-1,ytemp] == wincondi)
                        {
                        samecolorinarow += 1;
                        }
                    else
                        {
                        break;
                        }
                xtemp -= 1;
                }
            //checkright
            xtemp = x;
            while (xtemp < 6)
            {
                if (gameboard[xtemp+1, ytemp] == wincondi)
                    {
                    samecolorinarow += 1;
                    }
                else
                    {
                    break;
                    }
                xtemp += 1;
            }

            if (samecolorinarow >= 4)
                {
                win = true;
                }

            if (win == true)
                return (true);
            else
                return (false);
        }

        Boolean pat2(int x, int y, int wincondi)
        {
            bool win = false;
            int samecolorinarow = 1;

            int xtemp = x;
            int ytemp = y;
            //checkdown
            while (ytemp > 0)
            {
                if (gameboard[xtemp, ytemp-1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp -= 1;
            }
            //checkup
            ytemp = y;
            while (ytemp < 5)
            {
                if (gameboard[xtemp, ytemp + 1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp += 1;
            }

            if (samecolorinarow >= 4)
            {
                win = true;
            }

            if (win == true)
                return (true);
            else
                return (false);
        }

        Boolean pat3(int x, int y, int wincondi)
        {
            bool win = false;
            int samecolorinarow = 1;

            int xtemp = x;
            int ytemp = y;
            //checkleftdown
            while ((ytemp > 0) & (xtemp > 0))
            {
                if (gameboard[xtemp - 1, ytemp - 1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp -= 1;
                xtemp -= 1;
            }
            //checkrightup
            xtemp = x;
            ytemp = y;
            while ((ytemp < 5) & (xtemp < 6))
            {
                if (gameboard[xtemp + 1, ytemp + 1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp += 1;
                xtemp += 1;
            }

            if (samecolorinarow >= 4)
            {
                win = true;
            }

            if (win == true)
                return (true);
            else
                return (false);
        }

        Boolean pat4(int x, int y, int wincondi)
        {
            bool win = false;
            int samecolorinarow = 1;

            int xtemp = x;
            int ytemp = y;
            //checkleftup
            while ((ytemp < 5) & (xtemp > 0))
            {
                if (gameboard[xtemp - 1, ytemp + 1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp += 1;
                xtemp -= 1;
            }
            //checkrightdown
            xtemp = x;
            ytemp = y;
            while ((ytemp > 0) & (xtemp < 6))
            {
                if (gameboard[xtemp + 1, ytemp - 1] == wincondi)
                {
                    samecolorinarow += 1;
                }
                else
                {
                    break;
                }
                ytemp -= 1;
                xtemp += 1;
            }

            if (samecolorinarow >= 4)
            {
                win = true;
            }

            if (win == true)
                return (true);
            else
                return (false);
        }

        private void bt_s0_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(0);
        }

        private void bt_s1_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(1);
        }

        private void bt_s2_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(2);
        }

        private void bt_s3_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(3);
        }

        private void bt_s4_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(4);
        }

        private void bt_s5_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(5);
        }

        private void bt_s6_Click(object sender, EventArgs e)
        {
            sortinarrayandsetbuttoncolor(6);
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.D1:
                    this.bt_s0_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D2:
                    this.bt_s1_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D3:
                    this.bt_s2_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D4:
                    this.bt_s3_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D5:
                    this.bt_s4_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D6:
                    this.bt_s5_Click(this, null);
                    e.Handled = true;
                    break;

                case Keys.D7:
                    this.bt_s6_Click(this, null);
                    e.Handled = true;
                    break;
                    
            }

        }

    }
}
