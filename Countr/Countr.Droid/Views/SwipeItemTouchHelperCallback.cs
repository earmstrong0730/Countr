using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Countr.Core.ViewModels;

namespace Countr.Droid.Views
{
    public class SwipeItemTouchHelperCallback : ItemTouchHelper.SimpleCallback //This derives from ItemTouchHelper.Callback, the base callback class
    {
        readonly CountersViewModel viewModel;

        public SwipeItemTouchHelperCallback(CountersViewModel viewModel) //Stores an instance of the CountersViewModel that you can use to delete a counter
          : base(0, ItemTouchHelper.Start) //Specifies the supported swipe directions
        {
            this.viewModel = viewModel;
        }

        public override bool OnMove(RecyclerView recyclerView,
                            RecyclerView.ViewHolder viewHolder,
                            RecyclerView.ViewHolder target)
        {
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            viewModel.Counters[viewHolder.AdapterPosition].DeleteCommand.Execute(); //When an item is swiped, delete it
        }

        readonly Drawable background = new ColorDrawable(Color.Red); //Show a red background underneath the item being sweeped

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView,
                                         RecyclerView.ViewHolder viewHolder,
                                         float dX, float dY, int actionState,
                                         bool isCurrentlyActive)
        {
            background.SetBounds(viewHolder.ItemView.Right + (int)dX,
                                  viewHolder.ItemView.Top,
                                  viewHolder.ItemView.Right,
                                  viewHolder.ItemView.Bottom);
            background.Draw(c);

            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY,
                             actionState, isCurrentlyActive);
        }

    }
}