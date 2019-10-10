using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Countr.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;

//the counters master view 
namespace Countr.Droid.Views
{
    [Activity(Label = "CountersView")]
    public class CountersView : MvxAppCompatActivity<CountersViewModel>  //Uses the generic version of the MvvmCross activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.counters_view); 

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view); //Finds the recycler view in the UI, and sets its layout manager
            recyclerView.SetLayoutManager(new LinearLayoutManager(this)); //Layout managers arranges items in a verticle list
        }
    }
}