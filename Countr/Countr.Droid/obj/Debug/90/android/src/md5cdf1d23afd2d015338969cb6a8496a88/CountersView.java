package md5cdf1d23afd2d015338969cb6a8496a88;


public class CountersView
	extends md5716162e2cd7f7ce01364d7c5d961f40b.MvxAppCompatActivity_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Countr.Droid.Views.CountersView, Countr.Droid", CountersView.class, __md_methods);
	}


	public CountersView ()
	{
		super ();
		if (getClass () == CountersView.class)
			mono.android.TypeManager.Activate ("Countr.Droid.Views.CountersView, Countr.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
