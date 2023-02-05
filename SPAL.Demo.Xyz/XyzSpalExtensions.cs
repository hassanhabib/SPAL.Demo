using SPAL.Demo.Abstractions;

namespace SPAL.Demo.Xyz
{
    public static class XyzSpalExtensions
    {
        public static ISpal UseXyz(this ISpal spalModel) =>
            new XyzSpal();
    }
}
