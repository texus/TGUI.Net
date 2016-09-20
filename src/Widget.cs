using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using SFML.System;

namespace TGUI
{
	public abstract class Widget : ObjectBase
    {
		public Widget(IntPtr cPointer) :
			base(cPointer)
        {
        }

		public Vector2f Position {
			get { return tguiWidget_getPosition (CPointer); }
			set { tguiWidget_setPosition (CPointer, value); }
		}

        public bool Visible
        {
			get { return tguiWidget_isVisible(CPointer); }
            set
			{
				if (value == true)
					tguiWidget_show(CPointer);
				else
					tguiWidget_hide(CPointer);
			}
        }

		protected string GetStringFromC(IntPtr source)
		{
			// Find the length of the source string (find the terminating 0)
			uint length = 0;
			unsafe
			{
				for (uint* ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
					length++;
			}
				
			// Copy it to a byte array
			byte[] sourceBytes = new byte[length * 4];
			Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);

			// Convert it to a C# string
			return Encoding.UTF32.GetString(sourceBytes);
		}

		protected IntPtr ConvertStringForC(string source)
		{
			// Copy the string to a null-terminated UTF-32 byte array
			byte[] utf32 = Encoding.UTF32.GetBytes(source + '\0');

			// Pass it to the C API
			unsafe
			{
				fixed (byte* ptr = utf32)
				{
					return (IntPtr)ptr;
				}
			}
		}

        #region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setPosition(IntPtr CPointer, Vector2f pos);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getPosition(IntPtr CPointer);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void tguiWidget_show(IntPtr CPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_hide(IntPtr CPointer);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool tguiWidget_isVisible(IntPtr CPointer);

        #endregion
    }
}
