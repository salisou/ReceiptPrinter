using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiptPrinter
{
    public interface IPrinter
    {
        void Print(string ipAddress, int port, IList<String> linesToPrint);
    }
}
