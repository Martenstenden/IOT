using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using RolgordijnApp.Alarm;
using RolgordijnApp.Message;

[assembly: ExportFont("PatrickHand-Regular.ttf", Alias = "Patrick")]
namespace RolgordijnApp
{
    public partial class MainPage : TabbedPage
    {
        INotificationManager notificationManager;

        public string NextAlarmText { get; private set; }
        public string ConnectionStatusText { get; private set; }

        public string LdrStatusText { get; private set; }
        public string Gordijnstatus { get; private set; }

        public string ReceivedData;

        public MainPage()
        {
            try
            {
                InitializeComponent();

                notificationManager = DependencyService.Get<INotificationManager>();
                notificationManager.NotificationReceived += (sender, eventArgs) =>
                {
                    var evtData = (NotificationEventArgs)eventArgs;
                };

                ConnectionStatusText = "Connection Status: Closed";
                OnPropertyChanged(nameof(ConnectionStatusText));

                BindingContext = this;
               
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Graag opnieuw opstarten");
            }
            
        }

        #region Alarm

        private void SetBtnClicked(object sender, EventArgs e)
        {
            try
            {
                var TimeSpan = datePicker.Time;

                DateTime dateTime = DateTime.Now;

                DateTime today = DateTime.Today;
                DateTime alarmDate = new DateTime(today.Year, today.Month, today.Day, TimeSpan.Hours, TimeSpan.Minutes, TimeSpan.Milliseconds);

                if (DateTime.Now > alarmDate)
                {
                    alarmDate = alarmDate.AddDays(1);
                }

                var alarmDiff = alarmDate - dateTime;
                NextAlarmText = $"{alarmDiff.Hours}h {alarmDiff.Minutes}m until next alarm"; ;
                OnPropertyChanged(nameof(NextAlarmText));


                string title = $"{TimeSpan}";
                string message = $"Goeiemorgen {TimeSpan}";
                notificationManager.SendNotification(title, message, DateTime.Now.Add(alarmDiff));

                DependencyService.Get<IMessage>().LongAlert("Alarm gezet");
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Er is iets misgegaan");
            }
        }

        private void CancelBtnClicked(object sender, EventArgs e)
        {
            try
            {
                NextAlarmText = "";
                OnPropertyChanged(nameof(NextAlarmText));
                
                notificationManager.CancelNotification();

                DependencyService.Get<IMessage>().LongAlert("Alarm Uitgezet");
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Er is iets misgegaan");
            }
            
        }
        #endregion

        #region arduino code
        private void UpBtnClicked(object sender, EventArgs e)
        {
            UpBtn.Source = ImageSource.FromFile("ArrowIconYellowUp");
            DownBtn.Source = ImageSource.FromFile("ArrowIconDown");

            HapticFeedback.Perform(HapticFeedbackType.LongPress);

            try
            {
                Client.Send("<ROLLUP>");
                ReceivedData = Client.Receive();
                
                LdrStatusText = "Rolling up";
                OnPropertyChanged(nameof(LdrStatusText));

                if(ReceivedData == "1")
                {
                    Gordijnstatus = "Open";
                    OnPropertyChanged(nameof(Gordijnstatus));

                    LdrStatusText = "Done";
                    OnPropertyChanged(nameof(LdrStatusText));
                }
            }
            catch
            {
            }
        }

        private void DownBtnClicked(object sender, EventArgs e)
        {
            DownBtn.Source = ImageSource.FromFile("ArrowIconYellowDown");
            UpBtn.Source = ImageSource.FromFile("ArrowIconUp");

            HapticFeedback.Perform(HapticFeedbackType.LongPress);

            try
            {
                Client.Send("<ROLLDOWN>");
                ReceivedData = Client.Receive();
                LdrStatusText = "Rolling down";
                OnPropertyChanged(nameof(LdrStatusText));


                if (ReceivedData == "2")
                {
                    Gordijnstatus = "Closed";
                    OnPropertyChanged(nameof(Gordijnstatus));
                    
                    LdrStatusText = "Done";
                    OnPropertyChanged(nameof(LdrStatusText));
                }
            }
            catch
            {

            }
        }

        private void CloseBtnClicked(object sender, EventArgs e)
        {
            CloseBtn.BackgroundColor = Color.Green;
            OpenBtn.BackgroundColor = ColorConverters.FromHex("#FCA311");

            try
            {
                Client.CloseConnection();
                ConnectionStatusText = "Connection Status: Closed";
                OnPropertyChanged(nameof(ConnectionStatusText));
                DependencyService.Get<IMessage>().LongAlert("Connectie gesloten");
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Connectie reeds gesloten");
            }
            
        }

        private void OpenBtnClicked(object sender, EventArgs e)
        {
            OpenBtn.BackgroundColor = Color.Green;
            CloseBtn.BackgroundColor = ColorConverters.FromHex("#FCA311");

            try
            {
                Client.OpenConnection();
                ConnectionStatusText = "Connection Status: Open";
                OnPropertyChanged(nameof(ConnectionStatusText));
                DependencyService.Get<IMessage>().LongAlert("Connectie geopend");
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Connectie reeds geopend");
            }
            
        }

        private void ReceiveBtnClicked(object sender, EventArgs e)
        {
            try
            {
                LdrStatusText = "Received: ";
                //LdrStatusText = Client.Receive();
                OnPropertyChanged(nameof(LdrStatusText));
                DependencyService.Get<IMessage>().LongAlert("Ontvangen");
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert("Niks Ontvangen");
            }
            
        }
        #endregion

        private void ImageButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        private void UpBtn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            

        }

        public void UpdateText(string v)
        { 
            OnPropertyChanged(nameof(LdrStatusText));
        }
    }
}
