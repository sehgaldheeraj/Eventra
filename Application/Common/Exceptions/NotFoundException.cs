﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string entityName, object key)
        : base($"{entityName} with ID '{key}' was not found.") { }
    }
}
