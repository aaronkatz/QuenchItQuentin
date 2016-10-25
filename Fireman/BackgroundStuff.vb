Module BackgroundStuff
    Structure Background
        Dim bPic As Image
        Dim bLocation As Point
        Dim bHeight As Integer
        Dim bWidth As Integer
    End Structure
    Public mBackground As Background
    Public mG As Graphics
    Public mOffG As Graphics
    Public mOffScreenImage As Image

    Public Sub loadBackground()
        mBackground.bPic = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\Background.png")
        mBackground.bLocation.X = 0
        mBackground.bLocation.Y = 0
        mBackground.bWidth = mBackground.bPic.Width
        mBackground.bHeight = mBackground.bPic.Height

        mOffScreenImage = New Bitmap(mBackground.bPic)
    End Sub
    Public Sub drawBackground()
        mOffG.DrawImage(mBackground.bPic, mBackground.bLocation.X, mBackground.bLocation.Y)
    End Sub
End Module
