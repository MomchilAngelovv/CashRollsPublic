namespace CashRolls.Web.Exceptions
{
    using System;

    public class StripePaymentException : Exception
    {
        public StripePaymentException(string message)
            : base(message)
        {

        }
    }
}
