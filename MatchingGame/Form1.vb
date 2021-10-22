Public Class Form1
    ' sonido para fallo
    Dim soundFail = New System.Media.SoundPlayer("C:\Windows\Media\chord.wav")

    ' sonido para acierto
    Dim soundOK = New System.Media.SoundPlayer("C:\Windows\Media\chimes.wav")

    ' sonido reintentar
    Dim soundRetry = New System.Media.SoundPlayer("C:\Windows\Media\ding.wav")

    ' Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados.
    Dim random As New Random

    ' Cada una de estas letras es un icono interesante en la fuente Webdings,
    ' y cada icono aparece dos veces en esta lista.
    Dim temporaryArray() As String = {"e", "e", "f", "f", "j", "j", "m", "m",
                                      "t", "t", "B", "B", "E", "E", "Ñ", "Ñ"}
    Dim icons As List(Of String) = temporaryArray.ToList()

    ' firstClicked apunta al primer control de etiqueta
    ' que el jugador hace clic, pero no será nada
    ' si el jugador aún no ha hecho clic en una etiqueta
    Dim firstClicked As Label = Nothing

    ' secondClicked apunta al segundo control Label que el jugador hace clic
    Dim secondClicked As Label = Nothing

    Public Sub New()
        ' Esta llamada es requerida por el Diseñador de formularios de Windows
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada InitializeComponent ()
        AssignIconsToSquares()
    End Sub

    ''' <summary>
    ''' Asignar cada icono de la lista de iconos a un cuadrado aleatorio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignIconsToSquares()

        ' El TableLayoutPanel tiene 16 etiquetas,
        ' y la lista de iconos tiene 16 iconos,
        ' por lo que un icono se extrae al azar de la lista
        ' y se agrega a cada etiqueta
        For Each control In TableLayoutPanel1.Controls
            Dim iconLabel As Label = TryCast(control, Label)
            If iconLabel IsNot Nothing Then
                Dim randomNumber As Integer = random.Next(icons.Count)
                iconLabel.Text = icons.ElementAt(randomNumber)
                iconLabel.ForeColor = iconLabel.BackColor
                icons.RemoveAt(randomNumber)
            End If
        Next

    End Sub

    ''' <summary>
    ''' Comprueba cada icono para ver si coincide, 
    ''' comparando su color de primer plano con su color de fondo.
    ''' Si todos los iconos coinciden, el jugador gana
    ''' </summary>
    Private Sub CheckForWinner()

        ' Revisa todas las etiquetas en TableLayoutPanel,
        ' revisando cada una para ver si su ícono coincide
        For Each control In TableLayoutPanel1.Controls
            Dim iconLabel As Label = TryCast(control, Label)
            If iconLabel IsNot Nothing Then
                If (iconLabel.ForeColor = iconLabel.BackColor) Then
                    Return
                End If
            End If
        Next

        ' Si el bucle no regresó, no encontró cualquier ícono incomparable
        ' Eso significa que el usuario ganó. Muestra un mensaje y cierra el formulario
        MessageBox.Show("¡Has hecho coincidir todos los iconos!", "Felicidades")
        Close()

    End Sub

    ''' <summary>
    ''' El evento Click de cada etiqueta es manejado por este controlador de eventos
    ''' </summary>
    ''' <param name="sender">La etiqueta en la que se hizo click</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub label_Click(sender As Object, e As EventArgs) Handles Label9.Click, Label8.Click, Label7.Click, Label6.Click, Label5.Click, Label4.Click, Label3.Click, Label2.Click, Label16.Click, Label15.Click, Label14.Click, Label13.Click, Label12.Click, Label11.Click, Label10.Click, Label1.Click
        ' El temporizador solo se enciende después de 
        ' mostrar dos iconos al jugador,
        ' así que ignora cualquier click si el temporizador está funcionando
        If (Timer1.Enabled = True) Then
            Return
        End If

        Dim clickedLabel As Label = TryCast(sender, Label)

        If clickedLabel IsNot Nothing Then
            'Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
            'un icono que ya ha sido revelado -
            'ignora el clic
            If (clickedLabel.ForeColor = Color.Black) Then
                Return
            End If


            ' Si firstClicked es Nothing, este es el primer icono
            ' en el par en el que el jugador hizo clic,
            ' así que establezca firstClicked clic en la etiqueta que el jugador
            ' hace clic, cambia su color a negro y vuelve
            If (firstClicked Is Nothing) Then
                firstClicked = clickedLabel
                firstClicked.ForeColor = Color.Black
                Return
            End If

            ' Si el jugador llega tan lejos, el temporizador no
            ' corriendo y firstClicked no es nada,
            ' por lo que este debe ser el segundo icono en el que el jugador hizo clic
            ' Establezca su color en negro
            secondClicked = clickedLabel
            secondClicked.ForeColor = Color.Black

            ' Comprueba si el jugador ganó
            CheckForWinner()

            ' Si el jugador hizo clic en dos iconos coincidentes, manténgalos
            ' black y reset firstClicked y secondClicked
            ' para que el jugador pueda hacer clic en otro icono
            If (firstClicked.Text = secondClicked.Text) Then
                firstClicked = Nothing
                secondClicked = Nothing
                soundOK.Play()
                Return
            End If

            ' Si el jugador llega tan lejos, el jugador
            ' hizo clic en dos iconos diferentes, así que inicie el
            ' timer (que esperará tres cuartos de
            ' un segundo, y luego esconde los íconos)
            soundFail.Play()
            Timer1.Start()
        End If
    End Sub

    ''' <summary>
    ''' Este temporizador se inicia cuando el jugador hace clic
    ''' en dos iconos que no coinciden,
    ''' por lo que cuenta tres cuartos de segundo
    ''' y luego se apaga y oculta ambos íconos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' detener el temporizador
        Timer1.Stop()

        ' Ocultar ambos iconos
        firstClicked.ForeColor = firstClicked.BackColor
        secondClicked.ForeColor = secondClicked.BackColor
        soundRetry.Play()

        ' Restablecer firstClicked y secondClicked
        ' así la próxima vez que se haga click en una etiqueta
        ' el programa sabe que es el primer click
        firstClicked = Nothing
        secondClicked = Nothing
    End Sub
End Class
