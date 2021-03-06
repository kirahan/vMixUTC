using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace NewTek
{
	[SuppressUnmanagedCodeSecurity]
	public static partial class NDIlib
	{
		// The creation structure that is used when you are creating a finder
		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct find_create_t
		{
			// Do we want to incluide the list of NDI sources that are running
			// on the local machine ?
			// If TRUE then local sources will be visible, if FALSE then they
			// will not.
			[MarshalAsAttribute(UnmanagedType.U1)]
			public bool	show_local_sources;

			// Which groups do you want to search in for sources
			public IntPtr	p_groups;

			// The list of additional IP addresses that exist that we should query for
			// sources on. For instance, if you want to find the sources on a remote machine
			// that is not on your local sub-net then you can put a comma seperated list of
			// those IP addresses here and those sources will be available locally even though
			// they are not mDNS discoverable. An example might be "12.0.0.8,13.0.12.8".
			// When none is specified the registry is used.
			// Default = NULL;
			public IntPtr	p_extra_ips;
		}

		//**************************************************************************************************************************
		// Create a new finder instance. This will return NULL if it fails.
		// This function is deprecated, please use NDIlib_find_create_v2 if you can. This function
		// ignores the p_extra_ips member and sets it to the default.
		public static IntPtr find_create_v2(ref find_create_t p_create_settings)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_create_v2_64(ref p_create_settings);
			else
				return  UnsafeNativeMethods.find_create_v2_32(ref p_create_settings);
		}

		// For legacy reasons I called this the wrong thing. For backwards compatability.
		[Obsolete("find_create2 is obsolete.", false)]
		public static IntPtr find_create2(ref find_create_t p_create_settings)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_create2_64(ref p_create_settings);
			else
				return  UnsafeNativeMethods.find_create2_32(ref p_create_settings);
		}

		[Obsolete("find_create is obsolete.", false)]
		public static IntPtr find_create(ref find_create_t p_create_settings)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_create_64(ref p_create_settings);
			else
				return  UnsafeNativeMethods.find_create_32(ref p_create_settings);
		}

		// This will destroy an existing finder instance.
		public static void find_destroy(IntPtr p_instance)
		{
			if (IntPtr.Size == 8)
				 UnsafeNativeMethods.find_destroy_64( p_instance);
			else
				 UnsafeNativeMethods.find_destroy_32( p_instance);
		}

		// This function will recover the current set of sources (i.e. the ones that exist right this second).
		public static IntPtr find_get_current_sources(IntPtr p_instance, ref UInt32 p_no_sources)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_get_current_sources_64( p_instance, ref p_no_sources);
			else
				return  UnsafeNativeMethods.find_get_current_sources_32( p_instance, ref p_no_sources);
		}

		// This will allow you to wait until the number of online sources have changed.
		public static bool find_wait_for_sources(IntPtr p_instance, UInt32 timeout_in_ms)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_wait_for_sources_64( p_instance,  timeout_in_ms);
			else
				return  UnsafeNativeMethods.find_wait_for_sources_32( p_instance,  timeout_in_ms);
		}

		// DEPRECATED. This function is basically exactly the following and was confusing to use.
		//		if ((!timeout_in_ms) || (NDIlib_find_wait_for_sources(timeout_in_ms)))
		//				return NDIlib_find_get_current_sources(p_instance, p_no_sources);
		//		return NULL;
		[Obsolete("find_get_sources is obsolete.", false)]
		public static IntPtr find_get_sources(IntPtr p_instance, ref UInt32 p_no_sources, UInt32 timeout_in_ms)
		{
			if (IntPtr.Size == 8)
				return  UnsafeNativeMethods.find_get_sources_64( p_instance, ref p_no_sources,  timeout_in_ms);
			else
				return  UnsafeNativeMethods.find_get_sources_32( p_instance, ref p_no_sources,  timeout_in_ms);
		}

		[SuppressUnmanagedCodeSecurity]
		internal static partial class UnsafeNativeMethods
		{
			// find_create_v2 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_create_v2", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create_v2_64(ref find_create_t p_create_settings);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_create_v2", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create_v2_32(ref find_create_t p_create_settings);

			// find_create2 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_create2", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create2_64(ref find_create_t p_create_settings);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_create2", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create2_32(ref find_create_t p_create_settings);

			// find_create 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_create", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create_64(ref find_create_t p_create_settings);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_create", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_create_32(ref find_create_t p_create_settings);

			// find_destroy 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_destroy", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern void find_destroy_64(IntPtr p_instance);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_destroy", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern void find_destroy_32(IntPtr p_instance);

			// find_get_current_sources 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_get_current_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_get_current_sources_64(IntPtr p_instance, ref UInt32 p_no_sources);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_get_current_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_get_current_sources_32(IntPtr p_instance, ref UInt32 p_no_sources);

			// find_wait_for_sources 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_wait_for_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAsAttribute(UnmanagedType.U1)]
			internal static extern bool find_wait_for_sources_64(IntPtr p_instance, UInt32 timeout_in_ms);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_wait_for_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAsAttribute(UnmanagedType.U1)]
			internal static extern bool find_wait_for_sources_32(IntPtr p_instance, UInt32 timeout_in_ms);

			// find_get_sources 
			[DllImport("Processing.NDI.Lib.x64.dll", EntryPoint = "NDIlib_find_get_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_get_sources_64(IntPtr p_instance, ref UInt32 p_no_sources, UInt32 timeout_in_ms);
			[DllImport("Processing.NDI.Lib.x86.dll", EntryPoint = "NDIlib_find_get_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
			internal static extern IntPtr find_get_sources_32(IntPtr p_instance, ref UInt32 p_no_sources, UInt32 timeout_in_ms);

		} // UnsafeNativeMethods

	} // class NDIlib

} // namespace NewTek

