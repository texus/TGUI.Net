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
using SFML.System;
using SFML.Graphics;

namespace TGUI
{
	public class Canvas : ClickableWidget
	{
		public Canvas()
			: base(tguiCanvas_create())
		{
		}

		public Canvas(Vector2f size)
			: this()
		{
			Size = size;
		}

		public Canvas(float width, float height)
			: this(new Vector2f(width, height))
		{
		}

		protected internal Canvas(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Canvas(Canvas copy)
			: base(copy)
		{
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Clear the entire canvas with black color
		/// </summary>
		////////////////////////////////////////////////////////////
		public void Clear()
		{
			Clear(Color.Black);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Clear the entire canvas with a single color
		/// </summary>
		/// <param name="color">Color to use to clear the canvas</param>
		////////////////////////////////////////////////////////////
		public void Clear(Color color)
		{
			tguiCanvas_clear(CPointer, color);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a sprite to the canvas, with default render states
		/// </summary>
		/// <param name="sprite">Sprite to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(Sprite sprite)
		{
			Draw(sprite, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a sprite to the canvas
		/// </summary>
		/// <param name="sprite">Sprite to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(Sprite sprite, RenderStates states)
		{
			states.Transform *= sprite.Transform;
			RenderStatesMarshalData marshaledStates = MarshalRenderStates(states);
			tguiCanvas_drawSprite(CPointer, sprite.CPointer, ref marshaledStates);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw text to the canvas, with default render states
		/// </summary>
		/// <param name="text">Text to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(Text text)
		{
			Draw(text, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw text to the canvas
		/// </summary>
		/// <param name="text">Text to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(Text text, RenderStates states)
		{
			states.Transform *= text.Transform;
			RenderStatesMarshalData marshaledStates = MarshalRenderStates(states);
			tguiCanvas_drawText(CPointer, text.CPointer, ref marshaledStates);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a shape to the canvas, with default render states
		/// </summary>
		/// <param name="shape">Shape to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(Shape shape)
		{
			Draw(shape, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a shape to the canvas
		/// </summary>
		/// <param name="shape">Shape to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(Shape shape, RenderStates states)
		{
			states.Transform *= shape.Transform;
			RenderStatesMarshalData marshaledStates = MarshalRenderStates(states);
			tguiCanvas_drawShape(CPointer, shape.CPointer, ref marshaledStates);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a vertex array to the canvas, with default render states
		/// </summary>
		/// <param name="vertexArray">Vertex array to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(VertexArray vertexArray)
		{
			Draw(vertexArray, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw a vertex array to the canvas
		/// </summary>
		/// <param name="vertexArray">Vertex array to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(VertexArray vertexArray, RenderStates states)
		{
			RenderStatesMarshalData marshaledStates = MarshalRenderStates(states);
			tguiCanvas_drawVertexArray(CPointer, vertexArray.CPointer, ref marshaledStates);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw primitives defined by an array of vertices, with default render states
		/// </summary>
		/// <param name="vertices">Pointer to the vertices</param>
		/// <param name="type">Type of primitives to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(Vertex[] vertices, PrimitiveType type)
		{
			Draw(vertices, type, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw primitives defined by an array of vertices
		/// </summary>
		/// <param name="vertices">Pointer to the vertices</param>
		/// <param name="type">Type of primitives to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states)
		{
			Draw(vertices, 0, (uint)vertices.Length, type, states);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw primitives defined by a sub-array of vertices, with default render states
		/// </summary>
		/// <param name="vertices">Array of vertices to draw</param>
		/// <param name="start">Index of the first vertex to draw in the array</param>
		/// <param name="count">Number of vertices to draw</param>
		/// <param name="type">Type of primitives to draw</param>
		////////////////////////////////////////////////////////////
		public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type)
		{
			Draw(vertices, start, count, type, RenderStates.Default);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Draw primitives defined by a sub-array of vertices
		/// </summary>
		/// <param name="vertices">Pointer to the vertices</param>
		/// <param name="start">Index of the first vertex to use in the array</param>
		/// <param name="count">Number of vertices to draw</param>
		/// <param name="type">Type of primitives to draw</param>
		/// <param name="states">Render states to use for drawing</param>
		////////////////////////////////////////////////////////////
		public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states)
		{
			RenderStatesMarshalData marshaledStates = MarshalRenderStates(states);

			unsafe
			{
				fixed (Vertex* vertexPtr = vertices)
				{
					tguiCanvas_drawPrimitives(CPointer, vertexPtr + start, count, type, ref marshaledStates);
				}
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Update the contents of the canvas
		/// </summary>
		////////////////////////////////////////////////////////////
		public void Display()
		{
			tguiCanvas_display(CPointer);
		}


		// Return a marshalled version of the render states instance, so that can directly be passed to the C API
		// This is a copy of the internal code in SFML.Graphics.RenderStates
		protected RenderStatesMarshalData MarshalRenderStates(RenderStates renderStates)
		{
            RenderStatesMarshalData data = new RenderStatesMarshalData
            {
                blendMode = renderStates.BlendMode,
                transform = renderStates.Transform,
                texture = renderStates.Texture != null ? renderStates.Texture.CPointer : IntPtr.Zero,
                shader = renderStates.Shader != null ? renderStates.Shader.CPointer : IntPtr.Zero
            };

            return data;
		}

		[StructLayout(LayoutKind.Sequential)]
		protected struct RenderStatesMarshalData
		{
			public BlendMode blendMode;
			public Transform transform;
			public IntPtr texture;
			public IntPtr shader;
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiCanvas_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_clear(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_drawSprite(IntPtr cPointer, IntPtr drawableCPointer, ref RenderStatesMarshalData renderStates);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_drawText(IntPtr cPointer, IntPtr drawableCPointer, ref RenderStatesMarshalData renderStates);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_drawShape(IntPtr cPointer, IntPtr drawableCPointer, ref RenderStatesMarshalData renderStates);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_drawVertexArray(IntPtr cPointer, IntPtr drawableCPointer, ref RenderStatesMarshalData renderStates);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		unsafe static extern protected void tguiCanvas_drawPrimitives(IntPtr CPointer, Vertex* vertexPtr, uint vertexCount, PrimitiveType type, ref RenderStatesMarshalData renderStates);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiCanvas_display(IntPtr cPointer);

		#endregion
	}
}
