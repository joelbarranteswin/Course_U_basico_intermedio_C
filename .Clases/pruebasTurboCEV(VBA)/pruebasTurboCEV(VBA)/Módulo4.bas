Attribute VB_Name = "Módulo4"
Sub correr_casos_base()
'Código creado por Matías Yachán y Marcial Salaverry para ejecutar 8 casos base y 2 casos propuestos
'Modificado por Xavier Irazoqui
'Versión del 29/08/2018
'Principales cambios realziados en esta versión:
'- se deshabilita la autorecuperación para evitar que ante una falla de energía la planilla aparezca desbloqueada
'- se desbloquea planilla 3 para pegado de datos y posteriormente se bloquea
'- se incorpora modo administrador
'- se incorpora código que resguarda planilla ante cierre forzoso de macro
'- se permite elegir múltiples archivos de datos
'- se incorpora un resumen de los datos calculados
'- se ajusta código para limitar la ocurrencia de errores sólo al proceso de cálculo o pegado de datos
'- se agregan marcadores para identificar la porción de código que indujo a error
'- se especifican las referencias a "este" libro y de las variables
'- se realiza el borrado previo de la planilla 2 antes de recibir datos nuevos
'- se realiza el borrado previo de la planilla 3

'MARCADORES DE ERROR:
'01.1 ERROR EN LA APERTURA DE PLANILLA DATOS
'01.2 ERROR EN LIMPIEZA DE DATOS DE PLANILLA 2
'02.1 ERROR AL IMPORTAR DATOS PARA CÁLCULOS
'02.2 ERROR AL IMPORTAR DATOS CEV-CEVE
'03.caso ERROR AL COPIAR DATOS DEL CASO EN PLANILLA 2
'04.caso ERROR EN EL CÁLCULO DEL CASO
'05.1 ERROR AL ABRIR PLANILLA DE RESULTADOS
'05.2 ERROR AL COPIAR Y PEGAR DATOS EN HOJA RESULTADOS
'05.3 ERROR AL COPIAR Y PEGAR DATOS EN HOJA CEV-CEVE
'06 ERROR AL GUARDAR PLANILLA 3

'--------------------------------------------------------------------------------
'CONDICIONES INICIALES
'--------------------------------------------------------------------------------
'declara variables

Dim StartTime As Double, SecondsElapsed As Double, _
demanda_cal_prop As Double, demanda_ref_prop As Double, demanda_total_prop As Double, _
porc_ahorro_cal As Double, porc_ahorro_ref As Double, porc_ahorro_total As Double, _
disconfort_frio As Double, disconfort_calor As Double, _
QSol_v As Double, Qgen_v As Double, Qvent_v As Double, Qinf_v As Double, Qenv_v As Double, _
Qtecho_v As Double, Qmuros_v As Double, Qventanas_v As Double, QpisoV_v As Double, _
QpisoCT_v As Double, QPT_v As Double, Qmasa_v As Double, _
QSol_i As Double, Qgen_i As Double, Qvent_i As Double, Qinf_i As Double, Qenv_i As Double, _
Qtecho_i As Double, Qmuros_i As Double, Qventanas_i As Double, QPisoV_i As Double, _
QPisoCT_i As Double, QPT_i As Double, Qmasa_i As Double

Dim direccion_datos As Variant, direccion_resultados As Variant, nombre_datos As Variant, _
nombre_resultados As Variant, hojas As Variant, nombres As Variant
Dim estado As Boolean, admin As Boolean
Dim caso As Single
Dim contador As Integer, lContinue As Integer
Dim ruta As String, nombre_salida As String, strIllegals As String, indice_error As String, _
inforuta As String, letra As String, fecha As String, version_pbtd1 As String, caso_interno As String
Dim wb_datos As Workbook, wb_resultados As Workbook, xWB As Workbook
Dim ws As Worksheet

'deshabilita actualización de pantalla, alertas y AUTORECUPERACIÓN (OJO!)
Application.ScreenUpdating = False
Application.DisplayAlerts = False
Application.AutoRecover.Enabled = False

'modo administrador
If ThisWorkbook.Sheets("Calculo").Range("A1") = "DITEC" Then
    admin = True
Else
    admin = False
End If

'caracteres prohibidos como nombre de archivo
strIllegals = "\/|?*<>"":"

'arreglo para verificar nombres iguales de archivos de salida
ReDim nombres(1 To 1)

'verifica que el libro contiene todas las hojas necesarias para el código
hojas = Array("Datos", "Calculo", "CEV-CEVE", "Resultados", "SA", "Importado")
contador = 0

For Each ws In ThisWorkbook.Worksheets
    For n = LBound(hojas) To UBound(hojas)
        If ws.Name Like hojas(n) Then
            contador = contador + 1
            Exit For
        End If
    Next n
Next ws

If contador < UBound(hojas) + 1 Then
    Exit Sub 'Si no se cuenta con todas las hojas, se termina la sub rutina
End If

'--------------------------------------------------------------------------------
'ENTRADA DE DATOS
'--------------------------------------------------------------------------------
'advertencia
lContinue = MsgBox("Se recomienda guardar y cerrar todos los documentos antes de ejecutar el cálculo." & vbNewLine & _
"Presione Aceptar para continuar o Cancelar para detener.", vbOKCancel)
    If Not lContinue = vbOK Then
        Exit Sub
    End If

'se ubican planilla de datos y analisis
MsgBox "Ubique la(s) planilla(s):" & vbNewLine & "01. PBTD Datos de Arquitectura" _
& vbNewLine & vbNewLine & "Nota: puede seleccionar más de una", vbInformation, "PBTD: Ingreso de datos"

direccion_datos = Application _
 .GetOpenFilename("Hoja de cálculo habilitada para macros de Microsoft Excel (*.xlsm), *.xlsm", , , , True)

If Not IsArray(direccion_datos) Then
    'MsgBox "No se seleccionó ningún archivo de datos."
    Exit Sub 'Si no se selecciona uno o más archivos de datos, se termina la sub rutina
End If

MsgBox "Ubique la planilla:" & vbNewLine & "03. PBTD Datos de Equipos y Resultados" _
& vbNewLine & vbNewLine & "Nota: la planilla que se seleccione se utilizará como plantilla con la que se guardarán" _
& "los resultados de los cálculos realizados a cada una de las planillas 01 seleccionadas anteriormente." _
, vbInformation, "PBTD: Análisis de resultados"

direccion_resultados = Application _
 .GetOpenFilename("Hoja de cálculo habilitada para macros de Microsoft Excel (*.xlsm), *.xlsm", , , , False)

If direccion_resultados = False Then
    'MsgBox "No se seleccionó ningún archivo de resultados."
    Exit Sub 'Si no se selecciona un archivo de resultados, se termina la sub rutina
End If

'--------------------------------------------------------------------------------
'PROCESAMIENTO DE DATOS
'--------------------------------------------------------------------------------
Application.EnableCancelKey = xlErrorHandler
On Error GoTo CierreForzoso  'en caso de detener abruptamente el código, este se trata _
como error y se realizan las acciones correspondientes

'habiendo pasado los filtros anteriores, comienza el procesamiento de datos



'Remember time when macro starts
StartTime = Timer

'obtiene nombre de planilla de cálculo
nombre_motor = ThisWorkbook.Name

'desbloqueo de planilla
ThisWorkbook.Unprotect Password:="CEVCEVE2017"
ThisWorkbook.Sheets("Datos").Visible = True
ThisWorkbook.Sheets("SA").Visible = True
ThisWorkbook.Sheets("CEV-CEVE").Visible = True
ThisWorkbook.Sheets("Resultados").Visible = True
ThisWorkbook.Sheets("Importado").Visible = True

'borra resumen y reescribe columnas
Call columnas_resumen(admin)

'cálculo para cada planilla de datos seleccionada
For i = LBound(direccion_datos) To UBound(direccion_datos)
    
    On Error GoTo ErrorCalculo 'en caso de error se realizan las instrucciones del _
    control de errores y prosigue a la siguiente iteración
    estado = True
    indice_error = ""
    nombre_salida = "vivienda " & i

'abre planilla de datos y obtiene nombre y ruta
    indice_error = "01.1" 'ERROR EN LA APERTURA DE PLANILLA DATOS
    
    Workbooks.Open direccion_datos(i)
    nombre_datos = ActiveWorkbook.Name
    Set wb_datos = Workbooks(nombre_datos)
    ruta = wb_datos.Path & "\"
    inforuta = ""
    
'limpia datos anteriores en planilla 02
    indice_error = "01.2" 'ERROR EN LIMPIEZA DE DATOS
    Call Limpiar_datos
     
'importa datos necesarios desde planilla 1 y la cierra
    indice_error = "02.1" 'ERROR AL IMPORTAR DATOS PARA CÁLCULOS
    
    Call importar_datos(10, wb_datos)
    
    indice_error = "02.2" 'ERROR AL IMPORTAR DATOS CEV-CEVE
    wb_datos.Sheets("CEV-CEVE").Activate
    Range(Cells(1, 1), Cells(239, 26)).Select
    Selection.Copy
    
    ThisWorkbook.Activate
    Sheets("CEV-CEVE").Select
    Cells(1, 1).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False

    wb_datos.Sheets("3. Tablas Envolvente").Activate
    Range(Cells(10, 1), Cells(92, 13)).Select
    Selection.Copy
    
    ThisWorkbook.Activate
    Sheets("CEV-CEVE").Select
    Cells(10, 27).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False

    wb_datos.Close SaveChanges:=False
    Set wb_datos = Nothing
    
'llama al cálculo para cada situación evaluada
    For caso = 1 To 10
        indice_error = "03." & caso 'ERROR AL IMPORTAR DATOS DEL CASO
        Call datos_caso(caso)
        indice_error = "04." & caso 'ERROR EN EL CÁLCULO DEL CASO"
        Call calculo
    Next caso

'abre planilla de resultados, obtiene nombre y limpia resultados anteriores
    indice_error = "05.1" ' ERROR AL ABRIR PLANILLA DE RESULTADOS
    Workbooks.Open direccion_resultados
    nombre_resultados = ActiveWorkbook.Name
    Set wb_resultados = Workbooks(nombre_resultados)
    wb_resultados.Sheets("Resultados").Unprotect Password:="CEVCEVE2017" 'desbloquea hoja Resultados en planilla 3
    wb_resultados.Sheets("CEV-CEVE").Unprotect Password:="CEVCEVE2017" 'desbloquea hoja Resultados en planilla 3
    
    wb_resultados.Sheets("Resultados").Activate
    Range("C6:BE3126").Select
    Selection.UnMerge
    Selection.ClearContents

'selecciona datos de hoja Resultados en planilla de cálculo, copia y pega en destino
    indice_error = "05.2" 'ERROR AL COPIAR Y PEGAR DATOS EN HOJA RESULTADOS
    ThisWorkbook.Sheets("Resultados").Activate
    fila_i_dat = 6
    fila_f_dat = Hoja4.Cells(1, 1) - 2
    If fila_f_dat < fila_i_dat Then
        fila_f_dat = fila_i_dat
    End If
    Range(Cells(fila_i_dat, 3), Cells(fila_f_dat, 57)).Select
    Selection.Copy

    wb_resultados.Sheets("Resultados").Activate
    fila_i_res = wb_resultados.Sheets("Resultados").Range("A1")
    fila_f_res = fila_i_res + (fila_f_dat - fila_i_dat)
    Range(Cells(fila_i_res, 3), Cells(fila_f_res, 57)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
    :=False, Transpose:=False
    

'selecciona datos de hoja CEV-CEVE en planilla de cálculo, copia y pega en destino
    indice_error = "05.3" 'ERROR AL COPIAR Y PEGAR DATOS EN HOJA CEV-CEVE
    wb_resultados.Sheets("CEV-CEVE").Activate
    Range(Cells(1, 1), Cells(238, 39)).Select
    Selection.ClearContents
    
    ThisWorkbook.Sheets("CEV-CEVE").Activate
    Range(Cells(1, 1), Cells(238, 39)).Select
    Selection.Copy

    wb_resultados.Sheets("CEV-CEVE").Activate
    Sheets("CEV-CEVE").Select: Range("A1").Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
    :=False, Transpose:=False
    
'realiza verificaciones de nombre para guardar guarda copia de planilla de resultados de la iteración i-ésima
    indice_error = "06" 'ERROR AL GUARDAR PLANILLA 3
    
    caso_interno = wb_resultados.Sheets("CEV-CEVE").Range("E25")
    nombre_salida = wb_resultados.Sheets("CEV-CEVE").Range("E13") & " " & caso_interno 'usa nombre de vivienda como nombre de archivo
    
    If Not Len(nombre_salida) > 0 Then 'si no existe nombre le asigna uno por defecto
        nombre_salida = "vivienda " & i
        wb_resultados.Sheets("CEV-CEVE").Range("E13").Value = nombre_salida
    Else 'si existe nombre lo filtra
        If Left(nombre_salida, 1) Like " " Then 'si primer caracter es espacio, lo reemlaza por "_"
            nombre_salida = "_" & Right(nombre_salida, Len(nombre_salida) - 1)
        End If
        For ind = 1 To Len(strIllegals) 'elimina caracteres no permitidos
            nombre_salida = Replace(nombre_salida, Mid$(strIllegals, ind, 1), "_")
        Next ind
    End If
    
    ReDim Preserve nombres(1 To i) 'comprobación de que nombre no es igual a un nombre usado previamente
    For a = LBound(nombres) To UBound(nombres)
        If nombre_salida Like nombres(a) Then
            nombre_salida = nombre_salida & " (1)"
            wb_resultados.Sheets("CEV-CEVE").Range("E13").Value = nombre_salida
        End If
    Next a
    
    If Len(ruta & "03.-PBTD " & nombre_salida) > 240 Then 'si el nombre y la ruta son muy largos, usa valores por defecto
        ruta = Application.DefaultFilePath
        If Len(ruta & "03.-PBTD " & nombre_salida) > 240 Then
            nombre_salida = "vivienda " & i
            wb_resultados.Sheets("CEV-CEVE").Range("E13").Value = nombre_salida
            inforuta = " , guardado en " & ruta
        End If
    End If
    
    nombres(i) = nombre_salida
    
'obtiene datos para resumen y guarda

    wb_resultados.Sheets("Resumen").Activate
    demanda_cal_prop = ActiveSheet.Range("C72").Value
    demanda_ref_prop = ActiveSheet.Range("C73").Value
    demanda_total_prop = ActiveSheet.Range("C74").Value
    porc_ahorro_cal = ActiveSheet.Range("G8").Value
    porc_ahorro_ref = ActiveSheet.Range("H8").Value
    porc_ahorro_total = ActiveSheet.Range("C92").Value
    letra = ActiveSheet.Range("K7").Value
    disconfort_calor = ActiveSheet.Range("E15").Value
    disconfort_frio = ActiveSheet.Range("F15").Value
    
    If admin Then
        QSol_v = ActiveSheet.Range("BG4").Value
        Qgen_v = ActiveSheet.Range("BH4").Value
        Qvent_v = ActiveSheet.Range("BJ4").Value
        Qinf_v = ActiveSheet.Range("BK4").Value
        Qenv_v = ActiveSheet.Range("BI4").Value
        Qtecho_v = ActiveSheet.Range("BL4").Value
        Qmuros_v = ActiveSheet.Range("BM4").Value
        Qventanas_v = ActiveSheet.Range("BN4").Value
        QpisoV = ActiveSheet.Range("BO4").Value
        QpisoCT_v = ActiveSheet.Range("BP4").Value
        Qcal_v = ActiveSheet.Range("BS4").Value
        Qref_v = ActiveSheet.Range("BT4").Value
        Qmasa_v = ActiveSheet.Range("BU4").Value
        Qrec_v = ActiveSheet.Range("BR4").Value
    
        QSol_i = ActiveSheet.Range("BG10").Value
        Qgen_i = ActiveSheet.Range("BH10").Value
        Qvent_i = ActiveSheet.Range("BJ10").Value
        Qinf_i = ActiveSheet.Range("BK10").Value
        Qenv_i = ActiveSheet.Range("BI10").Value
        Qtecho_i = ActiveSheet.Range("BL10").Value
        Qmuros_i = ActiveSheet.Range("BM10").Value
        Qventanas_i = ActiveSheet.Range("BN10").Value
        QpisoV = ActiveSheet.Range("BO10").Value
        QPisoCT_i = ActiveSheet.Range("BP10").Value
        Qcal_i = ActiveSheet.Range("BS10").Value
        Qref_i = ActiveSheet.Range("BT10").Value
        Qmasa_i = ActiveSheet.Range("BU10").Value
        Qrec_i = ActiveSheet.Range("BR10").Value
    End If
    
    fecha = Format(Now, "yy-mm-dd hh-nn") 'para evitar sobreescribir resultados anteriores, se agrega marca de tiempo para obtener nombres de archivo únicos
    
    wb_resultados.Sheets("Resultados").Protect Password:="CEVCEVE2017" 'bloquea hoja Resultados en planilla 3
    wb_resultados.Sheets("CEV-CEVE").Protect Password:="CEVCEVE2017" 'bloquea hoja Resultados en planilla 3
    wb_resultados.SaveAs Filename:=ruta & "03.-PBTD " & nombre_salida & " " & fecha & ".xlsm", _
    AccessMode:=xlExclusive, ConflictResolution:=Excel.XlSaveConflictResolution.xlLocalSessionChanges
    
'cierra planilla de resultados "original"
    wb_resultados.Close SaveChanges:=False
    Set wb_resultados = Nothing

'en caso de error de cálculo, el código continua a partir de este punto
PostErrorCalculo:
On Error GoTo CierreForzoso
'resumen de estado de situación evaluada
    ThisWorkbook.Sheets("Calculo").Activate
    ActiveSheet.Cells(35 + i, 1).Select
    Selection.Value = i
    ActiveSheet.Cells(35 + i, 2).Select
    Selection.Value = nombre_salida & " " & fecha
        
    If estado = True Then
        ActiveSheet.Cells(35 + i, 3).Select
        Selection.Value = "OK" & inforuta
        Cells(35 + i, 4).Value = demanda_cal_prop
        Cells(35 + i, 5).Value = demanda_ref_prop
        Cells(35 + i, 6).Value = demanda_total_prop
        Cells(35 + i, 7).Value = porc_ahorro_cal
         Cells(35 + i, 8).Value = porc_ahorro_ref
         Cells(35 + i, 9).Value = porc_ahorro_total
        Cells(35 + i, 10).Value = letra
         Cells(35 + i, 11).Value = disconfort_calor
         Cells(35 + i, 12).Value = disconfort_frio
        If admin Then 'agrega al resumen los calores mensuales calculados en kWh
            Cells(35 + i, 13).Value = QSol_v
            Cells(35 + i, 14).Value = Qgen_v
            Cells(35 + i, 15).Value = Qvent_v
            Cells(35 + i, 16).Value = Qinf_v
            Cells(35 + i, 17).Value = Qenv_v
            Cells(35 + i, 18).Value = Qtecho_v
            Cells(35 + i, 19).Value = Qmuros_v
            Cells(35 + i, 20).Value = Qventanas_v
            Cells(35 + i, 21).Value = QpisoV
            Cells(35 + i, 22).Value = QpisoCT_v
            Cells(35 + i, 23).Value = Qmasa_v
            Cells(35 + i, 24).Value = Qcal_v
            Cells(35 + i, 25).Value = Qref_v
            Cells(35 + i, 26).Value = Qrec_v
            Cells(35 + i, 27).Value = QSol_i
            Cells(35 + i, 28).Value = Qgen_i
            Cells(35 + i, 29).Value = Qvent_i
            Cells(35 + i, 30).Value = Qinf_i
            Cells(35 + i, 31).Value = Qenv_i
            Cells(35 + i, 32).Value = Qtecho_i
            Cells(35 + i, 33).Value = Qmuros_i
            Cells(35 + i, 34).Value = Qventanas_i
            Cells(35 + i, 35).Value = QpisoV
            Cells(35 + i, 36).Value = QPisoCT_i
            Cells(35 + i, 37).Value = Qmasa_i
            Cells(35 + i, 38).Value = Qcal_i
            Cells(35 + i, 39).Value = Qref_i
            Cells(35 + i, 40).Value = Qrec_i
        End If
    Else
        ActiveSheet.Cells(35 + i, 3).Value = "ERROR " & indice_error & " (" & Err.Number & ")"
    End If
    If admin Then
        ThisWorkbook.Save
    ElseIf i Mod 5 = 0 Then 'cada 5 planillas, se guarda PBTD02 para mantener resumen en caso de falla de energía, para lo cual se bloquea planilla, se guarda y se vuelve a desbloquear
        ThisWorkbook.Activate
        ThisWorkbook.Sheets("Datos").Visible = False
        ThisWorkbook.Sheets("SA").Visible = False
        ThisWorkbook.Sheets("CEV-CEVE").Visible = False
        ThisWorkbook.Sheets("Resultados").Visible = False
        ThisWorkbook.Sheets("Importado").Visible = False
        ThisWorkbook.Protect Password:="CEVCEVE2017"
        ThisWorkbook.Save
        ThisWorkbook.Unprotect Password:="CEVCEVE2017"
        ThisWorkbook.Sheets("Datos").Visible = True
        ThisWorkbook.Sheets("SA").Visible = True
        ThisWorkbook.Sheets("CEV-CEVE").Visible = True
        ThisWorkbook.Sheets("Resultados").Visible = True
        ThisWorkbook.Sheets("Importado").Visible = True
    End If
Next i

'--------------------------------------------------------------------------------
'CIERRE
'--------------------------------------------------------------------------------
'bloqueo planilla de cálculo
ThisWorkbook.Activate
ThisWorkbook.Sheets("Datos").Visible = False
ThisWorkbook.Sheets("SA").Visible = False
ThisWorkbook.Sheets("CEV-CEVE").Visible = False
ThisWorkbook.Sheets("Resultados").Visible = False
ThisWorkbook.Sheets("Importado").Visible = False
ThisWorkbook.Protect Password:="CEVCEVE2017"
ThisWorkbook.Save

'recuento de tiempo utilizado y mensaje de salida
SecondsElapsed = Round(Timer - StartTime, 0)
MsgBox "Se ejecutó la evaluación de " & UBound(direccion_datos) & " caso(s) base + propuesto en " & Format(SecondsElapsed / 86400, "hh:mm:ss"), vbInformation

'redefinición de parámetros a su valor inicial
Application.DisplayAlerts = True
Application.ScreenUpdating = True
Application.AutoRecover.Enabled = True
Application.EnableCancelKey = xlInterrupt
Exit Sub

'--------------------------------------------------------------------------------
'GESTIÓN DE ERRORES
'--------------------------------------------------------------------------------
'gestión de errores de cálculo
ErrorCalculo:
If Err.Number = 18 Then
    GoTo CierreForzoso
Else
    estado = False
    For Each xWB In Application.Workbooks
        If Not (xWB Is Application.ThisWorkbook) Then
            xWB.Close SaveChanges:=False 'en caso de error cierra todos los libros abiertos sin guardar cambios, excepto este libro
        End If
    Next
End If
GoTo PostErrorCalculo

'gestión de error en caso de detención forzoso del código
CierreForzoso:
If Err.Number = 18 Then
    lContinue = MsgBox("Si desea continuar presione Sí. Si desea terminar ejecución, presione No", _
      Buttons:=vbYesNo)
    If lContinue = vbYes Then
        Resume
    Else
        For Each xWB In Application.Workbooks
        If Not (xWB Is Application.ThisWorkbook) Then
            xWB.Close SaveChanges:=False 'en caso de error cierra todos los libros abiertos sin guardar cambios, excepto este libro
        End If
        Next
        ThisWorkbook.Activate
        With ThisWorkbook
            If .ProtectStructure Or .ProtectWindows Then
                Exit Sub
            Else
                .Sheets("Datos").Visible = False
                .Sheets("SA").Visible = False
                .Sheets("CEV-CEVE").Visible = False
                .Sheets("Resultados").Visible = False
                .Sheets("Importado").Visible = False
                .Protect Password:="CEVCEVE2017"
            End If
        End With
        Application.DisplayAlerts = True
        Application.ScreenUpdating = True
        Application.AutoRecover.Enabled = True
        Application.EnableCancelKey = xlInterrupt
        'MsgBox ("Ejecución finalizada a solicitud del usuario")
        Exit Sub
    End If
Else
    With ThisWorkbook
            If .ProtectStructure Or .ProtectWindows Then
                Exit Sub
            Else
                .Sheets("Datos").Visible = False
                .Sheets("SA").Visible = False
                .Sheets("CEV-CEVE").Visible = False
                .Sheets("Resultados").Visible = False
                .Sheets("Importado").Visible = False
                .Protect Password:="CEVCEVE2017"
            End If
        End With
        Application.DisplayAlerts = True
        Application.ScreenUpdating = True
        Application.AutoRecover.Enabled = True
        Application.EnableCancelKey = xlInterrupt
        MsgBox ("Error")
End If

Application.EnableCancelKey = xlInterrupt

End Sub

Sub correr_casos_propuestos()
'Código creado por Matías Yachán y Marcial Salaverry para ejecutar solo los 2 casos propuestos
'Modificado por Xavier Irazoqui
'Versión del 29/08/2018
'Principales cambios realizados en esta versión:
'- se deshabilita la autorecuperación para evitar que ante una falla de energía la planilla aparezca desbloqueada
'- se desbloquea planilla 3 para pegado de datos y posteriormente se bloquea
'- se incorpora modo administrador
'- se incorpora código que resguarda planilla ante cierre forzoso de macro
'- se permite elegir múltiples archivos de datos (en modo administrador)
'- se incorpora un resumen de los datos calculados
'- se ajusta código para limitar la ocurrencia de errores sólo al proceso de cálculo o pegado de datos
'- se agregan marcadores para identificar la porción de código que indujo a error
'- se especifican las referencias a "este" libro y de las variables
'- se realiza el borrado previo de la planilla 2 antes de recibir datos nuevos
'- se realiza el borrado previo de la planilla 3

'MARCADORES DE ERROR:
'01.1 ERROR EN LA APERTURA DE PLANILLA DATOS
'01.2 ERROR EN LIMPIEZA DE DATOS DE PLANILLA 2
'02.1 ERROR AL IMPORTAR DATOS PARA CÁLCULOS
'02.2 ERROR AL IMPORTAR DATOS CEV-CEVE
'03.caso ERROR AL COPIAR DATOS DEL CASO EN PLANILLA 2
'04.caso ERROR EN EL CÁLCULO DEL CASO
'05.1 ERROR AL ABRIR PLANILLA DE RESULTADOS
'05.2 ERROR AL COPIAR Y PEGAR DATOS EN HOJA RESULTADOS
'05.3 ERROR AL COPIAR Y PEGAR DATOS EN HOJA CEV-CEVE
'06 ERROR AL GUARDAR

'--------------------------------------------------------------------------------
'CONDICIONES INICIALES
'--------------------------------------------------------------------------------
'declara variables
Dim StartTime As Double, SecondsElapsed As Double, _
demanda_cal_prop As Double, demanda_ref_prop As Double, demanda_total_prop As Double, _
porc_ahorro_cal As Double, porc_ahorro_ref As Double, porc_ahorro_total As Double, _
QSol_v As Double, Qgen_v As Double, Qvent_v As Double, Qinf_v As Double, Qenv_v As Double, _
Qtecho_v As Double, Qmuros_v As Double, Qventanas_v As Double, QpisoV_v As Double, _
QpisoCT_v As Double, QPT_v As Double, Qmasa_v As Double, _
QSol_i As Double, Qgen_i As Double, Qvent_i As Double, Qinf_i As Double, Qenv_i As Double, _
Qtecho_i As Double, Qmuros_i As Double, Qventanas_i As Double, QPisoV_i As Double, _
QPisoCT_i As Double, QPT_i As Double, Qmasa_i As Double
Dim caso As Single
Dim nombre_motor As Variant, direccion_datos As Variant, direccion_resultados As Variant, _
dir_datos As Variant, nombre_datos As Variant, nombre_resultados As Variant, hojas As Variant
Dim estado As Boolean, caso_oficial As Boolean, admin As Boolean
Dim contador As Integer, i_LB As Integer, i_UB As Integer
Dim ruta As String, nombre_salida As String, indice_error As String, letra As String, _
version_pbtd1 As String
Dim wb_datos As Workbook, wb_resultados As Workbook, xWB As Workbook
Dim ws As Worksheet

'deshabilita actualización de pantalla, alertas y AUTORECUPERACIÓN (OJO!)
Application.ScreenUpdating = False
Application.DisplayAlerts = False
Application.AutoRecover.Enabled = False

'modo administrador
If ThisWorkbook.Sheets("Calculo").Range("A1") = "DITEC" Then
    admin = True
Else
    admin = False
End If

'verifica que el libro contiene todas las hojas necesarias para el código
hojas = Array("Datos", "Calculo", "CEV-CEVE", "Resultados", "SA", "Importado")
contador = 0

For Each ws In Worksheets
    For n = LBound(hojas) To UBound(hojas)
        If ws.Name Like hojas(n) Then
            contador = contador + 1
            Exit For
        End If
    Next n
Next ws

If contador < UBound(hojas) + 1 Then
    Exit Sub
End If


'--------------------------------------------------------------------------------
'ENTRADA DE DATOS
'--------------------------------------------------------------------------------
'advertencia
lContinue = MsgBox("Se recomienda guardar y cerrar todos los documentos antes de ejecutar el cálculo." & vbNewLine & _
"Presione Aceptar para continuar o Cancelar para detener.", vbOKCancel)
    If Not lContinue = vbOK Then
        Exit Sub
    End If

'se ubican planillas de datos y analisis
If admin Then 'en modo administrador se permite elegir más de una planilla 01
    MsgBox "Ubique la(s) planilla(s):" & vbNewLine & "01. PBTD Datos de Arquitectura" _
    & vbNewLine & vbNewLine & "Nota: puede seleccionar más de una", vbInformation, "PBTD: Ingreso de datos"

    direccion_datos = Application _
    .GetOpenFilename("Hoja de cálculo habilitada para macros de Microsoft Excel (*.xlsm), *.xlsm", , , , True)
    If Not IsArray(direccion_datos) Then
        'MsgBox "No se seleccionó ningún archivo de datos."
        Exit Sub 'Si no se selecciona uno o más archivos de datos, se termina la sub rutina
    End If
    i_LB = LBound(direccion_datos)
    i_UB = UBound(direccion_datos)
    
    MsgBox "Ubique la planilla:" & vbNewLine & "03. PBTD Datos de Equipos y Resultados" _
    & vbNewLine & vbNewLine & "Nota: la planilla que se seleccione se utilizará como plantilla con la que se guardarán" _
    & "los resultados de los cálculos realizados a cada una de las planillas 01 seleccionadas anteriormente." _
    , vbInformation, "PBTD: Análisis de resultados"

Else 'en caso contrario se permite elegir solo una planilla 01
    MsgBox "Ubique la(s) planilla(s):" & vbNewLine & "01. PBTD Datos de Arquitectura" _
    & vbNewLine, vbInformation, "PBTD: Ingreso de datos"
    
    dir_datos = Application _
    .GetOpenFilename("Hoja de cálculo habilitada para macros de Microsoft Excel (*.xlsm), *.xlsm", , , , False)
    'si no se selecciona ningún archivo la subrutina se detiene
    If dir_datos = False Then
        'MsgBox "No se seleccionó ningún archivo de resultados."
        Exit Sub
    End If
    ReDim direccion_datos(1 To 1)
    direccion_datos(1) = dir_datos
    i_LB = 1
    i_UB = 1
    
    MsgBox "Ubique la planilla:" & vbNewLine & "03. PBTD Datos de Equipos y Resultados" _
    & vbNewLine & vbNewLine & "Correr Análisis", vbInformation, "PBTD: Análisis de resultados"

End If

direccion_resultados = Application _
 .GetOpenFilename("Hoja de cálculo habilitada para macros de Microsoft Excel (*.xlsm), *.xlsm", , , , False)

'si no se selecciona ningún archivo la subrutina se detiene
If direccion_resultados = False Then
    'MsgBox "No se seleccionó ningún archivo de resultados."
    Exit Sub
End If

'--------------------------------------------------------------------------------
'PROCESAMIENTO DE DATOS
'--------------------------------------------------------------------------------


Application.EnableCancelKey = xlErrorHandler
On Error GoTo CierreForzoso
'Remember time when macro starts
StartTime = Timer

'desbloqueo de planilla
ThisWorkbook.Unprotect Password:="CEVCEVE2017"
ThisWorkbook.Sheets("Datos").Visible = True
ThisWorkbook.Sheets("SA").Visible = True
ThisWorkbook.Sheets("CEV-CEVE").Visible = True
ThisWorkbook.Sheets("Resultados").Visible = True
ThisWorkbook.Sheets("Importado").Visible = True

'borra resumen y reescribe columnas
Call columnas_resumen(admin)

'cálculo para cada planilla de datos seleccionada
For i = i_LB To i_UB
    indice_error = ""
    On Error GoTo ErrorCalculo 'en caso de error se realizan las instrucciones del _
    control de errores y prosigue a la siguiente iteración
    estado = True
    indice_error = ""
    nombre_salida = "vivienda " & i 'valor por defecto
    
    indice_error = "01.1" 'ERROR EN LA APERTURA DE PLANILLA DATOS
    Workbooks.Open direccion_datos(i)
    nombre_datos = ActiveWorkbook.Name
    nombre_salida = nombre_datos
    Set wb_datos = Workbooks(nombre_datos)
    ruta = wb_datos.Path & "\"
    inforuta = ""
    
'limpia datos anteriores en planilla 02
    indice_error = "01.2" 'ERROR EN LIMPIEZA DE DATOS
    Call Limpiar_datos
     
'importa datos necesarios desde planilla 1 y la cierra
    indice_error = "02.1" 'ERROR AL IMPORTAR DATOS PARA CÁLCULOS
    
    Call importar_datos(2, wb_datos)
    
    indice_error = "02.2" 'ERROR AL IMPORTAR DATOS CEV-CEVE
    wb_datos.Sheets("CEV-CEVE").Activate
    'version_pbtd1 = Range("E24").Value
    Range(Cells(1, 1), Cells(239, 26)).Select
    Selection.Copy
    
    ThisWorkbook.Activate
    Sheets("CEV-CEVE").Select
    Cells(1, 1).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False

    wb_datos.Sheets("3. Tablas Envolvente").Activate
    Range(Cells(10, 1), Cells(92, 13)).Select
    Selection.Copy
    
    ThisWorkbook.Activate
    Sheets("CEV-CEVE").Select
    Cells(10, 27).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
        :=False, Transpose:=False
    
    wb_datos.Close SaveChanges:=False
    Set wb_datos = Nothing
    
'llama al cálculo
    For caso = 1 To 2
        indice_error = "03." & caso 'ERROR AL IMPORTAR DATOS DEL CASO
        Call datos_caso(caso)
        indice_error = "04." & caso 'ERROR EN EL CÁLCULO DEL CASO
        Call calculo
    Next caso

'abre planilla resultados y limpia resultados anteriores
    indice_error = "05.1" ' ERROR AL ABRIR PLANILLA DE RESULTADOS
    Workbooks.Open direccion_resultados
    nombre_resultados = ActiveWorkbook.Name
    Set wb_resultados = Workbooks(nombre_resultados)
    wb_resultados.Sheets("Resultados").Unprotect Password:="CEVCEVE2017" 'desbloquea hoja Resultados en planilla 3
    wb_resultados.Sheets("CEV-CEVE").Unprotect Password:="CEVCEVE2017" 'desbloquea hoja Resultados en planilla 3
    
    'ruta = wb_resultados.Path & "\"
    ThisWorkbook.Sheets("Resultados").Activate
    fila_i_dat = 6
    fila_f_dat = ActiveSheet.Cells(1, 1) - 2
    If fila_f_dat < fila_i_dat Then
        fila_f_dat = fila_i_dat
    End If
    wb_resultados.Sheets("Resultados").Activate
    Range(Cells(fila_i_dat, 3), Cells(fila_f_dat, 57)).Select
    Selection.UnMerge
    Selection.ClearContents

'selecciona datos de hoja Resultados, copia y pega en destino pisando las primeras 2 iteraciones
    indice_error = "05.2" 'ERROR AL COPIAR Y PEGAR DATOS EN HOJA RESULTADOS
    ThisWorkbook.Sheets("Resultados").Activate
    Range(Cells(fila_i_dat, 3), Cells(fila_f_dat, 57)).Select
    Selection.Copy

    wb_resultados.Sheets("Resultados").Activate
    fila_i_res = 6
    fila_f_res = fila_i_res + (fila_f_dat - fila_i_dat)
    Range(Cells(fila_i_res, 3), Cells(fila_f_res, 57)).Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
    :=False, Transpose:=False
    
    If ActiveSheet.Range("Resultados!C3000").Value Like "" Then 'se determina si el caso es oficial
        caso_oficial = False
    Else
        caso_oficial = True
    End If

'selecciona datos de hoja CEV-CEVE, copia y pega en destino
    indice_error = "05.3" 'ERROR AL COPIAR Y PEGAR DATOS EN HOJA CEV-CEVE
    wb_resultados.Sheets("CEV-CEVE").Activate
    Range(Cells(1, 1), Cells(238, 39)).Select
    Selection.ClearContents
    
    ThisWorkbook.Sheets("CEV-CEVE").Activate
    Range(Cells(1, 1), Cells(238, 39)).Select
    Selection.Copy

    wb_resultados.Sheets("CEV-CEVE").Activate
    Sheets("CEV-CEVE").Select: Range("a1").Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
    :=False, Transpose:=False
    
'realiza verificaciones de nombre para guardar guarda copia de planilla de resultados de la iteración i-ésima
    indice_error = "06" 'ERROR AL GUARDAR PLANILLA 3
    
    caso_interno = wb_resultados.Sheets("CEV-CEVE").Range("E25")
    nombre_salida = wb_resultados.Sheets("CEV-CEVE").Range("E13") & " " & caso_interno 'usa nombre de vivienda como nombre de archivo
    
    If Not Len(nombre_salida) > 0 Then 'si no existe nombre le asigna uno por defecto
        nombre_salida = "vivienda " & i
        wb_resultados.Sheets("CEV-CEVE").Range("E13").Value = nombre_salida
    Else 'si existe nombre lo filtra
        If Left(nombre_salida, 1) Like " " Then 'si primer caracter es espacio, lo reemlaza por "_"
            nombre_salida = "_" & Right(nombre_salida, Len(nombre_salida) - 1)
        End If
        For ind = 1 To Len(strIllegals) 'elimina caracteres no permitidos
            nombre_salida = Replace(nombre_salida, Mid$(strIllegals, ind, 1), "_")
        Next ind
    End If
    
   If Len(ruta & "03.-PBTD " & nombre_salida) > 240 Then 'si el nombre y la ruta son muy largos, usa valores por defecto
        ruta = Application.DefaultFilePath
        If Len(ruta & "03.-PBTD " & nombre_salida) > 240 Then
            nombre_salida = "vivienda " & i
            wb_resultados.Sheets("CEV-CEVE").Range("E13").Value = nombre_salida
            inforuta = " , guardado en " & ruta
        End If
    End If
    
'obtiene datos para resumen y guarda

    wb_resultados.Sheets("Resumen").Activate
    demanda_cal_prop = ActiveSheet.Range("C72").Value
    demanda_ref_prop = ActiveSheet.Range("C73").Value
    demanda_total_prop = ActiveSheet.Range("C74").Value
    disconfort_calor = ActiveSheet.Range("E15").Value
    disconfort_frio = ActiveSheet.Range("F15").Value
    If caso_oficial Then
        porc_ahorro_cal = ActiveSheet.Range("G8").Value
        porc_ahorro_ref = ActiveSheet.Range("H8").Value
        porc_ahorro_total = ActiveSheet.Range("C92").Value
        letra = ActiveSheet.Range("K7").Value
    End If
    
    If admin Then
        QSol_v = ActiveSheet.Range("BG4").Value
        Qgen_v = ActiveSheet.Range("BH4").Value
        Qvent_v = ActiveSheet.Range("BJ4").Value
        Qinf_v = ActiveSheet.Range("BK4").Value
        Qenv_v = ActiveSheet.Range("BI4").Value
        Qtecho_v = ActiveSheet.Range("BL4").Value
        Qmuros_v = ActiveSheet.Range("BM4").Value
        Qventanas_v = ActiveSheet.Range("BN4").Value
        QpisoV = ActiveSheet.Range("BO4").Value
        QpisoCT_v = ActiveSheet.Range("BP4").Value
        Qcal_v = ActiveSheet.Range("BS4").Value
        Qref_v = ActiveSheet.Range("BT4").Value
        Qmasa_v = ActiveSheet.Range("BU4").Value
        Qrec_v = ActiveSheet.Range("BR4").Value
    
        QSol_i = ActiveSheet.Range("BG10").Value
        Qgen_i = ActiveSheet.Range("BH10").Value
        Qvent_i = ActiveSheet.Range("BJ10").Value
        Qinf_i = ActiveSheet.Range("BK10").Value
        Qenv_i = ActiveSheet.Range("BI10").Value
        Qtecho_i = ActiveSheet.Range("BL10").Value
        Qmuros_i = ActiveSheet.Range("BM10").Value
        Qventanas_i = ActiveSheet.Range("BN10").Value
        QpisoV = ActiveSheet.Range("BO10").Value
        QPisoCT_i = ActiveSheet.Range("BP10").Value
        Qcal_i = ActiveSheet.Range("BS10").Value
        Qref_i = ActiveSheet.Range("BT10").Value
        Qmasa_i = ActiveSheet.Range("BU10").Value
        Qrec_i = ActiveSheet.Range("BR10").Value
    End If
    
    fecha = Format(Now, "yy-mm-dd hh-nn") 'para evitar sobreescribir resultados anteriores, se agrega marca de tiempo para obtener nombres de archivo únicos
    
    wb_resultados.Sheets("Resultados").Protect Password:="CEVCEVE2017" 'bloquea hoja Resultados en planilla 3
    wb_resultados.Sheets("CEV-CEVE").Protect Password:="CEVCEVE2017" 'bloquea hoja Resultados en planilla 3
    wb_resultados.SaveAs Filename:=ruta & "03.-PBTD " & nombre_salida & " " & fecha & ".xlsm", _
    AccessMode:=xlExclusive, ConflictResolution:=Excel.XlSaveConflictResolution.xlLocalSessionChanges

'cierra planilla de resultados "original"
    wb_resultados.Close SaveChanges:=False
    Set wb_resultados = Nothing

'en caso de error de cálculo, el código continua a partir de este punto
PostErrorCalculo:
On Error GoTo CierreForzoso
'resumen de estado de situación evaluada
    ThisWorkbook.Sheets("Calculo").Activate
    ActiveSheet.Cells(35 + i, 1).Value = i
    ActiveSheet.Cells(35 + i, 2).Value = nombre_salida & " " & fecha
        
    If estado = True Then
        ActiveSheet.Cells(35 + i, 3).Value = "OK" & inforuta
        Cells(35 + i, 4).Value = demanda_cal_prop
        Cells(35 + i, 5).Value = demanda_ref_prop
        Cells(35 + i, 6).Value = demanda_total_prop
        If caso_oficial Then
            Cells(35 + i, 7).Value = porc_ahorro_cal
            Cells(35 + i, 8).Value = porc_ahorro_ref
            Cells(35 + i, 9).Value = porc_ahorro_total
            Cells(35 + i, 10).Value = letra
        Else
            Cells(35 + i, 7).Value = "---"
            Cells(35 + i, 8).Value = "---"
            Cells(35 + i, 9).Value = "---"
            Cells(35 + i, 10).Value = "---"
        End If
        Cells(35 + i, 11).Value = disconfort_calor
        Cells(35 + i, 12).Value = disconfort_frio
        If admin Then 'agrega al resumen los calores mensuales calculados en kWh
            Cells(35 + i, 13).Value = QSol_v
            Cells(35 + i, 14).Value = Qgen_v
            Cells(35 + i, 15).Value = Qvent_v
            Cells(35 + i, 16).Value = Qinf_v
            Cells(35 + i, 17).Value = Qenv_v
            Cells(35 + i, 18).Value = Qtecho_v
            Cells(35 + i, 19).Value = Qmuros_v
            Cells(35 + i, 20).Value = Qventanas_v
            Cells(35 + i, 21).Value = QpisoV
            Cells(35 + i, 22).Value = QpisoCT_v
            Cells(35 + i, 23).Value = Qmasa_v
            Cells(35 + i, 24).Value = Qcal_v
            Cells(35 + i, 25).Value = Qref_v
            Cells(35 + i, 26).Value = Qrec_v
            Cells(35 + i, 27).Value = QSol_i
            Cells(35 + i, 28).Value = Qgen_i
            Cells(35 + i, 29).Value = Qvent_i
            Cells(35 + i, 30).Value = Qinf_i
            Cells(35 + i, 31).Value = Qenv_i
            Cells(35 + i, 32).Value = Qtecho_i
            Cells(35 + i, 33).Value = Qmuros_i
            Cells(35 + i, 34).Value = Qventanas_i
            Cells(35 + i, 35).Value = QpisoV
            Cells(35 + i, 36).Value = QPisoCT_i
            Cells(35 + i, 37).Value = Qmasa_i
            Cells(35 + i, 38).Value = Qcal_i
            Cells(35 + i, 39).Value = Qref_i
            Cells(35 + i, 40).Value = Qrec_i
        End If
    Else
        ActiveSheet.Cells(35 + i, 3).Value = "ERROR " & indice_error & " (" & Err.Number & ")"
    End If
    If admin Then
        ThisWorkbook.Save
    ElseIf i Mod 5 = 0 Then 'cada 5 planillas, se guarda PBTD02 para mantener resumen en caso de falla de energía, para lo cual se bloquea planilla, se guarda y se vuelve a desbloquear
        ThisWorkbook.Activate
        ThisWorkbook.Sheets("Datos").Visible = False
        ThisWorkbook.Sheets("SA").Visible = False
        ThisWorkbook.Sheets("CEV-CEVE").Visible = False
        ThisWorkbook.Sheets("Resultados").Visible = False
        ThisWorkbook.Sheets("Importado").Visible = False
        ThisWorkbook.Protect Password:="CEVCEVE2017"
        ThisWorkbook.Save
        ThisWorkbook.Unprotect Password:="CEVCEVE2017"
        ThisWorkbook.Sheets("Datos").Visible = True
        ThisWorkbook.Sheets("SA").Visible = True
        ThisWorkbook.Sheets("CEV-CEVE").Visible = True
        ThisWorkbook.Sheets("Resultados").Visible = True
        ThisWorkbook.Sheets("Importado").Visible = True
    End If

Next i

'--------------------------------------------------------------------------------
'CIERRE
'--------------------------------------------------------------------------------
'bloqueo planilla de cálculo
ThisWorkbook.Activate
ThisWorkbook.Sheets("Datos").Visible = False
ThisWorkbook.Sheets("SA").Visible = False
ThisWorkbook.Sheets("CEV-CEVE").Visible = False
ThisWorkbook.Sheets("Resultados").Visible = False
ThisWorkbook.Sheets("Importado").Visible = False
ThisWorkbook.Protect Password:="CEVCEVE2017"
ThisWorkbook.Save

'recuento de tiempo utilizado y mensaje de salida
SecondsElapsed = Round(Timer - StartTime, 0)
MsgBox "Se ejecutó la evaluación de " & UBound(direccion_datos) & " caso(s) propuesto en " & Format(SecondsElapsed / 86400, "hh:mm:ss"), vbInformation

'redefinición de parámetros a su valor inicial
Application.DisplayAlerts = True
Application.ScreenUpdating = True
Application.AutoRecover.Enabled = True
Application.EnableCancelKey = xlInterrupt

Exit Sub

'--------------------------------------------------------------------------------
'GESTIÓN DE ERRORES
'--------------------------------------------------------------------------------
'gestión de errores de cálculo
ErrorCalculo:
estado = False
For Each xWB In Application.Workbooks
    If Not (xWB Is Application.ThisWorkbook) Then
        xWB.Close SaveChanges:=False
    End If
Next
GoTo PostErrorCalculo

CierreForzoso:
If Err.Number = 18 Then
    lContinue = MsgBox("Si desea continuar presione Sí. Si desea terminar ejecución, presione No", _
      Buttons:=vbYesNo)
    If lContinue = vbYes Then
        Resume
    Else
        For Each xWB In Application.Workbooks
        If Not (xWB Is Application.ThisWorkbook) Then
            xWB.Close SaveChanges:=False 'en caso de error cierra todos los libros abiertos sin guardar cambios, excepto este libro
        End If
        Next
        ThisWorkbook.Activate
        With ThisWorkbook
            If .ProtectStructure Or .ProtectWindows Then
                Exit Sub
            Else
                .Sheets("Datos").Visible = False
                .Sheets("SA").Visible = False
                .Sheets("CEV-CEVE").Visible = False
                .Sheets("Resultados").Visible = False
                .Sheets("Importado").Visible = False
                .Protect Password:="CEVCEVE2017"
            End If
        End With
        Application.ScreenUpdating = True
        Application.DisplayAlerts = True
        Application.AutoRecover.Enabled = True
        Application.EnableCancelKey = xlInterrupt
        'MsgBox ("Ejecución finalizada a solicitud del usuario")
        Exit Sub
    End If
Else
    With ThisWorkbook
            If .ProtectStructure Or .ProtectWindows Then
                Exit Sub
            Else
                .Sheets("Datos").Visible = False
                .Sheets("SA").Visible = False
                .Sheets("CEV-CEVE").Visible = False
                .Sheets("Resultados").Visible = False
                .Sheets("Importado").Visible = False
                .Protect Password:="CEVCEVE2017"
            End If
        End With
        Application.ScreenUpdating = True
        Application.DisplayAlerts = True
        Application.AutoRecover.Enabled = True
        Application.EnableCancelKey = xlInterrupt
        MsgBox ("Error")
End If

Application.EnableCancelKey = xlInterrupt

End Sub
