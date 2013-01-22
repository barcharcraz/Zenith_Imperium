using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Exceptions
{
    [Serializable]
    class NotEnoughResourcesException : Exception
    {
        public NotEnoughResourcesException() {}
        public NotEnoughResourcesException(string message) : base(message) {}
        public NotEnoughResourcesException(string message, Exception innerException ) : base(message, innerException) {}
        protected NotEnoughResourcesException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}
