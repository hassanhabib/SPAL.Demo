using SPAL.Demo.Abstractions;

namespace SPAL.Demo.Xyz
{
    public class XyzSpal : ISpal
    {
        public void DoSomething<T>(T input) =>
            Console.Write("Doing from Xyz");
    }
}