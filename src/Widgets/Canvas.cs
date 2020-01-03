/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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
    /// <summary>
    /// Canvas widget
    /// </summary>
    public class Canvas : CustomWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Canvas()
        {
        }

        /// <summary>
        /// Constructor to create the Canvas with the given size
        /// </summary>
        /// <param name="size">Size of the canvas</param>
        public Canvas(Vector2f size)
        {
            Size = size;
        }

        /// <summary>
        /// Constructor to create the Canvas with the given size
        /// </summary>
        /// <param name="width">Width of the canvas</param>
        /// <param name="height">Height of the canvas</param>
        public Canvas(float width, float height)
            : this(new Vector2f(width, height))
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Canvas(Canvas copy)
            : base(copy)
        {
            Size = copy.Size;
        }

        /// <summary>
        /// Change the current active view
        /// </summary>
        /// <param name="view">New view</param>
        public View View
        {
            get { return myRenderTexture.GetView(); }
            set { myRenderTexture.SetView(value); }
        }

        /// <summary>
        /// Default view of the canvas
        /// </summary>
        public View DefaultView
        {
            get { return myRenderTexture.DefaultView; }
        }

        /// <summary>
        /// Get the viewport of the currently applied view
        /// </summary>
        public IntRect Viewport
        {
            get { return myRenderTexture.GetViewport(myRenderTexture.GetView()); }
        }

        /// <summary>
        /// Clear the entire canvas with black color
        /// </summary>
        public void Clear()
        {
            myRenderTexture?.Clear();
        }

        /// <summary>
        /// Clear the entire canvas with a single color
        /// </summary>
        /// <param name="color">Color to use to clear the canvas</param>
        public void Clear(Color color)
        {
            myRenderTexture?.Clear(color);
        }

        /// <summary>
        /// Draw something to the canvas, with default render states
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        public void Draw(Drawable drawable)
        {
            myRenderTexture?.Draw(drawable);
        }

        /// <summary>
        /// Draw something to the canvas
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(Drawable drawable, RenderStates states)
        {
            myRenderTexture?.Draw(drawable, states);
        }

        /// <summary>
        /// Draw primitives defined by an array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        public void Draw(Vertex[] vertices, PrimitiveType type)
        {
            myRenderTexture?.Draw(vertices, type);
        }

        /// <summary>
        /// Draw primitives defined by an array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states)
        {
            myRenderTexture?.Draw(vertices, type, states);
        }

        /// <summary>
        /// Draw primitives defined by a sub-array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="start">Index of the first vertex to draw in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type)
        {
            myRenderTexture?.Draw(vertices, start, count, type);
        }

        /// <summary>
        /// Draw primitives defined by a sub-array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="start">Index of the first vertex to use in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states)
        {
            myRenderTexture?.Draw(vertices, start, count, type, states);
        }

        /// <summary>
        /// Update the contents of the canvas
        /// </summary>
        public void Display()
        {
            myRenderTexture?.Display();
        }


        /// <summary>
        /// Function called when widget size changes
        /// </summary>
        /// <param name="size">New size of the widget</param>
        protected override void OnSizeChanged(Vector2f size)
        {
            if (((int)size.X <= 0) || ((int)size.Y <= 0))
            {
                myRenderTexture = null;
                mySprite = new Sprite();
                return;
            }

            myRenderTexture = new RenderTexture((uint)size.X, (uint)size.Y);
            mySprite = new Sprite(myRenderTexture.Texture);

            myRenderTexture.Clear();
            myRenderTexture.Display();
        }

        /// <summary>
        /// Function called when the widget wants to know if the mouse is on top of it
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        /// <returns>Whether the mouse is on top of the widget</returns>
        protected override bool OnMouseOnWidget(Vector2f pos)
        {
            return (new FloatRect(Position.X, Position.Y, Size.X, Size.Y)).Contains(pos.X, pos.Y);
        }

        /// <summary>
        /// Function called when a renderer property changes
        /// </summary>
        /// <param name="property">Property that was changed</param>
        /// <returns>
        /// True if the change has been fully processed, false when the
        /// base class should also be informed about the change.
        /// </returns>
        protected override bool OnRendererChanged(string property)
        {
            if (property == "opacity")
            {
                Color color = mySprite.Color;
                color.A = (byte)(255 * SharedRenderer.Opacity);
                mySprite.Color = color;
            }

            return false;
        }

        /// <summary>
        /// Function called when widget should draw itself
        /// </summary>
        /// <param name="states">States for drawing</param>
        protected override void OnDraw(RenderStates states)
        {
            states.Transform.Translate(Position);
            myParentGui.Target.Draw(mySprite, states);
        }


        private RenderTexture myRenderTexture = null;
        private Sprite mySprite = new Sprite();
    }
}
