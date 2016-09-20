using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TGUI
{
	public class Button : Widget
	{
		public Button () :
			base (tguiButton_create ())
		{
		}

		public Button (Button copy) :
			base (tguiButton_copy (copy.CPointer))
		{
			Text = copy.Text;
		}

		protected override void Destroy (bool disposing)
		{
			tguiButton_destroy (CPointer);
		}

		public string Text {
			get { return GetStringFromC (tguiButton_getText (CPointer)); }
			set { tguiButton_setText (CPointer, ConvertStringForC (value)); }
		}

		#region Imports

		[DllImport ("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiButton_create ();

		[DllImport ("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiButton_copy (IntPtr Text);

		[DllImport ("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiButton_destroy (IntPtr CPointer);

		[DllImport ("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiButton_setText (IntPtr CPointer, IntPtr Value);

		[DllImport ("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiButton_getText (IntPtr CPointer);

		#endregion
	}
}
