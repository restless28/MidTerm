using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MidTermProject
{
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        double totalPrice;
        Dictionary<string, int> productQuantities = new Dictionary<string, int>(); // Dictionary to store quantities of each product

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics; // Obtain the graphics object from PaintEventArgs

            // Define the first layer of gradient background (blue to black)
            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height);
            LinearGradientBrush backgroundBrush1 = new LinearGradientBrush(
                area,
                Color.DodgerBlue,
                Color.Black,
                LinearGradientMode.Vertical);

            graphics.FillRectangle(backgroundBrush1, area);

            // Define the second layer of gradient background (yellow with transparency)
            LinearGradientBrush backgroundBrush2 = new LinearGradientBrush(
                area,
                Color.White,
                Color.Transparent,
                LinearGradientMode.Vertical);

            graphics.FillRectangle(backgroundBrush2, area);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void checkBoxMonitor_CheckedChanged(object sender, EventArgs e)
        {
            txtBxMonitor.Enabled = checkBoxMonitor.Checked;
        }

        private void checkBoxKeyBoard_CheckedChanged(object sender, EventArgs e)
        {
            txtBxKeyboard.Enabled = checkBoxKeyBoard.Checked;
        }

        private void checkBoxMouse_CheckedChanged(object sender, EventArgs e)
        {
            txtBxMouse.Enabled = checkBoxMouse.Checked;
        }

        private void checkBoxMicrophone_CheckedChanged(object sender, EventArgs e)
        {
            txtBxADI.Enabled = checkBoxMicrophone.Checked;
        }

        private void checkBoxSpeaker_CheckedChanged(object sender, EventArgs e)
        {
            txtBxADO.Enabled = checkBoxSpeaker.Checked;
        }

        private void checkBoxTower_CheckedChanged(object sender, EventArgs e)
        {
            txtBxTower.Enabled = checkBoxTower.Checked;
        }

        private void txtBxMonitor_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Monitor", txtBxMonitor);
        }

        private void txtBxKeyboard_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Keyboard", txtBxKeyboard);
        }

        private void txtBxMouse_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Mouse", txtBxMouse);
        }

        private void txtBxADI_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Microphone", txtBxADI);
        }

        private void txtBxADO_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Speaker", txtBxADO);
        }

        private void txtBxTower_TextChanged(object sender, EventArgs e)
        {
            UpdateProductQuantity("Tower", txtBxTower);
        }

        private void UpdateProductQuantity(string productName, TextBox quantityTextBox)
        {
            try
            {
                int quantity = string.IsNullOrWhiteSpace(quantityTextBox.Text) ? 0 : Convert.ToInt32(quantityTextBox.Text);
                productQuantities[productName] = quantity; // Update quantity in dictionary
            }
            catch (FormatException)
            {
                MessageBox.Show("Error","Must be a number",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetTotal_Click(object sender, EventArgs e)
        {
            totalPrice = CalculateTotalPrice();
            ShowTotalPriceAndProducts();
        }

        private double CalculateTotalPrice()
        {
            double total = 0.0;

            // Calculate total based on stored quantities and prices (assuming labels are set correctly)
            if (productQuantities.ContainsKey("Monitor"))
                total += Convert.ToDouble(lblMonitorPrice.Text) * productQuantities["Monitor"];

            if (productQuantities.ContainsKey("Keyboard"))
                total += Convert.ToDouble(lblKeyboardPrice.Text) * productQuantities["Keyboard"];

            if (productQuantities.ContainsKey("Mouse"))
                total += Convert.ToDouble(lblMousePrice.Text) * productQuantities["Mouse"];

            if (productQuantities.ContainsKey("Microphone"))
                total += Convert.ToDouble(lblADI.Text) * productQuantities["Microphone"];

            if (productQuantities.ContainsKey("Speaker"))
                total += Convert.ToDouble(lblADO.Text) * productQuantities["Speaker"];

            if (productQuantities.ContainsKey("Tower"))
                total += Convert.ToDouble(lblTowerPrice.Text) * productQuantities["Tower"];

            return total;
        }

        private void ShowTotalPriceAndProducts()
        {
            List<string> productList = new List<string>();

            foreach (var kvp in productQuantities)
            {
                if (kvp.Value > 0)
                {
                    productList.Add($"{kvp.Key}: x{kvp.Value}");
                }
            }

            string products = string.Join("\n", productList);
            MessageBox.Show($"Total Build Cost: ${totalPrice:F2}\n\nProducts:\n{products}", "Total Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}