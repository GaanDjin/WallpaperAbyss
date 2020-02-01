using ManagedWiFi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedWiFi
{
    /// <summary>
    /// Helper class to get and check for metered WiFi networks.
    /// 
    /// http://managedwifi.codeplex.com/releases/view/7718
    /// </summary>
    public class MeteredConnection
    {
        /// <summary>
        /// Returns a list of wireless networks we're currently connected to. 
        /// It's a list just in case you have more than one wireless interface and are connected to several wireless netowrks at once.
        /// </summary>
        public static List<string> ConnectedWiFiName
        {
            get
            {
                List<string> connectedSsids = new List<string>();

                try
                {
                    WlanClient wlan = new WlanClient();


                    foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
                    {
                        Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                        connectedSsids.Add(new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength)));
                    }

                }
                catch (Exception) { }
                return connectedSsids;
            }
        }

        /// <summary>
        /// Checks to see if a wireless network is metered.
        /// </summary>
        /// <param name="ssidsthatshouldbemetered">List of SSIDs to check</param>
        /// <returns>True if one of the SSIDs is, infact, metered</returns>
        public static bool IsMetered(List<string> ssidsthatshouldbemetered)
        {
            if (ssidsthatshouldbemetered == null || ssidsthatshouldbemetered.Count == 0)
                return false;

            List<string> connectedSSID = ConnectedWiFiName;

            if (connectedSSID == null || connectedSSID.Count == 0)
                return false;

            foreach (string ssid in ssidsthatshouldbemetered)
                if (ssid != null)
                    foreach (string cssid in connectedSSID)
                        if (ssid.Equals(cssid, StringComparison.InvariantCultureIgnoreCase))
                            return true;

            return false;
        }
    }
}
