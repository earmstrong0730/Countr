using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Countr.Core.Repositories;
using Countr.Core.Services;
using Countr.Core.Models;
using MvvmCross.Plugins.Messenger;


namespace Countr.Core.Tests.Services
{
    [TestFixture]
    public class CountersServiceTests
    {
        ICountersService service;
        Mock<ICountersRepository> repo;
        Mock<IMvxMessenger> messenger;

        [SetUp]
        public void SetUp()
        {
            messenger = new Mock<IMvxMessenger>();
            repo = new Mock<ICountersRepository>(); //In the test fixture setup, a new mock repository is creaed so that it's ready for each test
            service = new CountersService(repo.Object, //A new instance of the CountersService created using the mock object
                                           messenger.Object); //Passes the mock to the service constructor 
        }

        [Test]
        public async Task IncrementCounter_IncrementsTheCounter() //Instead of returning void, these tests are async Task methods, so they can await async methods on the service
        {
            // Arrange
            var counter = new Counter { Count = 0 };
            // Act
            await service.IncrementCounter(counter);
            // Assert
            Assert.AreEqual(1, counter.Count); //This asserts that the counter now has a Count of 1
        }

        [Test]
        public async Task IncrementCounter_SavesTheIncrementedCounter() //Instead of returning void, these tests are async Task methods, so they can await async methods on the service
        {
            // Arrange
            var counter = new Counter { Count = 0 };
            // Act
            await service.IncrementCounter(counter);
            // Assert
            repo.Verify(r => r.Save(It.Is<Counter>(c => c.Count == 1)), //This verified that the Save method was called and successful with a counter with a Count of 1
                              Times.Once());
        }

        [Test]
        public async Task GetAllCounters_ReturnAllCountersFromTheRepository()
        {
            //Arrange
            var counters = new List<Counter>
            { new Counter {Name = "Counter1"},
              new Counter {Name = "Counter2"}
            };
            repo.Setup(r => r.GetAll()).ReturnsAsync(counters); //Sets up the GetAll method to return a defined list of counters
            //ACt
            var results = await service.GetAllCounters();
            //Assert
            CollectionAssert.AreEqual(results, counters); //Asserts that the collections contain the same items
        }

        [Test] //Testing that the emssage is published when a counter is deleted
        public async Task DeleteCounter_PubishesAMessage()
        {
            //Act
            await service.DeleteCounter(new Counter()); //Deletes a counter from the service

            //Assert
            messenger.Verify(m => m.Publish
                                    (It.IsAny<CountersChangedMessage > ())); //Verifies that the messenger publishes a message
        }
        
        
    }
}
