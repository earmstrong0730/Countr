using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Countr.Core.Models;

//The interface for the counters service

namespace Countr.Core.Services
{
    public interface ICountersService
    {
        Task<Counter> AddNewCounter(string name);

        //Methods to create, delete and get all the counters
        Task<List<Counter>> GetAllCounters();
        Task DeleteCounter(Counter counter);
        Task IncrementCounter(Counter counter); // a method to increment the counter
    }
}
