using System;
using System.Collections.Generic;
using Android.Runtime;

namespace StickyGridHeaders {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']"
	[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper", DoNotGenerateAcw=true)]
	public partial class StickyGridHeadersBaseAdapterWrapper : global::Android.Widget.BaseAdapter {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='ID_FILLER']"
		[Register ("ID_FILLER")]
		protected const int IdFiller = (int) -2;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='ID_HEADER']"
		[Register ("ID_HEADER")]
		protected const int IdHeader = (int) -1;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='ID_HEADER_FILLER']"
		[Register ("ID_HEADER_FILLER")]
		protected const int IdHeaderFiller = (int) -3;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='POSITION_FILLER']"
		[Register ("POSITION_FILLER")]
		protected const int PositionFiller = (int) -1;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='POSITION_HEADER']"
		[Register ("POSITION_HEADER")]
		protected const int PositionHeader = (int) -2;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='POSITION_HEADER_FILLER']"
		[Register ("POSITION_HEADER_FILLER")]
		protected const int PositionHeaderFiller = (int) -3;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='VIEW_TYPE_FILLER']"
		[Register ("VIEW_TYPE_FILLER")]
		protected const int ViewTypeFiller = (int) 0;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='VIEW_TYPE_HEADER']"
		[Register ("VIEW_TYPE_HEADER")]
		protected const int ViewTypeHeader = (int) 1;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/field[@name='VIEW_TYPE_HEADER_FILLER']"
		[Register ("VIEW_TYPE_HEADER_FILLER")]
		protected const int ViewTypeHeaderFiller = (int) 2;
		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.FillerView']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$FillerView", DoNotGenerateAcw=true)]
		protected internal partial class FillerView : global::Android.Views.View {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$FillerView", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (FillerView); }
			}

			protected FillerView (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.FillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.FillerView' and count(parameter)=2 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;)V", "")]
			public FillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (FillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;)V", new JValue (__self), new JValue (p1)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;)V", new JValue (__self), new JValue (p1));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_, new JValue (__self), new JValue (p1)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_, new JValue (__self), new JValue (p1));
			}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.FillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.FillerView' and count(parameter)=3 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context'] and parameter[3][@type='android.util.AttributeSet']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
			public FillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1, global::Android.Util.IAttributeSet p2) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (FillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (__self), new JValue (p1), new JValue (p2)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (__self), new JValue (p1), new JValue (p2));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (__self), new JValue (p1), new JValue (p2)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (__self), new JValue (p1), new JValue (p2));
			}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.FillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.FillerView' and count(parameter)=4 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context'] and parameter[3][@type='android.util.AttributeSet'] and parameter[4][@type='int']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;I)V", "")]
			public FillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1, global::Android.Util.IAttributeSet p2, int p3) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (FillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;I)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3));
			}

			static Delegate cb_setMeasureTarget_Landroid_view_View_;
#pragma warning disable 0169
			static Delegate GetSetMeasureTarget_Landroid_view_View_Handler ()
			{
				if (cb_setMeasureTarget_Landroid_view_View_ == null)
					cb_setMeasureTarget_Landroid_view_View_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetMeasureTarget_Landroid_view_View_);
				return cb_setMeasureTarget_Landroid_view_View_;
			}

			static void n_SetMeasureTarget_Landroid_view_View_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.FillerView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.FillerView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Views.View p0 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p0, JniHandleOwnership.DoNotTransfer);
				__this.SetMeasureTarget (p0);
			}
#pragma warning restore 0169

			static IntPtr id_setMeasureTarget_Landroid_view_View_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.FillerView']/method[@name='setMeasureTarget' and count(parameter)=1 and parameter[1][@type='android.view.View']]"
			[Register ("setMeasureTarget", "(Landroid/view/View;)V", "GetSetMeasureTarget_Landroid_view_View_Handler")]
			public virtual void SetMeasureTarget (global::Android.Views.View p0)
			{
				if (id_setMeasureTarget_Landroid_view_View_ == IntPtr.Zero)
					id_setMeasureTarget_Landroid_view_View_ = JNIEnv.GetMethodID (class_ref, "setMeasureTarget", "(Landroid/view/View;)V");

				if (GetType () == ThresholdType)
					JNIEnv.CallVoidMethod  (Handle, id_setMeasureTarget_Landroid_view_View_, new JValue (p0));
				else
					JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setMeasureTarget", "(Landroid/view/View;)V"), new JValue (p0));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderFillerView", DoNotGenerateAcw=true)]
		protected internal partial class HeaderFillerView : global::Android.Widget.FrameLayout {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderFillerView", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (HeaderFillerView); }
			}

			protected HeaderFillerView (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView' and count(parameter)=2 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;)V", "")]
			public HeaderFillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderFillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;)V", new JValue (__self), new JValue (p1)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;)V", new JValue (__self), new JValue (p1));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_, new JValue (__self), new JValue (p1)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_, new JValue (__self), new JValue (p1));
			}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView' and count(parameter)=3 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context'] and parameter[3][@type='android.util.AttributeSet']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
			public HeaderFillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1, global::Android.Util.IAttributeSet p2) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderFillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (__self), new JValue (p1), new JValue (p2)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;)V", new JValue (__self), new JValue (p1), new JValue (p2));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (__self), new JValue (p1), new JValue (p2)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_, new JValue (__self), new JValue (p1), new JValue (p2));
			}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView' and count(parameter)=4 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='android.content.Context'] and parameter[3][@type='android.util.AttributeSet'] and parameter[4][@type='int']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;I)V", "")]
			public HeaderFillerView (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, global::Android.Content.Context p1, global::Android.Util.IAttributeSet p2, int p3) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderFillerView)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";Landroid/content/Context;Landroid/util/AttributeSet;I)V", new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;Landroid/content/Context;Landroid/util/AttributeSet;I)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_Landroid_content_Context_Landroid_util_AttributeSet_I, new JValue (__self), new JValue (p1), new JValue (p2), new JValue (p3));
			}

			static Delegate cb_getHeaderId;
#pragma warning disable 0169
			static Delegate GetGetHeaderIdHandler ()
			{
				if (cb_getHeaderId == null)
					cb_getHeaderId = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetHeaderId);
				return cb_getHeaderId;
			}

			static int n_GetHeaderId (IntPtr jnienv, IntPtr native__this)
			{
				global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.HeaderFillerView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.HeaderFillerView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				return __this.HeaderId;
			}
#pragma warning restore 0169

			static Delegate cb_setHeaderId_I;
#pragma warning disable 0169
			static Delegate GetSetHeaderId_IHandler ()
			{
				if (cb_setHeaderId_I == null)
					cb_setHeaderId_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_SetHeaderId_I);
				return cb_setHeaderId_I;
			}

			static void n_SetHeaderId_I (IntPtr jnienv, IntPtr native__this, int p0)
			{
				global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.HeaderFillerView __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.HeaderFillerView> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				__this.HeaderId = p0;
			}
#pragma warning restore 0169

			static IntPtr id_getHeaderId;
			static IntPtr id_setHeaderId_I;
			public virtual int HeaderId {
				// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']/method[@name='getHeaderId' and count(parameter)=0]"
				[Register ("getHeaderId", "()I", "GetGetHeaderIdHandler")]
				get {
					if (id_getHeaderId == IntPtr.Zero)
						id_getHeaderId = JNIEnv.GetMethodID (class_ref, "getHeaderId", "()I");

					if (GetType () == ThresholdType)
						return JNIEnv.CallIntMethod  (Handle, id_getHeaderId);
					else
						return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getHeaderId", "()I"));
				}
				// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderFillerView']/method[@name='setHeaderId' and count(parameter)=1 and parameter[1][@type='int']]"
				[Register ("setHeaderId", "(I)V", "GetSetHeaderId_IHandler")]
				set {
					if (id_setHeaderId_I == IntPtr.Zero)
						id_setHeaderId_I = JNIEnv.GetMethodID (class_ref, "setHeaderId", "(I)V");

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod  (Handle, id_setHeaderId_I, new JValue (value));
					else
						JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setHeaderId", "(I)V"), new JValue (value));
				}
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderHolder']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderHolder", DoNotGenerateAcw=true)]
		protected internal partial class HeaderHolder : global::Java.Lang.Object {


			static IntPtr mHeaderView_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderHolder']/field[@name='mHeaderView']"
			[Register ("mHeaderView")]
			protected global::Android.Views.View MHeaderView {
				get {
					if (mHeaderView_jfieldId == IntPtr.Zero)
						mHeaderView_jfieldId = JNIEnv.GetFieldID (class_ref, "mHeaderView", "Landroid/view/View;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, mHeaderView_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Android.Views.View> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (mHeaderView_jfieldId == IntPtr.Zero)
						mHeaderView_jfieldId = JNIEnv.GetFieldID (class_ref, "mHeaderView", "Landroid/view/View;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					JNIEnv.SetField (Handle, mHeaderView_jfieldId, native_value);
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderHolder", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (HeaderHolder); }
			}

			protected HeaderHolder (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.HeaderHolder']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.HeaderHolder' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;)V", "")]
			protected HeaderHolder (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderHolder)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_, new JValue (__self)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_, new JValue (__self));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.Position']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position", DoNotGenerateAcw=true)]
		protected internal partial class Position : global::Java.Lang.Object {


			static IntPtr mHeader_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.Position']/field[@name='mHeader']"
			[Register ("mHeader")]
			protected int MHeader {
				get {
					if (mHeader_jfieldId == IntPtr.Zero)
						mHeader_jfieldId = JNIEnv.GetFieldID (class_ref, "mHeader", "I");
					return JNIEnv.GetIntField (Handle, mHeader_jfieldId);
				}
				set {
					if (mHeader_jfieldId == IntPtr.Zero)
						mHeader_jfieldId = JNIEnv.GetFieldID (class_ref, "mHeader", "I");
					JNIEnv.SetField (Handle, mHeader_jfieldId, value);
				}
			}

			static IntPtr mPosition_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.Position']/field[@name='mPosition']"
			[Register ("mPosition")]
			protected int MPosition {
				get {
					if (mPosition_jfieldId == IntPtr.Zero)
						mPosition_jfieldId = JNIEnv.GetFieldID (class_ref, "mPosition", "I");
					return JNIEnv.GetIntField (Handle, mPosition_jfieldId);
				}
				set {
					if (mPosition_jfieldId == IntPtr.Zero)
						mPosition_jfieldId = JNIEnv.GetFieldID (class_ref, "mPosition", "I");
					JNIEnv.SetField (Handle, mPosition_jfieldId, value);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (Position); }
			}

			protected Position (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_II;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper.Position']/constructor[@name='StickyGridHeadersBaseAdapterWrapper.Position' and count(parameter)=3 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapterWrapper'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;II)V", "")]
			protected Position (global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __self, int p1, int p2) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (Position)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";II)V", new JValue (__self), new JValue (p1), new JValue (p2)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";II)V", new JValue (__self), new JValue (p1), new JValue (p2));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_II == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_II = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper;II)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_II, new JValue (__self), new JValue (p1), new JValue (p2)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapterWrapper_II, new JValue (__self), new JValue (p1), new JValue (p2));
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (StickyGridHeadersBaseAdapterWrapper); }
		}

		protected StickyGridHeadersBaseAdapterWrapper (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Landroid_content_Context_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapter_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/constructor[@name='StickyGridHeadersBaseAdapterWrapper' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersGridView'] and parameter[3][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersBaseAdapter']]"
		[Register (".ctor", "(Landroid/content/Context;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;)V", "")]
		public StickyGridHeadersBaseAdapterWrapper (global::Android.Content.Context p0, global::StickyGridHeaders.StickyGridHeadersGridView p1, global::StickyGridHeaders.IStickyGridHeadersBaseAdapter p2) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			if (GetType () != typeof (StickyGridHeadersBaseAdapterWrapper)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;)V", new JValue (p0), new JValue (p1), new JValue (p2)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;)V", new JValue (p0), new JValue (p1), new JValue (p2));
				return;
			}

			if (id_ctor_Landroid_content_Context_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapter_ == IntPtr.Zero)
				id_ctor_Landroid_content_Context_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapter_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView;Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapter_, new JValue (p0), new JValue (p1), new JValue (p2)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersGridView_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersBaseAdapter_, new JValue (p0), new JValue (p1), new JValue (p2));
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
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.Count;
		}
#pragma warning restore 0169

		static IntPtr id_getCount;
		public override int Count {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getCount' and count(parameter)=0]"
			[Register ("getCount", "()I", "GetGetCountHandler")]
			get {
				if (id_getCount == IntPtr.Zero)
					id_getCount = JNIEnv.GetMethodID (class_ref, "getCount", "()I");

				if (GetType () == ThresholdType)
					return JNIEnv.CallIntMethod  (Handle, id_getCount);
				else
					return JNIEnv.CallNonvirtualIntMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getCount", "()I"));
			}
		}

		static Delegate cb_getWrappedAdapter;
#pragma warning disable 0169
		static Delegate GetGetWrappedAdapterHandler ()
		{
			if (cb_getWrappedAdapter == null)
				cb_getWrappedAdapter = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetWrappedAdapter);
			return cb_getWrappedAdapter;
		}

		static IntPtr n_GetWrappedAdapter (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.WrappedAdapter);
		}
#pragma warning restore 0169

		static IntPtr id_getWrappedAdapter;
		public virtual global::StickyGridHeaders.IStickyGridHeadersBaseAdapter WrappedAdapter {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getWrappedAdapter' and count(parameter)=0]"
			[Register ("getWrappedAdapter", "()Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;", "GetGetWrappedAdapterHandler")]
			get {
				if (id_getWrappedAdapter == IntPtr.Zero)
					id_getWrappedAdapter = JNIEnv.GetMethodID (class_ref, "getWrappedAdapter", "()Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;");

				if (GetType () == ThresholdType)
					return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.IStickyGridHeadersBaseAdapter> (JNIEnv.CallObjectMethod  (Handle, id_getWrappedAdapter), JniHandleOwnership.TransferLocalRef);
				else
					return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.IStickyGridHeadersBaseAdapter> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getWrappedAdapter", "()Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapter;")), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getHeaderId_I;
#pragma warning disable 0169
		static Delegate GetGetHeaderId_IHandler ()
		{
			if (cb_getHeaderId_I == null)
				cb_getHeaderId_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, long>) n_GetHeaderId_I);
			return cb_getHeaderId_I;
		}

		static long n_GetHeaderId_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetHeaderId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getHeaderId' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getHeaderId", "(I)J", "GetGetHeaderId_IHandler")]
		protected virtual long GetHeaderId (int p0)
		{
			if (id_getHeaderId_I == IntPtr.Zero)
				id_getHeaderId_I = JNIEnv.GetMethodID (class_ref, "getHeaderId", "(I)J");

			if (GetType () == ThresholdType)
				return JNIEnv.CallLongMethod  (Handle, id_getHeaderId_I, new JValue (p0));
			else
				return JNIEnv.CallNonvirtualLongMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getHeaderId", "(I)J"), new JValue (p0));
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
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetHeaderView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getHeaderView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
		[Register ("getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;", "GetGetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_Handler")]
		protected virtual global::Android.Views.View GetHeaderView (int p0, global::Android.Views.View p1, global::Android.Views.ViewGroup p2)
		{
			if (id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ == IntPtr.Zero)
				id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNIEnv.GetMethodID (class_ref, "getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;");

			global::Android.Views.View __ret;
			if (GetType () == ThresholdType)
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			else
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;"), new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
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
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetItem (p0));
		}
#pragma warning restore 0169

		static IntPtr id_getItem_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getItem' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getItem", "(I)Ljava/lang/Object;", "GetGetItem_IHandler")]
		public override global::Java.Lang.Object GetItem (int p0)
		{
			if (id_getItem_I == IntPtr.Zero)
				id_getItem_I = JNIEnv.GetMethodID (class_ref, "getItem", "(I)Ljava/lang/Object;");

			if (GetType () == ThresholdType)
				return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getItem_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
			else
				return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getItem", "(I)Ljava/lang/Object;"), new JValue (p0)), JniHandleOwnership.TransferLocalRef);
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
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetItemId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getItemId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getItemId' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getItemId", "(I)J", "GetGetItemId_IHandler")]
		public override long GetItemId (int p0)
		{
			if (id_getItemId_I == IntPtr.Zero)
				id_getItemId_I = JNIEnv.GetMethodID (class_ref, "getItemId", "(I)J");

			if (GetType () == ThresholdType)
				return JNIEnv.CallLongMethod  (Handle, id_getItemId_I, new JValue (p0));
			else
				return JNIEnv.CallNonvirtualLongMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getItemId", "(I)J"), new JValue (p0));
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
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='getView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
		[Register ("getView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;", "GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler")]
		public override global::Android.Views.View GetView (int p0, global::Android.Views.View p1, global::Android.Views.ViewGroup p2)
		{
			if (id_getView_ILandroid_view_View_Landroid_view_ViewGroup_ == IntPtr.Zero)
				id_getView_ILandroid_view_View_Landroid_view_ViewGroup_ = JNIEnv.GetMethodID (class_ref, "getView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;");

			global::Android.Views.View __ret;
			if (GetType () == ThresholdType)
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallObjectMethod  (Handle, id_getView_ILandroid_view_View_Landroid_view_ViewGroup_, new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			else
				__ret = global::Java.Lang.Object.GetObject<global::Android.Views.View> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;"), new JValue (p0), new JValue (p1), new JValue (p2)), JniHandleOwnership.TransferLocalRef);
			return __ret;
		}

		static Delegate cb_setNumColumns_I;
#pragma warning disable 0169
		static Delegate GetSetNumColumns_IHandler ()
		{
			if (cb_setNumColumns_I == null)
				cb_setNumColumns_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_SetNumColumns_I);
			return cb_setNumColumns_I;
		}

		static void n_SetNumColumns_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetNumColumns (p0);
		}
#pragma warning restore 0169

		static IntPtr id_setNumColumns_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='setNumColumns' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setNumColumns", "(I)V", "GetSetNumColumns_IHandler")]
		public virtual void SetNumColumns (int p0)
		{
			if (id_setNumColumns_I == IntPtr.Zero)
				id_setNumColumns_I = JNIEnv.GetMethodID (class_ref, "setNumColumns", "(I)V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_setNumColumns_I, new JValue (p0));
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setNumColumns", "(I)V"), new JValue (p0));
		}

		static Delegate cb_translatePosition_I;
#pragma warning disable 0169
		static Delegate GetTranslatePosition_IHandler ()
		{
			if (cb_translatePosition_I == null)
				cb_translatePosition_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_TranslatePosition_I);
			return cb_translatePosition_I;
		}

		static IntPtr n_TranslatePosition_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.TranslatePosition (p0));
		}
#pragma warning restore 0169

		static IntPtr id_translatePosition_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='translatePosition' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("translatePosition", "(I)Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position;", "GetTranslatePosition_IHandler")]
		protected virtual global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.Position TranslatePosition (int p0)
		{
			if (id_translatePosition_I == IntPtr.Zero)
				id_translatePosition_I = JNIEnv.GetMethodID (class_ref, "translatePosition", "(I)Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position;");

			if (GetType () == ThresholdType)
				return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.Position> (JNIEnv.CallObjectMethod  (Handle, id_translatePosition_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
			else
				return global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper.Position> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "translatePosition", "(I)Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position;"), new JValue (p0)), JniHandleOwnership.TransferLocalRef);
		}

		static Delegate cb_updateCount;
#pragma warning disable 0169
		static Delegate GetUpdateCountHandler ()
		{
			if (cb_updateCount == null)
				cb_updateCount = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_UpdateCount);
			return cb_updateCount;
		}

		static void n_UpdateCount (IntPtr jnienv, IntPtr native__this)
		{
			global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.UpdateCount ();
		}
#pragma warning restore 0169

		static IntPtr id_updateCount;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersBaseAdapterWrapper']/method[@name='updateCount' and count(parameter)=0]"
		[Register ("updateCount", "()V", "GetUpdateCountHandler")]
		protected virtual void UpdateCount ()
		{
			if (id_updateCount == IntPtr.Zero)
				id_updateCount = JNIEnv.GetMethodID (class_ref, "updateCount", "()V");

			if (GetType () == ThresholdType)
				JNIEnv.CallVoidMethod  (Handle, id_updateCount);
			else
				JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "updateCount", "()V"));
		}

	}
}
