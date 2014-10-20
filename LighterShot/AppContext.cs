using System.Windows.Forms;

namespace LighterShot
{
    class AppContext : ApplicationContext
    {
        public static FormMain FormMain;

        public AppContext()
        {
            FormMain = new FormMain();
        }
    }
}
