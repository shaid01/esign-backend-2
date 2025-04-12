using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateEsignEventViewer
{
    public partial class Form1 : Form
    {
        private EventLog m_eventLog = EventLogUtil.CreateEventLog("EsignWebAPI", "ComdaEsign");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_eventLog != null)
            {
                m_eventLog.WriteEntry("Esign event log source was created", EventLogEntryType.SuccessAudit);
                MessageBox.Show("Esign event log source was created.");
            }
            else
            {
                MessageBox.Show("Esign event log source creation failed.");
            }
        }
    }
}
