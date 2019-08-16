using System;
using System.Windows.Input;

namespace Controller.ViewCommand {
    public class MainWindowCommand:ICommand {
        private Action _Execute;
        private Func<Boolean> _CanExecute;

        public event EventHandler CanExecuteChanged{
            add {
                if (this._CanExecute!=null) {CommandManager.RequerySuggested+=value;}
            }
            remove {
                if (this._CanExecute!=null) {CommandManager.RequerySuggested-=value;}
            }
        }

        public MainWindowCommand(Action _Execute,Func<Boolean> _CanExecute){
            if(_Execute==null){return;}
            this._Execute=_Execute;
            this._CanExecute=_CanExecute;
        }

        public bool CanExecute(object parameter)=>_CanExecute==null?true:_CanExecute();
        public void Execute(object parameter)=>_Execute();
    }
}
