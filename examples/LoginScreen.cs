using System;
using SFML.Window;
using SFML.Graphics;
using TGUI;

namespace Program
{
    static class Program
    {
        static public void LoadWidgets ( Gui gui )
        {
            // Create the background image
            Picture picture = gui.Add (new Picture("xubuntu_bg_aluminium.jpg"));
            picture.Size = new Vector2f(800, 600);

            // Create the username label
            Label labelUsername = gui.Add (new Label());
            labelUsername.Text = "Username:";
            labelUsername.Position = new Vector2f(200, 100);

            // Create the password label
            Label labelPassword = gui.Add (new Label());
            labelPassword.Text = "Password:";
            labelPassword.Position = new Vector2f(200, 250);

            // Create the username edit box
            EditBox editBoxUsername = gui.Add (new EditBox("../widgets/Black.conf"), "Username");
            editBoxUsername.Size = new Vector2f(400, 40);
            editBoxUsername.Position = new Vector2f(200, 140);

            // Create the password edit box (we will copy the previously created edit box)
            EditBox editBoxPassword = gui.Add(new EditBox(editBoxUsername), "Password");
            editBoxPassword.Position = new Vector2f(200, 290);
            editBoxPassword.PasswordCharacter = "*";

            // Create the login button
            Button button = gui.Add (new Button("../widgets/Black.conf"));
            button.Size = new Vector2f(260, 60);
            button.Position = new Vector2f(270, 440);
            button.Text = "Login";
            button.CallbackId = 1;
            button.LeftMouseClickedCallback += OnButtonClick;
        }

        static void Main ()
        {
            // Create the window
            RenderWindow window = new RenderWindow (new VideoMode(800, 600), "TGUI.Net Login Screen Example");
            Gui gui = new Gui (window);

            window.Closed += OnClosed;

            // Load the font
            gui.GlobalFont = new Font("../fonts/DejaVuSans.ttf");

            // Load the widgets
            LoadWidgets (gui);

            // Main loop
            while (window.IsOpen())
            {
                // Process events
                window.DispatchEvents ();

                window.Clear();
                gui.Draw();
                window.Display();
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            ((Window)sender).Close ();
        }

        static void OnButtonClick(object sender, CallbackArgs e)
        {
            // Get the username and password
            EditBox editBoxUsername = ((Button)sender).Parent.Get<EditBox>("Username");
            EditBox editBoxPassword = ((Button)sender).Parent.Get<EditBox>("Password");

            System.Console.WriteLine("Username: " + editBoxUsername.Text);
            System.Console.WriteLine("Password: " + editBoxPassword.Text);
        }
    }
}