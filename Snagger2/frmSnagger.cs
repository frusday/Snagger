using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snagger2
{
    public partial class frmSnagger : Form
    {

        public int count = 0;
        int indexer = 0;
        List<string> lstReports;
        Color clrRed = Color.FromArgb(255, 145, 159);
        Color clrOrange = Color.FromArgb(255, 196, 145);
        Color clrYellow = Color.FromArgb(246, 255, 84);

        public frmSnagger()
        {
            lstReports = new List<string>();
            InitializeComponent();
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
        }

        private void frmSnagger_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void frmSnagger_DragDrop(object sender, DragEventArgs e)
        {
            string[] strFiles = (string[])e.Data.GetData("FileDrop", true);
            foreach (string file in strFiles)
            {
                LoadUccapilog(file);
            }
        }

        private void LoadUccapilog(string strFilePath)
        {
            string filename = Path.GetFileName(strFilePath);

            TreeNode vqParent = new TreeNode();
            vqParent = treeView.Nodes.Add(filename);
            vqParent.ToolTipText = strFilePath;
            vqParent.Name = "Parent";
            using (var fstream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fstream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("<VQReportEvent"))
                    {
                        lstReports.Add(line);

                        XNamespace ns = "ms-rtcp-metrics";
                        string report = line;
                        if (report.EndsWith("</VQSessionReport></VQReportEvent>") == false)
                        {
                            report = report + "</VQSessionReport></VQReportEvent>";
                        }
                        XDocument xmlDoc = XDocument.Parse(report);
                        DateTime eleStart = (DateTime)xmlDoc.Descendants(ns + "DialogInfo").Attributes("Start").FirstOrDefault();
                        DateTime eleEnd = (DateTime)xmlDoc.Descendants(ns + "DialogInfo").Attributes("End").FirstOrDefault();
                        string eleFrom = xmlDoc.Descendants(ns + "FromURI").First().Value;
                        string eleTo = xmlDoc.Descendants(ns + "ToURI").First().Value;

                        TreeNode vqChild = new TreeNode();
                        vqChild = vqParent.Nodes.Add(eleStart + " - " + eleEnd);
                        vqChild.Tag = "";
                        vqChild.Name = indexer.ToString();
                        vqChild.ToolTipText = "From: " + eleFrom + "\r\nTo: " + eleTo;

                        IEnumerable<XElement> eleMediaLine = from el in xmlDoc.Descendants(ns + "MediaLine")
                                                             where el.Name.LocalName.Contains("Separator") == false
                                                             select el;

                        foreach (var media in eleMediaLine)
                        {
                            ParseXml(media);
                            TreeNode vqGChild = new TreeNode();
                            vqGChild = vqChild.Nodes.Add(GetThresholdCounts() + media.Attribute("Label").Value);
                            vqGChild.Tag = media.Attribute("Label").Value;
                            vqGChild.Name = indexer.ToString();
                            vqGChild.ToolTipText = vqGChild.Name;
                            lstView.Items.Clear();
                        }
                        indexer++;
                        lstView.Visible = true;
                    }
                }
            }
            treeView.ExpandAll();
        }

        private void ParseXml(int index, string tag = "")
        {
            XNamespace ns = "ms-rtcp-metrics";
            XNamespace nsV2 = "ms-rtcp-metrics.v2";

            string report = lstReports[index];

            try
            {
                XDocument xmlDoc = XDocument.Parse(report);
                DateTime eleStart = (DateTime)xmlDoc.Descendants(ns + "DialogInfo").Attributes("Start").FirstOrDefault();
                DateTime eleEnd = (DateTime)xmlDoc.Descendants(ns + "DialogInfo").Attributes("End").FirstOrDefault();
                string eleFrom = xmlDoc.Descendants(ns + "FromURI").First().Value;
                string eleTo = xmlDoc.Descendants(ns + "ToURI").First().Value;
                string eleInternal = "";
                if (xmlDoc.Descendants(nsV2 + "RegisteredInside").Count() > 0)
                {
                    eleInternal = xmlDoc.Descendants(nsV2 + "RegisteredInside").First().Value;
                }

                txtStart.Text = eleStart.ToString();
                txtEnd.Text = eleEnd.ToString();
                txtFrom.Text = eleFrom;
                txtTo.Text = eleTo;
                txtInternal.Text = eleInternal;

                IEnumerable<XElement> eleMediaLine;

                if (tag != "")
                {
                    XElement eleMedia = xmlDoc.Descendants(ns + "MediaLine")
                        .Where(o => o.Attribute("Label").Value == tag)
                        .FirstOrDefault();

                    eleMediaLine = from el in eleMedia.Descendants()
                                   where el.Name.LocalName.Contains("Separator") == false
                                   select el;
                }
                else
                {
                    eleMediaLine = from el in xmlDoc.Descendants(ns + "MediaLine").Descendants()
                                   where el.Name.LocalName.Contains("Separator") == false
                                   select el;
                }


                lstView.ShowItemToolTips = true;
                lstView.View = View.Details;

                foreach (var item in eleMediaLine)
                {
                    string[] tmpItem = new string[3];

                    if (item.HasElements)
                    {
                        tmpItem[0] = item.Name.LocalName;
                        tmpItem[1] = "";
                        tmpItem[2] = "head";
                    }
                    else
                    {
                        tmpItem[0] = "    " + item.Name.LocalName;
                        tmpItem[1] = item.Value;
                        tmpItem[2] = "";
                    }

                    ListViewItem tmpListViewItem = new ListViewItem(tmpItem);
                    tmpListViewItem.Name = tmpItem[0];
                    lstView.Items.Add(tmpListViewItem);
                    if (tmpItem[2] == "head")
                    {
                        tmpListViewItem.Font = new Font(lstView.Font, FontStyle.Bold);
                    }
                }
                lstView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception)
            {
                string rprt = lstReports[index];
                rprt = rprt + "</VQSessionReport></VQReportEvent>";
                lstReports[index] = rprt;
                ParseXml(index);
                //throw;
            }

        }

        private void ParseXml(XElement media)
        {
            XNamespace ns = "ms-rtcp-metrics";
            IEnumerable<XElement> eleMediaLine;
            try
            {

                eleMediaLine = from el in media.Descendants()
                               where el.Name.LocalName.Contains("Separator") == false
                               select el;

                lstView.Visible = false;

                foreach (var item in eleMediaLine)
                {

                    string[] tmpItem = new string[3];

                    if (item.HasElements)
                    {
                        tmpItem[0] = item.Name.LocalName;
                        tmpItem[1] = "";
                        tmpItem[2] = "head";
                    }
                    else
                    {
                        tmpItem[0] = "    " + item.Name.LocalName;
                        tmpItem[1] = item.Value;
                        tmpItem[2] = "";
                    }

                    ListViewItem tmpListViewItem = new ListViewItem(tmpItem);
                    tmpListViewItem.Name = tmpItem[0];
                    lstView.Items.Add(tmpListViewItem);
                    if (tmpItem[2] == "head")
                    {
                        tmpListViewItem.Font = new Font(lstView.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void TreeViewNode_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
            lstView.Items.Clear();
            flpSide.Controls.Clear();

            if (e.Node.Name != "Parent")
            {
                int intIndex = Convert.ToInt32(e.Node.Name);
                lblStart.Visible = true;
                lblEnd.Visible = true;
                lblFrom.Visible = true;
                lblTo.Visible = true;
                if (e.Node.Tag.ToString() != "")
                {
                    string tag = e.Node.Tag.ToString();
                    ParseXml(intIndex, tag);
                }
                else
                {
                    ParseXml(intIndex);
                }
                GetIceWarnings();
                CheckThresholds();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstReports.Clear();
            lstView.Items.Clear();
            treeView.Nodes.Clear();
            indexer = 0;

            lblStart.Visible = false;
            txtStart.Text = "";

            lblEnd.Visible = false;
            txtEnd.Text = "";

            lblFrom.Visible = false;
            txtFrom.Text = "";

            lblTo.Visible = false;
            txtTo.Text = "";

            lblIceWarnings.Text = "";
            lblIce.Visible = false;

            flpSide.Controls.Clear();

        }

        private void GetIceWarnings()
        {
            ListViewItem[] iceListItem = lstView.Items.Find("    IceWarningFlags", false);

            if (iceListItem != null && iceListItem[0].SubItems[1].Text != "0")
            {
                string iceHex = iceListItem[0].SubItems[1].Text;
                int iceint = Convert.ToInt32(iceHex, 16);
                string iceBin = Convert.ToString(iceint, 2);

                int digits = iceBin.Length;
                int count = 1;
                if (iceint > 0)
                {
                    do
                    {
                        string tst = iceBin.Substring(iceBin.Length - count, 1);
                        if (tst == "1")
                        {
                            Label lbl = new Label();
                            lbl.MaximumSize = new Size(256, 0);
                            lbl.AutoSize = true;
                            ToolTip tip = new ToolTip();
                            tip.AutoPopDelay = 1000000;
                            switch (count)
                            {
                                case 1: //0x1
                                    lbl.Text = "--TURN server is unreachable.";
                                    tip.ToolTipTitle = "TURN server is unreachable.";
                                    tip.SetToolTip(lbl, "This flag is not expected. Administrator need to ensure that the right ports (443/3478—by default) \r\nare open on the firewall or the TURN server is running. This may result in an ICE protocol failure.");
                                    break;
                                case 2: //0x2
                                    lbl.Text = "--An attempt to allocate a UDP port on the TURN server failed.";
                                    tip.ToolTipTitle = "An attempt to allocate a UDP port on the TURN server failed.";
                                    tip.SetToolTip(lbl, "This flag may be expected if the client is behind a UDP blocking firewall. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 3: //0x4 
                                    lbl.Text = "--An attempt to send UDP on the TURN server failed.";
                                    tip.ToolTipTitle = "An attempt to send UDP on the TURN server failed.";
                                    tip.SetToolTip(lbl, "This flag may be expected if the client is behind a UDP blocking firewall. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 4: //0x8
                                    lbl.Text = "--An attempt to allocate a TCP port on the TURN server failed.";
                                    tip.ToolTipTitle = "An attempt to allocate a TCP port on the TURN server failed.";
                                    tip.SetToolTip(lbl, "This flag isn’t expected. The administrator needs to check the firewall policy, and ensure that Audio/Video Edge service is reachable. \r\nIf the client is behind an HTTP proxy, the administrator needs to ensure that the proxy isn’t failing the TLS attempt. \r\nThis failure may result in an ICE protocol failure.");
                                    break;
                                case 5: //0x10
                                    lbl.Text = "--An attempt to send TCP on the TURN server failed.";
                                    tip.ToolTipTitle = "An attempt to send TCP on the TURN server failed.";
                                    tip.SetToolTip(lbl, "This flag isn’t expected. The administrator needs to check the firewall policy, and ensure that Audio/Video Edge service is reachable. \r\nIf the remote client is behind an HTTP proxy, the admin needs to ensure that the proxy isn’t failing the TLS attempt. \r\nThis failure may result in an ICE protocol failure.");
                                    break;
                                case 6: //0x20
                                    lbl.Text = "--Local connectivity failed.(local UDP for audio/video and local TCP for application sharing and file transfer).";
                                    tip.ToolTipTitle = "Local connectivity failed.(local UDP for audio/video and local TCP for application sharing and file transfer).";
                                    tip.SetToolTip(lbl, "This flag can occur if the direct connection between clients isn’t possible due to NAT/firewalls. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 7: //0x40
                                    lbl.Text = "--UDP NAT connectivity failed.";
                                    tip.ToolTipTitle = "UDP NAT connectivity failed.";
                                    tip.SetToolTip(lbl, "This flag can occur if the direct connection between clients isn’t possible due to NAT/firewalls. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 8: //0x80
                                    lbl.Text = "--UDP TURN server connectivity failed.";
                                    tip.ToolTipTitle = "UDP TURN server connectivity failed.";
                                    tip.SetToolTip(lbl, "This flag can occur if one of the clients is behind a UDP blocking firewall/HTTP proxy. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 9: //0x100
                                    lbl.Text = "--TCP NAT connectivity failed.";
                                    tip.ToolTipTitle = "TCP NAT connectivity failed.";
                                    tip.SetToolTip(lbl, "This flag is expected. If local-to-local connectivity succeeded, the TCP NAT connectivity check may not have been tried. \r\nOr there is no direct TCP connection possible. TCP NAT connectivity failing may result in an ICE \r\nprotocol failure.");
                                    break;
                                case 10: //0x200
                                    lbl.Text = "--TCP TURN server connectivity failed.";
                                    tip.ToolTipTitle = "TCP TURN server connectivity failed.";
                                    tip.SetToolTip(lbl, "This flag is expected. If local-to-local connectivity succeeded, the TCP TURN connectivity check may not have been tried. \r\nOr one side didn’t have TURN server allocation. If connectivity checks were successful for the rest of the call, \r\nignore this ICE protocol warning. Otherwise, investigate why the TCP path was not possible. \r\nTCP TURN server connectivity failing may result in an ICE protocol failure.");
                                    break;
                                case 11: //0x400
                                    lbl.Text = "--Message integrity failed in connectivity check request.";
                                    tip.ToolTipTitle = "Message integrity failed in connectivity check request.";
                                    tip.SetToolTip(lbl, "This flag isn’t expected. If the admin sees this flag, it indicates some security attack. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 13: //0x1000
                                    lbl.Text = "--A policy server was configured.";
                                    tip.ToolTipTitle = "A policy server was configured.";
                                    tip.SetToolTip(lbl, "This flag is expected if there is a bandwidth policy configured on the call link. If there is a call failure with this flag, \r\nincrease the allocated bandwidth on the failed link. This flag isn’t indicating any ICE protocol failure, \r\nsimply that there was a bandwidth policy applied to this call.");
                                    break;
                                case 14: //0x2000
                                    lbl.Text = "--Connectivity check requested failed because of a memory problem or other reasons that prevented sending packets.";
                                    tip.ToolTipTitle = "Connectivity check requested failed...";
                                    tip.SetToolTip(lbl, "This flag is unexpected and may indicate that a computer is over capacity This may result in an ICE protocol failure.");
                                    break;
                                case 15: //0x4000
                                    lbl.Text = "--TURN server credentials have expired or are unknown.";
                                    tip.ToolTipTitle = "TURN server credentials have expired or are unknown.";
                                    tip.SetToolTip(lbl, "This flag is unexpected and may indicate that Audio/Video Edge service authorization service is down. This may result in an ICE protocol failure.");
                                    break;
                                case 16: //0x8000
                                    lbl.Text = "--Bandwidth policy restriction has removed some candidates.";
                                    tip.ToolTipTitle = "Bandwidth policy restriction has removed some candidates.";
                                    tip.SetToolTip(lbl, "If there is an ICE protocol failure with this flag set, this indicates that the policy server topology is misconfigured. \r\nIn this configuration the policy was configured to route over another connection, but the other connection failed. \r\n(Possibility of internal NATs in the org). This flag may result in an ICE protocol failure.");
                                    break;
                                case 17: //0x10000
                                    lbl.Text = "--Bandwidth policy restriction decreases the bandwidth.";
                                    tip.ToolTipTitle = "Bandwidth policy restriction decreases the bandwidth.";
                                    tip.SetToolTip(lbl, "This flag indicates that the bandwidth being used on this call isn’t optimal quality \r\n(may be using a narrow-band codec or may not be capable of HD video). \r\nThis flag does not indicate any ICE protocol failure.");
                                    break;
                                case 18: //0x20000
                                    lbl.Text = "--Bandwidth policy keep-alive failed.";
                                    tip.ToolTipTitle = "Bandwidth policy keep-alive failed.";
                                    tip.SetToolTip(lbl, "This is unexpected. The call continues but the bandwidth used by this call may not be reported \r\nproperly to the Bandwidth Policy Core Service. This can occur because the policy server or the TURN \r\nserver have failed. This flag does not indicate any ICE protocol failure.");
                                    break;
                                case 19: //0x40000
                                    lbl.Text = "--Bandwidth policy allocation failure.";
                                    tip.ToolTipTitle = "Bandwidth policy allocation failure.";
                                    tip.SetToolTip(lbl, "This flag is indicating that the policy server rejected the client to use a media path through two Audio/Video Edge services (relay to relay). \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 20: //0x80000
                                    lbl.Text = "--No TURN server configured.";
                                    tip.ToolTipTitle = "No TURN server configured.";
                                    tip.SetToolTip(lbl, "This flag is indicating that there was no TURN server configured or there is a Domain Name System (DNS) resolution failure, \r\nresulting in a communication issue between the client and the TURN server. This may result in a protocol ICE failure.");
                                    break;
                                case 21: //0x100000
                                    lbl.Text = "--Multiple TURN servers configured.";
                                    tip.ToolTipTitle = "Multiple TURN servers configured.";
                                    tip.SetToolTip(lbl, "This flag is expected. This is indicating that there were multiple TURN servers \r\nconfigured (due to DNS load balancing). This flag does not indicate any ICE protocol failure.");
                                    break;
                                case 22: //0x200000
                                    lbl.Text = "--Port range exhausted.";
                                    tip.ToolTipTitle = "Port range exhausted.";
                                    tip.SetToolTip(lbl, "This is indicating that the administrator manually configured ports on the client or the media server. \r\nA/V needs a minimum of 20 ports per call to start the call. Application sharing/file transfer \r\nneeds a minimum of 3 ports. The port range being exhausted may result in \r\nan ICE protocol failure.");
                                    break;
                                case 23: //0x400000
                                    lbl.Text = "--Received alternate server.";
                                    tip.ToolTipTitle = "Received alternate server.";
                                    tip.SetToolTip(lbl, "This is indicating that the TURN server is overloaded or preventing new connections. \r\nThis may result in  an ICE protocol failure if the alternate server isn’t running");
                                    break;
                                case 24: //0x800000
                                    lbl.Text = "--Pseudo-TLS failure.";
                                    tip.ToolTipTitle = "Pseudo-TLS failure.";
                                    tip.SetToolTip(lbl, "This is indicating that the HTTP proxy is performing deep Secure Sockets Layer (SSL) \r\ninspection and failing the connection with the TURN server. \r\nThis is not supported by Microsoft and may result in an ICE protocol failure.");
                                    break;
                                case 25: //0x1000000
                                    lbl.Text = "--HTTP proxy configured.";
                                    tip.ToolTipTitle = "HTTP proxy configured.";
                                    tip.SetToolTip(lbl, "This is indicating that the HTTP proxy is configured This flag does not indicate any ICE protocol failure.");
                                    break;
                                case 26: //0x2000000
                                    lbl.Text = "--HTTP proxy authentication failed.";
                                    tip.ToolTipTitle = "HTTP proxy authentication failed.";
                                    tip.SetToolTip(lbl, "This is indicating that the HTTP proxy failed the authentication. This isn’t expected \r\nand indicates that the HTTP proxy didn’t recognize the user’s credentials or authentication mode. \r\nThis may result in an ICE protocol failure.");
                                    break;
                                case 27: //0x4000000
                                    lbl.Text = "--TCP-TCP connectivity checks failed over the TURN Server.";
                                    tip.ToolTipTitle = "TCP-TCP connectivity checks failed over the TURN Server.";
                                    tip.SetToolTip(lbl, "This is indicating that TURN TCP-TCP connectivity check was tried and it failed. \r\nThe failure indicates that port 443 was not opened on the firewall. If one of the \r\nTURN servers was 2007 A/V Edge Server. The administrator needs to open ports from \r\n50,000 through 59,999 TCP to all external Audio/Video Edge services in the environment. \r\nThis flag isn’t expected and may result in an ICE protocol failure.");
                                    break;
                                case 28: //0x8000000
                                    lbl.Text = "--Use candidate checks failed.";
                                    tip.ToolTipTitle = "Use candidate checks failed.";
                                    tip.SetToolTip(lbl, "This is indicating that after receiving some packets the client didn’t receive the rest of the packets. \r\nThis may happen on a client because of a third Winsock layered service providers (LSPs). \r\nThese LSPs should be removed. This flag isn’t expected and may result in an ICE protocol failure.");
                                    break;

                                default:
                                    break;
                            }
                            flpSide.Controls.Add(lbl);
                        }
                        count++;

                    } while (digits - count != 0);
                    lblIce.Visible = true;
                }
            }
            else
            {
                lblIce.Visible = false;
                lblIceWarnings.Text = "";
            }

        }

        private void CheckThresholds()
        {

            foreach (ListViewItem item in lstView.Items)
            {
                // Jitter
                if (item.Text.ToLower().EndsWith("interarrival") || item.Text.ToLower().EndsWith("interarrivalmax"))
                {
                    int value = Convert.ToInt32(item.SubItems[1].Text);
                    if (value >= 40)
                    {
                        item.BackColor = clrRed;
                    }
                    else if (value >= 30)
                    {
                        item.BackColor = clrOrange;
                    }
                    else if (value >= 20)
                    {
                        item.BackColor = clrYellow;
                    }
                }

                // Round Trip
                if (item.Text.ToLower().EndsWith("roundtrip") || item.Text.ToLower().EndsWith("roundtripmax"))
                {
                    int value = Convert.ToInt32(item.SubItems[1].Text);
                    if (value >= 500)
                    {
                        item.BackColor = clrRed;
                    }
                    else if (value >= 200)
                    {
                        item.BackColor = clrOrange;
                    }
                    else if (value >= 150)
                    {
                        item.BackColor = clrYellow;
                    }
                }

                // Packet Loss
                if (item.Text.ToLower().EndsWith(" lossrate") || item.Text.ToLower().EndsWith(" lossratemax"))
                {
                    decimal value = Decimal.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.Float);
                    if (value >= 0.07M)
                    {
                        item.BackColor = clrRed;
                    }
                    else if (0.051M <= value && value <= 0.069M)
                    {
                        item.BackColor = clrOrange;
                    }
                    else if (0.03M <= value && value <= 0.05M)
                    {
                        item.BackColor = clrYellow;
                    }

                }

                // Concealed Samples Ratio
                if (item.Text.ToLower().EndsWith("ratioconcealedsamplesavg"))
                {
                    decimal value = Decimal.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.Float);
                    if (value >= 0.07M)
                    {
                        item.BackColor = clrRed;
                    }
                    else if (value >= 0.03M)
                    {
                        item.BackColor = clrOrange;
                    }
                    else if (value >= 0.02M)
                    {
                        item.BackColor = clrYellow;
                    }

                }
            }

        }

        private string GetThresholdCounts()
        {
            int countYellow = 0;
            int countOrange = 0;
            int countRed = 0;
            string counts = "";

            foreach (ListViewItem item in lstView.Items)
            {
                // Jitter
                if (item.Text.ToLower().EndsWith("interarrival") || item.Text.ToLower().EndsWith("interarrivalmax"))
                {
                    int value = Convert.ToInt32(item.SubItems[1].Text);
                    if (value >= 40)
                    {
                        countRed++;
                    }
                    else if (value >= 30)
                    {
                        countOrange++;
                    }
                    else if (value >= 20)
                    {
                        countYellow++;
                    }
                }

                // Round Trip
                if (item.Text.ToLower().EndsWith("roundtrip") || item.Text.ToLower().EndsWith("roundtripmax"))
                {
                    int value = Convert.ToInt32(item.SubItems[1].Text);
                    if (value >= 500)
                    {
                        countRed++;
                    }
                    else if (value >= 200)
                    {
                        countOrange++;
                    }
                    else if (value >= 150)
                    {
                        countYellow++;
                    }
                }

                // Packet Loss
                if (item.Text.ToLower().EndsWith(" lossrate") || item.Text.ToLower().EndsWith(" lossratemax"))
                {
                    decimal value = Decimal.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.Float);
                    if (value >= 0.07M)
                    {
                        countRed++;
                    }
                    else if (0.051M <= value && value <= 0.069M)
                    {
                        countOrange++;
                    }
                    else if (0.03M <= value && value <= 0.05M)
                    {
                        countYellow++;
                    }

                }

                // Concealed Samples Ratio
                if (item.Text.ToLower().EndsWith("ratioconcealedsamplesavg"))
                {
                    decimal value = Decimal.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.Float);
                    if (value >= 0.07M)
                    {
                        countRed++;
                    }
                    else if (value >= 0.03M)
                    {
                        countOrange++;
                    }
                    else if (value >= 0.02M)
                    {
                        countYellow++;
                    }

                }
            }

            counts = " " + countRed.ToString() + " | " + countOrange.ToString() + " | " + countYellow.ToString() + "  | ";
            return counts;
        }

        private void RightClickListItem(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lstView.SelectedItems.Count == 1)
                {
                    if (string.IsNullOrEmpty(lstView.SelectedItems[0].SubItems[1].Text))
                    {

                        lstView.ContextMenuStrip.Opening += (s, c) =>
                        {
                            if (lstView.SelectedItems.Count == 1 && string.IsNullOrEmpty(lstView.SelectedItems[0].SubItems[1].Text))
                            {
                                c.Cancel = true;
                            }
                            else
                            {
                                c.Cancel = false;
                            }

                        };
                    }
                }
            }

        }

        private void ContextMenuCopyValues_Click(object sender, EventArgs e)
        {
            string strCopy = "";
            foreach (ListViewItem item in lstView.SelectedItems)
            {
                if (!string.IsNullOrEmpty(item.SubItems[1].Text))
                {
                    strCopy += item.SubItems[1].Text + "\r\n";
                }
            }
            if (!string.IsNullOrEmpty(strCopy))
            {
                Clipboard.SetText(strCopy);
            }

        }

        private void copyWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strCopy = "";
            string strElement = "";
            string strValue = "";
            foreach (ListViewItem item in lstView.SelectedItems)
            {
                char trim = Char.Parse(" ");
                strElement = item.SubItems[0].Text.TrimStart(trim);
                strValue = item.SubItems[1].Text;
                strCopy += strElement + "\t" + strValue + "\r\n";
            }
            if (!string.IsNullOrEmpty(strCopy))
            {
                Clipboard.SetText(strCopy);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK)
            {
                string[] strFileNames = ofdOpen.FileNames;
                foreach (var file in strFileNames)
                {
                    LoadUccapilog(file);
                }
            }
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            string[] seperators = { "|" };
            string[] texts = e.Node.Text.Split(seperators, StringSplitOptions.None);
            using (Font font = new Font(this.Font, FontStyle.Regular))
            {
                using (Brush brushText = new SolidBrush(Color.Black))
                {
                    if (e.Node.Tag != "" && texts.Length > 1)
                    {
                        using (Brush brushBack = new SolidBrush(clrRed))
                        {
                            SizeF a = e.Graphics.MeasureString(texts[0], font);
                            Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, (int)a.Width, e.Bounds.Height);

                            e.Graphics.FillRectangle(brushBack, rect);
                            //e.Graphics.DrawString(texts[0], font, brushText, e.Bounds.Left, e.Bounds.Top);
                        }
                        if (texts.Length > 2)
                        {
                            using (Brush brushBack = new SolidBrush(clrOrange))
                            {
                                SizeF a = e.Graphics.MeasureString(texts[0], font);
                                SizeF b = e.Graphics.MeasureString(texts[1], font);
                                Rectangle rect = new Rectangle(e.Bounds.Left + (int)a.Width, e.Bounds.Top, (int)b.Width, e.Bounds.Height);

                                e.Graphics.FillRectangle(brushBack, rect);
                                //e.Graphics.DrawString(texts[1], font, brushText, e.Bounds.Left + (int)a.Width, e.Bounds.Top);
                            }
                        }
                        if (texts.Length > 3)
                        {
                            using (Brush brushBack = new SolidBrush(clrYellow))
                            {
                                SizeF a = e.Graphics.MeasureString(texts[0], font);
                                SizeF b = e.Graphics.MeasureString(texts[1], font);
                                SizeF c = e.Graphics.MeasureString(texts[2], font);
                                Rectangle rect = new Rectangle(e.Bounds.Left + (int)a.Width + (int)b.Width, e.Bounds.Top, (int)c.Width, e.Bounds.Height);

                                e.Graphics.FillRectangle(brushBack, rect);
                                //e.Graphics.DrawString(texts[2], font, brushText, e.Bounds.Left + (int)a.Width + (int)b.Width, e.Bounds.Top);
                            }
                        }
                        StringBuilder sb = new StringBuilder();
                        foreach (var str in texts)
                        {
                            sb.Append(str);
                        }
                        e.Graphics.DrawString(sb.ToString(), font, brushText, e.Bounds.Left, e.Bounds.Top);
                    }
                    else
                    {
                        e.Graphics.DrawString(texts[0], font, brushText, e.Bounds.Left, e.Bounds.Top);
                    }
                }
            }
        }

    }
}
