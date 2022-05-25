Attribute VB_Name = "Módulo2"
Sub Limpiar_datos()
Attribute Limpiar_datos.VB_ProcData.VB_Invoke_Func = " \n14"

    ThisWorkbook.Sheets("Resultados").Range("C6:BE1000000").ClearContents
    ThisWorkbook.Sheets("Importado").Range("B3:ID481").ClearContents
    ThisWorkbook.Sheets("CEV-CEVE").Range("A1:AM239").ClearContents
    
End Sub

Sub guardar_resumen()

ThisWorkbook.Sheets("Calculo").Activate
If Not IsEmpty(Hoja2.Cells(36, 1)) Then
    Cells(35, 1).Select
    ThisWorkbook.Sheets("Calculo").Range(Cells(35, 1), Cells(Selection.End(xlDown).Row, Selection.End(xlToRight).Column)).Select
    Selection.Copy
    Set resumen = Workbooks.Add
    Range("A1").Select
    Selection.PasteSpecial Paste:=xlPasteValues, Operation:=xlNone, SkipBlanks _
    :=False, Transpose:=False
    resumen.SaveAs Filename:=ThisWorkbook.Path & "\resumen - " & Format(Now, "yyyymmdd-hhmmss") & ".xlsx"
End If
End Sub

Sub limpiar_resumen()
'Hoja2.Range(Cells(35, 1), Cells(Selection.End(xlDown).Row, Selection.End(xlToRight).Column)).UnMerge 'con la hoja bloqueada esta línea no se justifica
If Not IsEmpty(Hoja2.Cells(36, 1)) Then
    ThisWorkbook.Sheets("Calculo").Unprotect Password:="CEVCEVE2017"
    Hoja2.Range(Cells(35, 1), Cells(Selection.End(xlDown).Row, Selection.End(xlToRight).Column)).Select
    Selection.ClearContents
    ThisWorkbook.Sheets("Calculo").Protect Password:="CEVCEVE2017"
End If
End Sub

Sub columnas_resumen(admin As Boolean)
ThisWorkbook.Sheets("Calculo").Activate
Range(Cells(35, 1), Cells(1000, 40)).Select
'Selection.UnMerge 'con hoja bloqueada no se justifica esta línea
Selection.ClearContents
Cells(35, 1).Select
Selection.Value = "#"
Cells(35, 2).Select
Selection.Value = "Situación evaluada"
Cells(35, 3).Select
Selection.Value = "Estado"
Cells(35, 4).Select
Selection.Value = "Demanda calefacción [kWh/m²año]"
Cells(35, 5).Select
Selection.Value = "Demanda refrigeración [kWh/m²año]"
Cells(35, 6).Select
Selection.Value = "Demanda total [kWh/m²año]"
Cells(35, 7).Select
Selection.Value = "% ahorro calefacción"
Cells(35, 8).Select
Selection.Value = "% ahorro refrigeración"
Cells(35, 9).Select
Selection.Value = "% ahorro demanda"
Cells(35, 10).Select
Selection.Value = "letra"
Cells(35, 11).Select
Selection.Value = "% Tiempo en disconfort frio"
Cells(35, 12).Select
Selection.Value = "% Tiempo en disconfort calor"
If admin Then
    Cells(35, 13).Select
    Selection.Value = "Q sol v"
    Cells(35, 14).Select
    Selection.Value = "Q gen v"
    Cells(35, 15).Select
    Selection.Value = "Q vent v"
    Cells(35, 16).Select
    Selection.Value = "Q inf v"
    Cells(35, 17).Select
    Selection.Value = "Q env v"
    Cells(35, 18).Select
    Selection.Value = "Q techo v"
    Cells(35, 19).Select
    Selection.Value = "Q muro v"
    Cells(35, 20).Select
    Selection.Value = "Q ventana v"
    Cells(35, 21).Select
    Selection.Value = "Q piso vent v"
    Cells(35, 22).Select
    Selection.Value = "Q piso ct v"
    Cells(35, 23).Select
    Selection.Value = "Q masa v"
    Cells(35, 24).Select
    Selection.Value = "Q cal v"
    Cells(35, 25).Select
    Selection.Value = "Q ref v"
    Cells(35, 26).Select
    Selection.Value = "Q recuperado v"
    Cells(35, 27).Select
    Selection.Value = "Q sol i"
    Cells(35, 28).Select
    Selection.Value = "Q gen i"
    Cells(35, 29).Select
    Selection.Value = "Q vent i"
    Cells(35, 30).Select
    Selection.Value = "Q inf i"
    Cells(35, 31).Select
    Selection.Value = "Q env i"
    Cells(35, 32).Select
    Selection.Value = "Q techo i"
    Cells(35, 33).Select
    Selection.Value = "Q muro i"
    Cells(35, 34).Select
    Selection.Value = "Q ventana i"
    Cells(35, 35).Select
    Selection.Value = "Q piso vent i"
    Cells(35, 36).Select
    Selection.Value = "Q piso ct i"
    Cells(35, 37).Select
    Selection.Value = "Q masa i"
    Cells(35, 38).Select
    Selection.Value = "Q cal i"
    Cells(35, 39).Select
    Selection.Value = "Q ref i"
    Cells(35, 40).Select
    Selection.Value = "Q recuperado i"
    
End If

End Sub
Sub condiciones_base(ws As Workbook)
'Dim Tabla1_OGUC2007(1 To 7, 1 To 4) As Double
'Tabla1_OGUC2007 = array(1, 0.84, 4, 3,6; _
                        2, 0.6, 3, 0.87; _
                        3, 0.47, 1.9, 0.7; _
                        4, 0.38, 1.7, 0.6; _
                        5, 0.33, 1,6, 0.5; _
                        6, 0.28, 1.1, 0.39; _
                        7, 0.25, 0.6, 0.32)
                        

ws.Sheets("0. Datos Arq Motor").Activate

Range("base_A_puerta_traslucida").Value = 0
Range("base_absortividad_techo").Value = 0.7
Range("base_cierre_ventanas").Value = "Corredera"
'Range("base_espesor_aislante_PT").Value '= fórmula
Range("base_FM_ventana").Value = 0.85
'Range("base_FS_vidrio").Value '= fórmula
Range("base_infiltracion_variable").Value = "No"
Range("base_marco_P05").Value = "Metalico"
Range("base_mat_marcos").Value = "Metalico"
Range("base_mat_puertas").Value = "Madera Liviana"
Range("base_pos_aislante_P04").Value = "Sin Aislación"
Range("base_pos_P05").Value = "Centrada"
Range("base_retorno_P05").Value = "Sin"
Range("base_U_marco").Value = 5.8
Range("base_U_muro").Value '= fórmula
Range("base_U_puerta").Value = 2.51
Range("base_U_PV").Value '= fórmula
Range("base_U_techo").Value '= fórmula
Range("base_U_vidrio").Value '= fórmula
Range("base_U_vidrio_techo").Value '= fórmula
Range("base_vidrio_infiltraciones").Value '= fórmula

End Sub
