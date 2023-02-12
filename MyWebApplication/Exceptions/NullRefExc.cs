using System;

namespace MyWebApplication.Exceptions
{
    public class NullRefExc : Exception
    {
        public NullRefExc(string Msg) : base()
        {
            Console.WriteLine(Msg);
        }

        public NullRefExc(string Msg, uint codeError) : base()
        {
            CodeError = codeError;
            Console.WriteLine(Msg);
        }

        public uint CodeError { get; }
    }
}