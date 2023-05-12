using System;
using System.Collections.Generic;
using System.Linq;
using Timer = System.Timers.Timer;

namespace Game.Controller
{
    public class GameTickController
    {
        private readonly Timer _timer;
        private int _counter;

        private Dictionary<int, List<Action>> _actions = new Dictionary<int, List<Action>>();

        public GameTickController(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += (o, e) => Update();
        }

        public void RegisterAction(Action action, int ticksCount)
        {
            if (_actions.ContainsKey(ticksCount))
                _actions[ticksCount].Add(action);
            else
                _actions[ticksCount] = new List<Action> { action };
        }

        private void Update()
        {
            if (_counter == int.MaxValue)
                _counter = 0;
            _counter++;
            foreach (var key in _actions.Keys.Where(key => _counter % key == 0))
                _actions[key].ForEach(action => action());
        }

        public void StartTimer() => _timer.Start();
    }
}