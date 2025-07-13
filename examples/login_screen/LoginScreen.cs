using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace TGUI.Example
{
    class Example
    {
        static TGUI.EditBox? editBoxUsername = null;
        static TGUI.EditBox? editBoxPassword = null;

        const uint width = 400;
        const uint height = 300;

        static void CreateWidgets(TGUI.Gui gui)
        {
            // TGUI objects should be disposed when you no longer need them in C# to reduce memory usage.
            // This can be done by calling "widget.Dispose()" or doing it automatically when the object
            // goes out of scope by making use of the "using" keyword.
            // Note that disposing only releases the C# handle. The internal c++ code relies on reference counting
            // to determine when an object is destroyed. Adding the widget to the gui added another reference,
            // so the widget will continue to exist internally until it is also removed from the gui.
            // If the C# object isn't disposed then a reference remains until the garbage collector finalizes the C# object.
            using var texture = new TGUI.Texture("background.jpg");
            using var picture = new TGUI.Picture();
            picture.Renderer.Texture = texture;
            picture.Size = new TGUI.Vector2f(width, height);
            gui.Add(picture);

            // We don't use "using" here because we still need the edit boxes in our button callback
            // after this function has finished executing. If the code contained "using var", the C# object
            // would be destroyed at the end of this function. While the edit box continues to exist in the gui,
            // we could no longer access it with our C# object (unless we get a new reference with gui.Get("EditUsername")).
            editBoxUsername = new TGUI.EditBox();
            editBoxUsername.Position = new Vector2f(width / 6, height / 6);
            editBoxUsername.Size = new Vector2f(width * 2/3, height / 8);
            editBoxUsername.DefaultText = "Username";
            gui.Add(editBoxUsername, "EditUsername");

            editBoxPassword = new TGUI.EditBox(editBoxUsername);
            editBoxPassword.Position = new Vector2f(width / 6, height * 5/12);
            editBoxPassword.PasswordCharacter = "*";
            editBoxPassword.DefaultText = "Password";
            gui.Add(editBoxPassword, "EditPassword");

            using var button = new TGUI.Button();
            button.Text = "Login";
            button.Position = new Vector2f(width / 4, height * 7/10);
            button.Size = new Vector2f(width / 2, height / 6);
            gui.Add(button, "ButtonLogin");

            // Print the values of the edit boxes when the login button is pressed.
            // The editBoxUsername and editBoxPassword variables should not have been disposed
            // by the time the user presses the button, or attempting to access them will throw.
            button.OnPress += (_,_) => Console.WriteLine("Username: " + editBoxUsername.Text + "\n"
                                                       + "Password: " + editBoxPassword.Text);
        }

        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(width, height), "TGUI.Net example");
            window.Closed += (_,e) => window.Close();

            // The first TGUI object to create should always be the Gui.
            // Other TGUI objects should only be used while a gui exists.
            var gui = new TGUI.Gui(window);

            // We create the widgets in a separate function in this example to demonstrate that
            // the widgets don't need to be stored in C# in order for them to be part of the gui.
            CreateWidgets(gui);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear();
                gui.Draw();
                window.Display();
            }

            // Cleanup TGUI resources. This isn't really needed here since this is the end of the program,
            // but the purpose of the example is to show how to properly do this. Note that if TGUI objects aren't disposed,
            // the same cleanup code will be executed by the finalizer. Disposing explicitly can reduce memory usage though,
            // mostly because the garbage collector doesn't know about the large amount of unmanaged c++ memory being reserved
            // until the lightweight C# object is destroyed. So the garbage collector can let objects linger around too long.
            // The other widgets were already disposed in CreateWidgets because of the "using" keyword.
            editBoxUsername?.Dispose();
            editBoxPassword?.Dispose();

            // After the gui is disposed or finialized, you should no longer execute any TGUI code. All remaining C# objects
            // are disposed internally when the gui is destroyed. Finalizers or Dispose calls (which will be no-ops) are still allowed though.
            gui.Dispose();
        }
    }
}
