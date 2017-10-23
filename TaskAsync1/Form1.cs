using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskAsync1
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isItFirstTime;
        public Form1()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
            _isItFirstTime = true;
        }

        private async void btbCalculate_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            if (!_isItFirstTime)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            _isItFirstTime = false;
            _cancellationTokenSource = new CancellationTokenSource();
            lblIndicator.Visible = true;
            int result = await CalculateAsycn(Convert.ToInt32(txtInput.Text), _cancellationTokenSource.Token);
            if (result != -1)
            {
                lblResult.Text = result.ToString();
            }
            lblIndicator.Visible = false;
        }


        private async Task<int> CalculateAsycn(int n, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(3000, cancellationToken);
            }
            catch
            {

            }
            while (!cancellationToken.IsCancellationRequested)
            {

                int sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += i;
                }
                return sum;
            }
            return -1;
        }
    }
}
