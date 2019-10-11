using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using Countr.Core.ViewModels;


namespace Countr.Droid.Views
{
    [Activity(Label = "Add a new counter")]
    public class CountrView : MvxAppCompatActivity<CountrViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.countr_view);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true); //Shows the Up button
        }

        public override bool OnOptionsItemSelected(IMenuItem item) //Overrides the OnoptionsItemSelected method
        {
         switch (item.ItemId) //ItemId is the ID of the menu item
            {
                case Android.Resource.Id.Home: //Android.Resource.Id.Home is the ID of hte Up button, and it comes from the Android SDK
                    ViewModel.CancelCommand.Execute(null);
                    return true;
                case Resource.Id.action_save_counter: //If the Done menu item is tapped, execute the save command
                    ViewModel.SaveCommand.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);

            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu) //Overrides the options menu creation
        {
            base.OnCreateOptionsMenu(menu);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar); //Finds the toolbar in the UI
            toolbar.InflateMenu(Resource.Menu.new_counter_menu); //inflates the new counter menu resource
            return true;
        }
    }
}