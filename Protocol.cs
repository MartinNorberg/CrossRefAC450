using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC450Communication
{
    public enum Protocol
    {
        TCP,
        UDP,
        None,
    }

    //public static bool TryParseInt(int protocol, out Protocol value)
    //{
    //    switch (protocol)
    //    {
    //        case 0:
    //            value = Protocol.TCP;
    //            return true;
    //        case 1:
    //            value = Protocol.UDP;
    //            return true;
    //        default:
    //            value = Protocol.None;
    //            return false;
    //    }
    //}
}
