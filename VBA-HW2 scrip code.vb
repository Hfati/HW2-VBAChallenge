VBA-HW2 scrip code 
Sub stocks():

    Dim i As Long ' row number
    Dim cell_vol As LongLong ' contents of column G (cell volume)
    Dim vol_total As LongLong ' what is going to go in column L
    Dim ticker As String ' what is going to go in column I

    Dim k As Long ' leaderboard row
    
    Dim ticker_close As Double
    Dim ticker_open As Double
    Dim price_change As Double
    Dim percent_change As Double
    
    ' asked the xpert
    Dim lastRow As Long
    lastRow = ActiveSheet.Cells(ActiveSheet.Rows.Count, 1).End(xlUp).Row

    vol_total = 0
    k = 2
    
    ' Write Leaderboard Columns
    Range("I1").Value = "Ticker"
    Range("J1").Value = "Quarterly Change"
    Range("K1").Value = "Percent Change"
    Range("L1").Value = "Volume"
    
    
    ' assign open for first ticker
    ticker_open = Cells(2, 3).Value

    For i = 2 To lastRow:
        cell_vol = Cells(i, 7).Value
        ticker = Cells(i, 1).Value

        ' LOOP rows 2 to 254
        ' check if next row ticker is DIFFERENT
        ' if the same, then we only need to add to the vol_total
        ' if DIFFERENT, then we need add last row, write out to the leaderboard
        ' reset the vol_total to 0

        If (Cells(i + 1, 1).Value <> ticker) Then
            ' OMG we have a different ticker, panic
            vol_total = vol_total + cell_vol
            
            ' get the closing price of the ticker
            ticker_close = Cells(i, 6).Value
            price_change = ticker_close - ticker_open
            
            ' Check if open price is 0 first - to protect from Kevin the Hacker
            If (ticker_open > 0) Then
                percent_change = price_change / ticker_open
            Else
                percent_change = 0
            End If

            Cells(k, 9).Value = ticker
            Cells(k, 10).Value = price_change
            Cells(k, 11).Value = percent_change
            Cells(k, 12).Value = vol_total
            
            ' formatting
            If (price_change > 0) Then
                Cells(k, 10).Interior.ColorIndex = 4 ' Green
            ElseIf (price_change < 0) Then
                Cells(k, 10).Interior.ColorIndex = 3 ' Red
            Else
                Cells(k, 10).Interior.ColorIndex = 2 ' White
            End If

            ' reset
            vol_total = 0
            k = k + 1
            ticker_open = Cells(i + 1, 3).Value ' look ahead to get next ticker open
        Else
            ' we just add to the total
            vol_total = vol_total + cell_vol
        End If
    Next i
    
    ' Style my leaderboard
    Columns("K:K").NumberFormat = "0.00%"
    Columns("I:L").AutoFit
    
    
End Sub

Sub reset()

    Range("I:L").Value = ""
    Range("I:L").Interior.ColorIndex = 2

End Sub

