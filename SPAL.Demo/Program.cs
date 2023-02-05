using SPAL.Demo.Abstractions;
using SPAL.Demo.Brokers;
using SPAL.Demo.Xyz;

public class Program
{
    static void Main(string[] args)
    {
        var broker = new SpalBroker();
        broker.DoSomething();
    }
}
