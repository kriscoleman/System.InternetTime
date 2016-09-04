
using System;
using System.InternetTime;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using static System.Double;

namespace Tests
{
    [TestFixture]
    public class InternetTimeClientTests
    {
        const string UnableToReachServerMessage = "Unable to connect";
        Client _fakeClient;

        [SetUp]
        public void SetUp()
        {
            Func<string, double> func = NistTime.NistReponseToMillisecondsFunction;
            _fakeClient =
                A.Fake<Client>(
                    options =>
                        options.WithArgumentsForConstructor(new object[]
                        {
                            "http://0.0.0.0", // fake that we can't reach server
                            NistTime.NistMediaTypeHeaderValue,
                            func
                        }));


        }

        [Test]
        public void IfWeCantReachServerWeShouldHaveWebException()
        {
            try
            {
                _fakeClient.Get();
            }
            catch (Exception e) //catch all, for asserts will narrow scope next
            {
                Assert.That(e is AggregateException, "When failing to connect to server, did not recieve aggregate exception.");
                var aggregateException = (AggregateException) e;
                if (aggregateException.InnerExceptions.Count == 1)
                    Assert.That(aggregateException.InnerException is WebException &&
                                aggregateException.InnerException.Message.Contains(UnableToReachServerMessage) ||
                                aggregateException.InnerException.InnerException.Message.Contains(
                                    UnableToReachServerMessage), "When failing to connect to server, did not recieve the expected web exception.");
                else
                    Assert.That(
                        aggregateException.InnerExceptions.Any(
                            ex => ex is AggregateException && ex.Message.Contains(UnableToReachServerMessage)), "When failing to connect to server, did not recieve the expected web exception.");
            }
            A.CallTo(() => _fakeClient.GetAsync()).MustHaveHappened(); //we're depending on this
        }

        [Test]
        public async Task IfWeCantReachServerWeShouldHaveWebExceptionAsync()
        {
            try
            {
                await _fakeClient.GetAsync();
            }
            catch (Exception e) //catch all, for asserts will narrow scope next
            {
                Assert.That(e is AggregateException, "When failing to connect to server, did not recieve aggregate exception.");
                var aggregateException = (AggregateException)e;
                if (aggregateException.InnerExceptions.Count == 1)
                    Assert.That(aggregateException.InnerException is WebException &&
                                aggregateException.InnerException.Message.Contains(UnableToReachServerMessage) ||
                                aggregateException.InnerException.InnerException.Message.Contains(
                                    UnableToReachServerMessage), "When failing to connect to server, did not recieve the expected web exception.");
                else
                    Assert.That(
                        aggregateException.InnerExceptions.Any(
                            ex => ex is AggregateException && ex.Message.Contains(UnableToReachServerMessage)), "When failing to connect to server, did not recieve the expected web exception.");
                
            }
            A.CallTo(() => _fakeClient.ConvertMillisecondsToDateTime(NaN)).WithAnyArguments().MustNotHaveHappened(); //we're depending on this not to fire if we didn't reach server
        }
    }
}
