Imports SFML
Imports SFML.Window
Imports SFML.Graphics
Imports TGUI
 
Module Program
 
    Dim WithEvents window As RenderWindow
 
    ''' <summary>
    ''' Entry point of application
    ''' </summary>
    Sub Main()
 
        window = New RenderWindow(New VideoMode(800, 600), "TGUI.Net example (Visual Basic)")
 
        ' Create the Gui and load the font that all widgets will use
        Dim gui = New Gui(window)
        gui.GlobalFont = New Font("../fonts/DejaVuSans.ttf")
 
        ' Load the widgets
        LoadWidgets(gui)
 
        While (window.IsOpen())
 
            window.DispatchEvents()
 
            window.Clear()
            gui.Draw()
            window.Display()
 
        End While
 
    End Sub
 
 
    ''' <summary>
    ''' Function that loads all needed widgets
    ''' </summary>
    Sub LoadWidgets(ByVal gui As Gui)
 
        ' Create the background image
        Dim picture = gui.Add(New Picture("xubuntu_bg_aluminium.jpg"))
        picture.Size = New Vector2f(800, 600)
 
        ' Create the username label
        Dim labelUsername = gui.Add(New Label())
        labelUsername.Text = "Username:"
        labelUsername.Position = New Vector2f(200, 100)
 
        ' Create the password label
        Dim labelPassword = gui.Add(New Label())
        labelPassword.Text = "Password:"
        labelPassword.Position = New Vector2f(200, 250)
 
        ' Create the username edit box
        Dim editBoxUsername = gui.Add(New EditBox("../widgets/Black.conf"), "Username")
        editBoxUsername.Size = New Vector2f(400, 40)
        editBoxUsername.Position = New Vector2f(200, 140)
 
        ' Create the password edit box (we will copy the previously created edit box)
        Dim editBoxPassword = gui.Add(New EditBox(editBoxUsername), "Password")
        editBoxPassword.Position = New Vector2f(200, 290)
        editBoxPassword.PasswordCharacter = "*"
 
        ' Create the login button
        Dim button = gui.Add(New Button("../widgets/Black.conf"))
        button.Size = New Vector2f(260, 60)
        button.Position = New Vector2f(270, 440)
        button.Text = "Login"
        button.CallbackId = 1
        AddHandler button.LeftMouseClickedCallback, AddressOf OnButtonClick
 
    End Sub
 
 
    ''' <summary>
    ''' Function called when the window is closed
    ''' </summary>
    Sub App_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        Dim window = CType(sender, RenderWindow)
        window.Close()
    End Sub
 
 
    ''' <summary>
    ''' Function called when the login button is clicked
    ''' </summary>
    Sub OnButtonClick(ByVal sender As Object, ByVal e As CallbackArgs)
 
        ' Get the username and password
        Dim editBoxUsername = CType(sender, Button).Parent.Get(Of EditBox)("Username")
        Dim editBoxPassword = CType(sender, Button).Parent.Get(Of EditBox)("Password")
 
        System.Console.WriteLine("Username: " + editBoxUsername.Text)
        System.Console.WriteLine("Password: " + editBoxPassword.Text)
 
    End Sub
 
End Module
