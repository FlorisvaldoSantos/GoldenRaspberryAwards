using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProducerServiceException : Exception
    {
        public ProducerServiceException(string message) : base (message)
        {
        }
    }
}
