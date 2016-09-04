using System.InternetTime;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GetNistTimeTests
    {

        [Test]
        public async Task GetNistTimeAsync()
        {
            var time = await InternetTime.GetNistTimeAsync();
            Assert.That(time != null, "Could not reach server");
        }

        [Test]
        public void GetNistTime()
        {
            var time = InternetTime.GetNistTime();
            Assert.That(time != null, "Could not reach server");
        }
    }
}