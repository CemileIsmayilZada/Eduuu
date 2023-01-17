using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Exceptions
{
    public class RoleCreateFailException:Exception
    {
        public RoleCreateFailException(string message) : base(message)
        {

        }
    }
}
