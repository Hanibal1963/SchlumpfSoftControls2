' *************************************************************************************************
' SizeMode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Legt fest, wie eine Grafik innerhalb der verfügbaren Client-Fläche eines
    ''' Controls angezeigt wird.
    ''' </summary>
    ''' <remarks>
    ''' Die Anzeigemodi bestimmen Ausrichtung und Skalierung der Grafik in Bezug auf die
    ''' Größe des Controls. <list type="bullet">
    '''  <item>
    '''   <description> <see cref="SizeMode.Normal"/><b>:</b> Keine Skalierung;
    ''' Ausrichtung oben links. </description>
    '''  </item>
    '''  <item>
    '''   <description><see cref="SizeMode.CenterImage"/><b>:</b> Keine Skalierung;
    ''' zentriert. </description>
    '''  </item>
    '''  <item>
    '''   <description><see cref="SizeMode.Zoom"/><b>:</b> Einheitliche Skalierung,
    ''' sodass die Grafik vollständig in den verfügbaren Bereich passt
    ''' (letterboxing/pillarboxing möglich). </description>
    '''  </item>
    '''  <item>
    '''   <description><see cref="SizeMode.Fill"/><b>:</b> Skalierung, sodass der
    ''' verfügbare Bereich vollständig gefüllt wird (Zuschnitt oder Verzerrung abhängig
    ''' von der Implementierung möglich). </description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <code language="vb"><![CDATA[
    ''' ' Beispiel: Den Anzeigemodus auf Zoom setzen
    ''' Me.MyGifControl.SizeMode = AniGifControl.SizeMode.Zoom
    '''  
    ''' ' Beispiel: Aus Enum zuweisen
    ''' Dim mode As AniGifControl.SizeMode = AniGifControl.SizeMode.CenterImage]]></code>
    ''' </example>
    ''' <seealso cref="System.Drawing.Image"/>
    ''' <seealso cref="System.Windows.Forms.Control"/>
    Public Enum SizeMode

        ''' <summary>
        ''' Die Grafik wird in Originalgröße angezeigt; Ausrichtung erfolgt oben links.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Es findet keine Skalierung statt. </description>
        '''  </item>
        '''  <item>
        '''   <description>Ist die Grafik größer als das Control, werden nicht sichtbare
        ''' Bereiche an den Rändern abgeschnitten. </description>
        '''  </item>
        '''  <item>
        '''   <description>Ist die Grafik kleiner, bleibt der restliche Bereich des Controls
        ''' leer.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        Normal = 0

        ''' <summary>
        ''' Die Grafik wird in Originalgröße zentriert angezeigt.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Es findet keine Skalierung statt. </description>
        '''  </item>
        '''  <item>
        '''   <description>Ist die Grafik größer als das Control, werden an den Rändern
        ''' nicht anzeigbare Teile abgeschnitten. </description>
        '''  </item>
        '''  <item>
        '''   <description>Ist die Grafik kleiner, entstehen umlaufende Ränder (leere
        ''' Flächen).</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        CenterImage = 1

        ''' <summary>
        ''' Die Größe der Grafik wird einheitlich skaliert, sodass sie in den verfügbaren
        ''' Bereich des Controls passt; zentrierte Ausrichtung (1–100%).
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Das Seitenverhältnis bleibt erhalten (keine Verzerrung).
        ''' </description>
        '''  </item>
        '''  <item>
        '''   <description>Die Grafik passt immer vollständig in das Control; ggf. mit
        ''' Rändern oben/unten oder links/rechts. </description>
        '''  </item>
        '''  <item>
        '''   <description>Eine Vergrößerung über 100% kann je nach Implementierung
        ''' unterbunden werden.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        Zoom = 2

        ''' <summary>
        ''' Die Grafik wird so skaliert, dass der verfügbare Bereich vollständig gefüllt
        ''' wird; zentrierte Ausrichtung.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Kleinere Grafiken werden vergrößert, größere verkleinert.
        ''' </description>
        '''  </item>
        '''  <item>
        '''   <description>Abhängig von der Implementierung kann entweder das
        ''' Seitenverhältnis beibehalten werden (dann erfolgt Zuschnitt an
        ''' gegenüberliegenden Kanten), oder die Grafik wird an das Seitenverhältnis des
        ''' Controls angepasst (mögliche Verzerrung).</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        Fill = 3

    End Enum

End Namespace
