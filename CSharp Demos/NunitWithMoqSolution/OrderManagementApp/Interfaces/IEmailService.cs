using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementApp.Interfaces
{
    public interface IEmailService
    {
        void SendOrderConfirmation(string customerEmail, string productName);
    }
}
