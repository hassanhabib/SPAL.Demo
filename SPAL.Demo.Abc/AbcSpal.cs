using SPAL.Demo.Abstractions;

namespace SPAL.Demo.Abc
{
    public class AbcSpal : ISpal
    {
        public void DoSomething<T>(T input) =>
            Console.WriteLine("Doing from Abc");
    }
}