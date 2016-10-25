Public Class MainForm
    Dim cInGame As Boolean
    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Left Then
            mHose.pSpeed = -10
        End If
        If e.KeyCode = Keys.Right Then
            mHose.pSpeed = 10
        End If
        If e.KeyCode = Keys.Space Then
            shootWater()
        End If
    End Sub
    Private Sub MainForm_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Left Then
            mHose.pSpeed = 0
        End If
        If e.KeyCode = Keys.Right Then
            mHose.pSpeed = 0
        End If
        If e.KeyCode = Keys.Enter Then
            cInGame = True
            loadGame()
        End If
    End Sub
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadBackground()

        cInGame = False

        Me.MaximumSize = New Size(mBackground.bWidth + 16, mBackground.bHeight + 38)
        Me.MinimumSize = New Size(mBackground.bWidth + 16, mBackground.bHeight + 38)

        Me.WindowState = FormWindowState.Maximized

        Me.Height = mBackground.bHeight + 38
        Me.Width = mBackground.bWidth + 16

        onScreen.Top = mBackground.bLocation.X
        onScreen.Left = mBackground.bLocation.Y
        onScreen.Height = mBackground.bHeight
        onScreen.Width = mBackground.bWidth


    End Sub
    Private Sub loadGame()
        gameTime = 0
        mScore = 0
        makeHose()
        loadPics()
        mWaterDrops.Clear()

        For i As Integer = 0 To 4
            randomFireMaker()
        Next i
        GameTimer.Start()

    End Sub
    Public Sub makeHose()
        mHose.pPic = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\nozzle.png")
        mHose.pSpeed = 0
        mHose.pTimeSinceLastShot = 50
        mHose.pLocation.X = 375
        mHose.pLocation.Y = Me.Height - 38 - mHose.pPic.Height
    End Sub
    Private Sub updateScreen()
        mG = onScreen.CreateGraphics
        mOffG = Graphics.FromImage(mOffScreenImage)
        drawBackground()


        If cInGame = True Then
            Dim index As Integer
            Dim jndex As Integer
            If winState() = False And loseState() = False Then
                For index = 0 To mFires.GetLength(0) - 1
                    For jndex = 0 To mFires.GetLength(1) - 1
                        If (mFires(index, jndex).aActive) Then
                            drawAnimatedSprite(mFires(index, jndex))
                            updateFire(mFires(index, jndex))
                        End If
                    Next jndex
                Next index
                Dim wndex As Integer
                For wndex = 0 To mWaterDrops.Count - 1
                    If (mWaterDrops(wndex).aActive) Then
                        drawAnimatedSprite(mWaterDrops(wndex))
                        updateWater(mWaterDrops(wndex))
                    End If
                Next wndex
                drawPlayer()
                updatePlayer()
            End If



            mOffG.DrawString("Score: " + mScore.ToString, New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 0, 0)
            If winState() Then
                mOffG.DrawString("You Win", New Font("Arial", 50, FontStyle.Bold), New SolidBrush(Color.Green), 300, 275)
            End If
            If loseState() Then
                mOffG.DrawString("You Lose", New Font("Arial", 50, FontStyle.Bold), New SolidBrush(Color.Red), 300, 275)
            End If
            mOffG.DrawString("Press ENTER to restart", New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 600, 0)
        Else
            mOffG.DrawString("Press ENTER to start", New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 600, 0)
            mOffG.DrawString("Quench it," & vbCrLf & "Quentin", New Font("Arial", 40, FontStyle.Bold), New SolidBrush(Color.Blue), 525, 225)
        End If
        mG.DrawImage(mOffScreenImage, 0, 0)
        mG.Dispose()
        mOffG.Dispose()
    End Sub
    Private Sub GameTimer_Tick(sender As Object, e As EventArgs) Handles GameTimer.Tick
        updateScreen()

        If winState() = False And loseState() = False Then

            gameTime += 1
            checkTouching()
            spawnFire()
            removeOld()
        End If

    End Sub
    Public Function winState()
        For index As Integer = 0 To mFires.GetLength(0) - 1
            For jndex As Integer = 0 To mFires.GetLength(1) - 1
                If mFires(index, jndex).aActive = True Then
                    Return False
                End If
            Next jndex
        Next index
        Return True
    End Function
    Public Function loseState()
        For index As Integer = 0 To mFires.GetLength(0) - 1
            For jndex As Integer = 0 To mFires.GetLength(1) - 1
                If mFires(index, jndex).aActive = False Then
                    Return False
                End If
            Next jndex
        Next index
        Return True
    End Function
    Private Sub checkTouching()
        For index As Integer = 0 To mFires.GetLength(0) - 1
            For jndex As Integer = 0 To mFires.GetLength(1) - 1
                For wndex As Integer = 0 To mWaterDrops.Count - 1
                    If touching(mFires(index, jndex), mWaterDrops(wndex)) = True Then
                        Dim temp As AnimatedSprite = mWaterDrops(wndex)
                        temp.aActive = False
                        mScore += 10
                        mWaterDrops(wndex) = temp
                        mFires(index, jndex).aActive = False
                    End If
                Next wndex
            Next jndex
        Next index
    End Sub
End Class
