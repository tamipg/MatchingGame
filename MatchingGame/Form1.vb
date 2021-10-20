Public Class Form1

    ' Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados.
    Dim random As New Random

    ' Cada una de estas letras es un icono interesante en la fuente Webdings,
    ' y cada icono aparece dos veces en esta lista.
    Dim temporaryArray() As String = {"!", "!", "N", "N", ",", ",", "k", "k",
                                      "b", "b", "v", "v", "w", "w", "z", "z"}
    Dim icons As List(Of String) = temporaryArray.ToList()
End Class
