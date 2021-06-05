using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.Configurations.CustomExceptions
{
    public class NullRegulationSectionHeadingException: Exception
    {
        public NullRegulationSectionHeadingException()
        {
        }

        public NullRegulationSectionHeadingException(string message)
            : base(message)
        {
        }

        public NullRegulationSectionHeadingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
