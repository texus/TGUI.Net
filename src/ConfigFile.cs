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
using System.IO;
using System.Collections.Generic;
using SFML.Graphics;

namespace TGUI
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// \internal
    // Reads the config files that are used to load widgets.
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class ConfigFile
    {
        private string m_Section;

        public List<string> Properties = new List<string> ();
        public List<string> Values = new List<string> ();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Opens the given file and searches for the given section.
        // The properties and the corresponding values in that section will be stored.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ConfigFile (string filename, string section)
        {
            m_Section = section;

            StreamReader reader = new StreamReader(filename);
            uint lineNr = 0;
            bool sectionFound = false;

            // Read until the end of the file is reached
            while (reader.Peek() >= 0) 
            {
                // Read the line
                lineNr++;
                string line = reader.ReadLine();

                // Split on quotes
                List<string> words = new List<string>(line.Split('"'));

                // If the last character is a '\' then it might have been an escape for the quote
                int i = 0;
                bool betweenQuotes = false;
                while (i < words.Count)
                {
                    // Check if you are between quotes
                    if (betweenQuotes)
                    {
                        // When the last char is a backslash, then you are escaping the next quote
                        if (words [i] [words[i].Length-1] == '\\')
                        {
                            if (i + 1 < words.Count)
                            {
                                words [i] += words [i+1];
                                words.RemoveAt (i+1);

                                betweenQuotes = !betweenQuotes;
                            }
                        }
                    }
                    else // Remove all whitespace when not between quotes
                    {
                        string[] wordList = words [i].Split (new char[] {' ', '\t', '\r', '\n'});
                        words [i] = "";

                        foreach (string word in wordList)
                            words [i] += word;
                    }

                    // Read the next part
                    betweenQuotes = !betweenQuotes;
                    ++i;
                }

                // Reassemble the line
                line = "";
                foreach (string w in words)
                    line += w + "\"";

                // Added one quote too much
                line = line.Remove (line.Length-1);

                // Only continue when the line isn't empty
                if (line != "")
                {
                    // Check if this is a section
                    if (line[line.Length-1] == ':')
                    {
                        // If we already found our section then this would be the next section
                        if (sectionFound)
                            return;

                        // Check if this is the section we were looking for
                        if (section.ToLower () == line.Substring (0, line.Length - 1).ToLower ())
                            sectionFound = true;
                    }
                    else // This isn't a section
                    {
                        // We are only interested in one section
                        if (sectionFound)
                        {
                            // Look for the assignment character
                            int index = line.IndexOf ('=');
                            if (index == -1)
                                throw new Exception("Parse error in " + filename + " on line " + lineNr + ".");

                            // Split the line in property and value
                            Properties.Add (line.Substring (0, index).ToLower());
                            Values.Add(line.Substring (index + 1));
                        }
                    }
                }
            }

            // Output an error when the section wasn't found
            if (!sectionFound)
                throw new Exception("Failed to find section " + section + " in " + filename + ".");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Convert the value to a bool.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool ReadBool (int i)
        {
            if ((Values [i] == "true") || (Values [i] == "True") || (Values [i] == "TRUE") || (Values [i] == "1"))
                return true;
            else if ((Values [i] == "false") || (Values [i] == "False") || (Values [i] == "FALSE") || (Values [i] == "0"))
                return false;
            
            throw new Exception("Property " + Properties[i] + " in section " + m_Section + " has a bad value: '" + Values[i] + "'.");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Convert the value to a color.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color ReadColor (int i)
        {
            Color color;
            if (!Internal.ExtractColor (Values[i], out color))
                Internal.Output ("TGUI warning: Property " + Properties[i] + " in section " + m_Section
                                 + " has a bad value: '" + Values[i] + "'. Using the default white color.");
            
            return color;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Load the texture that is described by the value.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ReadTexture (int i, string folder, Impl.Sprite sprite)
        {
            // The shortest string needs three character
            if (Values [i].Length < 2)
                throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                     + " doesn't contain a filename between quotes.");

            // The first character has to be a quote
            if (Values [i] [0] != '"')
                throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                     + " didn't begin with a double quote.");

            // There has to be another quote
            int index = Values [i].IndexOf ('"', 1);
            if (index == -1)
                throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                     + " didn't contain an ending quote.");

            // There can't be more than two quotes
            int unexistingIndex = Values [i].IndexOf ('"', index + 1);
            if (unexistingIndex != -1)
                throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                     + " contains more than two quotes.");

            // Check if there is still something behind the quotes
            IntRect rect = new IntRect ();
            if (Values [i].Length > index + 1)
            {
                // Drop the brackets
                if (Values [i] [index + 1] == '(' && Values [i] [Values [i].Length-1] == ')')
                {
                    string rectStr = Values [i].Substring (index + 2, Values [i].Length - index - 3);

                    // Extract the rect
                    string[] rectComponents = rectStr.Split (',');

                    // A rectangle has 4 components (left, top, right, height)
                    if (rectComponents.Length == 4)
                    {
                        rect = new IntRect (Convert.ToInt32(rectComponents [0]), Convert.ToInt32(rectComponents [1]),
                                            Convert.ToInt32(rectComponents [2]), Convert.ToInt32(rectComponents [3]));
                    }
                    else
                        throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                             + " contains brackets after the filename, but without four components split by commas.");
                }
                else // The string doesn't begin and end with a bracket
                    throw new Exception ("The value of property " + Properties[i] + " in section " + m_Section
                                         + " contains contains characters after the filename of which the first and last are not brackets.");
            }

            // Try to load the strings between the quotes
            Global.TextureManager.GetTexture (folder + Values[i].Substring(1, index - 1), sprite, rect);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

