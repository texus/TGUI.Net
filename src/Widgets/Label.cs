/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TGUI
{
	public enum HorizontalAlignment
	{
		Left,
		Center,
		Right
	}

	public enum VerticalAlignment
	{
		Top,
		Center,
		Bottom
	}

    public class Label : Widget
    {
		public Label(string text = "")
			: base(tguiLabel_create())
        {
            if (text.Length > 0)
                Text = text;
        }

		public Label(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Label(Button copy)
			: base(copy)
		{
		}

		public new LabelRenderer Renderer
        {
			get { return new LabelRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public string Text
        {
			get { return Util.GetStringFromC_UTF32(tguiLabel_getText(CPointer)); }
			set { tguiLabel_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        public uint TextSize
        {
			get { return tguiLabel_getTextSize(CPointer); }
			set { tguiLabel_setTextSize(CPointer, value); }
        }

		public HorizontalAlignment HorizontalAlignment
		{
			get { return tguiLabel_getHorizontalAlignment(CPointer); }
			set { tguiLabel_setHorizontalAlignment(CPointer, value); }
		}

		public VerticalAlignment VerticalAlignmentAlignment
		{
			get { return tguiLabel_getVerticalAlignment(CPointer); }
			set { tguiLabel_setVerticalAlignment(CPointer, value); }
		}

		public bool AutoSize
		{
			get { return tguiLabel_getAutoSize(CPointer); }
			set { tguiLabel_setAutoSize(CPointer, value); }
		}

		public float MaximumTextWidth
		{
			get { return tguiLabel_getMaximumTextWidth(CPointer); }
			set { tguiLabel_setMaximumTextWidth(CPointer, value); }
		}

        #region Imports

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiLabel_create();

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setText(IntPtr cPointer, IntPtr value);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiLabel_getText(IntPtr cPointer);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern uint tguiLabel_getTextSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setHorizontalAlignment(IntPtr cPointer, HorizontalAlignment alignment);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern HorizontalAlignment tguiLabel_getHorizontalAlignment(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setVerticalAlignment(IntPtr cPointer, VerticalAlignment alignment);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern VerticalAlignment tguiLabel_getVerticalAlignment(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setAutoSize(IntPtr cPointer, bool autoSize);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiLabel_getAutoSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiLabel_setMaximumTextWidth(IntPtr cPointer, float maximumTextWidth);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern float tguiLabel_getMaximumTextWidth(IntPtr cPointer);

        #endregion
    }
}
