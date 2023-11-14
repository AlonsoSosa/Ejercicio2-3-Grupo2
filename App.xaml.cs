using Ejercicio2_3_Grupo2.Convertidores;
namespace Ejercicio2_3_Grupo2
{
    public partial class App : Application
    {
        static BaseAudio db;


        public static BaseAudio DBase
        {
            get
            {
                if (db == null)
                {
                    String folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BaseAudio.db3");

                    db = new BaseAudio(folderPath);
                }

                return db;
            }
        }




        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}