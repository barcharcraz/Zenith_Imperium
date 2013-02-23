using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ManagedRecast;

namespace RecastSharp
{
    public class ReContext
    {
        private rcContextManaged m_managed;
        public ReContext()
        {
            m_managed = new rcContextManaged();
        }
    }
}
