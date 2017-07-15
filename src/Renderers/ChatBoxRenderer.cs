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
using SFML.Graphics;

namespace TGUI
{
	public class ChatBoxRenderer : WidgetRenderer
	{
		public ChatBoxRenderer()
			: base(tguiChatBoxRenderer_create())
		{
		}

		protected internal ChatBoxRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ChatBoxRenderer(ChatBoxRenderer copy)
			: base(tguiChatBoxRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return new Outline(tguiChatBoxRenderer_getBorders(CPointer)); }
			set { tguiChatBoxRenderer_setBorders(CPointer, value.CPointer); }
		}

		public Outline Padding
		{
			get { return new Outline(tguiChatBoxRenderer_getPadding(CPointer)); }
			set { tguiChatBoxRenderer_setPadding(CPointer, value.CPointer); }
		}

		public Color BackgroundColor
		{
			get { return tguiChatBoxRenderer_getBackgroundColor(CPointer); }
			set { tguiChatBoxRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiChatBoxRenderer_getBorderColor(CPointer); }
			set { tguiChatBoxRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureBackground
		{
			set { tguiChatBoxRenderer_setTextureBackground(CPointer, value.CPointer); }
		}

		public RendererData Scrollbar
		{
			get { return new RendererData(tguiChatBoxRenderer_getScrollbar(CPointer)); }
			set { tguiChatBoxRenderer_setScrollbar(CPointer, value.CPointer); }
		}


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiChatBoxRenderer_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiChatBoxRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setBorders(IntPtr cPointer, IntPtr borders);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiChatBoxRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setPadding(IntPtr cPointer, IntPtr padding);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiChatBoxRenderer_getPadding(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiChatBoxRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiChatBoxRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiChatBoxRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiChatBoxRenderer_getScrollbar(IntPtr cPointer);

		#endregion
	}
}
