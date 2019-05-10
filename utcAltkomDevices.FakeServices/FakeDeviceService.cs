using Microsoft.Extensions.Options;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.FakeServices
{
    public class FakeDeviceService : FakeEntitiesService<Device>
    {
        public FakeDeviceService(IOptions<EntityOptions> options,DeviceFaker faker) : base(options,faker)
        {

        }
    }
}
