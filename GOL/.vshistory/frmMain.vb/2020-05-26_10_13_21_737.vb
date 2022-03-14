Public Class frmMain
    Const ConfiniMondo As Integer = 256
    Dim WithEvents tmrGenerazione As Timer
    Dim Mondo(ConfiniMondo, ConfiniMondo) As Boolean
    Dim cntGenerazioni As Integer = 0
    Dim cntCelle As Integer = 0
    Dim cntCelleNate As Integer = 0
    Dim cntCelleMorte As Integer = 0
    Dim cntCelleSopravvisute As Integer = 0
    Dim cntGCS As Integer = ConfiniMondo
    Dim RNDM As New Random(CInt(Date.Now.Ticks And &HFFFF))

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        tmrGenerazione = New Timer With {
            .Interval = 1000 / 25
        }
        tmrGenerazione.Stop()

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        resetMondo()
        popolaMondo()
        disegnaMondo()
        tmrGenerazione.Start()

    End Sub

    Private Sub popolaMondo(Optional stp As Integer = 0)

        cntGenerazioni = 0
        cntCelleMorte = 0
        cntCelleSopravvisute = 0
        cntCelle = 0
        cntCelleNate = 0

        If stp = 0 Then
            stp = RNDM.Next(1, 3)

        End If

        If stp = 1 Then
            Dim X As Integer = CInt(ConfiniMondo / 2) ' RNDM.Next(0, ConfiniMondo)
            Dim y As Integer = CInt(ConfiniMondo / 2) ' RNDM.Next(0, ConfiniMondo)

            allocaCella(X + 1, y + 0)
            allocaCella(X + 3, y + 1)
            allocaCella(X + 0, y + 2)
            allocaCella(X + 1, y + 2)
            allocaCella(X + 4, y + 2)
            allocaCella(X + 5, y + 2)
            allocaCella(X + 6, y + 2)

            cntCelle = 7

        ElseIf stp = 2 Then
            stp = RNDM.Next(4, CInt(ConfiniMondo / 2))

            For X As Integer = 0 To ConfiniMondo - 1 Step stp
                For Y As Integer = 0 To ConfiniMondo - 1 Step stp
                    If RNDM.Next(0, 100) >= 50 Then
                        Mondo(X, Y) = True
                        cntCelle += 1

                    End If

                Next

            Next

        ElseIf stp = 3 Then
            stp = RNDM.Next(4, CInt(ConfiniMondo / 2))

            For X As Integer = 0 To ConfiniMondo - 1 Step stp
                For Y As Integer = 0 To ConfiniMondo - 1 Step stp
                    Select Case RNDM.Next(1, 3)
                        Case 1
                            allocaCella(X + 0, Y + 0)
                            allocaCella(X + 1, Y + 0)
                            allocaCella(X + 2, Y + 0)
                            allocaCella(X + 0, Y + 1)
                            allocaCella(X + 1, Y + 2)
                            cntCelle += 6

                        Case 2
                            allocaCella(X + 0, Y + 0)
                            allocaCella(X + 1, Y + 0)
                            allocaCella(X + 0, Y + 1)
                            allocaCella(X + 1, Y + 1)
                            cntCelle += 4

                        Case 3
                            allocaCella(X + 0, Y + 1)
                            allocaCella(X + 1, Y + 1)
                            allocaCella(X + 2, Y + 1)
                            cntCelle += 3

                    End Select

                Next

            Next


        End If

        cntCelleNate = cntCelle

        'If stp < 4 Then
        '    stp = RNDM.Next(4, CInt(ConfiniMondo / 2))

        'End If

        ''For X As Integer = 0 To ConfiniMondo - 1 Step stp
        ''    For Y As Integer = 0 To ConfiniMondo - 1 Step stp
        ''        If RNDM.Next(0, 100) >= 50 Then
        ''            Mondo(X, Y) = True

        ''        End If

        ''    Next

        ''Next

        'For X As Integer = 0 To ConfiniMondo - 1 Step stp
        '    For Y As Integer = 0 To ConfiniMondo - 1 Step stp
        '        'If X + 2 < ConfiniMondo - 1 AndAlso Y + 2 < ConfiniMondo - 1 Then
        '        '    Mondo(X + 0, Y + 0) = True
        '        '    Mondo(X + 1, Y + 0) = True
        '        '    Mondo(X + 2, Y + 0) = True
        '        '    Mondo(X + 0, Y + 1) = True
        '        '    Mondo(X + 1, Y + 2) = True

        '        'End If

        '        Select Case RNDM.Next(1, 3)
        '            Case 1
        '                allocaCella(X + 0, Y + 0)
        '                allocaCella(X + 1, Y + 0)
        '                allocaCella(X + 2, Y + 0)
        '                allocaCella(X + 0, Y + 1)
        '                allocaCella(X + 1, Y + 2)

        '            Case 2
        '                allocaCella(X + 0, Y + 0)
        '                allocaCella(X + 1, Y + 0)
        '                allocaCella(X + 0, Y + 1)
        '                allocaCella(X + 1, Y + 1)

        '            Case 3
        '                allocaCella(X + 0, Y + 1)
        '                allocaCella(X + 1, Y + 1)
        '                allocaCella(X + 2, Y + 1)

        '                'Case 4
        '                '    allocaCella(X + 0, Y + 0)
        '                '    allocaCella(X + 1, Y + 0)
        '                '    allocaCella(X + 1, Y + 1)

        '        End Select

        '    Next

        'Next

    End Sub

    Private Sub allocaCella(xa As Integer, ya As Integer)
        Dim xr As Integer = xa
        Dim yr As Integer = ya

        If xr < 0 Then
            xr = (ConfiniMondo + xa)
        End If

        If xr > (ConfiniMondo - 1) Then
            xr = Math.Abs(ConfiniMondo - xa)
        End If

        If yr < 0 Then
            yr = (ConfiniMondo + ya)
        End If

        If yr > (ConfiniMondo - 1) Then
            yr = Math.Abs(ConfiniMondo - ya)
        End If

        Mondo(xr, yr) = True

    End Sub

    Private Sub tmrGenerazione_Tick(sender As Object, e As EventArgs) Handles tmrGenerazione.Tick
        Dim cntCN As Integer = cntCelleNate
        Dim cntCM As Integer = cntCelleMorte
        Dim cntCS As Integer = cntCelleSopravvisute

        calcolaMondo()

        If cntCelle = 0 Then
            popolaMondo()

        ElseIf cntCelleNate = cntCN AndAlso cntCelleMorte = cntCM AndAlso cntCelleSopravvisute = cntCS Then
            If cntGCS < ConfiniMondo Then
                cntGCS += 1

            Else
                resetMondo()
                popolaMondo()

            End If

        Else
            cntGCS = 0

        End If

        disegnaMondo()
        Text &= "   D = " & Math.Abs(cntCS - cntCelle).ToString

    End Sub

    Private Sub resetMondo()
        For X As Integer = 0 To ConfiniMondo - 1
            For Y As Integer = 0 To ConfiniMondo - 1
                Mondo(X, Y) = False

            Next

        Next

        cntGenerazioni = 0
        cntCelle = 0
        cntCelleNate = 0
        cntCelleMorte = 0
        cntCelleSopravvisute = 0

    End Sub

    Private Sub calcolaMondo()
        cntGenerazioni += 1
        cntCelle = 0
        cntCelleNate = 0
        cntCelleMorte = 0
        cntCelleSopravvisute = 0

        Dim NewMondo(ConfiniMondo, ConfiniMondo) As Boolean

        For X As Integer = 0 To ConfiniMondo - 1
            For Y As Integer = 0 To ConfiniMondo - 1
                NewMondo(X, Y) = calcolaCella(X, Y)

            Next

        Next

        For X As Integer = 0 To ConfiniMondo - 1
            For Y As Integer = 0 To ConfiniMondo - 1
                Mondo(X, Y) = NewMondo(X, Y)
                If Mondo(X, Y) Then
                    cntCelle += 1

                End If

            Next

        Next

    End Sub

    Private Function calcolaCella(X As Integer, Y As Integer) As Boolean
        Dim c As Integer = 0

        For xa As Integer = (X - 1) To (X + 1)
            For ya As Integer = (Y - 1) To (Y + 1)
                If xa = X AndAlso ya = Y Then
                    ' ignora se stessa

                Else
                    Dim xr As Integer = xa
                    Dim yr As Integer = ya

                    If xr < 0 Then
                        xr = (ConfiniMondo - 1)
                    End If

                    If xr > (ConfiniMondo - 1) Then
                        xr = 0
                    End If

                    If yr < 0 Then
                        yr = (ConfiniMondo - 1)
                    End If

                    If yr > (ConfiniMondo - 1) Then
                        yr = 0
                    End If

                    If Mondo(xr, yr) Then
                        c += 1

                    End If

                End If

            Next

        Next

        If Mondo(X, Y) AndAlso c < 2 Then
            cntCelleMorte += 1
            Return False

        ElseIf Mondo(X, Y) AndAlso c >= 2 And c <= 3 Then
            cntCelleSopravvisute += 1
            Return True

        ElseIf Mondo(X, Y) AndAlso c > 3 Then
            cntCelleMorte += 1
            Return False

        ElseIf Not Mondo(X, Y) AndAlso c = 3 Then
            cntCelleNate += 1
            Return True

        Else
            Return False

        End If

    End Function

    Private Sub disegnaMondo()

        Dim ImmagineCorrente As Bitmap = New Bitmap(ConfiniMondo * 2, ConfiniMondo * 2)
        ' ImmagineCorrente.SetResolution(1, 1)

        Dim sdgX As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(ImmagineCorrente)
        sdgX.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
        sdgX.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

        sdgX.Clear(Color.White)
        'sdgX.DrawRectangle(New Pen(Color.Gray), 0, 0, ConfiniMondo * 2, ConfiniMondo * 2)
        For X As Integer = 0 To ConfiniMondo - 1
            For Y As Integer = 0 To ConfiniMondo - 1
                If Mondo(X, Y) Then
                    sdgX.FillRectangle(New SolidBrush(Color.Black), X * 2, Y * 2, 2, 2)

                End If

            Next

        Next

        BackgroundImage = ImmagineCorrente
        Text = "G.O.L. G = " & cntGenerazioni.ToString & "    C = " & cntCelle.ToString & "    CN = " & cntCelleNate.ToString & "    CM = " & cntCelleMorte.ToString & "    CS = " & cntCelleSopravvisute.ToString & "    R = " & cntGCS.ToString & "/" & ConfiniMondo.ToString

    End Sub

End Class
