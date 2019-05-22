using System;
using System.Collections.Generic;
using Android.Runtime;

namespace StickyGridHeaders {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']"
	[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView", DoNotGenerateAcw=true)]
	public partial class StickyGridHeadersGridView : global::Android.Widget.GridView, global::Android.Widget.AbsListView.IOnScrollListener, global::Android.Widget.AdapterView.IOnItemClickListener, global::Android.Widget.AdapterView.IOnItemLongClickListener, global::Android.Widget.AdapterView.IOnItemSelectedListener {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='TOUCH_MODE_DONE_WAITING']"
		[Register ("TOUCH_MODE_DONE_WAITING")]
		protected const int TouchModeDoneWaiting = (int) 2;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='TOUCH_MODE_DOWN']"
		[Register ("TOUCH_MODE_DOWN")]
		protected const int TouchModeDown = (int) 0;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='TOUCH_MODE_FINISHED_LONG_PRESS']"
		[Register ("TOUCH_MODE_FINISHED_LONG_PRESS")]
		protected const int TouchModeFinishedLongPress = (int) -2;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='TOUCH_MODE_REST']"
		[Register ("TOUCH_MODE_REST")]
		protected const int TouchModeRest = (int) -1;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='TOUCH_MODE_TAP']"
		[Register ("TOUCH_MODE_TAP")]
		protected const int TouchModeTap = (int) 1;

		static IntPtr mAdapter_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mAdapter']"
		[Register ("mAdapter")]
		protected global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper MAdapter {
			get {
				if (mAdapter_jfieldId == IntPtr.Zero)
					mAdapter_jfieldId = JNIEnv.GetFieldID (class_ref, "mAdapter", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;");
				IntPtr __ret = JNIEnv.GetObjectField (Handle, mAdapter_jfieldId);
				return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (mAdapter_jfieldId == IntPtr.Zero)
					mAdapter_jfieldId = JNIEnv.GetFieldID (class_ref, "mAdapter", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;");
				IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
				JNIEnv.SetField (Handle, mAdapter_jfieldId, native_value);
				JNIEnv.DeleteLocalRef (native_value);
			}
		}

		static IntPtr mDataChanged_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mDataChanged']"
		[Register ("mDataChanged")]
		protected bool MDataChanged {
			get {
				if (mDataChanged_jfieldId == IntPtr.Zero)
					mDataChanged_jfieldId = JNIEnv.GetFieldID (class_ref, "mDataChanged", "Z");
				return JNIEnv.GetBooleanField (Handle, mDataChanged_jfieldId);
			}
			set {
				if (mDataChanged_jfieldId == IntPtr.Zero)
					mDataChanged_jfieldId = JNIEnv.GetFieldID (class_ref, "mDataChanged", "Z");
				JNIEnv.SetField (Handle, mDataChanged_jfieldId, value);
			}
		}

		static IntPtr mMotionHeaderPosition_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mMotionHeaderPosition']"
		[Register ("mMotionHeaderPosition")]
		protected int MMotionHeaderPosition {
			get {
				if (mMotionHeaderPosition_jfieldId == IntPtr.Zero)
					mMotionHeaderPosition_jfieldId = JNIEnv.GetFieldID (class_ref, "mMotionHeaderPosition", "I");
				return JNIEnv.GetIntField (Handle, mMotionHeaderPosition_jfieldId);
			}
			set {
				if (mMotionHeaderPosition_jfieldId == IntPtr.Zero)
					mMotionHeaderPosition_jfieldId = JNIEnv.GetFieldID (class_ref, "mMotionHeaderPosition", "I");
				JNIEnv.SetField (Handle, mMotionHeaderPosition_jfieldId, value);
			}
		}

		static IntPtr mPendingCheckForLongPress_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mPendingCheckForLongPress']"
		[Register ("mPendingCheckForLongPress")]
		public global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderLongPress MPendingCheckForLongPress {
			get {
				if (mPendingCheckForLongPress_jfieldId == IntPtr.Zero)
					mPendingCheckForLongPress_jfieldId = JNIEnv.GetFieldID (class_ref, "mPendingCheckForLongPress", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderLongPress;");
				IntPtr __ret = JNIEnv.GetObjectField (Handle, mPendingCheckForLongPress_jfieldId);
				return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderLongPress> (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (mPendingCheckForLongPress_jfieldId == IntPtr.Zero)
					mPendingCheckForLongPress_jfieldId = JNIEnv.GetFieldID (class_ref, "mPendingCheckForLongPress", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderLongPress;");
				IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
				JNIEnv.SetField (Handle, mPendingCheckForLongPress_jfieldId, native_value);
				JNIEnv.DeleteLocalRef (native_value);
			}
		}

		static IntPtr mPendingCheckForTap_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mPendingCheckForTap']"
		[Register ("mPendingCheckForTap")]
		public global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderTap MPendingCheckForTap {
			get {
				if (mPendingCheckForTap_jfieldId == IntPtr.Zero)
					mPendingCheckForTap_jfieldId = JNIEnv.GetFieldID (class_ref, "mPendingCheckForTap", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderTap;");
				IntPtr __ret = JNIEnv.GetObjectField (Handle, mPendingCheckForTap_jfieldId);
				return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderTap> (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (mPendingCheckForTap_jfieldId == IntPtr.Zero)
					mPendingCheckForTap_jfieldId = JNIEnv.GetFieldID (class_ref, "mPendingCheckForTap", "Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderTap;");
				IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
				JNIEnv.SetField (Handle, mPendingCheckForTap_jfieldId, native_value);
				JNIEnv.DeleteLocalRef (native_value);
			}
		}

		static IntPtr mTouchMode_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/field[@name='mTouchMode']"
		[Register ("mTouchMode")]
		protected int MTouchMode {
			get {
				if (mTouchMode_jfieldId == IntPtr.Zero)
					mTouchMode_jfieldId = JNIEnv.GetFieldID (class_ref, "mTouchMode", "I");
				return JNIEnv.GetIntField (Handle, mTouchMode_jfieldId);
			}
			set {
				if (mTouchMode_jfieldId == IntPtr.Zero)
					mTouchMode_jfieldId = JNIEnv.GetFieldID (class_ref, "mTouchMode", "I");
				JNIEnv.SetField (Handle, mTouchMode_jfieldId, value);
			}
		}
		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.CheckForHeaderLongPress']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderLongPress", DoNotGenerateAcw=true)]
		public partial class CheckForHeaderLongPress : global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable, global::Java.Lang.IRunnable {

			internal static new IntPtr java_class_handle;
			internal static new IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderLongPress", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (CheckForHeaderLongPress); }
			}

			protected CheckForHeaderLongPress (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_run;
#pragma warning disable 0169
			static Delegate GetRunHandler ()
			{
				if (cb_run == null)
					cb_run = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Run);
				return cb_run;
			}

			static void n_Run (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderLongPress __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.CheckForHeaderLongPress> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				__this.Run ();
			}
#pragma warning restore 0169

			static IntPtr id_run;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.CheckForHeaderLongPress']/method[@name='run' and count(parameter)=0]"
			[Register ("run", "()V", "GetRunHandler")]
			public virtual void Run ()
			{
				if (id_run == IntPtr.Zero)
					id_run = JNIEnv.GetMethodID (class_ref, "run", "()V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_run);
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "run", "()V"));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.CheckForHeaderTap']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderTap", DoNotGenerateAcw=true)]
		public sealed partial class CheckForHeaderTap : global::Java.Lang.Object, global::Java.Lang.IRunnable {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderTap", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (CheckForHeaderTap); }
			}

			internal CheckForHeaderTap (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_run;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.CheckForHeaderTap']/method[@name='run' and count(parameter)=0]"
			[Register ("run", "()V", "")]
			public void Run ()
			{
				if (id_run == IntPtr.Zero)
					id_run = JNIEnv.GetMethodID (class_ref, "run", "()V");
				JNIEnv.CallVoidMethod  (Handle, id_run);
			}

		}

		// Metadata.xml XPath interface reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/interface[@name='StickyGridHeadersGridView.OnHeaderClickListener']"
		[Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener", "", "StickyGridHeaders.StickyGridHeadersGridView/IOnHeaderClickListenerInvoker")]
		public partial interface IOnHeaderClickListener : IJavaObject {

			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/interface[@name='StickyGridHeadersGridView.OnHeaderClickListener']/method[@name='onHeaderClick' and count(parameter)=3 and parameter[1][@type='android.widget.AdapterView'] and parameter[2][@type='android.view.View'] and parameter[3][@type='long']]"
			[Register ("onHeaderClick", "(Landroid/widget/AdapterView;Landroid/view/View;J)V", "GetOnHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_JHandler:StickyGridHeaders.StickyGridHeadersGridView/IOnHeaderClickListenerInvoker, StickyGridHeadersBinding")]
			void OnHeaderClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2);

		}

		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener", DoNotGenerateAcw=true)]
		internal class IOnHeaderClickListenerInvoker : global::Java.Lang.Object, IOnHeaderClickListener {

			static IntPtr java_class_ref = JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener");
			IntPtr class_ref;

			public static IOnHeaderClickListener GetObject (IntPtr handle, JniHandleOwnership transfer)
			{
				return global::Java.Lang.Object.GetObject<IOnHeaderClickListener> (handle, transfer);
			}

			static IntPtr Validate (IntPtr handle)
			{
				if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
					throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
								JNIEnv.GetClassNameFromInstance (handle), "com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView.OnHeaderClickListener"));
				return handle;
			}

			protected override void Dispose (bool disposing)
			{
				if (this.class_ref != IntPtr.Zero)
					JNIEnv.DeleteGlobalRef (this.class_ref);
				this.class_ref = IntPtr.Zero;
				base.Dispose (disposing);
			}

			public IOnHeaderClickListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
			{
				IntPtr local_ref = JNIEnv.GetObjectClass (Handle);
				this.class_ref = JNIEnv.NewGlobalRef (local_ref);
				JNIEnv.DeleteLocalRef (local_ref);
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override System.Type ThresholdType {
				get { return typeof (IOnHeaderClickListenerInvoker); }
			}

			static Delegate cb_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J;
#pragma warning disable 0169
			static Delegate GetOnHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_JHandler ()
			{
				if (cb_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J == null)
					cb_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, long>) n_OnHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J);
				return cb_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J;
			}

			static void n_OnHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, long p2)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
				global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
				__this.OnHeaderClick (p0, p1, p2);
			}
#pragma warning restore 0169

			IntPtr id_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J;
			public void OnHeaderClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				if (id_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J == IntPtr.Zero)
					id_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J = JNIEnv.GetMethodID (class_ref, "onHeaderClick", "(Landroid/widget/AdapterView;Landroid/view/View;J)V");
				JNIEnv.CallVoidMethod (Handle, id_onHeaderClick_Landroid_widget_AdapterView_Landroid_view_View_J, new JValue (p0), new JValue (p1), new JValue (p2));
			}

		}

		public partial class HeaderClickEventArgs : global::System.EventArgs {

			public HeaderClickEventArgs (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				this.p0 = p0;
				this.p1 = p1;
				this.p2 = p2;
			}

			global::Android.Widget.AdapterView p0;
			public global::Android.Widget.AdapterView P0 {
				get { return p0; }
			}

			global::Android.Views.View p1;
			public global::Android.Views.View P1 {
				get { return p1; }
			}

			long p2;
			public long P2 {
				get { return p2; }
			}
		}

		[global::Android.Runtime.Register ("mono/com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView_OnHeaderClickListenerImplementor")]
		internal sealed class IOnHeaderClickListenerImplementor : global::Java.Lang.Object, IOnHeaderClickListener {

			object sender;

			public IOnHeaderClickListenerImplementor (object sender)
				: base (
					global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView_OnHeaderClickListenerImplementor", "()V"),
					JniHandleOwnership.TransferLocalRef)
			{
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "()V");
				this.sender = sender;
			}

#pragma warning disable 0649
			public EventHandler<HeaderClickEventArgs> Handler;
#pragma warning restore 0649

			public void OnHeaderClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				var __h = Handler;
				if (__h != null)
					__h (sender, new HeaderClickEventArgs (p0, p1, p2));
			}

			internal static bool __IsEmpty (IOnHeaderClickListenerImplementor value)
			{
				return value.Handler == null;
			}
		}


		// Metadata.xml XPath interface reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/interface[@name='StickyGridHeadersGridView.OnHeaderLongClickListener']"
		[Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener", "", "StickyGridHeaders.StickyGridHeadersGridView/IOnHeaderLongClickListenerInvoker")]
		public partial interface IOnHeaderLongClickListener : IJavaObject {

			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/interface[@name='StickyGridHeadersGridView.OnHeaderLongClickListener']/method[@name='onHeaderLongClick' and count(parameter)=3 and parameter[1][@type='android.widget.AdapterView'] and parameter[2][@type='android.view.View'] and parameter[3][@type='long']]"
			[Register ("onHeaderLongClick", "(Landroid/widget/AdapterView;Landroid/view/View;J)Z", "GetOnHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_JHandler:StickyGridHeaders.StickyGridHeadersGridView/IOnHeaderLongClickListenerInvoker, StickyGridHeadersBinding")]
			bool OnHeaderLongClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2);

		}

		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener", DoNotGenerateAcw=true)]
		internal class IOnHeaderLongClickListenerInvoker : global::Java.Lang.Object, IOnHeaderLongClickListener {

			static IntPtr java_class_ref = JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener");
			IntPtr class_ref;

			public static IOnHeaderLongClickListener GetObject (IntPtr handle, JniHandleOwnership transfer)
			{
				return global::Java.Lang.Object.GetObject<IOnHeaderLongClickListener> (handle, transfer);
			}

			static IntPtr Validate (IntPtr handle)
			{
				if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
					throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
								JNIEnv.GetClassNameFromInstance (handle), "com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView.OnHeaderLongClickListener"));
				return handle;
			}

			protected override void Dispose (bool disposing)
			{
				if (this.class_ref != IntPtr.Zero)
					JNIEnv.DeleteGlobalRef (this.class_ref);
				this.class_ref = IntPtr.Zero;
				base.Dispose (disposing);
			}

			public IOnHeaderLongClickListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
			{
				IntPtr local_ref = JNIEnv.GetObjectClass (Handle);
				this.class_ref = JNIEnv.NewGlobalRef (local_ref);
				JNIEnv.DeleteLocalRef (local_ref);
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override System.Type ThresholdType {
				get { return typeof (IOnHeaderLongClickListenerInvoker); }
			}

			static Delegate cb_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J;
#pragma warning disable 0169
			static Delegate GetOnHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_JHandler ()
			{
				if (cb_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J == null)
					cb_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr, long, bool>) n_OnHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J);
				return cb_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J;
			}

			static bool n_OnHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, long p2)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
				global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
				bool __ret = __this.OnHeaderLongClick (p0, p1, p2);
				return __ret;
			}
#pragma warning restore 0169

			IntPtr id_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J;
			public bool OnHeaderLongClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				if (id_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J == IntPtr.Zero)
					id_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J = JNIEnv.GetMethodID (class_ref, "onHeaderLongClick", "(Landroid/widget/AdapterView;Landroid/view/View;J)Z");
				bool __ret = JNIEnv.CallBooleanMethod (Handle, id_onHeaderLongClick_Landroid_widget_AdapterView_Landroid_view_View_J, new JValue (p0), new JValue (p1), new JValue (p2));
				return __ret;
			}

		}

		public partial class HeaderLongClickEventArgs : global::System.EventArgs {

			public HeaderLongClickEventArgs (bool handled, global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				this.handled = handled;
				this.p0 = p0;
				this.p1 = p1;
				this.p2 = p2;
			}

			bool handled;
			public bool Handled {
				get { return handled; }
				set { handled = value; }
			}

			global::Android.Widget.AdapterView p0;
			public global::Android.Widget.AdapterView P0 {
				get { return p0; }
			}

			global::Android.Views.View p1;
			public global::Android.Views.View P1 {
				get { return p1; }
			}

			long p2;
			public long P2 {
				get { return p2; }
			}
		}

		[global::Android.Runtime.Register ("mono/com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView_OnHeaderLongClickListenerImplementor")]
		internal sealed class IOnHeaderLongClickListenerImplementor : global::Java.Lang.Object, IOnHeaderLongClickListener {

			object sender;

			public IOnHeaderLongClickListenerImplementor (object sender)
				: base (
					global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView_OnHeaderLongClickListenerImplementor", "()V"),
					JniHandleOwnership.TransferLocalRef)
			{
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "()V");
				this.sender = sender;
			}

#pragma warning disable 0649
			public EventHandler<HeaderLongClickEventArgs> Handler;
#pragma warning restore 0649

			public bool OnHeaderLongClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, long p2)
			{
				var __h = Handler;
				if (__h == null)
					return false;
				var __e = new HeaderLongClickEventArgs (true, p0, p1, p2);
				__h (sender, __e);
				return __e.Handled;
			}

			internal static bool __IsEmpty (IOnHeaderLongClickListenerImplementor value)
			{
				return value.Handler == null;
			}
		}


		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.PerformHeaderClick']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$PerformHeaderClick", DoNotGenerateAcw=true)]
		public partial class PerformHeaderClick : global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable, global::Java.Lang.IRunnable {

			internal static new IntPtr java_class_handle;
			internal static new IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$PerformHeaderClick", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (PerformHeaderClick); }
			}

			protected PerformHeaderClick (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_run;
#pragma warning disable 0169
			static Delegate GetRunHandler ()
			{
				if (cb_run == null)
					cb_run = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Run);
				return cb_run;
			}

			static void n_Run (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.PerformHeaderClick __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.PerformHeaderClick> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				__this.Run ();
			}
#pragma warning restore 0169

			static IntPtr id_run;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.PerformHeaderClick']/method[@name='run' and count(parameter)=0]"
			[Register ("run", "()V", "GetRunHandler")]
			public virtual void Run ()
			{
				if (id_run == IntPtr.Zero)
					id_run = JNIEnv.GetMethodID (class_ref, "run", "()V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_run);
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "run", "()V"));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.RuntimePlatformSupportException']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$RuntimePlatformSupportException", DoNotGenerateAcw=true)]
		public partial class RuntimePlatformSupportException : global::Java.Lang.RuntimeException {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$RuntimePlatformSupportException", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (RuntimePlatformSupportException); }
			}

			protected RuntimePlatformSupportException (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Ljava_lang_Exception_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.RuntimePlatformSupportException']/constructor[@name='StickyGridHeadersGridView.RuntimePlatformSupportException' and count(parameter)=2 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView'] and parameter[2][@type='java.lang.Exception']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Ljava/lang/Exception;)V", "")]
			public RuntimePlatformSupportException (global::StickyGridHeaders.StickyGridHeadersGridView __self, global::Java.Lang.Exception p1) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (RuntimePlatformSupportException)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Ljava/lang/Exception;)V", new JValue (__self), new JValue (p1)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Ljava/lang/Exception;)V", new JValue (__self), new JValue (p1));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Ljava_lang_Exception_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Ljava_lang_Exception_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Ljava/lang/Exception;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Ljava_lang_Exception_, new JValue (__self), new JValue (p1)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Ljava_lang_Exception_, new JValue (__self), new JValue (p1));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.SavedState']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$SavedState", DoNotGenerateAcw=true)]
		public partial class SavedState : global::Android.Views.View.BaseSavedState {


			static IntPtr CREATOR_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.SavedState']/field[@name='CREATOR']"
			[Register ("CREATOR")]
			public static global::Android.OS.IParcelableCreator Creator {
				get {
					if (CREATOR_jfieldId == IntPtr.Zero)
						CREATOR_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "CREATOR", "Landroid/os/Parcelable$Creator;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, CREATOR_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Android.OS.IParcelableCreator> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (CREATOR_jfieldId == IntPtr.Zero)
						CREATOR_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "CREATOR", "Landroid/os/Parcelable$Creator;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					JNIEnv.SetStaticField (class_ref, CREATOR_jfieldId, native_value);
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$SavedState", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (SavedState); }
			}

			protected SavedState (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Landroid_os_Parcelable_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.SavedState']/constructor[@name='StickyGridHeadersGridView.SavedState' and count(parameter)=1 and parameter[1][@type='android.os.Parcelable']]"
			[Register (".ctor", "(Landroid/os/Parcelable;)V", "")]
			public SavedState (global::Android.OS.IParcelable p0) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (SavedState)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/os/Parcelable;)V", new JValue (p0)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/os/Parcelable;)V", new JValue (p0));
					return;
				}

				if (id_ctor_Landroid_os_Parcelable_ == IntPtr.Zero)
					id_ctor_Landroid_os_Parcelable_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/os/Parcelable;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_os_Parcelable_, new JValue (p0)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_os_Parcelable_, new JValue (p0));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.WindowRunnable']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$WindowRunnable", DoNotGenerateAcw=true)]
		public partial class WindowRunnable : global::Java.Lang.Object {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$WindowRunnable", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (WindowRunnable); }
			}

			protected WindowRunnable (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_rememberWindowAttachCount;
#pragma warning disable 0169
			static Delegate GetRememberWindowAttachCountHandler ()
			{
				if (cb_rememberWindowAttachCount == null)
					cb_rememberWindowAttachCount = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_RememberWindowAttachCount);
				return cb_rememberWindowAttachCount;
			}

			static void n_RememberWindowAttachCount (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				__this.RememberWindowAttachCount ();
			}
#pragma warning restore 0169

			static IntPtr id_rememberWindowAttachCount;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.WindowRunnable']/method[@name='rememberWindowAttachCount' and count(parameter)=0]"
			[Register ("rememberWindowAttachCount", "()V", "GetRememberWindowAttachCountHandler")]
			public virtual void RememberWindowAttachCount ()
			{
				if (id_rememberWindowAttachCount == IntPtr.Zero)
					id_rememberWindowAttachCount = JNIEnv.GetMethodID (class_ref, "rememberWindowAttachCount", "()V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_rememberWindowAttachCount);
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "rememberWindowAttachCount", "()V"));
			}

			static Delegate cb_sameWindow;
#pragma warning disable 0169
			static Delegate GetSameWindowHandler ()
			{
				if (cb_sameWindow == null)
					cb_sameWindow = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_SameWindow);
				return cb_sameWindow;
			}

			static bool n_SameWindow (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.WindowRunnable> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				return __this.SameWindow ();
			}
#pragma warning restore 0169

			static IntPtr id_sameWindow;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView.WindowRunnable']/method[@name='sameWindow' and count(parameter)=0]"
			[Register ("sameWindow", "()Z", "GetSameWindowHandler")]
			public virtual bool SameWindow ()
			{
				if (id_sameWindow == IntPtr.Zero)
					id_sameWindow = JNIEnv.GetMethodID (class_ref, "sameWindow", "()Z");

				if (GetType () == ThresholdType)
					return JNIEnv.CallBooleanMethod  (Handle, id_sameWindow);
				else
					return JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "sameWindow", "()Z"));
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (StickyGridHeadersGridView); }
		}

		protected StickyGridHeadersGridView (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Landroid_content_Context_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/constructor[@name='StickyGridHeadersGridView' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register (".ctor", "(Landroid/content/Context;)V", "")]
		public StickyGridHeadersGridView (global::Android.Content.Context p0) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersGridView)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;)V", new JValue (p0)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;)V", new JValue (p0));
				return;
			}

			if (id_ctor_Landroid_content_Context_ == IntPtr.Zero)
				id_ctor_Landroid_content_Context_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_, new JValue (p0)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_, new JValue (p0));
		}

		static IntPtr id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_I;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/constructor[@name='StickyGridHeadersGridView' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.util.AttributeSet'] and parameter[3][@type='int']]"
		[Register (".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;I)V", "")]
		public StickyGridHeadersGridView (global::Android.Content.Context p0, global::Android.Util.IAttributeSet p1, int p2) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersGridView)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (p0), new JValue (p1), new JValue (p2)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (p0), new JValue (p1), new JValue (p2));
				return;
			}

			if (id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_I == IntPtr.Zero)
				id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_I = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Landroid/util/AttributeSet;I)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (p0), new JValue (p1), new JValue (p2)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (p0), new JValue (p1), new JValue (p2));
		}

		static IntPtr id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/constructor[@name='StickyGridHeadersGridView' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.util.AttributeSet']]"
		[Register (".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
		public StickyGridHeadersGridView (global::Android.Content.Context p0, global::Android.Util.IAttributeSet p1) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersGridView)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (p0), new JValue (p1)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (p0), new JValue (p1));
				return;
			}

			if (id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_ == IntPtr.Zero)
				id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Landroid/util/AttributeSet;)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (p0), new JValue (p1)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (p0), new JValue (p1));
		}

		static Delegate cb_getStickiedHeader;
#pragma warning disable 0169
		static Delegate GetGetStickiedHeaderHandler ()
		{
			if (cb_getStickiedHeader == null)
				cb_getStickiedHeader = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetStickiedHeader);
			return cb_getStickiedHeader;
		}

		static IntPtr n_GetStickiedHeader (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.StickiedHeader);
		}
#pragma warning restore 0169

		static IntPtr id_getStickiedHeader;
		public virtual global::Android.Views.View StickiedHeader {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='getStickiedHeader' and count(parameter)=0]"
			[Register ("getStickiedHeader", "()Landroid/view/View;", "GetGetStickiedHeaderHandler")]
			get {
				if (id_getStickiedHeader == IntPtr.Zero)
					id_getStickiedHeader = JNIEnv.GetMethodID (class_ref, "getStickiedHeader", "()Landroid/view/View;");

				if (GetType () == ThresholdType)
					return global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getStickiedHeader), JniHandleOwnership.TransferLocalRef);
				else
					return global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getStickiedHeader", "()Landroid/view/View;")), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getStickyHeaderIsTranscluent;
#pragma warning disable 0169
		static Delegate GetGetStickyHeaderIsTranscluentHandler ()
		{
			if (cb_getStickyHeaderIsTranscluent == null)
				cb_getStickyHeaderIsTranscluent = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_GetStickyHeaderIsTranscluent);
			return cb_getStickyHeaderIsTranscluent;
		}

		static bool n_GetStickyHeaderIsTranscluent (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.StickyHeaderIsTranscluent;
		}
#pragma warning restore 0169

		static Delegate cb_setStickyHeaderIsTranscluent_Z;
#pragma warning disable 0169
		static Delegate GetSetStickyHeaderIsTranscluent_ZHandler ()
		{
			if (cb_setStickyHeaderIsTranscluent_Z == null)
				cb_setStickyHeaderIsTranscluent_Z = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, bool>) n_SetStickyHeaderIsTranscluent_Z);
			return cb_setStickyHeaderIsTranscluent_Z;
		}

		static void n_SetStickyHeaderIsTranscluent_Z (IntPtr jnienv, IntPtr native__this, bool p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.StickyHeaderIsTranscluent = p0;
		}
#pragma warning restore 0169

		static IntPtr id_getStickyHeaderIsTranscluent;
		static IntPtr id_setStickyHeaderIsTranscluent_Z;
		public virtual bool StickyHeaderIsTranscluent {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='getStickyHeaderIsTranscluent' and count(parameter)=0]"
			[Register ("getStickyHeaderIsTranscluent", "()Z", "GetGetStickyHeaderIsTranscluentHandler")]
			get {
				if (id_getStickyHeaderIsTranscluent == IntPtr.Zero)
					id_getStickyHeaderIsTranscluent = JNIEnv.GetMethodID (class_ref, "getStickyHeaderIsTranscluent", "()Z");

				if (GetType () == ThresholdType)
					return JNIEnv.CallBooleanMethod  (Handle, id_getStickyHeaderIsTranscluent);
				else
					return JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getStickyHeaderIsTranscluent", "()Z"));
			}
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='setStickyHeaderIsTranscluent' and count(parameter)=1 and parameter[1][@type='boolean']]"
			[Register ("setStickyHeaderIsTranscluent", "(Z)V", "GetSetStickyHeaderIsTranscluent_ZHandler")]
			set {
				if (id_setStickyHeaderIsTranscluent_Z == IntPtr.Zero)
					id_setStickyHeaderIsTranscluent_Z = JNIEnv.GetMethodID (class_ref, "setStickyHeaderIsTranscluent", "(Z)V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_setStickyHeaderIsTranscluent_Z, new JValue (value));
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setStickyHeaderIsTranscluent", "(Z)V"), new JValue (value));
			}
		}

		static Delegate cb_areHeadersSticky;
#pragma warning disable 0169
		static Delegate GetAreHeadersStickyHandler ()
		{
			if (cb_areHeadersSticky == null)
				cb_areHeadersSticky = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_AreHeadersSticky);
			return cb_areHeadersSticky;
		}

		static bool n_AreHeadersSticky (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.AreHeadersSticky ();
		}
#pragma warning restore 0169

		static IntPtr id_areHeadersSticky;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='areHeadersSticky' and count(parameter)=0]"
		[Register ("areHeadersSticky", "()Z", "GetAreHeadersStickyHandler")]
		public virtual bool AreHeadersSticky ()
		{
			if (id_areHeadersSticky == IntPtr.Zero)
				id_areHeadersSticky = JNIEnv.GetMethodID (class_ref, "areHeadersSticky", "()Z");

			if (GetType () == ThresholdType)
				return JNIEnv.CallBooleanMethod  (Handle, id_areHeadersSticky);
			else
				return JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "areHeadersSticky", "()Z"));
		}

		static Delegate cb_getHeaderAt_I;
#pragma warning disable 0169
		static Delegate GetGetHeaderAt_IHandler ()
		{
			if (cb_getHeaderAt_I == null)
				cb_getHeaderAt_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_GetHeaderAt_I);
			return cb_getHeaderAt_I;
		}

		static IntPtr n_GetHeaderAt_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetHeaderAt (p0));
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderAt_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='getHeaderAt' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getHeaderAt", "(I)Landroid/view/View;", "GetGetHeaderAt_IHandler")]
		public virtual global::Android.Views.View GetHeaderAt (int p0)
		{
			if (id_getHeaderAt_I == IntPtr.Zero)
				id_getHeaderAt_I = JNIEnv.GetMethodID (class_ref, "getHeaderAt", "(I)Landroid/view/View;");

			if (GetType () == ThresholdType)
				return global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getHeaderAt_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
			else
				return global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getHeaderAt", "(I)Landroid/view/View;"), new JValue (p0)), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
#pragma warning disable 0169
		static Delegate GetOnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler ()
		{
			if (cb_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ == null)
				cb_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, int, long>) n_OnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ);
			return cb_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		}

		static void n_OnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, int p2, long p3)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.OnItemClick (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		static IntPtr id_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onItemClick' and count(parameter)=4 and parameter[1][@type='android.widget.AdapterView'] and parameter[2][@type='android.view.View'] and parameter[3][@type='int'] and parameter[4][@type='long']]"
		[Register ("onItemClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V", "GetOnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler")]
		public virtual void OnItemClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, int p2, long p3)
		{
			if (id_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ == IntPtr.Zero)
				id_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNIEnv.GetMethodID (class_ref, "onItemClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_onItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJ, new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onItemClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V"), new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
		}

		static Delegate cb_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
#pragma warning disable 0169
		static Delegate GetOnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler ()
		{
			if (cb_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ == null)
				cb_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr, int, long, bool>) n_OnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ);
			return cb_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		}

		static bool n_OnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, int p2, long p3)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			bool __ret = __this.OnItemLongClick (p0, p1, p2, p3);
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onItemLongClick' and count(parameter)=4 and parameter[1][@type='android.widget.AdapterView'] and parameter[2][@type='android.view.View'] and parameter[3][@type='int'] and parameter[4][@type='long']]"
		[Register ("onItemLongClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z", "GetOnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler")]
		public virtual bool OnItemLongClick (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, int p2, long p3)
		{
			if (id_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ == IntPtr.Zero)
				id_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNIEnv.GetMethodID (class_ref, "onItemLongClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z");

			bool __ret;
			if (GetType () == ThresholdType)
				__ret = JNIEnv.CallBooleanMethod  (Handle, id_onItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJ, new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
			else
				__ret = JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onItemLongClick", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z"), new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
			return __ret;
		}

		static Delegate cb_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ;
#pragma warning disable 0169
		static Delegate GetOnItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJHandler ()
		{
			if (cb_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ == null)
				cb_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, int, long>) n_OnItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ);
			return cb_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		}

		static void n_OnItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, int p2, long p3)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.OnItemSelected (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		static IntPtr id_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onItemSelected' and count(parameter)=4 and parameter[1][@type='android.widget.AdapterView'] and parameter[2][@type='android.view.View'] and parameter[3][@type='int'] and parameter[4][@type='long']]"
		[Register ("onItemSelected", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V", "GetOnItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJHandler")]
		public virtual void OnItemSelected (global::Android.Widget.AdapterView p0, global::Android.Views.View p1, int p2, long p3)
		{
			if (id_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ == IntPtr.Zero)
				id_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ = JNIEnv.GetMethodID (class_ref, "onItemSelected", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_onItemSelected_Landroid_widget_AdapterView_Landroid_view_View_IJ, new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onItemSelected", "(Landroid/widget/AdapterView;Landroid/view/View;IJ)V"), new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
		}

		static Delegate cb_onNothingSelected_Landroid_widget_AdapterView_;
#pragma warning disable 0169
		static Delegate GetOnNothingSelected_Landroid_widget_AdapterView_Handler ()
		{
			if (cb_onNothingSelected_Landroid_widget_AdapterView_ == null)
				cb_onNothingSelected_Landroid_widget_AdapterView_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_OnNothingSelected_Landroid_widget_AdapterView_);
			return cb_onNothingSelected_Landroid_widget_AdapterView_;
		}

		static void n_OnNothingSelected_Landroid_widget_AdapterView_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AdapterView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AdapterView> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.OnNothingSelected (p0);
		}
#pragma warning restore 0169

		static IntPtr id_onNothingSelected_Landroid_widget_AdapterView_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onNothingSelected' and count(parameter)=1 and parameter[1][@type='android.widget.AdapterView']]"
		[Register ("onNothingSelected", "(Landroid/widget/AdapterView;)V", "GetOnNothingSelected_Landroid_widget_AdapterView_Handler")]
		public virtual void OnNothingSelected (global::Android.Widget.AdapterView p0)
		{
			if (id_onNothingSelected_Landroid_widget_AdapterView_ == IntPtr.Zero)
				id_onNothingSelected_Landroid_widget_AdapterView_ = JNIEnv.GetMethodID (class_ref, "onNothingSelected", "(Landroid/widget/AdapterView;)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_onNothingSelected_Landroid_widget_AdapterView_, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onNothingSelected", "(Landroid/widget/AdapterView;)V"), new JValue (p0));
		}

		static Delegate cb_onScroll_Landroid_widget_AbsListView_III;
#pragma warning disable 0169
		static Delegate GetOnScroll_Landroid_widget_AbsListView_IIIHandler ()
		{
			if (cb_onScroll_Landroid_widget_AbsListView_III == null)
				cb_onScroll_Landroid_widget_AbsListView_III = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int, int, int>) n_OnScroll_Landroid_widget_AbsListView_III);
			return cb_onScroll_Landroid_widget_AbsListView_III;
		}

		static void n_OnScroll_Landroid_widget_AbsListView_III (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2, int p3)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AbsListView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AbsListView> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.OnScroll (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		static IntPtr id_onScroll_Landroid_widget_AbsListView_III;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onScroll' and count(parameter)=4 and parameter[1][@type='android.widget.AbsListView'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register ("onScroll", "(Landroid/widget/AbsListView;III)V", "GetOnScroll_Landroid_widget_AbsListView_IIIHandler")]
		public virtual void OnScroll (global::Android.Widget.AbsListView p0, int p1, int p2, int p3)
		{
			if (id_onScroll_Landroid_widget_AbsListView_III == IntPtr.Zero)
				id_onScroll_Landroid_widget_AbsListView_III = JNIEnv.GetMethodID (class_ref, "onScroll", "(Landroid/widget/AbsListView;III)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_onScroll_Landroid_widget_AbsListView_III, new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onScroll", "(Landroid/widget/AbsListView;III)V"), new JValue (p0), new JValue (p1), new JValue (p2), new JValue (p3));
		}

		static Delegate cb_onScrollStateChanged_Landroid_widget_AbsListView_I;
#pragma warning disable 0169
		static Delegate GetOnScrollStateChanged_Landroid_widget_AbsListView_IHandler ()
		{
			if (cb_onScrollStateChanged_Landroid_widget_AbsListView_I == null)
				cb_onScrollStateChanged_Landroid_widget_AbsListView_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int>) n_OnScrollStateChanged_Landroid_widget_AbsListView_I);
			return cb_onScrollStateChanged_Landroid_widget_AbsListView_I;
		}

		static void n_OnScrollStateChanged_Landroid_widget_AbsListView_I (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int native_p1)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.AbsListView p0 = global::Java.Lang.Object.GetObject<global::Android.Widget.AbsListView> (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Widget.ScrollState p1 = (global::Android.Widget.ScrollState) native_p1;
			__this.OnScrollStateChanged (p0, p1);
		}
#pragma warning restore 0169

		static IntPtr id_onScrollStateChanged_Landroid_widget_AbsListView_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='onScrollStateChanged' and count(parameter)=2 and parameter[1][@type='android.widget.AbsListView'] and parameter[2][@type='int']]"
		[Register ("onScrollStateChanged", "(Landroid/widget/AbsListView;I)V", "GetOnScrollStateChanged_Landroid_widget_AbsListView_IHandler")]
		public virtual void OnScrollStateChanged (global::Android.Widget.AbsListView p0, [global::Android.Runtime.GeneratedEnum] global::Android.Widget.ScrollState p1)
		{
			if (id_onScrollStateChanged_Landroid_widget_AbsListView_I == IntPtr.Zero)
				id_onScrollStateChanged_Landroid_widget_AbsListView_I = JNIEnv.GetMethodID (class_ref, "onScrollStateChanged", "(Landroid/widget/AbsListView;I)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_onScrollStateChanged_Landroid_widget_AbsListView_I, new JValue (p0), new JValue ((int) p1));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "onScrollStateChanged", "(Landroid/widget/AbsListView;I)V"), new JValue (p0), new JValue ((int) p1));
		}

		static Delegate cb_performHeaderClick_Landroid_view_View_J;
#pragma warning disable 0169
		static Delegate GetInvokePerformHeaderClick_Landroid_view_View_JHandler ()
		{
			if (cb_performHeaderClick_Landroid_view_View_J == null)
				cb_performHeaderClick_Landroid_view_View_J = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, long, bool>) n_InvokePerformHeaderClick_Landroid_view_View_J);
			return cb_performHeaderClick_Landroid_view_View_J;
		}

		static bool n_InvokePerformHeaderClick_Landroid_view_View_J (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, long p1)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p0 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p0, JniHandleOwnership.DoNotTransfer);
			bool __ret = __this.InvokePerformHeaderClick (p0, p1);
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_performHeaderClick_Landroid_view_View_J;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='performHeaderClick' and count(parameter)=2 and parameter[1][@type='android.view.View'] and parameter[2][@type='long']]"
		[Register ("performHeaderClick", "(Landroid/view/View;J)Z", "GetInvokePerformHeaderClick_Landroid_view_View_JHandler")]
		public virtual bool InvokePerformHeaderClick (global::Android.Views.View p0, long p1)
		{
			if (id_performHeaderClick_Landroid_view_View_J == IntPtr.Zero)
				id_performHeaderClick_Landroid_view_View_J = JNIEnv.GetMethodID (class_ref, "performHeaderClick", "(Landroid/view/View;J)Z");

			bool __ret;
			if (GetType () == ThresholdType)
				__ret = JNIEnv.CallBooleanMethod  (Handle, id_performHeaderClick_Landroid_view_View_J, new JValue (p0), new JValue (p1));
			else
				__ret = JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "performHeaderClick", "(Landroid/view/View;J)Z"), new JValue (p0), new JValue (p1));
			return __ret;
		}

		static Delegate cb_performHeaderLongPress_Landroid_view_View_J;
#pragma warning disable 0169
		static Delegate GetPerformHeaderLongPress_Landroid_view_View_JHandler ()
		{
			if (cb_performHeaderLongPress_Landroid_view_View_J == null)
				cb_performHeaderLongPress_Landroid_view_View_J = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, long, bool>) n_PerformHeaderLongPress_Landroid_view_View_J);
			return cb_performHeaderLongPress_Landroid_view_View_J;
		}

		static bool n_PerformHeaderLongPress_Landroid_view_View_J (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, long p1)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p0 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p0, JniHandleOwnership.DoNotTransfer);
			bool __ret = __this.PerformHeaderLongPress (p0, p1);
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_performHeaderLongPress_Landroid_view_View_J;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='performHeaderLongPress' and count(parameter)=2 and parameter[1][@type='android.view.View'] and parameter[2][@type='long']]"
		[Register ("performHeaderLongPress", "(Landroid/view/View;J)Z", "GetPerformHeaderLongPress_Landroid_view_View_JHandler")]
		public virtual bool PerformHeaderLongPress (global::Android.Views.View p0, long p1)
		{
			if (id_performHeaderLongPress_Landroid_view_View_J == IntPtr.Zero)
				id_performHeaderLongPress_Landroid_view_View_J = JNIEnv.GetMethodID (class_ref, "performHeaderLongPress", "(Landroid/view/View;J)Z");

			bool __ret;
			if (GetType () == ThresholdType)
				__ret = JNIEnv.CallBooleanMethod  (Handle, id_performHeaderLongPress_Landroid_view_View_J, new JValue (p0), new JValue (p1));
			else
				__ret = JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "performHeaderLongPress", "(Landroid/view/View;J)Z"), new JValue (p0), new JValue (p1));
			return __ret;
		}

		static Delegate cb_setAreHeadersSticky_Z;
#pragma warning disable 0169
		static Delegate GetSetAreHeadersSticky_ZHandler ()
		{
			if (cb_setAreHeadersSticky_Z == null)
				cb_setAreHeadersSticky_Z = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, bool>) n_SetAreHeadersSticky_Z);
			return cb_setAreHeadersSticky_Z;
		}

		static void n_SetAreHeadersSticky_Z (IntPtr jnienv, IntPtr native__this, bool p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetAreHeadersSticky (p0);
		}
#pragma warning restore 0169

		static IntPtr id_setAreHeadersSticky_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='setAreHeadersSticky' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setAreHeadersSticky", "(Z)V", "GetSetAreHeadersSticky_ZHandler")]
		public virtual void SetAreHeadersSticky (bool p0)
		{
			if (id_setAreHeadersSticky_Z == IntPtr.Zero)
				id_setAreHeadersSticky_Z = JNIEnv.GetMethodID (class_ref, "setAreHeadersSticky", "(Z)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_setAreHeadersSticky_Z, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setAreHeadersSticky", "(Z)V"), new JValue (p0));
		}

		static Delegate cb_setHeadersIgnorePadding_Z;
#pragma warning disable 0169
		static Delegate GetSetHeadersIgnorePadding_ZHandler ()
		{
			if (cb_setHeadersIgnorePadding_Z == null)
				cb_setHeadersIgnorePadding_Z = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, bool>) n_SetHeadersIgnorePadding_Z);
			return cb_setHeadersIgnorePadding_Z;
		}

		static void n_SetHeadersIgnorePadding_Z (IntPtr jnienv, IntPtr native__this, bool p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetHeadersIgnorePadding (p0);
		}
#pragma warning restore 0169

		static IntPtr id_setHeadersIgnorePadding_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='setHeadersIgnorePadding' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setHeadersIgnorePadding", "(Z)V", "GetSetHeadersIgnorePadding_ZHandler")]
		public virtual void SetHeadersIgnorePadding (bool p0)
		{
			if (id_setHeadersIgnorePadding_Z == IntPtr.Zero)
				id_setHeadersIgnorePadding_Z = JNIEnv.GetMethodID (class_ref, "setHeadersIgnorePadding", "(Z)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_setHeadersIgnorePadding_Z, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setHeadersIgnorePadding", "(Z)V"), new JValue (p0));
		}

		static Delegate cb_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_;
#pragma warning disable 0169
		static Delegate GetSetOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_Handler ()
		{
			if (cb_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_ == null)
				cb_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_);
			return cb_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_;
		}

		static void n_SetOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener p0 = (global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener)global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetOnHeaderClickListener (p0);
		}
#pragma warning restore 0169

		static IntPtr id_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='setOnHeaderClickListener' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView.OnHeaderClickListener']]"
		[Register ("setOnHeaderClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener;)V", "GetSetOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_Handler")]
		public virtual void SetOnHeaderClickListener (global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener p0)
		{
			if (id_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_ == IntPtr.Zero)
				id_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_ = JNIEnv.GetMethodID (class_ref, "setOnHeaderClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener;)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_setOnHeaderClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderClickListener_, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setOnHeaderClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderClickListener;)V"), new JValue (p0));
		}

		static Delegate cb_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_;
#pragma warning disable 0169
		static Delegate GetSetOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_Handler ()
		{
			if (cb_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_ == null)
				cb_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_);
			return cb_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_;
		}

		static void n_SetOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::StickyGridHeaders.StickyGridHeadersGridView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener p0 = (global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener)global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetOnHeaderLongClickListener (p0);
		}
#pragma warning restore 0169

		static IntPtr id_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersGridView']/method[@name='setOnHeaderLongClickListener' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView.OnHeaderLongClickListener']]"
		[Register ("setOnHeaderLongClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener;)V", "GetSetOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_Handler")]
		public virtual void SetOnHeaderLongClickListener (global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener p0)
		{
			if (id_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_ == IntPtr.Zero)
				id_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_ = JNIEnv.GetMethodID (class_ref, "setOnHeaderLongClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener;)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_setOnHeaderLongClickListener_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_OnHeaderLongClickListener_, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setOnHeaderLongClickListener", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$OnHeaderLongClickListener;)V"), new JValue (p0));
		}

#region "Event implementation for StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener"
		public event EventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.HeaderClickEventArgs> HeaderClick {
			add {
				global::Java.Interop.EventHelper.AddEventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener, global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListenerImplementor>(
						ref weak_implementor_SetOnHeaderClickListener,
						__CreateIOnHeaderClickListenerImplementor,
						SetOnHeaderClickListener,
						__h => __h.Handler += value);
			}
			remove {
				global::Java.Interop.EventHelper.RemoveEventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListener, global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListenerImplementor>(
						ref weak_implementor_SetOnHeaderClickListener,
						global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListenerImplementor.__IsEmpty,
						__v => SetOnHeaderClickListener (null),
						__h => __h.Handler -= value);
			}
		}

		WeakReference weak_implementor_SetOnHeaderClickListener;

		global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListenerImplementor __CreateIOnHeaderClickListenerImplementor ()
		{
			return new global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderClickListenerImplementor (this);
		}
#endregion
#region "Event implementation for StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener"
		public event EventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.HeaderLongClickEventArgs> HeaderLongClick {
			add {
				global::Java.Interop.EventHelper.AddEventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener, global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListenerImplementor>(
						ref weak_implementor_SetOnHeaderLongClickListener,
						__CreateIOnHeaderLongClickListenerImplementor,
						SetOnHeaderLongClickListener,
						__h => __h.Handler += value);
			}
			remove {
				global::Java.Interop.EventHelper.RemoveEventHandler<global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListener, global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListenerImplementor>(
						ref weak_implementor_SetOnHeaderLongClickListener,
						global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListenerImplementor.__IsEmpty,
						__v => SetOnHeaderLongClickListener (null),
						__h => __h.Handler -= value);
			}
		}

		WeakReference weak_implementor_SetOnHeaderLongClickListener;

		global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListenerImplementor __CreateIOnHeaderLongClickListenerImplementor ()
		{
			return new global::StickyGridHeaders.StickyGridHeadersGridView.IOnHeaderLongClickListenerImplementor (this);
		}
#endregion
	}
}
