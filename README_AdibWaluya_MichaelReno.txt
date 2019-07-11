Adib Ghassani Waluya (567271):

Ort:
In diesem Projekt habe ich 2 DLLs erstellt:
1. DataModel befindet sich in team1\PP-STL Datei Oeffner\DataModel
2. importSTL befindet sich in team1\PP-STL Datei Oeffner\STLDataHandlingTool\importSTL
--> F�r importSTL habe ich eine Testklasse "importSTLTest" als StartProjekt erstellt, die in team1\PP-STL Datei Oeffner\STLDataHandlingTool\importSTL
    gefunden werden kann

Anmerkungen:
- Die Methode f�r ReadASCIIFile und ReadBinaryFile k�nnen bisher noch nicht gleichzeitig implementiert werden. Das hei�t, wenn bspw. ReadASCIIFile
  Methode getestet wird, dann muss die ReadBinaryFile Methode als Kommentare eingestellt werden.

- Bei der TestKlasse importSTLTest sieht es auch �hnlich aus. Wenn bspw. eine ASCII Datei importiert wird, dann muss die Methode "ReadASCIIFile"
  in der If-Schleife implementiert werden (read.ReadASCIIFile(openFile.FileName)).




Michael Reno (565907):

Ort:

Die DLL, die ich entwickelt habe, befindet sich in team1\PP-STL Datei Oeffner\StlExport namens DataWriter.dll.
Die GUI befindet sich in team1\PP-STL Datei Oeffner\STLDataHandlingTool\View namens MainWindow.xaml und MainWindow.xaml.cs

Startprojekt:

Da ich die dll und die GUI entwickelt habe, habe ich mich fuer die GUI als das Startprojekt der beiden Teilen entschieden.

NuGet Package:

Xceed.Wpf.Toolkit, davon wird ColorPicker benutzt.

Anmerkungen:

- Leider sind ein paar Buttons noch nicht implementiert. Ich entschuldige mich.
- Als dummy fuer STLImport wird das Open Button nur ein OpenFileDialog aufrufen. Das Dialog konnte aber keine Datei oeffnen
  und erstellt nur mein Test Datenmodell.
