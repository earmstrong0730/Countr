using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Countr.Core.ViewModels;

namespace Countr.Droid.Views
{
    public class SwipeItemTouchHelperCallback : ItemTouchHelper.SimpleCallback //This derives from ItemTouchHelper.Callback, the base callback class
    {
        readonly CountersViewModel viewmodel;

        public SwipeItemTouchHelperCallback(CountersViewModel viewModel) //Stores an instance of the CountersViewModel that you can use to delete a counter
          : base(0, ItemTouchHelper.Start) //Specifies the supported swipe directions
        {
            this.viewModel = viewModel;
        }

    }
}