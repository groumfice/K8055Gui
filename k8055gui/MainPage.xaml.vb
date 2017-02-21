
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
    Private _DIGITAL_OUTPUT() As Boolean = New Boolean() {False, False, False, False, False, False, False, False, False}
    Private dispatcherTimer As DispatcherTimer
    Private dttest As DispatcherTimer
    Private _TEST_RTN As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        dttest = New DispatcherTimer
        AddHandler dttest.Tick, AddressOf dttest_Tick
        dttest.Interval = New TimeSpan(0, 0, 1)
        dttest.Stop()
    End Sub

    Private Sub btconnect_Click(sender As Object, e As RoutedEventArgs) Handles btconnect.Click
        raspk8055.raspk8055.openK8055(cbAdresse.SelectedIndex)
        dispatcherTimer = New DispatcherTimer
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 0.2)
        dispatcherTimer.Start()
    End Sub

#Region "Send method"
    Public Sub btWriteAllDigital_Click(sender As Object, e As RoutedEventArgs) Handles btWriteAllDigital.Click
        writeAllDigital()
    End Sub

    Private Sub writeAllDigital()
        _DIGITAL_OUTPUT(1) = True
        btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(2) = True
        btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(3) = True
        btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(4) = True
        btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(5) = True
        btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(6) = True
        btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(7) = True
        btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        _DIGITAL_OUTPUT(8) = True
        btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        raspk8055.raspk8055.writeAllDigital()
    End Sub

    Private Sub clearAllDigital()
        _DIGITAL_OUTPUT(1) = False
        btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(2) = False
        btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(3) = False
        btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(4) = False
        btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(5) = False
        btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(6) = False
        btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(7) = False
        btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        _DIGITAL_OUTPUT(8) = False
        btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        raspk8055.raspk8055.clearAllDigital()
    End Sub

    Private Sub clearDigitalChannel(ByVal channel As Integer)
        _DIGITAL_OUTPUT(channel) = False
        raspk8055.raspk8055.clearDigitalChannel(channel)
        Select Case channel
            Case 1
                btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 2
                btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 3
                btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 4
                btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 5
                btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 6
                btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 7
                btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
            Case 8
                btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End Select
    End Sub

    Private Sub writeDigitalChannel(ByVal channel As Integer)
        _DIGITAL_OUTPUT(channel) = True
        raspk8055.raspk8055.writeDigitalChannel(channel)
        Select Case channel
            Case 1
                btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 2
                btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 3
                btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 4
                btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 5
                btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 6
                btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 7
                btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            Case 8
                btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End Select
    End Sub

    Private Sub setAnalogChannelTo(ByVal channel As Integer, ByVal _VALUE As Integer)
        Select Case channel
            Case 1
                btAnalogOut1.Background = New SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255 - _VALUE, 255 - _VALUE))
            Case 2
                btAnalogOut2.Background = New SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255 - _VALUE, 255 - _VALUE))
        End Select
        raspk8055.raspk8055.writeAnalogChannel(channel, _VALUE)
    End Sub

#End Region

    Public Sub btClearAllDigital_Click(sender As Object, e As RoutedEventArgs) Handles btClearAllDigital.Click
        clearAllDigital()
    End Sub

    Public Sub dispatcherTimer_Tick(sender As Object, e As EventArgs)
        tbDebug.Text = "Error:" & raspk8055.raspk8055.getError
        tbDebug.UpdateLayout()

        If raspk8055.raspk8055.isK8055Open Then
            btconnect.Background = New SolidColorBrush(Windows.UI.Colors.Green)
            cbAdresse.IsEnabled = False
            enableAll(True)
        Else
            btconnect.Background = New SolidColorBrush(Windows.UI.Colors.Red)
            cbAdresse.IsEnabled = True
            enableAll(False)
        End If

        If raspk8055.raspk8055.readDigitalChannel(1) = 1 Then
            btInput1.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        Else
            btInput1.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End If

        If raspk8055.raspk8055.readDigitalChannel(2) = 1 Then
            btInput2.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        Else
            btInput2.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End If
        If raspk8055.raspk8055.readDigitalChannel(3) = 1 Then
            btInput3.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        Else
            btInput3.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End If
        If raspk8055.raspk8055.readDigitalChannel(4) = 1 Then
            btInput4.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        Else
            btInput4.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End If
        If raspk8055.raspk8055.readDigitalChannel(5) = 1 Then
            btInput5.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        Else
            btInput5.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        End If

        TBCounter1.Text = raspk8055.raspk8055.readCounter(1)
        TBCounter2.Text = raspk8055.raspk8055.readCounter(2)

        pgb1.Value = raspk8055.raspk8055.readAnalogChannel(1)
        pgb2.Value = raspk8055.raspk8055.readAnalogChannel(2)

        'Debug.WriteLine(raspk8055.raspk8055.getBufArray())
    End Sub

    Private Sub btResetCounter1_Click(sender As Object, e As RoutedEventArgs) Handles btResetCounter1.Click
        raspk8055.raspk8055.resetCounter(1)
    End Sub

    Private Sub btResetCounter2_Click(sender As Object, e As RoutedEventArgs) Handles btResetCounter2.Click
        raspk8055.raspk8055.resetCounter(2)
    End Sub

    Private Sub btOutput1_Click_1(sender As Object, e As RoutedEventArgs) Handles btOutput1.Click
        If _DIGITAL_OUTPUT(1) Then
            clearDigitalChannel(1)
            btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(1)
            btOutput1.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput8_Click(sender As Object, e As RoutedEventArgs) Handles btOutput8.Click
        If _DIGITAL_OUTPUT(8) Then
            clearDigitalChannel(8)
            btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(8)
            btOutput8.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput7_Click(sender As Object, e As RoutedEventArgs) Handles btOutput7.Click
        If _DIGITAL_OUTPUT(7) Then
            clearDigitalChannel(7)
            btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(7)
            btOutput7.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput6_Click(sender As Object, e As RoutedEventArgs) Handles btOutput6.Click
        If _DIGITAL_OUTPUT(6) Then
            clearDigitalChannel(6)
            btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(6)
            btOutput6.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput5_Click(sender As Object, e As RoutedEventArgs) Handles btOutput5.Click
        If _DIGITAL_OUTPUT(5) Then
            clearDigitalChannel(5)
            btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(5)
            btOutput5.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput4_Click(sender As Object, e As RoutedEventArgs) Handles btOutput4.Click
        If _DIGITAL_OUTPUT(4) Then
            clearDigitalChannel(4)
            btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(4)
            btOutput4.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput3_Click(sender As Object, e As RoutedEventArgs) Handles btOutput3.Click
        If _DIGITAL_OUTPUT(3) Then
            clearDigitalChannel(3)
            btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(3)
            btOutput3.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btOutput2_Click(sender As Object, e As RoutedEventArgs) Handles btOutput2.Click
        If _DIGITAL_OUTPUT(2) Then
            clearDigitalChannel(2)
            btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Gray)
        Else
            writeDigitalChannel(2)
            btOutput2.Background = New SolidColorBrush(Windows.UI.Colors.Red)
        End If
    End Sub

    Private Sub btAnalogOut1_Click(sender As Object, e As RoutedEventArgs) Handles btAnalogOut1.Click
        setAnalogChannelTo(1, 0)
        sliderAnalog1.Value = 0

    End Sub

    Private Sub btAnalogOut2_Click(sender As Object, e As RoutedEventArgs) Handles btAnalogOut2.Click
        setAnalogChannelTo(2, 0)
        sliderAnalog2.Value = 0
    End Sub

    Private Sub sliderAnalog1_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderAnalog1.ValueChanged
        setAnalogChannelTo(1, sliderAnalog1.Value)
    End Sub

    Private Sub sliderAnalog2_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderAnalog2.ValueChanged
        setAnalogChannelTo(2, sliderAnalog2.Value)
    End Sub

    Private Sub btQuit_Click(sender As Object, e As RoutedEventArgs) Handles btQuit.Click
        raspk8055.raspk8055.closeDevice()
        Application.Current.Exit()
    End Sub

    Private Sub enableAll(ByVal truefalse As Boolean)
        sliderAnalog1.IsEnabled = truefalse
        sliderAnalog2.IsEnabled = truefalse
        btAnalogOut1.IsEnabled = truefalse
        btAnalogOut2.IsEnabled = truefalse
        btWriteAllDigital.IsEnabled = truefalse
        btClearAllDigital.IsEnabled = truefalse
        btOutput1.IsEnabled = truefalse
        btOutput2.IsEnabled = truefalse
        btOutput3.IsEnabled = truefalse
        btOutput4.IsEnabled = truefalse
        btOutput5.IsEnabled = truefalse
        btOutput6.IsEnabled = truefalse
        btOutput7.IsEnabled = truefalse
        btOutput8.IsEnabled = truefalse
        btResetCounter1.IsEnabled = truefalse
        btResetCounter2.IsEnabled = truefalse
    End Sub
#Region "Test K8055"

    Private _TEST_RTN_STEP As Integer = 0

    Private Sub btTest_Click(sender As Object, e As RoutedEventArgs) Handles btTest.Click
        If _TEST_RTN Then
            dttest.Stop()
            _TEST_RTN = False
        Else
            _TEST_RTN_STEP = 0
            dttest.Start()
            _TEST_RTN = True
        End If
    End Sub

    Public Sub dttest_Tick(sender As Object, e As EventArgs)
        Select Case _TEST_RTN_STEP
            Case 0
                clearAllDigital()
                setAnalogChannelTo(1, 0)
                setAnalogChannelTo(2, 0)
            Case 1 To 8
                writeDigitalChannel(_TEST_RTN_STEP)
            Case 9 To 10
                setAnalogChannelTo(_TEST_RTN_STEP - 8, 255)
            Case Else
                _TEST_RTN_STEP = 0
                Exit Sub
        End Select
        _TEST_RTN_STEP += 1
    End Sub

#End Region
End Class
