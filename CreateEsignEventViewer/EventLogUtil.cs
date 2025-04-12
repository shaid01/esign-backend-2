using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;

namespace CreateEsignEventViewer
{
    public static class EventLogUtil
    {
        public static EventLog CreateEventLog(string logSourceName, string logEventName)
        {
            try
            {
                if (!EventLog.SourceExists(logSourceName))
                {
                    EventLog.CreateEventSource(logSourceName, logEventName);
                }
            }
            catch (System.Security.SecurityException) // Win 7
            {
                //throw new Exception(string.Format("You must have administrative privileges to search for an event source! Event log name: {0}", logEventName));
                MessageBox.Show(string.Format("You must have administrative privileges to search for an event source! Event log name: {0}", logEventName));

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error CreateEventSource: {ex}");

                return null;
            }

            try
            {
                if (!string.Equals(EventLog.LogNameFromSourceName(logSourceName, ".").ToUpper(), logEventName.ToUpper()))
                {
                    EventLog.DeleteEventSource(logSourceName);
                    EventLog.CreateEventSource(logSourceName, logEventName);

                    //throw new Exception(string.Format("Event source {0} found in another log and was recreated under log {1}. Your must restart PC to take effect of this change.", logSourceName, logEventName));
                    MessageBox.Show(string.Format("Event source {0} found in another log and was recreated under log {1}. Your must restart PC to take effect of this change.", logSourceName, logEventName));

                    return null;
                }

                EventLog result = new EventLog(logEventName, ".", logSourceName);
                //result.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creation event log: {ex}");

                return null;
            }
        }
    }
}
