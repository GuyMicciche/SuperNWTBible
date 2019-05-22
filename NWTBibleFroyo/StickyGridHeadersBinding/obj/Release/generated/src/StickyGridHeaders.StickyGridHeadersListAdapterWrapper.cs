using System;
using System.Collections.Generic;
using Android.Runtime;

namespace StickyGridHeaders {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']"
	[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersListAdapterWrapper", DoNotGenerateAcw=true)]
	public partial class StickyGridHeadersListAdapterWrapper : global::Android.Widget.BaseAdapter, global::StickyGridHeaders.IStickyGridHeadersBaseAdapter {

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersListAdapterWrapper", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (StickyGridHeadersListAdapterWrapper); }
		}

		protected StickyGridHeadersListAdapterWrapper (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Landroid_widget_ListAdapter_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/constructor[@name='StickyGridHeadersListAdapterWrapper' and count(parameter)=1 and parameter[1][@type='android.widget.ListAdapter']]"
		[Register (".ctor", "(Landroid/widget/ListAdapter;)V", "")]
		public StickyGridHeadersListAdapterWrapper (global::Android.Widget.IListAdapter p0) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersListAdapterWrapper)) {
				SetHandle (global::Android.Runtime.JNIEnv.CreateInstance (GetType (), "(Landroid/widget/ListAdapter;)V", new JValue (p0)), JniHandleOwnership.TransferLocalRef);
				return;
			}

			if (id_ctor_Landroid_widget_ListAdapter_ == IntPtr.Zero)
				id_ctor_Landroid_widget_ListAdapter_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/widget/ListAdapter;)V");
			SetHandle (JNIEnv.NewObject (class_ref, id_ctor_Landroid_widget_ListAdapter_, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_getCount;
#pragma warning disable 0169
		static Delegate GetGetCountHandler ()
		{
			if (cb_getCount == null)
				cb_getCount = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetCount);
			return cb_getCount;
		}

		static int n_GetCount (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.Count;
		}
#pragma warning restore 0169

		static IntPtr id_getCount;
		public override int Count {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getCount' and count(parameter)=0]"
			[Register ("getCount", "()I", "GetGetCountHandler")]
			get {
				if (id_getCount == IntPtr.Zero)
					id_getCount = JNIEnv.GetMethodID (class_ref, "getCount", "()I");

				if (GetType () == ThresholdType)
					return JNIEnv.CallIntMethod  (Handle, id_getCount);
				else
					return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, id_getCount);
			}
		}

		static Delegate cb_getNumHeaders;
#pragma warning disable 0169
		static Delegate GetGetNumHeadersHandler ()
		{
			if (cb_getNumHeaders == null)
				cb_getNumHeaders = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetNumHeaders);
			return cb_getNumHeaders;
		}

		static int n_GetNumHeaders (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.NumHeaders;
		}
#pragma warning restore 0169

		static IntPtr id_getNumHeaders;
		public virtual int NumHeaders {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getNumHeaders' and count(parameter)=0]"
			[Register ("getNumHeaders", "()I", "GetGetNumHeadersHandler")]
			get {
				if (id_getNumHeaders == IntPtr.Zero)
					id_getNumHeaders = JNIEnv.GetMethodID (class_ref, "getNumHeaders", "()I");

				if (GetType () == ThresholdType)
					return JNIEnv.CallIntMethod  (Handle, id_getNumHeaders);
				else
					return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, id_getNumHeaders);
			}
		}

		static Delegate cb_getCountForHeader_I;
#pragma warning disable 0169
		static Delegate GetGetCountForHeader_IHandler ()
		{
			if (cb_getCountForHeader_I == null)
				cb_getCountForHeader_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, int>) n_GetCountForHeader_I);
			return cb_getCountForHeader_I;
		}

		static int n_GetCountForHeader_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetCountForHeader (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getCountForHeader_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getCountForHeader' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getCountForHeader", "(I)I", "GetGetCountForHeader_IHandler")]
		public virtual int GetCountForHeader (int p0)
		{
			if (id_getCountForHeader_I == IntPtr.Zero)
				id_getCountForHeader_I = JNIEnv.GetMethodID (class_ref, "getCountForHeader", "(I)I");

			if (GetType () == ThresholdType)
				return JNIEnv.CallIntMethod  (Handle, id_getCountForHeader_I, new JValue (p0));
			else
				return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, id_getCountForHeader_I, new JValue (p0));
		}

		static Delegate cb_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
#pragma warning disable 0169
		static Delegate GetGetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_Handler ()
		{
			if (cb_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ == null)
				cb_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr, IntPtr, IntPtr>) n_GetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_);
			return cb_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
		}

		static IntPtr n_GetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ (IntPtr jnienv, IntPtr native__this, int p0, IntPtr native_p1, IntPtr native_p2)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetHeaderView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getHeaderView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
		[Register ("getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;", "GetGetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_Handler")]
		public virtual global::Android.Views.View GetHeaderView (int p0, global::Android.Views.View p1, global::Android.Views.ViewGroup p2)
		{
			if (id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ == IntPtr.Zero)
				id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNIEnv.GetMethodID (class_ref, "getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;");

			global::Android.Views.View __ret;
			if (GetType () == ThresholdType)
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			else
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_getItem_I;
#pragma warning disable 0169
		static Delegate GetGetItem_IHandler ()
		{
			if (cb_getItem_I == null)
				cb_getItem_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_GetItem_I);
			return cb_getItem_I;
		}

		static IntPtr n_GetItem_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetItem (p0));
		}
#pragma warning restore 0169

		static IntPtr id_getItem_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getItem' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getItem", "(I)Ljava/lang/Object;", "GetGetItem_IHandler")]
		public override global::Java.Lang.Object GetItem (int p0)
		{
			if (id_getItem_I == IntPtr.Zero)
				id_getItem_I = JNIEnv.GetMethodID (class_ref, "getItem", "(I)Ljava/lang/Object;");

			if (GetType () == ThresholdType)
				return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getItem_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
			else
				return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, id_getItem_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_getItemId_I;
#pragma warning disable 0169
		static Delegate GetGetItemId_IHandler ()
		{
			if (cb_getItemId_I == null)
				cb_getItemId_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, long>) n_GetItemId_I);
			return cb_getItemId_I;
		}

		static long n_GetItemId_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetItemId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getItemId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getItemId' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getItemId", "(I)J", "GetGetItemId_IHandler")]
		public override long GetItemId (int p0)
		{
			if (id_getItemId_I == IntPtr.Zero)
				id_getItemId_I = JNIEnv.GetMethodID (class_ref, "getItemId", "(I)J");

			if (GetType () == ThresholdType)
				return JNIEnv.CallLongMethod  (Handle, id_getItemId_I, new JValue (p0));
			else
				return JNIEnv.CallNonvirtualLongMethod  (Handle, ThresholdClass, id_getItemId_I, new JValue (p0));
		}

		static Delegate cb_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
#pragma warning disable 0169
		static Delegate GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler ()
		{
			if (cb_getView_ILandroid_view_View_Landroid_view_ViewGroup_ == null)
				cb_getView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr, IntPtr, IntPtr>) n_GetView_ILandroid_view_View_Landroid_view_ViewGroup_);
			return cb_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
		}

		static IntPtr n_GetView_ILandroid_view_View_Landroid_view_ViewGroup_ (IntPtr jnienv, IntPtr native__this, int p0, IntPtr native_p1, IntPtr native_p2)
		{
			global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersListAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersListAdapterWrapper']/method[@name='getView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
		[Register ("getView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;", "GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler")]
		public override global::Android.Views.View GetView (int p0, global::Android.Views.View p1, global::Android.Views.ViewGroup p2)
		{
			if (id_getView_ILandroid_view_View_Landroid_view_ViewGroup_ == IntPtr.Zero)
				id_getView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNIEnv.GetMethodID (class_ref, "getView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;");

			global::Android.Views.View __ret;
			if (GetType () == ThresholdType)
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			else
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, id_getView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

	}
}
