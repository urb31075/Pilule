// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MordaWPF
{
    using System;
    using PiluleDataProvider;
    using Unity;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="App"/> class from being created.
        /// </summary>
        private App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The main.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var app = new App();
            var uc = new UnityContainer();
            uc.RegisterType<IPiluleDataProvider, WorkDataProvider>();
            var window = uc.Resolve<MainWindow>();             
            app.Run(window);
        }
    }
}
