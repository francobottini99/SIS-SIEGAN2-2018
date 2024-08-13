﻿Imports MySql.Data.MySqlClient

Module consTropasPorProdYFecha
    Public Function Ver(ByVal IdT As Integer) As List(Of mdlTropa)
        Dim Cmd As New MySqlCommand
        Dim Lista As New List(Of mdlTropa)
        Dim E As New mdlTropa
        Dim Sdr As MySqlDataReader

        Try
            Using Conn As MySqlConnection = New MySqlConnection(Servidor)
                Cmd = New MySqlCommand("feedlot.consTropasPorProdYFecha")
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Conn

                Cmd.Parameters.AddWithValue("@IdP", IdT)

                Conn.Open()
                Sdr = Cmd.ExecuteReader

                Lista.Clear()
                While Sdr.Read
                    Lista.Add(New mdlTropa(
                        Sdr("idTropa"),
                        Sdr("NombreTropa"),
                        Sdr("OrigenProcedencia"),
                        Sdr("FechaIngreso"),
                        Sdr("Cabezas"),
                        Sdr("Muertes"),
                        Sdr("Ventas"),
                        Sdr("Stock"),
                        Sdr("PesoIngreso"),
                        Sdr("TotalKgTropa"),
                        Sdr("Observaciones"),
                        Sdr("PRODUCTOR_idProductor"),
                        Sdr("CAMPO_idCampo"),
                        Sdr("PROVEEDOR_idProveedor"),
                        Sdr("USUARIOS_idUsuario"),
                        "OK"))
                End While

                If Lista.Count = 0 Then
                    E.Err = "No hay Tropas cargadas para ese Productor !"
                    Lista.Insert(0, E)
                Else
                    Return Lista
                End If

                Sdr.Close()
            End Using

            Return Lista
        Catch ex As Exception
            E.Err = ex.Message.ToString
            Lista.Insert(0, E)
            Return E.Err
        End Try
    End Function
End Module
