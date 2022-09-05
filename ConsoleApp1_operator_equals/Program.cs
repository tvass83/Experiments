using System;

namespace ConsoleApp1_operator_equals
{
    class Program
    {
        static void Main(string[] args)
        {
            Tom tom1 = new Tom(1);
            Tom tom2 = new Tom(1);
            Console.WriteLine(tom1 == tom2);
            Console.WriteLine(tom1.Equals(tom2));
        }
    }

    public class Tom : IEquatable<Tom>
    {
        private readonly int _nr;

        public Tom(int nr)
        {
            _nr = nr;
        }

        public bool Equals(Tom other)
        {
            if (other == null)
            {
                return false;
            }

            return _nr == other._nr;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Tom);
        }

        public static bool operator ==(Tom first, Tom second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Tom first, Tom second)
        {
            return !first.Equals(second);
        }
    }
}
