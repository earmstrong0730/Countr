using System;
using System.Collections.Generic;
using System.Text;
using Countr.Core.Models;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using Countr.Core.Services;
using MvvmCross.Core.Navigation;


//This viewmodel provides state for the name and count of a counter, backed up by an instance of the Counter data model from the model layer.
//It is the "add new counter" view

namespace Countr.Core.ViewModels
{
    
    //The implementaation of CountrViewModel wraps a counter
    public class CountrViewModel : MvxViewModel<Counter> //This view model derives from MvxViewModel
    {
        Counter counter; //the view model uses an instace of Counter to hold the state

        public override void Prepare(Counter counter)
        {
            this.counter = counter;
        }


        //Wrapping the properties on the underlying counter
        public string Name
        {
            get { return counter.Name; } //The Name property getter returns the value from the counter
            set
            {
                if (Name == value) return; //the name property setter checks to see if the value has actually changed, and if so sets the value on the counter and raises a property - changed information
                counter.Name = value;
                RaisePropertyChanged();
            }
        }
        public int Count => counter.Count; //the Count property is read-only, so it only has a getter that reutrns the value from the counter

        readonly ICountersService service; //a readonly field to store the ICountersService
        readonly IMvxNavigationService navigationService; //Injects and stores an instance of the MvvmCross navigation service
        
        public CountrViewModel(ICountersService service, //The counters service is passed in as a constructor parameter and stored in the backing field
                               IMvxNavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;

            IncrementCommand = new MvxAsyncCommand(IncrementCounter); //Created a new MvxAsyncCommand wrapping a method
            DeleteCommand = new MvxAsyncCommand(DeleteCounter); //Creates a new MvxAsyncCommand for deleting counters

            CancelCommand = new MvxAsyncCommand(Cancel);
            SaveCommand = new MvxAsyncCommand(Save);

        }

        public IMvxAsyncCommand CancelCommand { get; } //A public property that exposes the command
        public IMvxAsyncCommand SaveCommand { get; }
        public IMvxAsyncCommand IncrementCommand { get; }

        async Task Cancel()
        {
            await navigationService.Close(this); //Closes the viewmodel, removing the view from the stack
        }

        async Task Save() //Adds a new counter and then closes the view model
        {
            await service.AddNewCounter(counter.Name); 
            await navigationService.Close(this);
        }

        async Task IncrementCounter() //The method called by the command increments the counter using the service and then raises a property-changed notification for the count
        {
            await service.IncrementCounter(counter); 
            RaisePropertyChanged(() => Count);
        }

        public IMvxAsyncCommand DeleteCommand { get; } //A public property that exposes the command
        async Task DeleteCounter()
        {
            await service.DeleteCounter(counter); //The command deletes the counter from the service

        }

    }
}
