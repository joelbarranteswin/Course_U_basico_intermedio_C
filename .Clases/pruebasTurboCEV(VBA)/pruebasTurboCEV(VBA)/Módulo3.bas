Attribute VB_Name = "Módulo3"
Sub importar_datos(casos As Single, wb_datos As Workbook)
Application.ScreenUpdating = False
Application.DisplayAlerts = False

wb_datos.Activate
wb_datos.Unprotect Password:="CEVCEVE2017"
wb_datos.Sheets("0. Datos Arq Motor").Visible = True
wb_datos.Sheets("0. Datos Clima Motor").Visible = True

For i = 1 To casos
    wb_datos.Sheets("0. Datos Arq Motor").Activate
    ActiveSheet.Range("C33") = i 'establece el caso a copiar
    wb_datos.Sheets("0. Datos Clima Motor").Activate 'rescata los datos de clima para cada caso
    Range(Cells(8, 5), Cells(388, 25)).Select
    Selection.Copy

    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(3, 2 + (i - 1) * 24), Cells(383, 22 + (i - 1) * 24)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
    
    wb_datos.Sheets("0. Datos Arq Motor").Activate 'rescata datos de arq
    Range(Cells(2, 2), Cells(29, 15)).Select
    Selection.Copy
    
    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(386, 2 + (i - 1) * 24), Cells(386 + 27, 15 + (i - 1) * 24)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
            
    wb_datos.Sheets("0. Datos Arq Motor").Activate 'rescata datos de masa
    Range(Cells(7, 34), Cells(18, 36)).Select
    Selection.Copy
    
    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(416, 2 + (i - 1) * 24), Cells(416 + 11, 2 + 3 + (i - 1) * 24)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
    
    wb_datos.Sheets("0. Datos Arq Motor").Activate 'rescata datos de SA T°libre
    Range(Cells(9, 24), Cells(32, 32)).Select
    Selection.Copy
    
    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(431, 2 + (i - 1) * 24), Cells(431 + 23, 2 + 8 + (i - 1) * 24)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
            
    wb_datos.Sheets("0. Datos Arq Motor").Activate 'rescato datos de SA con clima
    Range(Cells(36, 24), Cells(59, 32)).Select
    Selection.Copy
    
    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(458, 2 + (i - 1) * 24), Cells(458 + 23, 2 + 8 + (i - 1) * 24)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
    
Next i

'---------------------------------------------
wb_datos.Sheets("0. Datos Arq Motor").Activate
ActiveSheet.Range("C33") = 1 ' reinicia caso a 1

End Sub
Sub datos_caso(caso As Single)
Application.ScreenUpdating = False
Application.DisplayAlerts = False
'--------------------------------------------
'rescata datos de clima
fila_i = 9
fila_f = 34
For i = 1 To 12
    ThisWorkbook.Sheets("Importado").Activate
    Range(Cells(fila_i - 5, 2 + (caso - 1) * 24), Cells(fila_f - 5, 22 + (caso - 1) * 24)).Select
    Selection.Copy

    ThisWorkbook.Sheets("Datos").Activate
    Range(Cells(fila_i, 5), Cells(fila_f, 25)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
            :=False, Transpose:=False
    fila_i = fila_i + 32
    fila_f = fila_f + 32
Next

'---------------------------------------------
'rescata datos de arq
ThisWorkbook.Sheets("Importado").Activate
Range(Cells(386, 2 + (caso - 1) * 24), Cells(386 + 27, 15 + (caso - 1) * 24)).Select
Selection.Copy

ThisWorkbook.Activate
Sheets("Calculo").Select
Range(Cells(2, 2), Cells(29, 15)).Select
'ActiveSheet.Paste
Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False
        
'---------------------------------------------
'rescata datos de masa
ThisWorkbook.Sheets("Importado").Activate
Range(Cells(416, 2 + (caso - 1) * 24), Cells(416 + 11, 2 + 2 + (caso - 1) * 24)).Select
Selection.Copy

ThisWorkbook.Activate
Sheets("SA").Select
Range(Cells(34, 16), Cells(45, 18)).Select
'ActiveSheet.Paste
Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False
'---------------------------------------------
'rescata datos de SA T°libre
ThisWorkbook.Sheets("Importado").Activate
Range(Cells(431, 2 + (caso - 1) * 24), Cells(431 + 23, 2 + 8 + (caso - 1) * 24)).Select
Selection.Copy

ThisWorkbook.Activate
Sheets("SA").Select
Range(Cells(36, 6), Cells(59, 14)).Select
Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False

'rescato datos de SA con clima
ThisWorkbook.Sheets("Importado").Activate
Range(Cells(458, 2 + (caso - 1) * 24), Cells(458 + 23, 2 + 8 + (caso - 1) * 24)).Select
Selection.Copy

ThisWorkbook.Activate
Sheets("SA").Select
Range(Cells(64, 6), Cells(87, 14)).Select
Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False

End Sub
