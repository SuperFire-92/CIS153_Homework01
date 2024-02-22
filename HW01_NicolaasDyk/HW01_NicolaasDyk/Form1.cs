using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW01_NicolaasDyk
{
    public partial class Form1 : Form
    {
        private float applePrice, pearPrice, avocadoPrice, lemonPrice, bananaPrice, limePrice;
        private float[] allPrices;
        private string[] allNames;
        private float[] allAmounts = { 0, 0, 0, 0, 0, 0 };

        private float total = 0;

        public Form1()
        {
            InitializeComponent();
            applePrice = float.Parse(lbl_price_apple.Text);
            pearPrice = float.Parse(lbl_price_pear.Text);
            avocadoPrice = float.Parse(lbl_price_avocado.Text);
            lemonPrice = float.Parse(lbl_price_lemon.Text);
            bananaPrice = float.Parse(lbl_price_banana.Text);
            limePrice = float.Parse(lbl_price_lime.Text);
            allPrices = new float[] { applePrice, pearPrice, avocadoPrice, lemonPrice, bananaPrice, limePrice };
            allNames = new string[] { "Apple", "Pear", "Avocado", "Lemon", "Banana", "Lime" };

            resetDateTime();
            fillPrice();
            fillItems();
        }

        private void addClick(object sender, EventArgs e)
        {
            //First we get the type of item
            Button button = sender as Button;
            string type = button.Name.Substring(8);

            //add the price to the total
            compareTypes(type, true);
            fillPrice();
            fillItems();
            Console.WriteLine(total);

        }
        
        private void subClick(object sender, EventArgs e)
        {
            //First we get the type of item
            Button button = sender as Button;
            string type = button.Name.Substring(10);

            //add the price to the total
            compareTypes(type, false);
            fillPrice();
            fillItems();
            Console.WriteLine(total);

        }



        private void compareTypes(string type, bool add)
        {
            //If the type matches, add that item's price to the total
            if (type == "apple")
            {
                if (!(!add && float.Parse(lbl_apple.Text) == 0))
                {
                    total = total + (add ? applePrice : -applePrice);
                    lbl_apple.Text = (allAmounts[0] + (add ? 1 : -1)).ToString();
                    allAmounts[0] += (add ? 1 : -1);
                }
            }
            if (type == "pear")
            {
                if (!(!add && float.Parse(lbl_pear.Text) == 0))
                {
                    total = total + (add ? pearPrice : -pearPrice);
                    lbl_pear.Text = (allAmounts[1] + (add ? 1 : -1)).ToString();
                    allAmounts[1] += (add ? 1 : -1);
                }
            }
            if (type == "avocado")
            {
                if (!(!add && float.Parse(lbl_avocado.Text) == 0))
                {
                    total = total + (add ? avocadoPrice : -avocadoPrice);
                    lbl_avocado.Text = (allAmounts[2] + (add ? 1 : -1)).ToString();
                    allAmounts[2] += (add ? 1 : -1);
                }
            }
            if (type == "lemon")
            {
                if (!(!add && float.Parse(lbl_lemon.Text) == 0))
                {
                    total = total + (add ? lemonPrice : -lemonPrice);
                    lbl_lemon.Text = (allAmounts[3] + (add ? 1 : -1)).ToString();
                    allAmounts[3] += (add ? 1 : -1);
                }
            }
            if (type == "banana")
            {
                if (!(!add && float.Parse(lbl_banana.Text) == 0))
                {
                    total = total + (add ? bananaPrice : -bananaPrice);
                    lbl_banana.Text = (allAmounts[4] + (add ? 1 : -1)).ToString();
                    allAmounts[4] += (add ? 1 : -1);
                }
            }
            if (type == "lime")
            {
                if (!(!add && float.Parse(lbl_lime.Text) == 0))
                {
                    total = total + (add ? limePrice : -limePrice);
                    lbl_lime.Text = (allAmounts[5] + (add ? 1 : -1)).ToString();
                    allAmounts[5] += (add ? 1 : -1);
                }
            }
        }

        //Resets the date, time, and label order number
        private void resetDateTime()
        {
            DateTime dt = DateTime.Now;
            lbl_date.Text = dt.Month + "/" + dt.Day + "/" + dt.Year;
            lbl_time.Text = (dt.Hour > 12 ? dt.Hour - 12 : dt.Hour) + ":" + dt.Minute + " " + (dt.Hour > 12 ? "PM" : "AM");
            Random rnd = new Random();
            lbl_order.Text = rnd.Next(0,1000).ToString("000");
        }

        //Fills in the subtotal, tax, and total of the current order
        private void fillPrice()
        {
            lbl_totalText.Text = "Subtotal\nTax\nTotal";
            lbl_price.Text = "$" + total.ToString("0.00") + "\n" + "$" + (total * 0.06).ToString("0.00") + "\n" + "$" + (total + (total * 0.06)).ToString("0.00");
        }

        //Fills in all of the current items the user has in their cart
        private void fillItems()
        {
            lbl_itemName.Text = "";
            lbl_itemAmount.Text = "";
            lbl_itemPrice.Text = "";
            for(int i = 0; i < allAmounts.Length; i++)
            {
                if (allAmounts[i] > 0)
                {
                    lbl_itemName.Text += allNames[i] + "\n";
                    lbl_itemAmount.Text += allAmounts[i] + "\n";
                    lbl_itemPrice.Text += "$" + (allAmounts[i] * allPrices[i]).ToString("0.00") + "\n";
                }
            }
        }

        //Resets everything on the screen when the user finishes their order (does nothing if the user
        //has nothing in their cart when they click the New Order button)
        private void resetAll(object sender, EventArgs e)
        {
            bool isEmpty = true;
            for (int i = 0; i < allAmounts.Length; i++)
            {
                if (allAmounts[i] != 0)
                {
                    isEmpty = false;
                }
            }

            if (!isEmpty)
            {
                allAmounts = new float[] { 0, 0, 0, 0, 0, 0 };
                total = 0;
                resetDateTime();
                fillPrice();
                fillItems();
                resetLabelNumbers();
            }
        }

        //Resets the numbers that display how many items the user has
        private void resetLabelNumbers()
        {
            lbl_apple.Text = "0";
            lbl_pear.Text = "0";
            lbl_avocado.Text = "0";
            lbl_lemon.Text = "0";
            lbl_banana.Text = "0";
            lbl_lime.Text = "0";
        }
    }
}
