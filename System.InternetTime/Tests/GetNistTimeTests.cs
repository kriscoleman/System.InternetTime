using System.InternetTime;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{ 
    
    [TestFixture]
    public class GetNistTimeTests
    {

        /// <summary>
        /// Just test that we can reach the server. Gets the nist time asynchronously.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetNistTimeAsync()
        {
            var time = await NistTime.GetTimeAsync();
            Assert.That(time != null, "Could not reach server");
        }

        /// <summary>
        /// Just test that we can reach the server. Gets the nist time.
        /// </summary>
        [Test]
        public void GetNistTime()
        {
            var time = NistTime.GetTime();
            Assert.That(time != null, "Could not reach server");
        }
    }
}