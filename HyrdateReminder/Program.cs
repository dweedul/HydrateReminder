using System;
using System.Diagnostics;
using System.Threading;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace HyrdateReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SendToastNotification();

                // Notification doesn't show without a sleep.
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("Hydrate Reminder", $"Hydrate reminder failed. Exception message: { e.Message }", EventLogEntryType.Error);
            }
        }

        private static void SendToastNotification()
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            // Fill in the text element
            IXmlNode node = toastXml.GetElementsByTagName("text")[0];
            node.AppendChild(toastXml.CreateTextNode("Drink water! " + char.ConvertFromUtf32(0x1F4A7)));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Hydrate Reminder").Show(toast);
        }
    }
}
