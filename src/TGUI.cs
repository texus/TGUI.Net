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
using System.Resources;
using SFML.Graphics;

namespace TGUI
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class Global
    {
        public static TextureManager TextureManager = new TextureManager();

        public static ResourceManager ResourceManager = null;

        public static bool TabKeyUsageEnabled = true;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class Internal
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Output(string message)
        {
            System.Console.WriteLine (message);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool ExtractColor(string str, out Color color)
        {
            color = Color.White;

            // If the string is too small then it has to be wrong
            if (str.Length < 7)
                return false;

            // Drop the brackets
            if (str [0] == '(' && str [str.Length-1] == ')')
            {
                str = str.Substring (1, str.Length - 2);

                // Extract the colors
                string[] colors = str.Split (',');

                // Return the color
                if (colors.Length == 3)
                {
                    color = new Color (Convert.ToByte(colors [0]), Convert.ToByte(colors [1]), Convert.ToByte(colors [2]));
                    return true;
                }
                else if (colors.Length == 4)
                {
                    color = new Color (Convert.ToByte(colors [0]), Convert.ToByte(colors [1]),
                                       Convert.ToByte(colors [2]), Convert.ToByte(colors [3]));
                    return true;
                }
                else
                    return false;
            }
            else // The string doesn't begin and end with a bracket
                return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool ExtractBorders(string str, out Borders borders)
        {
            borders = new Borders(0, 0, 0, 0);

            // If the string is too small then it has to be wrong
            if (str.Length < 9)
                return false;

            // Drop the brackets
            if (str [0] == '(' && str [str.Length-1] == ')')
            {
                str = str.Substring (1, str.Length - 2);

                // Extract the borders
                string[] strBorders = str.Split (',');

                // Return the color
                if (strBorders.Length == 4)
                {
                    borders = new Borders(Convert.ToUInt32(strBorders [0]), Convert.ToUInt32(strBorders [1]),
                                          Convert.ToUInt32(strBorders [2]), Convert.ToUInt32(strBorders [3]));
                    return true;
                }
                else
                    return false;
            }
            else // The string doesn't begin and end with a bracket
                return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Exception : System.Exception
    {
        public Exception() : base("An unknown error was thrown.") {}
        public Exception(string message) : base(message) {}
        public Exception(string message, System.Exception inner) : base("An unknown error was thrown with message:" + message) {}

        // Constructor needed for serialization 
        // when exception propagates from a remoting server to the client.
        protected Exception(System.Runtime.Serialization.SerializationInfo info,
                            System.Runtime.Serialization.StreamingContext context) {}
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum WidgetPhase
    {
        Hover     = 1,
        MouseDown = 2,
        Focused   = 4,
        Selected  = 8
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

