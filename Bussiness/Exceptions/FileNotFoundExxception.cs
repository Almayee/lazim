using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Exceptions
{
    public class FileNotFoundExxception : Exception
    {
        public FileNotFoundExxception(string? message) : base(message)
        {
        }
    }
}
