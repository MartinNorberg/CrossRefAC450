namespace AC450Communication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class PcData : INotifyPropertyChanged
    {
        public PcData(string file, string itemDesignation, string callName, string net, string node, string link, string channel, string port, Protocol protocol, Mode mode, string page, string msgID, string remoteIpAddress, Direction direction)
        {
            this.File = file;
            this.ItemDesignation = itemDesignation;
            this.CallName = callName;
            this.Net = net;
            this.Node = node;
            this.Link = link;
            this.Channel = channel;
            this.Port = port;
            this.Page = page;
            this.MsgId = msgID;
            this.RemoteIpAddress = remoteIpAddress;
            this.Dir = direction;
        }

        public event PropertyChangedEventHandler PropertyChanged;

       

        public string ItemDesignation { get; }

        public string CallName { get; }

        public string Net { get; }

        public string Node { get; }

        public string ChIdent { get; }

        public string Link { get; }

        public string Channel { get; }

        public string MsgId { get; }

        public Direction Dir { get; }

        public string IpAddress { get; }

        public string RemoteIpAddress { get; }

        public string Port { get; }

        public Protocol Protocol {get; }

        public enum Mode
        {
            Client,
            Server,
            None,
        }
        
        public string Page { get; }

        public string File { get; }

        public static bool TryParse(string file, out IReadOnlyList<PcData> pcdata)
        {
            var foundPCCRD = false;
            var foundPCCRDF = false;
            var foundPCCWR = false;
            var foundVipChan = false;
            var foundVipLink = false;
            var founVipNetw = false;
            var foundvipNode = false;
            var foundvipW = false;
            var foundVipR = false;

            var fileName = file;
            var net = string.Empty;
            var node = string.Empty;
            var itemdesignation = string.Empty;
            var callName = string.Empty;
            var channelIdentity = string.Empty;
            var link = string.Empty;
            var chan1 = string.Empty;
            var msgID = string.Empty;
            var prot1 = string.Empty;
            var networkName = string.Empty;
            var ipAdd = string.Empty;
            var gateway = string.Empty;
            var remoteIp = string.Empty;
            var first = string.Empty;
            var last = string.Empty;
            var port = string.Empty;
            var page = string.Empty;
            var mode = Mode.None;
            var protocol = Protocol.None;
            var direction = Direction.None;

            var results = new List<PcData>();

            foreach (var line in System.IO.File.ReadAllLines(file))
            {
                var tmpLine = line.Trim();
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                tmpLine = regex.Replace(tmpLine, " ");

                if (line.ToUpper().Contains("PCD-PAGE"))
                {
                    var array = tmpLine.Split(' ');

                    if (array.Length >1)
                    {
                        page = tmpLine.Split(' ')[1];
                    }
                    else
                    {
                        page = string.Empty;
                    }
                }

                if (line.ToUpper().Contains("PCC-RD ("))
                {
                    foundPCCRD = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                    
                }
                if (line.ToUpper().Contains("PCC-RDF ("))
                {
                    foundPCCRDF = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0]; 
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                }
                if (line.ToUpper().Contains("PCC-WR ("))
                {
                    foundPCCWR = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                 
                }
                if (line.ToUpper().Contains("VIP-CHAN ("))
                {
                    foundVipChan = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length >4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                    
                }
                if (line.ToUpper().Contains("VIP-LINK ("))
                {
                    foundVipChan = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                }
                if (line.ToUpper().Contains("VIP-NETW ("))
                {
                    founVipNetw = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                }
                if (line.ToUpper().Contains("VIP-NODE ("))
                {
                    foundvipNode = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                    
                }
                if (line.ToUpper().Contains("VIP-W ("))
                {
                    foundvipW = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length > 4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                    
                }
                if (line.ToUpper().Contains("VIP-R ("))
                {
                    foundVipR = true;
                    var array = tmpLine.Split(' ');
                    itemdesignation = array[0];
                    if (array.Length >4)
                    {
                        callName = array[4];
                    }
                    else
                    {
                        callName = array[1];
                    }
                }

                if (line == string.Empty)
                {
                    foundPCCRD = false;
                    foundPCCRDF = false;
                    foundPCCWR = false;
                    foundVipChan = false;
                    foundVipLink = false;
                    founVipNetw = false;
                    foundvipNode = false;
                    foundvipW = false;
                    foundVipR = false;
                }

                if (foundPCCRD)
                {
                    if (tmpLine.ToUpper().Contains("CH-IDENT"))
                    {
                        channelIdentity = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, channelIdentity, port, Protocol.None, Mode.None, page, string.Empty, string.Empty, Direction.None));
                        foundPCCRD = false;
                    }
                }
                if (foundPCCRDF)
                {
                    if (tmpLine.ToUpper().Contains("CH-IDENT"))
                    {
                        channelIdentity = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, channelIdentity, port, Protocol.None, Mode.None, page, string.Empty, string.Empty, Direction.None));
                        foundPCCRD = false;
                    }
                }
                if (foundPCCWR)
                {
                    if (tmpLine.ToUpper().Contains("CH-IDENT"))
                    {
                        var array = tmpLine.Split(' ');
                        channelIdentity = array[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, channelIdentity, port, Protocol.None, Mode.None, page, string.Empty, string.Empty, Direction.None));
                        foundPCCWR = false;
                    }
                }
                if (foundVipChan)
                {
                    var array = tmpLine.Split(' ');
                    if (line.ToUpper().Contains(":NET"))
                    {
                        net = tmpLine.Split(' ')[1];
                    }
                    if (line.ToUpper().Contains(":NODE")) node = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":LINK")) link = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":CHAN1")) chan1 = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":MSGID1"))
                    {
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, "", port, Protocol.TCP, Mode.None, page, tmpLine.Split(' ')[1], string.Empty, Direction.None));
                        foundVipChan = false;
                    }
                }
                if (foundvipNode)
                {
                    if (line.ToUpper().Contains(":NET")) net = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":NODE")) node = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":NAME1")) link = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":INET1"))
                    {
                        msgID = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, "", port, Protocol.None, Mode.None, page, string.Empty, tmpLine.Split(' ')[1], Direction.None));
                        foundvipNode = false;
                    }
                }
                if (foundVipLink)
                {
                    if (line.ToUpper().Contains(":NET")) net = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":NODE")) node = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":LINK")) link = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":PROT1")) prot1 = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":DIR1"))
                    {
                        if (int.Parse(tmpLine.Split(' ')[1]) == 1)
                        {
                            direction = Direction.Send;
                        }
                        else if (int.Parse(tmpLine.Split(' ')[1]) == 2)
                        {
                            direction = Direction.Bidirectional;
                        }
                        else if (int.Parse(tmpLine.Split(' ')[1]) == 0)
                        {
                            direction = Direction.Receive;
                        }
                        else
                        {
                            direction = Direction.None;
                        }

                    }
                    if (line.ToUpper().Contains(":PORT1")) port = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":TCP/UDP1"))
                    {
                        if (int.Parse(tmpLine.Split(' ')[1]) ==1)
                        {
                            protocol = Protocol.UDP;
                        }
                        else
                        {
                            protocol = Protocol.TCP;
                        }
                        

                    }
                    if (line.ToUpper().Contains(":CLN/SRV1"))
                    {

                        mode = Mode.None;
                        if (int.Parse(tmpLine.Split(' ')[1])==1)
                        {
                            mode = Mode.Server;
                        }
                        else
                        {
                            mode = Mode.Client;
                        }
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, "", port, protocol, mode, page, msgID, "", direction));
                        foundVipLink = false;
                    }
                }
                if (founVipNetw)
                {
                    if (line.ToUpper().Contains(":NET")) net = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":NODE")) node = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":NAME")) networkName = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":OWNINET")) ipAdd = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":GWINET")) gateway = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":REMINET"))
                    {
                        remoteIp = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, port, "", port, Protocol.None, Mode.None, page, string.Empty, remoteIp, Direction.None));
                        founVipNetw = false;
                    }
                }
                if (foundvipW)
                {
                    if (line.ToUpper().Contains(":CHAN")) chan1 = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":FIRST")) first = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":LAST"))
                    {
                        last = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, "", chan1, "", Protocol.None, Mode.None, page, string.Empty, "", Direction.None));
                        foundvipW = false;
                    }
                }
                if (foundVipR)
                {
                    if (line.ToUpper().Contains(":CHAN")) chan1 = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":FIRST")) first = tmpLine.Split(' ')[1];
                    if (line.ToUpper().Contains(":LAST"))
                    {
                        last = tmpLine.Split(' ')[1];
                        results.Add(new PcData(fileName, itemdesignation, callName, net, node, link, chan1, port, Protocol.None, Mode.None, page, string.Empty, "", Direction.None));
                        foundVipR = false;
                    }
                }

            }

            if (results.Count > 0)
            {
                pcdata = results;
                return true;
            }

            pcdata = null;
            return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
