---
layout: default
title: Example code (C#)
---

The example code below will create a window with two edit boxes and a button.

Positions and sizes are hardcoded in the example, but TGUI also supports relative positions/sizes:

```csharp
editBoxUsername.SetSize(new Layout2d("65%", "12.5%"));
```


```csharp
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

            RenderWindow window = new RenderWindow(new VideoMode(width, height), "TGUI.Net example");
            Gui gui = new Gui(window);

            window.Closed += (s,e) => window.Close();

            Picture picture = new Picture("background.jpg");
            picture.Size = new Vector2f(width, height);
            gui.Add(picture);

            EditBox editBoxUsername = new EditBox();
            editBoxUsername.Position = new Vector2f(width / 6, height / 6);
            editBoxUsername.Size = new Vector2f(width * 2/3, height / 8);
            editBoxUsername.DefaultText = "Username";
            gui.Add(editBoxUsername);

            EditBox editBoxPassword = new EditBox(editBoxUsername);
            editBoxPassword.Position = new Vector2f(width / 6, height * 5/12);
            editBoxPassword.PasswordCharacter = '*';
            editBoxPassword.DefaultText = "Password";
            gui.Add(editBoxPassword);

            Button button = new Button("Login");
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
```
