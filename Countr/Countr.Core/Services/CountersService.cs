using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Countr.Core.Repositories;
using Countr.Core.Models;
using MvvmCross.Plugins.Messenger;

//the initial implementation of the counters service

namespace Countr.Core.Services
{
    public class CountersService : ICountersService
    {
        readonly ICountersRepository repository;
        readonly IMvxMessenger messenger; //a field to store a subscription token

        public CountersService (ICountersRepository repository, //The repository comes from a constructor parameter and is stored in a field
                                IMvxMessenger messenger) //the messenger comes from a constructor parameter and is stored in a field
        {
            this.repository = repository;
            this.messenger = messenger;
        }


        //Implementing the ICountersService interface
        public async Task<Counter> AddNewCounter(string name)
        {
            //A new counter is created from a name, stored in the repository, then removed.
            var counter = new Counter { Name = name };
            await repository.Save(counter).ConfigureAwait(false);
            messenger.Publish(new CountersChangedMessage(this)); //Once a counter is saved, publish the message
            return counter;
        }

        public Task<List<Counter>> GetAllCounters()
        {
            return repository.GetAll(); //Getting all counters returns all counters from the repository
        }

        public async Task DeleteCounter(Counter counter) //This method is async and awaits the Delete call
        {
            await repository.Delete(counter).ConfigureAwait(false); 
            messenger.Publish(new CountersChangedMessage(this)); //Whenever a counter is deleted, the message is published
        }

        public Task IncrementCounter(Counter counter)
        {
            //Incrementing a counter will increment the Count property and the update the counter in the repository
            counter.Count += 1;
            return repository.Save(counter);
        }


    }

}

