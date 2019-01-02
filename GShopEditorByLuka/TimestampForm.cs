using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GShopEditorByLuka
{
    public partial class TimestampForm : Form
    {
        public TimestampForm(string time, Form1 f)
        {
            InitializeComponent();
            DateTime dt = Convert.ToDateTime(time);
            Year.Value = dt.Year;
            Month.Value = dt.Month;
            Day.Value = dt.Day;
            Hour.Value = dt.Hour;
            Minute.Value = dt.Minute;
            Second.Value = dt.Second;
            // var FormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            // string[] sp = time.Split(' ');
            // string[] Date = sp[0].Split(FormatInfo.DateSeparator.ToCharArray()[0]);
            // string[] Time = sp[1].Split(FormatInfo.TimeSeparator.ToCharArray()[0]);
            // Year.Value = Convert.ToInt32(Date[2]);
            // Month.Value = Convert.ToInt32(Date[1]);
            // Day.Value = Convert.ToInt32(Date[0]);
            // Hour.Value = Convert.ToInt32(Time[0]);
            // Minute.Value = Convert.ToInt32(Time[1]);
            // Second.Value = Convert.ToInt32(Time[2]);
            fm = f;
        }
        Form1 fm;
        int[] time = new int[6];
        private void Accept_Click(object sender, EventArgs e)
        {
            time[0] = (int)Year.Value;
            time[1] = (int)Month.Value;
            time[2] = (int)Day.Value;
            time[3] = (int)Hour.Value;
            time[4] = (int)Minute.Value;
            time[5] = (int)Second.Value;
            this.Close();
        }
        public int[] WaitForValue()
        {
            this.ShowDialog();
            return time;
        }

        private void DateTimeNow_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Year.Value = dt.Year;
            Month.Value = dt.Month;
            Day.Value = dt.Day;
            Hour.Value = dt.Hour;
            Minute.Value = dt.Minute;
            Second.Value = dt.Second;
        }
    }
}
