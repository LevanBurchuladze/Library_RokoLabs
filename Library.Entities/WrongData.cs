
using System;

namespace Library.Entities
{
    public class WrongData : Exception
    {
        public WrongData(string message) : base(message) { }
    }
}
