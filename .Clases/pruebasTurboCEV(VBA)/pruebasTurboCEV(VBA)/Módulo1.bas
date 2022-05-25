Attribute VB_Name = "Módulo1"
Sub calculo()
'Algoritmo creado por Matías Yachán y Marcial Salaverry para calcular la demanda energética de una vivienda
'Modificado por Xavier Irazoqui
'Versión del 14/11/2018
'Principales cambios realziados en esta versión:
'- lectura de datos para inercia térmica se saca de ciclo Do While y se traslada a la primera sección
'- se divide algoritmo en secciones

'-------------------------------------------------------------------------------------------------------------
'DEFINICIÓN DE PARÁMETROS DE CÁLCULO
'-------------------------------------------------------------------------------------------------------------

Application.ScreenUpdating = False


'limpio resultados anteriores
'Call Limpiar_resultados
 
'constantes del aire
roaire = 1.2             'kg/m3
cpaire = 1000           'J/kgK

'constantes de transferencia de calor
hsol = 17               'h para calculo de Tsol
hint = 8.3             'h para transferencia de calor en la masa con el interior, es igual al valor hinterior de la NCh853 1/0.12
hext = 20               'h para transferencia de calor en la masa con el exterior, es igual al valor hinterior de la NCh853 1/0.05

Pmaxclima = 100000 '[W] potencia maxima para evaluar tiempo de ajuste a la banda, permite mas realismo.

Hoja1.Range("C2") = 0   'tiempo en hoja 1 [s]

'TEMPERATURAS DE CONFORT PARA ENERO (t=0)
Tmax1 = Hoja1.Range("Z3")
Tmin1 = Hoja1.Range("AA3")
Tmax2 = Hoja1.Range("AB3")
Tmin2 = Hoja1.Range("AC3")
infilt = Hoja1.Range("AE3")

'variables de cálculo
recup_cal = Hoja2.Range("C4")
ef_recup = Hoja2.Range("C5")
hay_clima = Hoja2.Range("C6")


'variables recinto
'------Volumen-------
Aplanta = Hoja2.Range("G26")
Vaire = Hoja2.Range("I26")

'------Techo-------
Atecho_v = Hoja2.Range("K8")
Atecho = Hoja2.Range("H8")
Utecho_v = Hoja2.Range("L8")
Utecho = Hoja2.Range("I8")

'------Norte-------
Anorte_v = Hoja2.Range("K9")
Anorte = Hoja2.Range("H9")
FiLnorte = Hoja2.Range("N9")
Unorte_v = Hoja2.Range("L9")
Unorte = Hoja2.Range("I9")

'------Nor-Este-------
Anoreste_v = Hoja2.Range("K10")
Anoreste = Hoja2.Range("H10")
FiLnoreste = Hoja2.Range("N10")
Unoreste_v = Hoja2.Range("L10")
Unoreste = Hoja2.Range("I10")

'------Este-------
Aeste_v = Hoja2.Range("K11")
Aeste = Hoja2.Range("H11")
FiLeste = Hoja2.Range("N11")
Ueste_v = Hoja2.Range("L11")
Ueste = Hoja2.Range("I11")

'------Sur-Este-------
Asureste_v = Hoja2.Range("K12")
Asureste = Hoja2.Range("H12")
FiLsureste = Hoja2.Range("N12")
Usureste_v = Hoja2.Range("L12")
Usureste = Hoja2.Range("I12")

'------Sur-------
Asur_v = Hoja2.Range("K13")
Asur = Hoja2.Range("H13")
FiLsur = Hoja2.Range("N13")
Usur_v = Hoja2.Range("L13")
Usur = Hoja2.Range("I13")

'------Sur Oeste-------
Asuroeste_v = Hoja2.Range("K14")
Asuroeste = Hoja2.Range("H14")
FiLsuroeste = Hoja2.Range("N14")
Usuroeste_v = Hoja2.Range("L14")
Usuroeste = Hoja2.Range("I14")

'------Oeste-------
Aoeste_v = Hoja2.Range("K15")
Aoeste = Hoja2.Range("H15")
FiLoeste = Hoja2.Range("N15")
Uoeste_v = Hoja2.Range("L15")
Uoeste = Hoja2.Range("I15")

'------Nor oeste-------
Anoroeste_v = Hoja2.Range("K16")
Anoroeste = Hoja2.Range("H16")
FiLnoroeste = Hoja2.Range("N16")
Unoroeste_v = Hoja2.Range("L16")
Unoroeste = Hoja2.Range("I16")

'------Piso Ventilado-------
Apiso_v = Hoja2.Range("H17")
Upiso_v = Hoja2.Range("I17")

'------Piso contra terreno-------
FiLpiso_t = Hoja2.Range("N17")

' participacion de masa
m_techo = Hoja5.Range("Q41")
m_muros = Hoja5.Range("Q42")
m_piso_v = Hoja5.Range("Q43")
m_piso_t = Hoja5.Range("Q44")
m_masa_ad = Hoja5.Range("Q45")

'Variables numericas
dt = 60                                    'paso de tiempo
dpm = Hoja5.Range("Q47")                   'dias por mes zona de convergencia
Hoja1.Range("C11") = dpm
iter = (dpm * 24 * 60 * 60) * 12 / dt       'total iteraciones solo enero (para el año completo cambiar 1 por 12)
iter_base = 86400 / dt                   'iteraciones dia base (parte en 0, de ahi +1)

'datos para T° inicial sinusoidal
amort = Hoja5.Range("P38")
Aext = Hoja1.Range("H37") 'Amplitud Text
a = Aext * amort / 2
T = 13751 '24 * 3600 / (2 * 3.1416) esto es el inverso de la frecuencia angular para tiempos en segundos W=2*pi/T donde T=24h*3600
CTm = Hoja5.Range("Q38")
fi = Hoja5.Range("R38") 'desfase Tint [rad] el desfase de la Text es como 4 rad por lo que el desfase real es de 1.2rad =>1.2*24h/2pi=4.56hrs
Tm = Hoja1.Range("H38") + Hoja1.Range("H39") / Aplanta / CTm ' Tint media

'datos para inercia térmica
roHOR = 2400 'kg/m3
areaHOR = Hoja5.Range("Q35")
espHOR = Hoja5.Range("S34")    'cm
masaHOR = roHOR * areaHOR * espHOR / 100
cpHOR = 920
capHOR = masaHOR * cpHOR

'------------------------------------------------------------------------------------------------------
'defino matriz de resultados

ReDim matriz(1 To (iter + iter_base + 1), 1 To 55)
ReDim SA_H(1 To 24)
ReDim SA_N(1 To 24)
ReDim SA_NE(1 To 24)
ReDim SA_E(1 To 24)
ReDim SA_SE(1 To 24)
ReDim SA_S(1 To 24)
ReDim SA_SO(1 To 24)
ReDim SA_O(1 To 24)
ReDim SA_NO(1 To 24)
ReDim SA_PV(1 To 24)
ReDim SA_PT(1 To 24)

For i = 1 To 24
    SA_H(i) = 0
    SA_N(i) = 0
    SA_NE(i) = 0
    SA_E(i) = 0
    SA_SE(i) = 0
    SA_S(i) = 0
    SA_SO(i) = 0
    SA_O(i) = 0
    SA_NO(i) = 0
    SA_PV(i) = 0
    SA_PT(i) = 0
Next

'-------------------------------------------------------------------------------------------------------------
'INTERPOLACIÓN TEMPERATURAS INICIALES - DÍA BASE
'-------------------------------------------------------------------------------------------------------------
'fila 1 en t=0
'condiciones iniciales
'tiempo inicial
Hoja1.Range("C2") = 0            'tiempo en hoja 1 [s]

fila = 1
Do While fila <= iter_base
    'tiempo inicial
    Hoja1.Range("C2") = (fila - 1) * dt        'tiempo en hoja 1 [s]
    matriz(fila, 1) = (fila - 1) * dt / 60        'tiempo inicial en matriz [hrs]
    matriz(fila, 2) = (fila - 1) * dt             'tiempo inicial en matriz [s]
    
    'valores iniciales desde hoja 1 datos, desde mes hasta Tsol
    matriz(fila, 3) = Hoja1.Cells(3, 7)       'mes
    matriz(fila, 4) = Hoja1.Cells(3, 8)       'temperatura exterior inicial
    
    'temperatura interior del aire
    
    If hay_clima = "No" Then
        matriz(fila, 18) = Tm + a * Sin((fila - 1) * dt / T + fi) 'T° inicial aire
        matriz(fila, 31) = Tm 'HOR
    
    ElseIf hay_clima = "Si" Then
        matriz(fila, 18) = (Tmax2 + Tmin2) / 2 'T° inicial aire
        matriz(fila, 31) = (Tmax2 + Tmin2) / 2 'HOR
    End If
    
    fila = fila + 1
Loop
'-------------------------------------------------------------------------------------------------------------
'CÁLCULO DE DEMANDA POR MES
'-------------------------------------------------------------------------------------------------------------
'se llenan todas las demas celdas (N dias de periodo de convergencia)
'defino celda de comienzo (primera fila dia 2, dia uno del periodo de convergencia)
'fila = iter_base + 1
   
Do While fila <= iter + iter_base + 1

    For i = 1 To 24
        SA_H(i) = Hoja5.Cells(i + 3, 4)
        SA_N(i) = Hoja5.Cells(i + 3, 5)
        SA_NE(i) = Hoja5.Cells(i + 3, 6)
        SA_E(i) = Hoja5.Cells(i + 3, 7)
        SA_SE(i) = Hoja5.Cells(i + 3, 8)
        SA_S(i) = Hoja5.Cells(i + 3, 9)
        SA_SO(i) = Hoja5.Cells(i + 3, 10)
        SA_O(i) = Hoja5.Cells(i + 3, 11)
        SA_NO(i) = Hoja5.Cells(i + 3, 12)
        SA_PV(i) = Hoja5.Cells(i + 3, 13)
        SA_PT(i) = Hoja5.Cells(i + 3, 14)
    Next

    'tiempo
    matriz(fila, 2) = matriz(fila - 1, 2) + dt
    matriz(fila, 1) = matriz(fila, 2) / 3600
    
    'temperatura exterior, radiacion solar y renovaciones de aire
    Hoja1.Range("C2") = matriz(fila, 2)                     'tiempo en hoja 1
    matriz(fila, 3) = Hoja1.Cells(3, 7)                     'mes
    matriz(fila, 4) = Hoja1.Cells(3, 8)                     'temperatura exterior
    
    matriz(fila, 5) = Hoja1.Cells(3, 9) + Hoja1.Cells(3, 10) 'radiacion total
    qsol = matriz(fila - 1, 5)                               'calor por ventanas total [W]
    
    'valores iniciales calor generado interior
    'generacion total de calor interior personas
    matriz(fila, 6) = Hoja1.Cells(3, 11) * Aplanta
    
    'generacion total de calor interior iluminacion
    matriz(fila, 7) = Hoja1.Cells(3, 12) * Aplanta
    
    'generacion total de calor interior equipos
    matriz(fila, 8) = Hoja1.Cells(3, 13) * Aplanta
    
    qint = matriz(fila - 1, 6) + matriz(fila - 1, 7) + matriz(fila - 1, 8)
    
    'Temperaturas sol-air
    matriz(fila, 9) = Hoja1.Cells(3, 14)                       'TsolH
    matriz(fila, 10) = Hoja1.Cells(3, 15)                      'TsolN
    matriz(fila, 11) = Hoja1.Cells(3, 16)                      'TsolNE
    matriz(fila, 12) = Hoja1.Cells(3, 17)                      'TsolE
    matriz(fila, 13) = Hoja1.Cells(3, 18)                      'TsolSE
    matriz(fila, 14) = Hoja1.Cells(3, 19)                      'TsolS
    matriz(fila, 15) = Hoja1.Cells(3, 20)                      'TsolSO
    matriz(fila, 16) = Hoja1.Cells(3, 21)                      'TsolO
    matriz(fila, 17) = Hoja1.Cells(3, 22)                      'TsolNO

    'calores transferidos por la envolvente
    'variables iniciales
    'techo
    q_techo_SA = 0
    'norte
    q_norte_SA = 0
    'noreste
    q_noreste_SA = 0
    'este
    q_este_SA = 0
    'sureste
    q_sureste_SA = 0
    'sur
    q_sur_SA = 0
    'suroeste
    q_suroeste_SA = 0
    'oeste
    q_oeste_SA = 0
    'noroeste
    q_noroeste_SA = 0
    'piso ventilado
    q_piso_v_SA = 0
    'piso contra terreno
    q_piso_t_SA = 0
        
    
    'suma_DT=suma(Tsol((fila-1)-3600/dt*(i-1))-Tinterior((fila-1)-3600/dt*(i-1))*Sa(i)
        
    For i = 1 To 24 'no hay SA's con peaks mas alla de 6 hrs
        'Techo
        q_techo_SA = q_techo_SA + Utecho * Atecho * (matriz((fila - 1) - 3600 / dt * (i - 1), 9) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_H(i) / 100 'U*A*DT*CTS
                
        'Norte
        q_norte_SA = q_norte_SA + (Unorte * Anorte + FiLnorte) * (matriz((fila - 1) - 3600 / dt * (i - 1), 10) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_N(i) / 100
                            
        'Noreste
        q_noreste_SA = q_noreste_SA + (Unoreste * Anoreste + FiLnoreste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 11) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_NE(i) / 100
                
        'Este
        q_este_SA = q_este_SA + (Ueste * Aeste + FiLeste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 12) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_E(i) / 100
                
        'Sureste
        q_sureste_SA = q_sureste_SA + (Usureste * Asureste + FiLsureste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 13) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_SE(i) / 100
                
        'Sur
        q_sur_SA = q_sur_SA + (Usur * Asur + FiLsur) * (matriz((fila - 1) - 3600 / dt * (i - 1), 14) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_S(i) / 100
                
        'Suroeste
        q_suroeste_SA = q_suroeste_SA + (Usuroeste * Asuroeste + FiLsuroeste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 15) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_SO(i) / 100
                
        'Oeste
        q_oeste_SA = q_oeste_SA + (Uoeste * Aoeste + FiLoeste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 16) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_O(i) / 100
                
        'Noroeste
        q_noroeste_SA = q_noroeste_SA + (Unoroeste * Anoroeste + FiLnoroeste) * (matriz((fila - 1) - 3600 / dt * (i - 1), 17) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_NO(i) / 100
                
        'calor por pisos
        'Piso ventilado
        q_piso_v_SA = q_piso_v_SA + Upiso_v * Apiso_v * (matriz((fila - 1) - 3600 / dt * (i - 1), 4) - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_PV(i) / 100
                
        'Piso contra terreno
        Tsuelo = Hoja1.Range("AD3")
        q_piso_t_SA = q_piso_t_SA + FiLpiso_t * (Tsuelo - matriz((fila - 1) - 3600 / dt * (i - 1), 18)) * SA_PT(i) / 100
                
    Next
        
    ' componentes sin desface (ventanas)
    'Techo
    Q_techo_v = Utecho_v * Atecho_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))            'calor por lucarna inicial [W] a temperatura variable exterior
    matriz(fila, 36) = Q_techo_v
    matriz(fila, 37) = q_techo_SA
    
    'Norte
    q_norte_v = Unorte_v * Anorte_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))            'calor por ventana norte [W] a temperatura variable exterior
    matriz(fila, 38) = q_norte_v
    matriz(fila, 39) = q_norte_SA
    
    'noreste
    q_noreste_v = Unoreste_v * Anoreste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))      'calor por ventana noreste inicial [W] a temperatura variable exterior
    matriz(fila, 40) = q_noreste_v
    matriz(fila, 41) = q_noreste_SA
    
    'este
    q_este_v = Ueste_v * Aeste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))               'calor por ventana este inicial [W] a temperatura variable exterior
    matriz(fila, 42) = q_este_v
    matriz(fila, 43) = q_este_SA
    
    'sureste
    q_sureste_v = Usureste_v * Asureste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))      'calor por ventana sur este inicial [W] a temperatura variable exterior
    matriz(fila, 44) = q_sureste_v
    matriz(fila, 45) = q_sureste_SA
    
    'sur
    q_sur_v = Usur_v * Asur_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))                  'calor por ventana sur inicial [W] a temperatura variable exterior
    matriz(fila, 46) = q_sur_v
    matriz(fila, 47) = q_sur_SA
    
    'suroeste
    q_suroeste_v = Usuroeste_v * Asuroeste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))   'calor por ventana sur oeste inicial [W] a temperatura variable exterior
    matriz(fila, 48) = q_suroeste_v
    matriz(fila, 49) = q_suroeste_SA
    
    'oeste
    q_oeste_v = Uoeste_v * Aoeste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))            'calor por ventana oeste inicial [W] a temperatura variable exterior
    matriz(fila, 50) = q_oeste_v
    matriz(fila, 51) = q_oeste_SA
    
    'noroeste
    q_noroeste_v = Unoroeste_v * Anoroeste_v * (matriz(fila - 1, 4) - matriz(fila - 1, 18))   'calor por ventana nor oeste inicial [W] a temperatura variable exterior
    matriz(fila, 52) = q_noroeste_v
    matriz(fila, 53) = q_noroeste_SA
    
    'piso ventilado
    matriz(fila, 54) = q_piso_v_SA
    
    'piso contra terreno
    matriz(fila, 55) = q_piso_t_SA
            
                  
    'Calor absorbido por la masa interior
    'temperatura y calores de absorción masa interior
    
    qHOR = hint * areaHOR * (matriz(fila - 1, 18) + (qsol * 0.85) / (hint * areaHOR) - matriz(fila - 1, 31)) '+ (qsol * 0.85)
    matriz(fila, 31) = matriz(fila - 1, 31) + (qHOR * dt / capHOR)   'T° HOR
    
    qmasa = hint * areaHOR * (matriz(fila - 1, 31) - matriz(fila - 1, 18)) 'calor que intercambia el aire con la masa
    matriz(fila, 32) = qmasa
    
    'Calores agrupados
    'totales---------------------------
    qventanas = q_norte_v + q_noreste_v + q_este_v + q_sureste_v + q_sur_v + q_suroeste_v + q_oeste_v + q_noroeste_v + Q_techo_v
    qtecho = q_techo_SA + qmasa * m_techo
    qmuros = q_norte_SA + q_noreste_SA + q_este_SA + q_sureste_SA + q_sur_SA + q_suroeste_SA + q_oeste_SA + q_noroeste_SA + qmasa * m_muros
    qpv = q_piso_v_SA + qmasa * m_piso_v
    qpt = q_piso_t_SA + qmasa * m_piso_t
    qpiso = qpv + qpt
    
    qenv = qventanas + qtecho + qmuros + qpiso
    
    qmasa_ad = qmasa * m_masa_ad
    matriz(fila, 32) = qmasa_ad   'se informa el negativo del calor masa porque se presenta resultado en términos del efecto en el aire
    
    matriz(fila, 19) = qenv      'calor total por la envolvente
    'Ventanas
    matriz(fila, 21) = qventanas    'calor por ventanas
    'Techo y lucarna
    matriz(fila, 22) = qtecho     'calor por techo
    matriz(fila, 23) = qmuros     'calor por muros
    'Pisos
    matriz(fila, 20) = qpv
    matriz(fila, 24) = qpt
    
    
    'Infiltraciones
    infilt = Hoja1.Range("AE3")
    qinfilt = Vaire * (infilt / 3600 * dt) * roaire * cpaire * (matriz(fila - 1, 4) - matriz(fila - 1, 18)) / dt
    matriz(fila, 27) = qinfilt
    
    ' ventilacion
    'calor potencial a perder por ventilacion mecanica
    qpotencial = ((Vaire * (n / 3600 * dt) * roaire * cpaire * (matriz(fila - 1, 4) - matriz(fila - 1, 18))) / dt)
    
    qrecup = 0
    
    If recup_cal = "Si" And Hoja5.Range("B26") = "i" Then
        qrecup = -0.5 * ef_recup * qpotencial     'calor a recuperar
    End If
    
    n = Hoja1.Range("X3")                      'se recupera el valor después del cálculo de modo de usar el correspondiente al dt anterior
    qvent = qpotencial
    matriz(fila, 25) = qvent
    matriz(fila, 26) = qrecup
            
    '--------------------
    'condicion inicial, el calor aportado por clima es 0
    qclima = 0
    clima = Hoja1.Range("W3")
    Tmax1 = Hoja1.Range("Z3")           'Las temperaturas de confor se determinan en cada ciclo para poder variarlas segun confort adaptativo mes a mes
    Tmin1 = Hoja1.Range("AA3")
    Tmax2 = Hoja1.Range("AB3")
    Tmin2 = Hoja1.Range("AC3")
        
                
    '---------------------CLIMA 1----------------------
    'calor aportado por clima1
    If clima = 1 And matriz(fila - 1, 18) >= Tmax1 Then
        
        If (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) > 0 Then
            qclima = -(qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmax1 - matriz(fila - 1, 18)) / dt
        ElseIf (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) < 0 Then
            qclima = (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmax1 - matriz(fila - 1, 18)) / dt
        End If
        
        If qclima < -Pmaxclima Then
            qclima = -Pmaxclima
        End If
    End If
    
    If clima = 1 And matriz(fila - 1, 18) <= Tmin1 Then
        If (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) < 0 Then
            qclima = -(qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmin1 - matriz(fila - 1, 18)) / dt
        ElseIf (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) > 0 Then
            qclima = (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmin1 - matriz(fila - 1, 18)) / dt
        End If
        
        If qclima > Pmaxclima Then
            qclima = Pmaxclima
        End If
    
    End If
        
    'calor aportado por clima2
    'Defino cuanto vale y si corresponde calcularlo o se mantiene como 0
    If clima = 2 And matriz(fila - 1, 18) >= Tmax2 Then
        
        If (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) > 0 Then
            qclima = -(qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmax2 - matriz(fila - 1, 18)) / dt
        ElseIf (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) < 0 Then
            qclima = (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmax2 - matriz(fila - 1, 18)) / dt
        End If
        
        If qclima < -Pmaxclima Then
            qclima = -Pmaxclima
        End If
    End If
    
    If clima = 2 And matriz(fila - 1, 18) <= Tmin2 Then
        If (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) < 0 Then
            qclima = -(qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmin2 - matriz(fila - 1, 18)) / dt
        ElseIf (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) > 0 Then
            qclima = (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qrecup + qinfilt) + Vaire * roaire * cpaire * (Tmin2 - matriz(fila - 1, 18)) / dt
        End If
        
        If qclima > Pmaxclima Then
        qclima = Pmaxclima
        End If
    
    End If
    '-------------------------------------------
    'Calor aportado por clima
    matriz(fila, 28) = qclima
    
    'calculo de T° interior
    q = (qsol * 0.15 + qint + qenv + qvent + qmasa_ad + qclima + qrecup + qinfilt) * dt
    matriz(fila, 18) = matriz(fila - 1, 18) + (q) / (Vaire * roaire * cpaire)
    matriz(fila, 35) = q / dt
        
    'integral de demanda
    If qclima >= 0 And Hoja1.Range("C8") <> dt Then
        matriz(fila, 33) = matriz(fila - 1, 33) + qclima * dt / 3600          'Demanda acumulada calefaccion wh
        matriz(fila, 34) = matriz(fila - 1, 34) + 0                           'Demanda acumulada refrigeracion wh
    
    ElseIf qclima < 0 And Hoja1.Range("C8") <> dt Then
        matriz(fila, 33) = matriz(fila - 1, 33) + 0                           'Demanda acumulada calefaccion wh
        matriz(fila, 34) = matriz(fila - 1, 34) + qclima * dt / 3600          'Demanda acumulada refrigeracion wh
        
    ElseIf Hoja1.Range("C8") = dt Then
        matriz(fila, 33) = 0
        matriz(fila, 34) = 0
    End If
       
    fila = fila + 1
Loop

'-------------------------------------------------------------------------------------------------------------
'SALIDA DE DATOS
'-------------------------------------------------------------------------------------------------------------

fila_matriz = 86400 * (dpm) / dt + 1
fila_hoja4 = Hoja4.Cells(1, 1)              'valor inicial

Dim arr(1 To 55) As Double
contador = 1

Do While fila_matriz <= iter + iter_base + 1
    For k = 1 To 25
        For i = 1 To 55
            arr(i) = matriz(fila_matriz, i)
        Next
        Hoja4.Range(Hoja4.Cells(fila_hoja4, 3), Hoja4.Cells(fila_hoja4, 55 + 2)) = arr()
        fila_hoja4 = fila_hoja4 + 1
        fila_matriz = fila_matriz + 3600 / dt
    Next
    
    fila_hoja4 = fila_hoja4 + 1
    fila_matriz = fila_matriz + 86400 / dt * (dpm - 1) - 3600 / dt
Loop

Application.ScreenUpdating = True

End Sub



