using System;
using System.Collections.Generic;
using Android.Runtime;

namespace StickyGridHeaders {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']"
	[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter", DoNotGenerateAcw=true)]
	public partial class StickyGridHeadersSimpleArrayAdapter : global::Android.Widget.BaseAdapter, global::StickyGridHeaders.IStickyGridHeadersSimpleAdapter {


		static IntPtr TAG_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/field[@name='TAG']"
		[Register ("TAG")]
		protected static string Tag {
			get {
				if (TAG_jfieldId == IntPtr.Zero)
					TAG_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "TAG", "Ljava/lang/String;");
				IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, TAG_jfieldId);
				return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (TAG_jfieldId == IntPtr.Zero)
					TAG_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "TAG", "Ljava/lang/String;");
				IntPtr native_value = JNIEnv.NewString (value);
				JNIEnv.SetStaticField (class_ref, TAG_jfieldId, native_value);
				JNIEnv.DeleteLocalRef (native_value);
			}
		}
		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.HeaderViewHolder']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$HeaderViewHolder", DoNotGenerateAcw=true)]
		protected internal partial class HeaderViewHolder : global::Java.Lang.Object {


			static IntPtr textView_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.HeaderViewHolder']/field[@name='textView']"
			[Register ("textView")]
			public global::Android.Widget.TextView TextView {
				get {
					if (textView_jfieldId == IntPtr.Zero)
						textView_jfieldId = JNIEnv.GetFieldID (class_ref, "textView", "Landroid/widget/TextView;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, textView_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Android.Widget.TextView> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (textView_jfieldId == IntPtr.Zero)
						textView_jfieldId = JNIEnv.GetFieldID (class_ref, "textView", "Landroid/widget/TextView;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					JNIEnv.SetField (Handle, textView_jfieldId, native_value);
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$HeaderViewHolder", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (HeaderViewHolder); }
			}

			protected HeaderViewHolder (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.HeaderViewHolder']/constructor[@name='StickyGridHeadersSimpleArrayAdapter.HeaderViewHolder' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersSimpleArrayAdapter']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter;)V", "")]
			protected HeaderViewHolder (global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __self) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (HeaderViewHolder)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_, new JValue (__self)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_, new JValue (__self));
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.ViewHolder']"
		[global::Android.Runtime.Register ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$ViewHolder", DoNotGenerateAcw=true)]
		protected internal partial class ViewHolder : global::Java.Lang.Object {


			static IntPtr textView_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.ViewHolder']/field[@name='textView']"
			[Register ("textView")]
			public global::Android.Widget.TextView TextView {
				get {
					if (textView_jfieldId == IntPtr.Zero)
						textView_jfieldId = JNIEnv.GetFieldID (class_ref, "textView", "Landroid/widget/TextView;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, textView_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Android.Widget.TextView> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (textView_jfieldId == IntPtr.Zero)
						textView_jfieldId = JNIEnv.GetFieldID (class_ref, "textView", "Landroid/widget/TextView;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					JNIEnv.SetField (Handle, textView_jfieldId, native_value);
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$ViewHolder", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (ViewHolder); }
			}

			protected ViewHolder (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter.ViewHolder']/constructor[@name='StickyGridHeadersSimpleArrayAdapter.ViewHolder' and count(parameter)=1 and parameter[1][@type='com.tonicartos.widget.stickygridheaders.StickyGridHeadersSimpleArrayAdapter']]"
			[Register (".ctor", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter;)V", "")]
			protected ViewHolder (global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __self) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				if (GetType () != typeof (ViewHolder)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self)),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";)V", new JValue (__self));
					return;
				}

				if (id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_ == IntPtr.Zero)
					id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_, new JValue (__self)),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_tonicartos_widget_stickygridheaders_StickyGridHeadersSimpleArrayAdapter_, new JValue (__self));
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (StickyGridHeadersSimpleArrayAdapter); }
		}

		protected StickyGridHeadersSimpleArrayAdapter (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Landroid_content_Context_arrayLjava_lang_Object_II;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/constructor[@name='StickyGridHeadersSimpleArrayAdapter' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.Object[]'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register (".ctor", "(Landroid/content/Context;[Ljava/lang/Object;II)V", "")]
		public StickyGridHeadersSimpleArrayAdapter (global::Android.Content.Context p0, global::Java.Lang.Object[] p1, int p2, int p3) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			IntPtr native_p1 = JNIEnv.NewArray (p1);;
			if (GetType () != typeof (StickyGridHeadersSimpleArrayAdapter)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;[Ljava/lang/Object;II)V", new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;[Ljava/lang/Object;II)V", new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3));
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
				return;
			}

			if (id_ctor_Landroid_content_Context_arrayLjava_lang_Object_II == IntPtr.Zero)
				id_ctor_Landroid_content_Context_arrayLjava_lang_Object_II = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;[Ljava/lang/Object;II)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_arrayLjava_lang_Object_II, new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_arrayLjava_lang_Object_II, new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3));
			if (p1 != null) {
				JNIEnv.CopyArray (native_p1, p1);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_ctor_Landroid_content_Context_Ljava_util_List_II;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/constructor[@name='StickyGridHeadersSimpleArrayAdapter' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.util.List'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register (".ctor", "(Landroid/content/Context;Ljava/util/List;II)V", "")]
		public StickyGridHeadersSimpleArrayAdapter (global::Android.Content.Context p0, global::System.Collections.IList p1, int p2, int p3) : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			IntPtr native_p1 = global::Android.Runtime.JavaList.ToLocalJniHandle (p1);;
			if (GetType () != typeof (StickyGridHeadersSimpleArrayAdapter)) {
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Ljava/util/List;II)V", new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3)),
						JniHandleOwnership.TransferLocalRef);
				global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Ljava/util/List;II)V", new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3));
				JNIEnv.DeleteLocalRef (native_p1);
				return;
			}

			if (id_ctor_Landroid_content_Context_Ljava_util_List_II == IntPtr.Zero)
				id_ctor_Landroid_content_Context_Ljava_util_List_II = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Ljava/util/List;II)V");
			SetHandle (
					global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Ljava_util_List_II, new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3)),
					JniHandleOwnership.TransferLocalRef);
			JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Ljava_util_List_II, new JValue (p0), new JValue (native_p1), new JValue (p2), new JValue (p3));
			JNIEnv.DeleteLocalRef (native_p1);
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.Count;
		}
#pragma warning restore 0169

		static IntPtr id_getCount;
		public override int Count {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getCount' and count(parameter)=0]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetHeaderId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getHeaderId' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getHeaderId", "(I)J", "GetGetHeaderId_IHandler")]
		public virtual long GetHeaderId (int p0)
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetHeaderView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getHeaderView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
		[Register ("getHeaderView", "(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;", "GetGetHeaderView_ILandroid_view_View_Landroid_view_ViewGroup_Handler")]
		public virtual global::Android.Views.View GetHeaderView (int p0, global::Android.Views.View p1, global::Android.Views.ViewGroup p2)
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GetItem (p0));
		}
#pragma warning restore 0169

		static IntPtr id_getItem_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getItem' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getItem", "(I)Ljava/lang/Object;", "GetGetItem_IHandler")]
		public override global::Java.Lang.Object GetItem (int p0)
		{
			if (id_getItem_I == IntPtr.Zero)
				id_getItem_I = JNIEnv.GetMethodID (class_ref, "getItem", "(I)Ljava/lang/Object;");

			if (GetType () == ThresholdType)
				return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod  (Handle, id_getItem_I, new JValue (p0)), JniHandleOwnership.TransferLocalRef);
			else
				return (Java.Lang.Object) global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getItem", "(I)Ljava/lang/Object;"), new JValue (p0)), JniHandleOwnership.TransferLocalRef);
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.GetItemId (p0);
		}
#pragma warning restore 0169

		static IntPtr id_getItemId_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getItemId' and count(parameter)=1 and parameter[1][@type='int']]"
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
			global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter __this = global::Java.Lang.Object.GetObject<global::StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.View p1 = global::Java.Lang.Object.GetObject<global::Android.Views.View> (native_p1, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p2 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetView (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		static IntPtr id_getView_ILandroid_view_View_Landroid_view_ViewGroup_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.tonicartos.widget.stickygridheaders']/class[@name='StickyGridHeadersSimpleArrayAdapter']/method[@name='getView' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='android.view.View'] and parameter[3][@type='android.view.ViewGroup']]"
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

	}
}
