using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPAL.Demo.Abc;
using SPAL.Demo.Abstractions;
using SPAL.Demo.Xyz;

namespace SPAL.Demo.Brokers
{
    public class SpalBroker
    {
        private readonly ISpal client;

        public SpalBroker() =>
            this.client = IntializeSpal();

        public void DoSomething() =>
            this.client.DoSomething();

        private ISpal IntializeSpal() =>
            this.client.UseXyz();
    }
}
