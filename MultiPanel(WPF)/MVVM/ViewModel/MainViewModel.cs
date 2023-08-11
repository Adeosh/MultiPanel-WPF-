using MultiPanel_WPF_.Core;

namespace MultiPanel_WPF_.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CaclViewCommand { get; set; }
        public RelayCommand SpInvViewCommand { get; set; }
        public RelayCommand PacmanViewCommand { get; set; }
        public RelayCommand CurConvViewCommand { get; set; }
        public RelayCommand MusStoreViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CalculatorViewModel CalculatorVM { get; set; }
        public SpaceInvadersViewModel SpaceInvadersVM { get; set; }
        public PacmanViewModel PacmanVM { get; set; }
        public CurConverterViewModel CurConvVM { get; set; }
        public MusicStoreViewModel MusicStoreVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CalculatorVM = new CalculatorViewModel();
            SpaceInvadersVM = new SpaceInvadersViewModel();
            PacmanVM = new PacmanViewModel();
            CurConvVM = new CurConverterViewModel();
            MusicStoreVM = new MusicStoreViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            CaclViewCommand = new RelayCommand(x =>
            {
                CurrentView = CalculatorVM;
            });

            SpInvViewCommand = new RelayCommand(x =>
            {
                CurrentView = SpaceInvadersVM;
            });

            PacmanViewCommand = new RelayCommand(x =>
            {
                CurrentView = PacmanVM;
            });

            CurConvViewCommand = new RelayCommand(x =>
            {
                CurrentView = CurConvVM;
            });

            MusStoreViewCommand = new RelayCommand(x =>
            {
                CurrentView = MusicStoreVM;
            });
        }
    }
}
