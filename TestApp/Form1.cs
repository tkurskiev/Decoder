using Decoding;
using System;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        MyDecoder decoder = new MyDecoder("");

        public Form1()
        {
            InitializeComponent();
        }

        private void UserInputText_TextChanged(object sender, EventArgs e)
        {
            this.ResultLable.Text = decoder.Decode(this.UserInputText.Text);
        }
    }
}
