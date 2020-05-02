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
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace TGUI.Example
{
    class Example
    {
        static void Main(string[] args)
        {
            const uint width = 400;
            const uint height = 300;

            var window = new RenderWindow(new VideoMode(width, height), "TGUI.Net example");
            var gui = new Gui(window);

            window.Closed += (s,e) => window.Close();

            var picture = new Picture("background.jpg");
            picture.Size = new Vector2f(width, height);
            gui.Add(picture);

            var editBoxUsername = new EditBox();
            editBoxUsername.Position = new Vector2f(width / 6, height / 6);
            editBoxUsername.Size = new Vector2f(width * 2/3, height / 8);
            editBoxUsername.DefaultText = "Username";
            gui.Add(editBoxUsername);

            var editBoxPassword = new EditBox(editBoxUsername);
            editBoxPassword.Position = new Vector2f(width / 6, height * 5/12);
            editBoxPassword.PasswordCharacter = '*';
            editBoxPassword.DefaultText = "Password";
            gui.Add(editBoxPassword);

            var button = new Button("Login");
            button.Position = new Vector2f(width / 4, height * 7/10);
            button.Size = new Vector2f(width / 2, height / 6);
            gui.Add(button);

            button.Pressed += (s, e) => Console.WriteLine("Username: " + editBoxUsername.Text + "\n"
                                                          + "Password: " + editBoxPassword.Text);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear();
                gui.Draw();
                window.Display();
            }
        }
    }
}
