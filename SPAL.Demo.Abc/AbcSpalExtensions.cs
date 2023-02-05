using SPAL.Demo.Abstractions;

namespace SPAL.Demo.Abc
{
    public static class AbcSpalExtensions
    {
        public static ISpal UseAbc(this ISpal spalModel) =>
            new AbcSpal();
    }
}
