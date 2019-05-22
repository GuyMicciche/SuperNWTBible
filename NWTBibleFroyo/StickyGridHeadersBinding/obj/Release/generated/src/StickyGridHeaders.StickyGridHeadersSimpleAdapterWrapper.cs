using System;
using System.Collections.Generic;
using Android.Runtime;

namespace StickyGridHeaders {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']"
	[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper", DoNotGenerateAcw=true)]
	public partial class StickyGridHeadersSimpleAdapterWrapper : global::Android.Widget.BaseAdapter, global::StickyGridHeaders.IStickyGridHeadersBaseAdapter {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.DataSetObserverExtension']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper$DataSetObserverExtension", DoNotGenerateAcw=true)]
		public sealed partial class DataSetObserverExtension : global::Android.Database.DataSetObserver {

			internal DataSetObserverExtension (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper$HeaderData", DoNotGenerateAcw=true)]
		public partial class HeaderData : global::Java.Lang.Object {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper$HeaderData", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (HeaderData); }
			}

			protected HeaderData (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapterWrapper_I;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData']/constructor[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData' and count(parameter)=2 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersSimpleAdapterWrapper'] and parameter[2][@type='int']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper;I)V", "")]
			public HeaderData (global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __self, int p1) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderData)) {
					SetHandle (global::Android.Runtime.JNIEnv.CreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";I)V", new JValue (__self), new JValue (p1)), JniHandleOwnership.TransferLocalRef);
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapterWrapper_I == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapterWrapper_I = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper;I)V");
				SetHandle (JNIEnv.NewObject (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapterWrapper_I, new JValue (__self), new JValue (p1)), JniHandleOwnership.TransferLocalRef);
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
				global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData> (native__this, JniHandleOwnership.DoNotTransfer);
				return __this.Count;
			}
#pragma warning restore 0169

			static IntPtr id_getCount;
			public virtual int Count {
				// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData']/method[@name='getCount' and count(parameter)=0]"
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

			static Delegate cb_getRefPosition;
#pragma warning disable 0169
			static Delegate GetGetRefPositionHandler ()
			{
				if (cb_getRefPosition == null)
					cb_getRefPosition = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetRefPosition);
				return cb_getRefPosition;
			}

			static int n_GetRefPosition (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData> (native__this, JniHandleOwnership.DoNotTransfer);
				return __this.RefPosition;
			}
#pragma warning restore 0169

			static IntPtr id_getRefPosition;
			public virtual int RefPosition {
				// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData']/method[@name='getRefPosition' and count(parameter)=0]"
				[Register ("getRefPosition", "()I", "GetGetRefPositionHandler")]
				get {
					if (id_getRefPosition == IntPtr.Zero)
						id_getRefPosition = JNIEnv.GetMethodID (class_ref, "getRefPosition", "()I");

					if (GetType () == ThresholdType)
						return JNIEnv.CallIntMethod  (Handle, id_getRefPosition);
					else
						return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, id_getRefPosition);
				}
			}

			static Delegate cb_incrementCount;
#pragma warning disable 0169
			static Delegate GetIncrementCountHandler ()
			{
				if (cb_incrementCount == null)
					cb_incrementCount = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_IncrementCount);
				return cb_incrementCount;
			}

			static void n_IncrementCount (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper.HeaderData> (native__this, JniHandleOwnership.DoNotTransfer);
				__this.IncrementCount ();
			}
#pragma warning restore 0169

			static IntPtr id_incrementCount;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper.HeaderData']/method[@name='incrementCount' and count(parameter)=0]"
			[Register ("incrementCount", "()V", "GetIncrementCountHandler")]
			public virtual void IncrementCount ()
			{
				if (id_incrementCount == IntPtr.Zero)
					id_incrementCount = JNIEnv.GetMethodID (class_ref, "incrementCount", "()V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_incrementCount);
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, id_incrementCount);
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (StickyGridHeadersSimpleAdapterWrapper); }
		}

		protected StickyGridHeadersSimpleAdapterWrapper (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapter_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/constructor[@name='StickyGridHeadersSimpleAdapterWrapper' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersSimpleAdapter']]"
		[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapter;)V", "")]
		public StickyGridHeadersSimpleAdapterWrapper (global::StickyGridHeaders.IStickyGridHeadersSimpleAdapter p0) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersSimpleAdapterWrapper)) {
				SetHandle (global::Android.Runtime.JNIEnv.CreateInstance (GetType (), "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapter;)V", new JValue (p0)), JniHandleOwnership.TransferLocalRef);
				return;
			}

			if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapter_ == IntPtr.Zero)
				id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapter_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapter;)V");
			SetHandle (JNIEnv.NewObject (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleAdapter_, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.Count;
		}
#pragma warning restore 0169

		static IntPtr id_getCount;
		public override int Count {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getCount' and count(parameter)=0]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.NumHeaders;
		}
#pragma warning restore 0169

		static IntPtr id_getNumHeaders;
		public virtual int NumHeaders {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getNumHeaders' and count(parameter)=0]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetCountForHeader (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getCountForHeader_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getCountForHeader' and count(parameter)=1 and parameter[1][@type='int']]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetHeaderView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getHeaderView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetItem (p0));
		}
#pragma warning restore 0169

		static IntPtr id_getItem_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getItem' and count(parameter)=1 and parameter[1][@type='int']]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetItemId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getItemId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getItemId' and count(parameter)=1 and parameter[1][@type='int']]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper> (native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleAdapterWrapper']/method[@name='getView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
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
