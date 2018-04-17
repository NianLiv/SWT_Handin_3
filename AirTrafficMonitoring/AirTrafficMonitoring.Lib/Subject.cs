using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib
{
    public abstract class Subject<T>
    {
        private List<IObserver<T>> _observers = new List<IObserver<T>>();

        public void Attach(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Detach(IObserver<T> observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void Notify(T obj)
        {
            foreach (var sub in _observers)
                sub.Update(obj);
        }
    }
}
