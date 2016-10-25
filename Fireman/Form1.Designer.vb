<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.onScreen = New System.Windows.Forms.PictureBox()
        Me.GameTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.onScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'onScreen
        '
        Me.onScreen.Location = New System.Drawing.Point(135, 87)
        Me.onScreen.Name = "onScreen"
        Me.onScreen.Size = New System.Drawing.Size(100, 50)
        Me.onScreen.TabIndex = 0
        Me.onScreen.TabStop = False
        '
        'GameTimer
        '
        Me.GameTimer.Enabled = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.onScreen)
        Me.Name = "MainForm"
        Me.Text = "Fireman"
        CType(Me.onScreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents onScreen As System.Windows.Forms.PictureBox
    Friend WithEvents GameTimer As System.Windows.Forms.Timer

End Class
