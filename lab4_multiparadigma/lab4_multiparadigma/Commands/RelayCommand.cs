using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4_multiparadigma.Commands
{
    /// <summary>
    ///  Permite delegar los eventos disparados en base a comandos (Command) hacia su correspondiente manejador de eventos
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Accion a ejecutar
        /// </summary>
        readonly Action<object> _execute;

        /// <summary>
        /// Predicado que permite saber si una accion se puede ejecutar o no.
        /// </summary>
        readonly Predicate<object> _canExecute;

        /// <summary>
        /// Constructor que recibe una accion a ejecutar
        /// </summary>
        /// <param name="execute">accion a ejecutar</param>
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        /// <summary>
        /// Constructor que recibe una accion a ejecutar, y un predicado
        /// que permite saber si la accion en cuestion se puede ejecutar.
        /// </summary>
        /// <param name="execute">accion a ejecutar</param>
        /// <param name="canExecute">predicado...</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }

        /// <summary>
        /// La accion se puede ejecutar o no.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>booleano que dice si la accion se puede ejecutar.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// evento que permite suscribirse y desuscribirse a los manejadores de eventos
        ///     que detectan si una accion se puede ejecutar o no.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Ejecuta la accion dado un parametro.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) { _execute(parameter); }

    }
}
