//made by AradHSamiee
//asam235711@yahoo.com


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//imported
using System.Collections;

namespace AI_SlideNumber_Puzzle_V003
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int minimumvalue = 0; //to store minimum values anytime needed
        int minimumtemp  = 0; //a  help  storage for    minimumvalue
        int minmin = 0;       //recently added - apparantly, we need the first two least nv points to proceed in case of there is a collision with recentaction
        int choice;           //recently added - to choose between minimumtemp or minmin

        int ci, cj; //current i, j
        int ip, jp; // i, j  prime
        int nv; // negative values
        int pv; // positive values //doesn't matter actually (^_^(( 😝

        int[,] puzzle = new int[3, 3];
        //the fake generated matrix
        int[] inputseries = new int[9] { 8, 7, 3, 2, 4, 1, 6, 5, 0 };
        //the ideal  form of matrix
        int[,] ideal = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
        //array to compare values
        int[] nvtemp = new int[4];

        // added recently
        /* storing the opposite of latest decision made :: to stop it happening later */
        int recentaction; /* UP = 0, DOWN = 2, LEFT = 1, RIGHT = 3 ==> raUP = 2 (ra:recentaction) raDown = 0, raLEFT = 3, raRIGHT = 1 */
        int moves = 0;

        public void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    puzzle[i, j] = 0;
                }
            ShowSteps();
        }

        public void ShowSteps()
        {
            string s = "";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[i, j] != 0)
                        s += puzzle[i, j].ToString() + " | ";
                    else s += "   | ";
                }
                listBox1.Items.Add(s);
                s = "";
                listBox1.Items.Add(s);
            }
            listBox1.Items.Add("=====================");
        }

        public int checkpuzzle()
        {
            nv = 0;
            pv = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[i, j] == ideal[i, j])
                        pv++;
                    else nv++;
                }
            }
            return nv;
        }

        public void blockswap(int newpositioni, int newpositionj, int currentpositioni, int currentpositionj)
        {
            /*
            MessageBox.Show("swaping initialized!");*/
            puzzle[currentpositioni, currentpositionj] = puzzle[newpositioni, newpositionj];
            puzzle[newpositioni, newpositionj] = 0;
            /*
            MessageBox.Show("swaped");*/
        }

        public void unswap(int newpositioni, int newpositionj, int currentpositioni, int currentpositionj)
        {
            puzzle[newpositioni, newpositionj] = puzzle[currentpositioni, currentpositionj];
            puzzle[currentpositioni, currentpositionj] = 0;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        //-------------------------------------------------------------------------

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(recentaction.ToString());
            recentaction = 5;
            
            MessageBox.Show(recentaction.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------

        
        public void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            //filling the puzzle
            int m = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    puzzle[i, j] = inputseries[m]; //also provides the option to fill the matrix with user's inputs
                    m++;
                } ShowSteps(); //shows it

            //testing
            checkpuzzle();
            label3.Text = nv.ToString();

            while (nv != 0)
            {

                //STEP1
                //searching for the state we're currently in
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (puzzle[i, j] == 0)
                        {
                            ci = i;
                            cj = j; //saving current
                        }
                    }
                } //first step done



                //---------------------------------------------
                //filling the whole nvtemp[] array with a large number every time to make sure we don't fall into traps (comparing traps)

                for (int i = 0; i < 4; i++)
                    nvtemp[i] = 10;

                //---------------------------------------------



                //STEP2 - STEP3 - STEP4
                //2.movement in four directions
                //3.swap gets done
                //4.stores every nv from every direction into nvtemp[] to compare'em later on

                //Up
                if (ci >= 1)
                {
                    ip = ci;
                    jp = cj;
                    ip--;
                    /*
                    MessageBox.Show("UP" + ip.ToString() + jp.ToString() + ci.ToString() + cj.ToString());*/
                    blockswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for first time");*/
                    nvtemp[0] = checkpuzzle();
                    /*
                    MessageBox.Show("UP DONE! nv[0] = " + nvtemp[0]);
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                    //unswap
                    unswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for second time");
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                }
                else nvtemp[0] = 10;

                //Left
                if (cj >= 1)
                {
                    ip = ci;
                    jp = cj;
                    jp--;
                    /*
                    MessageBox.Show("Left" + ip.ToString() + jp.ToString() + ci.ToString() + cj.ToString());*/
                    blockswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for first time");*/
                    nvtemp[1] = checkpuzzle();
                    /*
                    MessageBox.Show("Left DONE! nv[1] = " + nvtemp[1]);
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                    //unswap
                    unswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for second time");
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                }
                else nvtemp[1] = 10;


                //Down
                if (ci <= 1)
                {
                    ip = ci;
                    jp = cj;
                    ip++;
                    /*
                    MessageBox.Show("Down" + ip.ToString() + jp.ToString() + ci.ToString() + cj.ToString());*/
                    blockswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for first time");*/
                    nvtemp[2] = checkpuzzle();
                    /*
                    MessageBox.Show("Down DONE! nv[2] = " + nvtemp[2]);*/
                    //unswap
                    unswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for second time");
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                }
                else nvtemp[2] = 10;

                //Right
                if (cj <= 1)
                {
                    ip = ci;
                    jp = cj;
                    jp++;
                    /*
                    MessageBox.Show("Right" + ip.ToString() + jp.ToString() + ci.ToString() + cj.ToString());
                    */
                    blockswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for first time");
                    */
                    nvtemp[3] = checkpuzzle();
                    /*
                    MessageBox.Show("Right DONE! nv[3] = " + nvtemp[3]);
                    */
                    //unswap
                    unswap(ip, jp, ci, cj);
                    /*
                    MessageBox.Show("Swaped for second time");
                    MessageBox.Show(puzzle[0, 0].ToString() + puzzle[0, 1].ToString() + puzzle[0, 2].ToString() + puzzle[1, 0].ToString() + puzzle[1, 1].ToString() + puzzle[1, 2].ToString() + puzzle[2, 0].ToString() + puzzle[2, 1].ToString() + puzzle[2, 2].ToString());
                    */
                }
                else nvtemp[3] = 10;

                //message
                
                MessageBox.Show("[" + nvtemp[0].ToString() + nvtemp[1].ToString() + nvtemp[2].ToString() + nvtemp[3].ToString() + "]");



                //*** comparision ***
                /*
                MessageBox.Show("compare initialized!");*/
                MessageBox.Show("nvtemp [*]: " + nvtemp[0] + ", " + nvtemp[1] + ", " + nvtemp[2] + ", " + nvtemp[3]);
                minimumvalue = 11;
                minmin = 11;
                for (int i = 0; i < 4; i++)
                {
                    if (nvtemp[i] < minimumvalue)
                    {
                        minimumvalue = nvtemp[i];
                        minimumtemp = i; // direction found
                        
                        MessageBox.Show("minval: " + minimumvalue + " | mintemp: " + minimumtemp);
                    }

                    /* WRONG THINKING
                    // recently added
                    Array.Sort(nvtemp);
                    minimumtemp = 0;
                    minmin = 1;
                    */

                    /*
                    MessageBox.Show("min value = " + minimumvalue);
                    MessageBox.Show("min temp  = " + minimumtemp);
                    */
                }

                // recently added
                // two steps to choose from
                nvtemp[minimumtemp] = 11; //kinda removing the minimum value from second comparison
                for (int i = 0; i < 4; i++)
                {
                    if (nvtemp[i] <= minimumvalue)
                    {
                        /*
                        minmin = nvtemp[i]; //? :DD donno why
                        */
                        minmin = i;
                        MessageBox.Show("minmin: " + minmin);
                    }
                    continue;
                }

                /*
                MessageBox.Show("compare is done!");*/


                //////     EEEEEE     CCCC  IIIIII  SSSSSS  IIIIII     ////    NNNN   NN
                //   //    EE       CC        ||    SS        ||     //   //   NN NN  NN
                //    //   EEEEEE  CC         ||      SS      ||    //     //  NN  NN NN
                //   //    EE       CC        ||        SS    ||     //   //   NN   NNNN
                /////      EEEEEE    CCCCC  IIIIII  SSSSSS  IIIIII    /////    NN     NN


                // *** Decision ***
                // recently added
                
                MessageBox.Show(" recentaction: " + recentaction + " | minimumtemp: " + minimumtemp + " | minmin: " + minmin);
                if (minimumtemp == recentaction)
                    choice = minmin;
                else choice = minimumtemp;


                switch (choice)
                {
                    case 0:
                        MessageBox.Show("Going UP");

                        //positioning
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (puzzle[i, j] == 0)
                                {
                                    ci = i;
                                    cj = j;

                                    
                                    MessageBox.Show("I am currently at " + ci + "," + cj);
                                }
                            }
                        }

                        /*
                        // recently added
                        // new condition to stop endless loops
                        if (recentaction != 0)
                        {
                        */

                        // recently added
                        recentaction = 2;
                        
                        MessageBox.Show("recentaction value = " + recentaction.ToString());

                        //movement
                        ip = ci;
                        jp = cj;
                        ip--;
                        
                        MessageBox.Show("ready to swap");
                        blockswap(ip, jp, ci, cj);
                        
                        MessageBox.Show("ready to show");
                        ShowSteps();

                        moves++;
                        label4.Text = "moves : " + moves.ToString();

                        break;

                    case 1:
                        MessageBox.Show("Going Left");

                        //pos
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (puzzle[i, j] == 0)
                                {
                                    ci = i;
                                    cj = j;

                                    
                                    MessageBox.Show("I am currently at " + ci + "," + cj);
                                }
                            }
                        }

                        /*
                        // recently added
                        // new condition to stop endless loops
                        if (recentaction != 1)
                        {
                        */

                        // recently added
                        recentaction = 3;
                        
                        MessageBox.Show("recentaction value = " + recentaction.ToString());

                        //movement
                        ip = ci;
                        jp = cj;
                        jp--;
                        
                        MessageBox.Show("Swaping for real this time!");
                        blockswap(ip, jp, ci, cj);
                        ShowSteps();

                        moves++;
                        label4.Text = "moves : " + moves.ToString();

                        break;

                    case 2:
                        MessageBox.Show("Going Down");

                        //pos
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (puzzle[i, j] == 0)
                                {
                                    ci = i;
                                    cj = j;
                                    
                                    
                                    MessageBox.Show("I am currently at " + ci + "," + cj);
                                }
                            }
                        }

                        /*
                        // recently added
                        // new condition to stop endless loops
                        if (recentaction != 2)
                        {
                        */

                        // recently added
                        recentaction = 0;
                        
                        MessageBox.Show("recentaction value = " + recentaction.ToString());

                        //movement
                        ip = ci;
                        jp = cj;
                        ip++;
                        
                        MessageBox.Show("ready to swap");
                        blockswap(ip, jp, ci, cj);
                        
                        MessageBox.Show("ready to show");
                        ShowSteps();

                        moves++;
                        label4.Text = "moves : " + moves.ToString();

                        break;

                    case 3:
                        MessageBox.Show("Going Right");

                        //pos
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (puzzle[i, j] == 0)
                                {
                                    ci = i;
                                    cj = j;

                                    
                                    MessageBox.Show("I am currently at " + ci + "," + cj);
                                }
                            }
                        }

                        /*
                        // recently added
                        // new condition to stop endless loops
                        if (recentaction != 1)
                        {
                        */

                        // recently added
                        recentaction = 1;
                        
                        MessageBox.Show("recentaction value = " + recentaction.ToString());

                        //movement
                        ip = ci;
                        jp = cj;
                        jp++;
                        blockswap(ip, jp, ci, cj);
                        ShowSteps();

                        moves++;
                        label4.Text = "moves : " + moves.ToString();

                        break;

                    default:
                        break;
                }

            }/*
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string s = puzzle[i, j].ToString();
                    listBox1.Items.Add(s);
                }
            }*/
            label2.Text = "nv = 0";
        }
    }
}
