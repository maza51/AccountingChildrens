﻿using System;
namespace AccountingChildrens.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message)
            : base(message)
        {
        }
    }
}
