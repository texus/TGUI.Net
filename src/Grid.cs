/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus's Graphical User Interface
// Copyright (C) 2012-2013 Bruno Van de Velde (vdv_b@tgui.eu)
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
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class Grid : Container
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Grid ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Grid (Grid copy) : base(copy)
        {
            m_Size                  = copy.m_Size;
            m_IntendedSize          = copy.m_IntendedSize;

            var widgets = copy.GetWidgets();

            for (int row = 0; row < copy.m_GridWidgets.Count; ++row)
            {
                for (int col = 0; col < copy.m_GridWidgets[row].Count; ++col)
                {
                    // Find the widget that belongs in this square
                    for (int i = 0; i < widgets.Count; ++i)
                    {
                        // If an widget matches then add it to the grid
                        if (widgets[i] == copy.m_GridWidgets[row][col])
                            AddWidget(widgets[i], (uint)row, (uint)col, copy.m_ObjBorders[row][col], copy.m_ObjLayout[row][col]);
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the grid
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                // A negative size is not allowed for this object
                if (value.X  < 0) value.X  = -value.X;
                if (value.Y < 0) value.Y = -value.Y;

                // Change the intended size of the grid
                m_IntendedSize.X = value.X;
                m_IntendedSize.Y = value.Y;

                UpdatePositionsOfAllWidgets();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes a single widget that was added to the grid
        /// </summary>
        ///
        /// <param name="widget">The widget to remove</param>
        ///
        /// Usage example:
        /// <code>
        /// Picture pic = grid.Add(new Picture("1.png"), "picName");
        /// Picture pic2 = grid.Add(new Picture("2.png"), "picName2")
        /// grid.remove(pic);
        /// grid.remove(grid.get("picName2"));
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Remove(Widget widget)
        {
            // Find the widget in the grid
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] == widget)
                    {
                        // Remove the widget from the grid
                        m_GridWidgets [row].RemoveAt (col);
                        m_ObjBorders [row].RemoveAt (col);
                        m_ObjLayout [row].RemoveAt (col);

                        // Check if this is the last column
                        if (m_ColumnWidth.Count == m_GridWidgets[row].Count + 1)
                        {
                            // Check if there is another row with this many columns
                            bool rowFound = false;
                            for (int i = 0; i < m_GridWidgets.Count; ++i)
                            {
                                if (m_GridWidgets[i].Count >= m_ColumnWidth.Count)
                                {
                                    rowFound = true;
                                    break;
                                }
                            }

                            // Erase the last column if no other row is using it
                            if (rowFound == false)
                                m_ColumnWidth.RemoveAt(m_ColumnWidth.Count - 1);
                        }

                        // If the row is empty then remove it as well
                        if (m_GridWidgets[row].Count == 0)
                        {
                            m_GridWidgets.RemoveAt (row);
                            m_ObjBorders.RemoveAt (row);
                            m_ObjLayout.RemoveAt (row);
                            m_RowHeight.RemoveAt (row);
                        }

                        // Update the positions of all remaining widgets
                        UpdatePositionsOfAllWidgets();
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void RemoveAllWidgets()
        {
            m_GridWidgets.Clear();
            m_ObjBorders.Clear();
            m_ObjLayout.Clear();

            m_RowHeight.Clear();
            m_ColumnWidth.Clear();

            base.RemoveAllWidgets();

            m_Size.X = 0;
            m_Size.Y = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add a widget to the grid
        /// </summary>
        ///
        /// <param name="widget">Fully created widget that will be added to the grid</param>
        /// <param name="row">The row in which the widget should be placed</param>
        /// <param name="col">The column in which the widget should be placed</param>
        /// <param name="borders">Distance from the grid square to the widget (left, top, right, bottom)</param>
        /// <param name="layout">Where the widget is located in the square</param>
        ///
        /// Usage example:
        /// <code>
        /// Picture pic = grid.Add(new Picture("1.png"));
        /// pic.Size = new Vector2f(400, 300);
        /// grid.AddWidget(pic, 0, 0);
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddWidget (Widget widget, uint row, uint col, Borders borders, Layouts layout)
        {
            // Create the row if it didn't exist yet
            while (m_GridWidgets.Count < row + 1) m_GridWidgets.Add (new List<Widget>());
            while (m_ObjBorders.Count < row + 1)  m_ObjBorders.Add (new List<Borders>());
            while (m_ObjLayout.Count < row + 1)   m_ObjLayout.Add (new List<Layouts>());

            while (m_GridWidgets[(int)row].Count < col + 1) m_GridWidgets [(int)row].Add (null);
            while (m_ObjBorders[(int)row].Count < col + 1)  m_ObjBorders [(int)row].Add (new Borders(0, 0, 0, 0));
            while (m_ObjLayout[(int)row].Count < col + 1)   m_ObjLayout [(int)row].Add (Layouts.Center);

            // If this is a new row then reserve some space for it
            while (m_RowHeight.Count < row + 1)
                m_RowHeight.Add(0);

            // If this is the first row to have so many columns then reserve some space for it
            if (m_ColumnWidth.Count < col + 1)
                m_ColumnWidth.Add(0);
           
            // Add the widget to the grid
            m_GridWidgets[(int)row][(int)col] = widget;
            m_ObjBorders[(int)row][(int)col] = borders;
            m_ObjLayout[(int)row][(int)col] = layout;

            // Update the widgets
            UpdateWidgets();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the widget in a specific square of the grid
        /// </summary>
        ///
        /// <param name="row">The row that the widget is in</param>
        /// <param name="col">The column that the widget is in</param>
        ///
        /// <returns>The widget inside the given square, or null when the square doesn't contain a widget</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Widget getWidget (uint row, uint col)
        {
            if (((uint)m_GridWidgets.Count > row) && ((uint)m_GridWidgets[(int)row].Count > col))
                return m_GridWidgets[(int)row][(int)col];
            else
                return null;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the position and size of the widget.
        /// </summary>
        ///
        /// Once a widget has been added to the grid, you will have to call this function every time you change the size of the widget.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UpdateWidgets ()
        {
            // Reset the column widths
            for (int i = 0; i < m_ColumnWidth.Count; ++i)
                m_ColumnWidth[i] = 0;
           
            // Loop through all widgets
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                // Reset the row height
                m_RowHeight[row] = 0;

                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] == null)
                        continue;

                    // Remember the biggest column width
                    if (m_ColumnWidth[col] < m_GridWidgets[row][col].Size.X + m_ObjBorders[row][col].Left + m_ObjBorders[row][col].Right)
                        m_ColumnWidth[col] = (uint)(m_GridWidgets[row][col].Size.X + m_ObjBorders[row][col].Left + m_ObjBorders[row][col].Right);

                    // Remember the biggest row height
                    if (m_RowHeight[row] < m_GridWidgets[row][col].Size.Y + m_ObjBorders[row][col].Top + m_ObjBorders[row][col].Bottom)
                        m_RowHeight[row] = (uint)(m_GridWidgets[row][col].Size.Y + m_ObjBorders[row][col].Top + m_ObjBorders[row][col].Bottom);
                }
            }

            // Reposition all widgets
            UpdatePositionsOfAllWidgets();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Changes borders of a given widget
        /// </summary>
        ///
        /// <param name="widget">The widget to which borders should be added</param>
        /// <param name="borders">The new borders around the widget</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ChangeWidgetBorders(Widget widget, Borders borders)
        {
            // Find the widget in the grid
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] == widget)
                    {
                        // Change borders of the widget
                        m_ObjBorders[row][col] = borders;

                        // Update all widgets
                        UpdateWidgets();
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Changes the layout of a given widget
        /// </summary>
        ///
        /// <param name="widget">The widget for which the layout should be changed</param>
        /// <param name="layout">The new layout</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void changeWidgetLayout (Widget widget, Layouts layout)
        {
            // Find the widget in the grid
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] == widget)
                    {
                        // Change the layout of the widget
                        m_ObjLayout[row][col] = layout;

                        // Recalculate the position of the widget
                        {
                            // Calculate the available space which is distributed when widgets are positionned.
                            Vector2f availableSpace = new Vector2f(0, 0);
                            Vector2f minSize = GetMinSize();
                            if (m_Size.X > minSize.X)
                                availableSpace.X = m_Size.X - minSize.X;
                            if (m_Size.Y > minSize.Y)
                                availableSpace.Y = m_Size.Y - minSize.Y;

                            Vector2f availSpaceOffset = new Vector2f(0.5f * availableSpace.X / m_ColumnWidth.Count,
                                                                     0.5f * availableSpace.Y / m_RowHeight.Count);
                            float left = 0;
                            float top = 0;

                            for (int i = 0; i < row; ++i)
                                top += m_RowHeight[i] + 2 * availSpaceOffset.Y;

                            for (int i = 0; i < col; ++i)
                                left += m_ColumnWidth[i] + 2 * availSpaceOffset.X;

                            switch (m_ObjLayout[row][col])
                            {
                            case Layouts.UpperLeft:
                                left += m_ObjBorders [row] [col].Left + availSpaceOffset.X;
                                top += m_ObjBorders [row] [col].Top + availSpaceOffset.Y;
                                break;

                            case Layouts.Up:
                                left += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                                top += m_ObjBorders[row][col].Top + availSpaceOffset.Y;
                                break;

                            case Layouts.UpperRight:
                                left += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                                top += m_ObjBorders[row][col].Top + availSpaceOffset.Y;
                                break;

                            case Layouts.Right:
                                left += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                                top += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                                break;

                            case Layouts.BottomRight:
                                left += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                                top += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                                break;

                            case Layouts.Bottom:
                                left += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                                top += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                                break;

                            case Layouts.BottomLeft:
                                left += m_ObjBorders[row][col].Left + availSpaceOffset.X;
                                top += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                                break;

                            case Layouts.Left:
                                left += m_ObjBorders[row][col].Left + availSpaceOffset.X;
                                top += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                                break;

                            case Layouts.Center:
                                left += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                                top += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                                break;
                            }

                            m_GridWidgets[row][col].Position = new Vector2f(left, top);
                        }
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the minimum size required by the grid to display correctly all widgets
        /// </summary>
        ///
        /// <returns>Minimum size</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private Vector2f GetMinSize ()
        {
            // Calculate the required place to have all widgets in the grid.
            Vector2f minSize = new Vector2f(0, 0);

            // Loop through all rows to find the minimum height required by the grid
            for (int i = 0; i < m_RowHeight.Count; ++i)
                minSize.Y += m_RowHeight[i];

            // Loop through all columns to find the minimum width required by the grid
            for (int i = 0; i < m_ColumnWidth.Count; ++i)
                minSize.X += m_ColumnWidth[i];

            return minSize;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Reposition all the widgets
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdatePositionsOfAllWidgets ()
        {
            Vector2f position = new Vector2f(0, 0);
            Vector2f previousPosition = new Vector2f(0, 0);

            // Calculate m_Size and the available space which will be distributed when widgets will be positionned.
            Vector2f availableSpace = new Vector2f(0, 0);
            m_Size = m_IntendedSize;
            Vector2f minSize = GetMinSize();

            if (m_IntendedSize.X > minSize.X)
                availableSpace.X = m_IntendedSize.X - minSize.X;
            else
                m_Size.X = minSize.X;

            if (m_IntendedSize.Y > minSize.Y)
                availableSpace.Y = m_IntendedSize.Y - minSize.Y;
            else
                m_Size.Y = minSize.Y;

            Vector2f availSpaceOffset = new Vector2f(0.5f * availableSpace.X / m_ColumnWidth.Count,
                                                     0.5f * availableSpace.Y / m_RowHeight.Count);

            // Loop through all rows
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                // Remember the current position
                previousPosition = position;

                // Loop through all widgets in the row
                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] == null)
                    {
                        position.X += m_ColumnWidth[col] + 2 * availSpaceOffset.X;
                        continue;
                    }

                    Vector2f cellPosition = position;

                    // Place the next widget on the correct position
                    switch (m_ObjLayout[row][col])
                    {
                    case Layouts.UpperLeft:
                        cellPosition.X += m_ObjBorders[row][col].Left + availSpaceOffset.X;
                        cellPosition.Y += m_ObjBorders[row][col].Top + availSpaceOffset.Y;
                        break;

                    case Layouts.Up:
                        cellPosition.X += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                        cellPosition.Y += m_ObjBorders[row][col].Top + availSpaceOffset.Y;
                        break;

                    case Layouts.UpperRight:
                        cellPosition.X += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                        cellPosition.Y += m_ObjBorders[row][col].Top + availSpaceOffset.Y;
                        break;

                    case Layouts.Right:
                        cellPosition.X += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                        cellPosition.Y += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                        break;

                    case Layouts.BottomRight:
                        cellPosition.X += m_ColumnWidth[col] - m_ObjBorders[row][col].Right - m_GridWidgets[row][col].Size.X + availSpaceOffset.X;
                        cellPosition.Y += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                        break;

                    case Layouts.Bottom:
                        cellPosition.X += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                        cellPosition.Y += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                        break;

                    case Layouts.BottomLeft:
                        cellPosition.X += m_ObjBorders[row][col].Left + availSpaceOffset.X;
                        cellPosition.Y += m_RowHeight[row] - m_ObjBorders[row][col].Bottom - m_GridWidgets[row][col].Size.Y + availSpaceOffset.Y;
                        break;

                    case Layouts.Left:
                        cellPosition.X += m_ObjBorders[row][col].Left + availSpaceOffset.Y;
                        cellPosition.Y += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                        break;

                    case Layouts.Center:
                        cellPosition.X += m_ObjBorders[row][col].Left + (((m_ColumnWidth[col] - m_ObjBorders[row][col].Left - m_ObjBorders[row][col].Right) - m_GridWidgets[row][col].Size.X) / 2.0f) + availSpaceOffset.X;
                        cellPosition.Y += m_ObjBorders[row][col].Top + (((m_RowHeight[row] - m_ObjBorders[row][col].Top - m_ObjBorders[row][col].Bottom) - m_GridWidgets[row][col].Size.Y) / 2.0f) + availSpaceOffset.Y;
                        break;
                    }

                    m_GridWidgets[row][col].Position = cellPosition;
                    position.X += m_ColumnWidth[col] + 2 * availSpaceOffset.X;
                }

                // Go to the next row
                position = previousPosition;
                position.Y += m_RowHeight[row] + 2 * availSpaceOffset.Y;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Ask the widget if the mouse is on top of it
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            // Check if the mouse might be on top of the grid
            if ((x > Position.X) && (y > Position.Y))
            {
                // Check if the mouse is on the grid
                if ((x < Position.X + Size.X) && (y < Position.Y + Size.Y))
                    return true;
            }

            if (m_MouseHover)
            {
                MouseLeftWidget();

                // Tell the widgets inside the grid that the mouse is no longer on top of them
                for (int i = 0; i < m_EventManager.m_Widgets.Count; ++i)
                    m_EventManager.m_Widgets[i].MouseNotOnWidget();

                m_MouseHover = false;
            }

            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Set the transformation
            states.Transform *= Transform;

            // Draw all widgets
            for (int row = 0; row < m_GridWidgets.Count; ++row)
            {
                for (int col = 0; col < m_GridWidgets[row].Count; ++col)
                {
                    if (m_GridWidgets[row][col] != null)
                        target.Draw(m_GridWidgets[row][col], states);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The layout of the widget
        /// </summary>
        ///
        /// Where in the cell is the widget located?
        /// The widget is centered by default.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum Layouts
        {
            /// Draw the widget in the upper left corner of the cell
            UpperLeft,

            /// Draw the widget at the upper side of the cell (horizontally centered)
            Up,

            /// Draw the widget in the upper right corner of the cell
            UpperRight,

            /// Draw the widget at the right side of the cell (vertically centered)
            Right,

            /// Draw the widget in the bottom right corner of the cell
            BottomRight,

            /// Draw the widget at the bottom of the cell (horizontally centered)
            Bottom,

            /// Draw the widget in the bottom left corner of the cell
           BottomLeft,

            /// Draw the widget at the left side of the cell (vertically centered)
            Left,

            /// Center the widget in the cell
            Center
        };

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<List<Widget>> m_GridWidgets = new List<List<Widget>> ();
        private List<List<Borders>> m_ObjBorders = new List<List<Borders>> ();
        private List<List<Layouts>> m_ObjLayout = new List<List<Layouts>> ();

        private List<uint> m_RowHeight = new List<uint> ();
        private List<uint> m_ColumnWidth = new List<uint> ();

        private Vector2f m_Size = new Vector2f(0, 0);
        private Vector2f m_IntendedSize = new Vector2f(0, 0);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
