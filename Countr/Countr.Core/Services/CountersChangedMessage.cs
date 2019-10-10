using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugins.Messenger;

// A message you can publish, telling anyone that the counters have changed

namespace Countr.Core.Services
{
   public class CountersChangedMessage : MvxMessage
    {
        public CountersChangedMessage(object sender)
            : base(sender)
        { }
    }
}
