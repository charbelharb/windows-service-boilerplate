using System.ServiceProcess;
using System.Timers;

namespace Service
{
    public partial class Service : ServiceBase
    {
        private Timer timer;
        private readonly double interval = 1000;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(interval);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Do Work
        }
    }
}
