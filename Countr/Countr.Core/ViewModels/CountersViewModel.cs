using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Core.ViewModels;
using Countr.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using Countr.Core.Models;
using MvvmCross.Core.Navigation;

//This viewmodel represents a list of counters, each represented by CountrViewModel instances that wrap each counter
//It is the "view all counters" view
//adding another comment to change another file

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel //This view model is derived from the mvvmCross base view model
    {
        readonly ICountersService service; //The constructor takes an IcountersService instance, which will be used to get all the counters
        readonly MvxSubscriptionToken token; //A field to store subscription token
        readonly IMvxNavigationService navigationService;

        public CountersViewModel(ICountersService service,
                                 IMvxMessenger messenger, //the messenger come from a construcot parameter
                                 IMvxNavigationService navigationService) //Injects and stores an instance of the MvvmCross navigation service
        {
            token = messenger
                .SubscribeOnMainThread<CountersChangedMessage>
                (async m => await LoadCounters()); //Subscribes to all CountersChangedMessage messages on the UI thread

            this.service = service;
            Counters = new ObservableCollection<CountrViewModel>(); //The Counters property is an ObservableCollection of CounterViewModel

            this.navigationService = navigationService;
            ShowAddNewCounterCommand = new MvxAsyncCommand(ShowAddNewCounter); //Creates the new command
        }

        public ObservableCollection<CountrViewModel> Counters { get; }

        //Listing 8.21 page 265, Creating counter view models from counters loaded from service
        public override async Task Initialize() //Initialize comes from the MvxViewModel base class, and it waits another method
        {
            await LoadCounters();
        }

        public async Task LoadCounters() //LoadCounters loads the counters from the service and populates the observable collection with the view models prepared with counters
        {
            Counters.Clear();//LoadCounters has been tweaked to clear all counters before reloading
            var counters = await service.GetAllCounters();
            foreach (var counter in counters) //for each counter, create an instance of CountrViewModel, prepared using that counter and add to observable collection
            {
                var viewModel = new CountrViewModel(service,  //The service is passed to the constructor of the CountrViewModel
                                                    navigationService); //Passes the navigation service through to the CounterViewModel constructor
                viewModel.Prepare(counter);
                Counters.Add(viewModel);

            }
        }
        public IMvxAsyncCommand ShowAddNewCounterCommand { get; } // A public property for the new command

        async Task ShowAddNewCounter()
        {
            await navigationService.Navigate(typeof(CountrViewModel),
                                             new Counter()); // Shows the counter view model, initialized with a new counter


        }
    }
}
