Module SpriteStuff
    Structure AnimatedSprite
        Dim aPic As Image
        Dim aLocation As Point
        Dim aActive As Boolean
        Dim aTotalFrames As Integer
        Dim aCurrentFrame As Integer
    End Structure
    Structure Player
        Dim pPic As Image
        Dim pLocation As Point
        Dim pSpeed As Single
        Dim pTimeSinceLastShot As Integer
    End Structure
    Public mWaterDrops As List(Of AnimatedSprite) = New List(Of AnimatedSprite)
    Public mRemovableDrops As List(Of AnimatedSprite) = New List(Of AnimatedSprite)
    Public mFires(4, 9) As AnimatedSprite
    Public mHose As Player
    Public mScore As Integer
    Public gameTime As Integer
    Dim mFireFiles(4) As Image
    Dim mWaterFiles(1) As Image
    Public Sub loadPics()
        Dim wndex As Integer
        For wndex = 0 To 1
            mWaterFiles(wndex) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\waters" & wndex & ".png")
        Next wndex
        makeFires()
    End Sub
    Public Function touching(ByVal guy1 As AnimatedSprite, ByVal guy2 As AnimatedSprite)
        If guy1.aActive = True And guy2.aActive = True Then
            If guy1.aLocation.X < guy2.aLocation.X + guy2.aPic.Width And guy1.aLocation.X + guy1.aPic.Width > guy2.aLocation.X Then
                If guy1.aLocation.Y < guy2.aLocation.Y + guy2.aPic.Height And guy1.aLocation.Y + guy1.aPic.Height > guy2.aLocation.Y Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Sub removeOld()
        For Each water As AnimatedSprite In mWaterDrops
            If water.aActive = False Then
                mRemovableDrops.Add(water)
            End If
        Next water
        For Each rwater As AnimatedSprite In mRemovableDrops
            mWaterDrops.Remove(rwater)
        Next rwater
        mRemovableDrops.Clear()
    End Sub
    Public Sub makeWater()
        Dim water As New AnimatedSprite
        water.aTotalFrames = 2
        water.aCurrentFrame = 0
        water.aLocation.X = mHose.pLocation.X + 5
        water.aLocation.Y = mHose.pLocation.Y
        water.aActive = True
        water.aPic = mWaterFiles(water.aCurrentFrame)
        mWaterDrops.Add(water)
    End Sub
    Public Sub shootWater()
        If mHose.pTimeSinceLastShot > 7 Then

            makeWater()
            mHose.pTimeSinceLastShot = 0
        End If
    End Sub
    Public Sub makeFires()
        Dim fndex As Integer
        For fndex = 0 To 4
            mFireFiles(fndex) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\fires" & fndex & ".png")
        Next fndex
        Dim index As Integer
        Dim jndex As Integer
        For index = 0 To mFires.GetLength(0) - 1
            For jndex = 0 To mFires.GetLength(1) - 1
                mFires(index, jndex).aCurrentFrame = 0
                mFires(index, jndex).aTotalFrames = 5
                mFires(index, jndex).aActive = False
                mFires(index, jndex).aLocation.X = 280 + 44 * index
                mFires(index, jndex).aLocation.Y = 38 + 46 * jndex
                mFires(index, jndex).aPic = mFireFiles(mFires(index, jndex).aCurrentFrame)
            Next jndex
        Next index
    End Sub
    Public Sub drawAnimatedSprite(ByVal guy As AnimatedSprite)
        mOffG.DrawImage(guy.aPic, guy.aLocation.X, guy.aLocation.Y)
    End Sub
    Public Sub drawPlayer()
        mOffG.DrawImage(mHose.pPic, mHose.pLocation.X, mHose.pLocation.Y)
    End Sub
    Public Sub updatePlayer()
        mHose.pTimeSinceLastShot += 1
        mHose.pLocation.X += mHose.pSpeed
        If mHose.pLocation.X <= 0 Then
            mHose.pLocation.X = 0
        ElseIf mHose.pLocation.X >= 800 - mHose.pPic.Width Then
            mHose.pLocation.X = 800 - mHose.pPic.Width
        End If
    End Sub
    Public Sub updateFire(ByRef guy As AnimatedSprite)
        guy.aCurrentFrame += 1
        If (guy.aCurrentFrame >= guy.aTotalFrames) Then
            guy.aCurrentFrame = 0
        End If
        guy.aPic = mFireFiles(guy.aCurrentFrame)
    End Sub
    Public Sub updateWater(ByRef guy As AnimatedSprite)
        guy.aCurrentFrame += 1
        If (guy.aCurrentFrame >= guy.aTotalFrames) Then
            guy.aCurrentFrame = 0
        End If
        guy.aPic = mWaterFiles(guy.aCurrentFrame)
        guy.aLocation.Y -= 15
        If guy.aLocation.Y + guy.aPic.Height < 0 Then
            mScore -= 3
            guy.aActive = False
        End If
    End Sub
    Public Sub randomFireMaker()
        Dim index As Integer
        Dim jndex As Integer
        Dim counter As Integer
        counter = 0
        For index = 0 To mFires.GetLength(0) - 1
            For jndex = 0 To mFires.GetLength(1) - 1
                If mFires(index, jndex).aActive = True Then
                    counter += 1
                End If

            Next jndex
        Next index
        If counter = 50 Then
            Return
        End If
        Dim x As Integer
        Dim y As Integer
        Randomize()
        x = Int(Rnd() * 5)
        y = Int(Rnd() * 10)
        If mFires(x, y).aActive = True Then
            randomFireMaker()
        End If
        mFires(x, y).aActive = True
    End Sub
    Public Sub spawnFire()
        If gameTime Mod 20 = 0 Then
            randomFireMaker()
        End If
    End Sub
End Module
