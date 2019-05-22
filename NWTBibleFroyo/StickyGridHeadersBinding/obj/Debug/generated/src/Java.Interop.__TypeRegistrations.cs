using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Java.Interop {

	partial class __TypeRegistrations {

		public static void RegisterPackages ()
		{
#if MONODROID_TIMING
			var start = DateTime.Now;
			Android.Util.Log.Info ("MonoDroid-Timing", "RegisterPackages start: " + (start - new DateTime (1970, 1, 1)).TotalMilliseconds);
#endif // def MONODROID_TIMING
			Java.Interop.TypeManager.RegisterPackages (
					new string[]{
						"com/tonicartos/widget/stickygridheaders",
					},
					new Converter<string, Type>[]{
						lookup_com_tonicartos_widget_stickygridheaders_package,
					});
#if MONODROID_TIMING
			var end = DateTime.Now;
			Android.Util.Log.Info ("MonoDroid-Timing", "RegisterPackages time: " + (end - new DateTime (1970, 1, 1)).TotalMilliseconds + " [elapsed: " + (end - start).TotalMilliseconds + " ms]");
#endif // def MONODROID_TIMING
		}

		static Type Lookup (string[] mappings, string javaType)
		{
			string managedType = Java.Interop.TypeManager.LookupTypeMapping (mappings, javaType);
			if (managedType == null)
				return null;
			return Type.GetType (managedType);
		}

		static string[] package_com_tonicartos_widget_stickygridheaders_mappings;
		static Type lookup_com_tonicartos_widget_stickygridheaders_package (string klass)
		{
			if (package_com_tonicartos_widget_stickygridheaders_mappings == null) {
				package_com_tonicartos_widget_stickygridheaders_mappings = new string[]{
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper:StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$FillerView:StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper/FillerView",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderFillerView:StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper/HeaderFillerView",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$HeaderHolder:StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper/HeaderHolder",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersBaseAdapterWrapper$Position:StickyGridHeaders.StickyGridHeadersBaseAdapterWrapper/Position",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView:StickyGridHeaders.StickyGridHeadersGridView",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderLongPress:StickyGridHeaders.StickyGridHeadersGridView/CheckForHeaderLongPress",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$CheckForHeaderTap:StickyGridHeaders.StickyGridHeadersGridView/CheckForHeaderTap",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$PerformHeaderClick:StickyGridHeaders.StickyGridHeadersGridView/PerformHeaderClick",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$RuntimePlatformSupportException:StickyGridHeaders.StickyGridHeadersGridView/RuntimePlatformSupportException",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$SavedState:StickyGridHeaders.StickyGridHeadersGridView/SavedState",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersGridView$WindowRunnable:StickyGridHeaders.StickyGridHeadersGridView/WindowRunnable",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersListAdapterWrapper:StickyGridHeaders.StickyGridHeadersListAdapterWrapper",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper:StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper$DataSetObserverExtension:StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper/DataSetObserverExtension",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleAdapterWrapper$HeaderData:StickyGridHeaders.StickyGridHeadersSimpleAdapterWrapper/HeaderData",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter:StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$HeaderViewHolder:StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter/HeaderViewHolder",
					"com/tonicartos/widget/stickygridheaders/StickyGridHeadersSimpleArrayAdapter$ViewHolder:StickyGridHeaders.StickyGridHeadersSimpleArrayAdapter/ViewHolder",
				};
			}

			return Lookup (package_com_tonicartos_widget_stickygridheaders_mappings, klass);
		}
	}
}
