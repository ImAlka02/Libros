using LibroApp.Services;

namespace LibroApp
{
	public partial class App : Application
	{
		public static LibrosServices LibrosServices { get; set; } = new();
		public App()
		{
			InitializeComponent();

			Thread thread = new(Sincronizador) { IsBackground = true };
			thread.Start();

			MainPage = new AppShell();
		}

		async void Sincronizador()
		{
			while (true)
			{
				await LibrosServices.GetLibros();
				Thread.Sleep(TimeSpan.FromSeconds(15));
			}
			
		}
	}
}
