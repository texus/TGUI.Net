using System;
using SFML.Window;
using SFML.Graphics;
using TGUI;

namespace Program
{
    static class Program
    {
        static void Main ()
        {
            RenderWindow window = new RenderWindow (new VideoMode(800, 600), "TGUI.Net Tabs Example");
            window.Closed += OnClosed;

            Gui gui = new Gui (window);
            gui.GlobalFont = new Font("../fonts/DejaVuSans.ttf");

            // Create the tabs
            Tab tabs = gui.Add (new Tab("../widgets/Black.conf"));
            tabs.Add ("First");
            tabs.Add ("Second");
            tabs.Position = new Vector2f (20, 20);

            // Create the first panel
            Panel panel1 = gui.Add (new Panel(), "FirstPanel");
            panel1.Size = new Vector2f (400, 300);
            panel1.Position = new Vector2f (tabs.Position.X, tabs.Position.Y + tabs.TabHeight);
            panel1.BackgroundTexture = new SFML.Graphics.Texture ("xubuntu_bg_aluminium.jpg");

            // Create the second panel (copy of first one, but with different image)
            Panel panel2 = gui.Add (new Panel(panel1), "SecondPanel");
            panel2.BackgroundTexture = new SFML.Graphics.Texture ("Linux.jpg");

            // Enable callback when another tab is selected
            tabs.TabChangedCallback += OnTabSelected;

            // Select the first tab and only show the first panel
            tabs.Select ("First");
            panel1.Visible = true;
            panel2.Visible = false;

            while (window.IsOpen())
            {
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

        static void OnTabSelected(object sender, CallbackArgs e)
        {
            // Get the tab that sent the callback
            Tab tab = (Tab)sender;

            // Access the gui.
            Container gui = tab.Parent;

            // Show the correct panel
            if (tab.GetSelected() == "First")
            {
                gui.Get<Panel> ("FirstPanel").Visible = true;
                gui.Get<Panel> ("SecondPanel").Visible = false;
            }
            else if (tab.GetSelected() == "Second")
            {
                gui.Get<Panel> ("FirstPanel").Visible = false;
                gui.Get<Panel> ("SecondPanel").Visible = true;
            }
        }
    }
}