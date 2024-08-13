﻿Imports MySql.Data.MySqlClient

Module verStkDia
    Public Function Ver(ByVal Fec As Date, ByVal usID As Integer) As List(Of mdlStock)
        Dim Cmd As New MySqlCommand
        Dim Lista As New List(Of mdlStock)
        Dim E As New mdlStock
        Dim Sdr As MySqlDataReader

        Try
            Using Conn As MySqlConnection = New MySqlConnection(Servidor)
                Cmd = New MySqlCommand("feedlot.verStkDia")
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Conn

                Cmd.Parameters.AddWithValue("@Fec", Fec)
                Cmd.Parameters.AddWithValue("@idUs", usID)

                Conn.Open()
                Sdr = Cmd.ExecuteReader

                Lista.Clear()
                While Sdr.Read
                    Lista.Add(New mdlStock(
                                Sdr("idSTOCK"),
                                Sdr("Fecha"),
                                Sdr("Caravana"),
                                Sdr("Descripcion"),
                                Sdr("Cabezas"),
                                Sdr("TROPA_idTropa"),
                                Sdr("DETALLE_TROPA_idDetTropa"),
                                Sdr("CORRALES_idCorrales"),
                                Sdr("CATEGORIAS_idCategorias"),
                                Sdr("USUARIOS_idUsuario"),
                                "OK"))
                End While

                If Lista.Count = 0 Then
                    E.Err = "No hay Stock de animales para esa Fecha !"
                    Lista.Insert(0, E)
                Else
                    Return Lista
                End If
                Sdr.Close()
            End Using

            Return Lista
        Catch ex As Exception
            E.Err = "Error: " & ex.Message.ToString
            Lista.Insert(0, E)
            Return Lista
        End Try
    End Function
End Module
