Public Class Form1

    ' Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados.
    Dim random As New Random

    ' Cada una de estas letras es un icono interesante en la fuente Webdings,
    ' y cada icono aparece dos veces en esta lista.
    Dim temporaryArray() As String = {"!", "!", "N", "N", ",", ",", "k", "k",
                                      "b", "b", "v", "v", "w", "w", "z", "z"}
    Dim icons As List(Of String) = temporaryArray.ToList()

    Public Sub New()
        ' Esta llamada es requerida por el Diseñador de formularios de Windows
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada InitializeComponent ()
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
                ' iconLabel.ForeColor = iconLabel.BackColor
                icons.RemoveAt(randomNumber)
            End If
        Next

    End Sub
End Class
