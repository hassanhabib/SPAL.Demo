using SPAL.Demo.Abstractions;

namespace SPAL.Demo.Abc
{
    public class AbcSpal : ISpal
    {
        public void DoSomething() =>
            Console.WriteLine("Doing from Abc");
    }
}