using System;

namespace SFA.ROATP.Types.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
