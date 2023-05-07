using ScalarGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalarGraph.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand GraphViewCommand { get; set; }

        public RelayCommand ProfileViewCommand { get; set; }
        public GraphViewModel GraphVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }

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
            GraphVM = new GraphViewModel();
            ProfileVM = new ProfileViewModel();
            
            CurrentView = GraphVM;

            GraphViewCommand = new RelayCommand(o => 
            {
                CurrentView = GraphVM;
            });

            ProfileViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
            });
        }
    }
}
