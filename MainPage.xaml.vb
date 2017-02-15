
Imports Windows.Devices.Enumeration
Imports Windows.Devices.HumanInterfaceDevice

Imports System.Runtime.InteropServices
Imports Windows.Storage
Imports Windows.Storage.Streams
Imports System.Threading
Imports System.Threading.Tasks
Imports raspk8055

Public NotInheritable Class MainPage
    Inherits Page
    'Private Shared Function Add(ByVal value1 As Double, ByVal value2 As Double) As Double
    '<DllImport("raspk8055.dll", SetLastError:=True, CharSet:=CharSet.Ansi)>
    'Public Shared Function openDevice(ByVal cardAddr As Integer)
    'End Function
    '<DllImport("raspk8055.dll", SetLastError:=True, CharSet:=CharSet.Ansi)>
    'Public Shared Function writeAllDigital()
    'End Function
    '<DllImport("raspk8055.dll", SetLastError:=True, CharSet:=CharSet.Ansi)>
    'Public Shared Function readDigitalChannel(ByVal channel As Integer) As Integer
    'End Function
    'Private Declare Function Add Lib "raspk8055.dll" (ByVal value1 As Double, ByVal value2 As Double) As Double
    'Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    'Dim k8055 As HidDevice
    'Dim K8055Detected As Boolean
    'Dim vendorId As UInt32 = &H10CF
    'Dim productId As UInt32 = &H5500
    'Dim usagePage As UInt32 = &HFF00
    'Dim usageId As UInt32 = &H1
    'Dim WithEvents device As HidDevice
    'Public CardAddr As Integer = 0
    'Private Shared result As Double
    'Public Shared digitalIn() As Integer = New Integer() {0, 0, 0, 0, 0, 0}
    'Public digitalOut() As Integer = New Integer() {0, 1, 2, 4, 8, 16, 32, 64, 128}
    Private dispatcherTimer As DispatcherTimer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'TestMethod1()
        'raspk8055.raspk8055.openDevice(0)

    End Sub

    'Public Async Function EnumerateHidDevices() As Task

    '    Debug.WriteLine("Configuration de la carte")
    '    Dim Selector As String = HidDevice.GetDeviceSelector(usagePage, usageId, vendorId, productId + CardAddr)
    '    Debug.WriteLine("Début de l'énumération des interfaces HID...")
    '    'Dim devices = Await DeviceInformation.FindAllAsync(Selector)
    '    Dim devices = Await DeviceInformation.FindAllAsync(Selector)

    '    'Dim devices As Windows.Foundation.IAsyncOperation(Of Windows.Devices.Enumeration.DeviceInformationCollection) = DeviceInformation.FindAllAsync(Selector)
    '    Debug.WriteLine("Enumération des interfaces HID terminée...")
    '    'If (devices.GetAwaiter.GetResult.Count > 0) Then
    '    If (devices.Count > 0) Then
    '        Debug.WriteLine("Trouvé " + devices.Count.ToString + " interfaces HID.")
    '        'Debug.WriteLine(Selector.ToString)
    '        Try
    '            ' Open the target HID device
    '            device = Await HidDevice.FromIdAsync(devices.ElementAt(0).Id, FileAccessMode.ReadWrite)

    '            'Dim device As HidDevice = Await HidDevice.FromIdAsync(devices.ElementAt(0).Id, FileAccessMode.ReadWrite)
    '            'Dim device As HidDevice = HidDevice.FromIdAsync(devices2.GetResults.ElementAt(0).Id, FileAccessMode.ReadWrite)
    '            Debug.WriteLine("Sending to Vendor class")
    '            'Await Vendor.ReadToHidDevice(device)
    '            'Await Vendor.ReadWriteToHidDevice(device)
    '            AddHandler device.InputReportReceived, AddressOf inputrec

    '        Catch nrex As NullReferenceException
    '            Debug.WriteLine(nrex.ToString)
    '        Catch ex As Exception
    '            Debug.WriteLine(ex.ToString)
    '        End Try
    '    Else
    '        ' There were no HID devices that met the selector criteria
    '        Debug.WriteLine("K8055 device not found!")
    '    End If
    'End Function

    '   Cmd 1, Set debounce Counter 1
    '+---+---+---+---+---+---+---+---+
    '|CMD|   |   |   |   |   |Dbv|   |
    '+---+---+---+---+---+---+---+---+
    'Cmd 2, Set debounce Counter 2
    '+---+---+---+---+---+---+---+---+
    '|CMD|   |   |   |   |   |   |Dbv|
    '+---+---+---+---+---+---+---+---+
    'Cmd 3, Reset counter 1
    '+---+---+---+---+---+---+---+---+
    '| 3 |   |   |   | 00|   |   |   |
    '+---+---+---+---+---+---+---+---+
    'Cmd 4, Reset counter 2
    '+---+---+---+---+---+---+---+---+
    '| 4 |   |   |   |   | 00|   |   |
    '+---+---+---+---+---+---+---+---+
    'cmd 5, Set analog/digital
    '+---+---+---+---+---+---+---+---+
    '| 5 |DIG|An1|An2|   |   |   |   |
    '+---+---+---+---+---+---+---+---+

    'Private Async Function inputrec(sender As HidDevice, args As HidInputReportReceivedEventArgs) As Task
    '    Try


    '        Dim buf As IBuffer = args.Report.Data
    '        Dim bufarray = buf.ToArray
    '        'Debug.WriteLine("Array:" + bufarray(0).ToString + "," + bufarray(1).ToString + "," + bufarray(2).ToString + "," + bufarray(3).ToString + "," + bufarray(4).ToString + "," + bufarray(5).ToString + "," + bufarray(6).ToString + "," + bufarray(7).ToString)
    '        'If bufarray(1) > 0 Then
    '        'Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
    '        '                     Sub()
    '        'Debug.WriteLine("Array:" + bufarray(0).ToString + "," + bufarray(1).ToString + "," + bufarray(2).ToString + "," + bufarray(3).ToString + "," + bufarray(4).ToString + "," + bufarray(5).ToString + "," + bufarray(6).ToString + "," + bufarray(7).ToString + "," + bufarray(8).ToString)
    '        '    Debug.WriteLine(Convert.ToString(bufarray(1), 2).PadLeft(8, "0"))
    '        Dim _DIGITAL_DATA As String = Convert.ToString(bufarray(1), 2).PadLeft(8, "0")
    '        digitalIn(0) = 0
    '        digitalIn(1) = Integer.Parse(_DIGITAL_DATA.Substring(3, 1))
    '        digitalIn(2) = Integer.Parse(_DIGITAL_DATA.Substring(2, 1))
    '        digitalIn(3) = Integer.Parse(_DIGITAL_DATA.Substring(7, 1))
    '        digitalIn(4) = Integer.Parse(_DIGITAL_DATA.Substring(1, 1))
    '        digitalIn(5) = Integer.Parse(_DIGITAL_DATA.Substring(0, 1))
    '        '                     End Sub)
    '        'End If
    '    Catch ex As Exception
    '        Debug.WriteLine(ex.ToString)
    '    End Try

    'End Function

    'Private Function ToBinary(dec As Integer) As String
    '    Dim bin As Integer
    '    Dim output As String
    '    While dec <> 0
    '        If dec Mod 2 = 0 Then
    '            bin = 0
    '        Else
    '            bin = 1
    '        End If
    '        dec = dec \ 2
    '        output = Convert.ToString(bin) & output
    '    End While
    '    If output Is Nothing Then
    '        Return "0"
    '    Else
    '        Return output
    '    End If
    'End Function
    'Private Shared Function ToBinary(num As ULong) As String

    '    Dim bin = New System.Text.StringBuilder()

    '    Dim modnum As Decimal

    '    Do
    '        modnum = num Mod 2
    '        bin.Insert(0, (modnum))
    '        num = CType((num - modnum) / 2, ULong)

    '    Loop While num <> 0

    '    Return bin.ToString()

    'End Function

    'Class Vendor

    '    'Public Shared Async Sub ReadWriteToHidDevice(device As HidDevice)
    '    '    Dim outReport As HidOutputReport
    '    '    If device IsNot Nothing Then
    '    '        Debug.WriteLine("1")
    '    '        Try
    '    '            ' construct a HID output report to send to the device
    '    '            outReport = device.CreateOutputReport()
    '    '        Catch ex As Exception
    '    '            Debug.WriteLine(ex.ToString)
    '    '        End Try

    '    '        Debug.WriteLine("1b")
    '    '        ' Initialize the data buffer And fill it in
    '    '        'Byte[] buffer = New Byte[] { 10, 20, 30, 40 };

    '    '        Dim buffer As Byte() = New Byte() {10, 20, 30, 40, 0, 0, 0, 0, 0}
    '    '        Dim dataWriter As DataWriter = New DataWriter()

    '    '        Try
    '    '            dataWriter.WriteBytes(buffer)
    '    '            outReport.Data = dataWriter.DetachBuffer()
    '    '        Catch ex As Exception
    '    '            Debug.WriteLine(ex.ToString)
    '    '        End Try

    '    '        Debug.WriteLine("2")
    '    '        ' Send the output report asynchronously
    '    '        Dim a As UInteger = Await device.SendOutputReportAsync(outReport)
    '    '        Debug.WriteLine("3")
    '    '        '
    '    '        ' Sent output report successfully 
    '    '        ' Now lets try read an input report 
    '    '        '
    '    '        Dim inReport As HidInputReport
    '    '        Try
    '    '            inReport = Await device.GetInputReportAsync(a)
    '    '        Catch ex As Exception
    '    '            Debug.WriteLine(ex.ToString)
    '    '        End Try
    '    '        Debug.WriteLine("4")
    '    '        If inReport IsNot Nothing Then

    '    '            Dim id As UInt16 = inReport.Id
    '    '            Dim bytes As Byte() = New Byte(9) {}
    '    '            Dim DataReader As DataReader = DataReader.FromBuffer(inReport.Data)
    '    '            DataReader.ReadBytes(bytes)

    '    '        Else

    '    '            Debug.WriteLine("Invalid input report received")
    '    '        End If

    '    '    Else

    '    '        Debug.WriteLine("device is NULL")
    '    '    End If
    '    'End Sub

    '    'Public Shared Async Function ReadToHidDevice(device As HidDevice) As Task
    '    '    Debug.WriteLine("Tentative de création de rapport.")
    '    '    If device IsNot Nothing Then
    '    '        Try
    '    '            'Dim r As HidFeatureReport = Await device.GetFeatureReportAsync()
    '    '            Dim inReport = Await device.GetFeatureReportAsync(1)

    '    '            'Dim inReport As Windows.Foundation.IAsyncOperation(Of HidInputReport) = Await device.GetInputReportAsync()
    '    '            Debug.WriteLine("Réception du rapport de réponse terminée.")
    '    '            If inReport IsNot Nothing Then
    '    '                Debug.WriteLine("Le format du rapport est cohérent.")
    '    '                Dim id As UInt16 = inReport.Id
    '    '                Debug.WriteLine("Id du rapport : " + inReport.Id.ToString)
    '    '                Dim bytes As Byte() = New Byte(9) {}
    '    '                'Dim ibuffer As IBuffer = inReport.Data
    '    '                Debug.WriteLine("Configuration du buffer de réception.")

    '    '                Dim dataReader As DataReader = DataReader.FromBuffer(inReport.Data)
    '    '                'Dim dataReader As DataReader = dataReader.ReadBytes(inReport.Data)

    '    '                'Dim dataReader As DataReader = DataReader.FromBuffer(inReport.GetResults.Data)
    '    '                'Dim dataReader As DataReader = DataReader.FromBuffer(ibuffer)
    '    '                Debug.WriteLine("Lecture du rapport.")
    '    '                'dataReader.ReadBytes(inReport)
    '    '            Else
    '    '                Debug.WriteLine("Invalid input report received")
    '    '            End If
    '    '        Catch ex As Exception
    '    '            Debug.WriteLine(ex.ToString)
    '    '        End Try
    '    '    End If
    '    'End Function

    '    'Public Shared Async Function ReadWriteToHidDevice(device As HidDevice) As Task
    '    '    Debug.WriteLine("Tentative de création de rapport.")

    '    '    If device IsNot Nothing Then
    '    '        Debug.WriteLine("Création du Rapport de sortie.")
    '    '        ' construct a HID output report to send to the device
    '    '        Dim outReport As HidOutputReport = device.CreateOutputReport()

    '    '        Debug.WriteLine("Rapport de sortie terminé.")
    '    '        ' Initialize the data buffer and fill it in
    '    '        'Dim buffer As Byte() = New Byte() {&H0, &H80, &H1, &H1, &H1, &H1, &H1, &H1, &H1}
    '    '        Dim buffer As Byte() = New Byte() {0, 5, 128, 0, 0, 0, 0, 0, 0}
    '    '        'Dim buffer() As Byte = New Byte() {0, 1, 2}
    '    '        'Dim ibuffer As IBuffer = New Byte() {}

    '    '        'Dim buffer As Byte = 255
    '    '        'buffer(0) = &H0
    '    '        'buffer(1) = &H80
    '    '        'buffer(2) = &H1

    '    '        Dim dataWriter As New DataWriter()
    '    '        Debug.WriteLine("Ecriture dans le buffer.")
    '    '        dataWriter.WriteBytes(buffer)
    '    '        Debug.WriteLine("Ecriture dans le rapport.")

    '    '        Try
    '    '            outReport.Data = dataWriter.DetachBuffer()
    '    '        Catch ex As Exception
    '    '            Debug.WriteLine(ex.ToString)
    '    '        End Try
    '    '        'outReport.Data = dataWriter.DetachBuffer()
    '    '        ' Send the output report asynchronously
    '    '        Debug.WriteLine("Envoi du rapport.")
    '    '        'Dim a As Windows.Foundation.IAsyncOperation(Of UInteger) = device.SendOutputReportAsync(outReport)
    '    '        Dim resultOfSOR As UInteger = Await device.SendOutputReportAsync(outReport)
    '    '        'Dim aze As HidInputReportReceivedEventArgs = device.InputReportReceived
    '    '        'Await device.SendOutputReportAsync(outReport)
    '    '        'Dim hidreportdescription As HidReportType = Await device.GetNumericControlDescriptions
    '    '        ' Sent output report successfully 
    '    '        'Debug.WriteLine("Succès de l'envoi : " + a.Status.ToString)
    '    '        ''While a.Status = AsyncStatus.Started
    '    '        'Debug.WriteLine("Result of a.Id : " + a.Id.ToString)
    '    '        'Debug.WriteLine("Result of getResult() : " + a.GetResults.ToString)


    '    '        Debug.WriteLine("Rapport envoyé.")
    '    '        ' Now lets try read an input report 
    '    '        'Dim inReport As Windows.Foundation.IAsyncOperation(Of HidInputReport) = device.GetInputReportAsync(a.Id)
    '    '        'Try
    '    '        '    'Dim r As HidFeatureReport = Await device.GetFeatureReportAsync()
    '    '        '    'Dim inReport As HidInputReport = Await device.GetInputReportAsync()

    '    '        '    'Dim inReport As Windows.Foundation.IAsyncOperation(Of HidInputReport) = device.GetInputReportAsync().GetAwaiter.GetResult.Data
    '    '        '    Dim inReport As HidInputReport = Await device.GetInputReportAsync()
    '    '        '    Debug.WriteLine("Réception du rapport de réponse terminée.")
    '    '        '    If inReport IsNot Nothing Then
    '    '        '        Debug.WriteLine("Le format du rapport est cohérent.")
    '    '        '        'Dim id As UInt16 = inReport.Id
    '    '        '        Debug.WriteLine("Id du rapport : " + inReport.Id.ToString)
    '    '        '        Debug.WriteLine("Configuration du buffer de réception.")
    '    '        '        Dim buf As IBuffer = inReport.Data
    '    '        '        Debug.WriteLine("Lecture du rapport.")
    '    '        '        Dim bufarray = buf.ToArray
    '    '        '        'If bufarray(0) = InputReport.Buttons Then
    '    '        '        '    'Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
    '    '        '        '    '              Sub()
    '    '        '        '    '                  'txtButton.Text = ParseButtons(bufarray)
    '    '        '        '    '              End Sub)
    '    '        '        'End If




    '    '        '        'Dim bytes As Byte() = New Byte(9) {}
    '    '        '        'Dim ibuffer As IBuffer = inReport.Data
    '    '        '        'Debug.WriteLine("Configuration du buffer de réception.")

    '    '        '        'Dim dataReader = Windows.Storage.Streams.DataReader.FromBuffer(inReport.Data)
    '    '        '        'Dim dataReader As DataReader = dataReader.ReadBytes(inReport)

    '    '        '        'Dim dataReader As DataReader = DataReader.FromBuffer(inReport.GetResults.Data)
    '    '        '        'Dim dataReader As DataReader = DataReader.FromBuffer(ibuffer)
    '    '        '        'Debug.WriteLine("Lecture du rapport.")
    '    '        '        'dataReader.ReadBytes(bytes)
    '    '        '    Else
    '    '        '        Debug.WriteLine("Invalid input report received")
    '    '        '    End If
    '    '        'Catch ex As Exception
    '    '        '    Debug.WriteLine(ex.ToString)
    '    '        'End Try

    '    '    Else
    '    '        Debug.WriteLine("device is NULL")
    '    '    End If
    '    'End Function

    '    Public Shared Async Function ClearAll(device As HidDevice) As Task
    '        Debug.WriteLine("Tentative de création de rapport.")

    '        If device IsNot Nothing Then
    '            Debug.WriteLine("Création du Rapport de sortie.")
    '            ' construct a HID output report to send to the device
    '            Dim outReport As HidOutputReport = device.CreateOutputReport()

    '            Debug.WriteLine("Rapport de sortie terminé.")
    '            ' Initialize the data buffer and fill it in
    '            Dim buffer As Byte() = New Byte() {0, 5, 0, 0, 0, 0, 0, 0, 0}
    '            Dim dataWriter As New DataWriter()
    '            Debug.WriteLine("Ecriture dans le buffer.")
    '            dataWriter.WriteBytes(buffer)
    '            Debug.WriteLine("Ecriture dans le rapport.")

    '            Try
    '                outReport.Data = dataWriter.DetachBuffer()
    '            Catch ex As Exception
    '                Debug.WriteLine(ex.ToString)
    '            End Try
    '            Debug.WriteLine("Envoi du rapport.")
    '            Dim resultOfSOR As UInteger = Await device.SendOutputReportAsync(outReport)
    '            Debug.WriteLine("Rapport envoyé.")

    '        Else
    '            Debug.WriteLine("device is NULL")
    '        End If
    '    End Function
    'End Class

    Public Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        raspk8055.raspk8055.writeAllDigital()
    End Sub

    Public Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Debug.WriteLine(raspk8055.raspk8055.readDigitalChannel(1).ToString)
    End Sub

    Private Sub btconnect_Click(sender As Object, e As RoutedEventArgs) Handles btconnect.Click
        raspk8055.raspk8055.openDevice(0)
        dispatcherTimer = New DispatcherTimer
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 0.5)
        dispatcherTimer.Start()
    End Sub

    Public Sub dispatcherTimer_Tick(sender As Object, e As EventArgs)
        If raspk8055.raspk8055.readDigitalChannel(1) = 1 Then
            cbDigitalChannelIn1.IsChecked = True
        Else
            cbDigitalChannelIn1.IsChecked = False
        End If

        If raspk8055.raspk8055.readDigitalChannel(2) = 1 Then
            cbDigitalChannelIn2.IsChecked = True
        Else
            cbDigitalChannelIn2.IsChecked = False
        End If
        If raspk8055.raspk8055.readDigitalChannel(3) = 1 Then
            cbDigitalChannelIn3.IsChecked = True
        Else
            cbDigitalChannelIn3.IsChecked = False
        End If
        If raspk8055.raspk8055.readDigitalChannel(4) = 1 Then
            cbDigitalChannelIn4.IsChecked = True
        Else
            cbDigitalChannelIn4.IsChecked = False
        End If
        If raspk8055.raspk8055.readDigitalChannel(5) = 1 Then
            cbDigitalChannelIn5.IsChecked = True
        Else
            cbDigitalChannelIn5.IsChecked = False
        End If
    End Sub
End Class
