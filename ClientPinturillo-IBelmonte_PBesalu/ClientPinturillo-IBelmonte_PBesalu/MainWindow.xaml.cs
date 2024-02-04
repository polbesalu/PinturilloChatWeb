using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientPinturillo_IBelmonte_PBesalu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point PuntAnterior { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            infoSupplier manager = infoSupplier.Instance;
            tbUser.Text = manager.CreaUsuari(tbUser.Text);
            manager.IniciaConexio(this, "localhost:8081");
            secretWord.Text(manager.listen());
        }
        private void cnvPissarra_MouseEnter(object sender, MouseEventArgs e)
        {
            PuntAnterior = e.GetPosition((Canvas)sender);
        }
        private void cnvPissarra_MouseMove(object sender, MouseEventArgs e)
        {
            Point puntActual;
            puntActual = e.GetPosition((Canvas)sender);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line linia = new Line();
                linia.Stroke = Brushes.Black;
                linia.StrokeThickness = 2;
                linia.X1 = PuntAnterior.X;
                linia.Y1 = PuntAnterior.Y;
                linia.X2 = puntActual.X;
                linia.Y2 = puntActual.Y;

                cnvPissarra.Children.Add(linia);

                if ((ModifierKeys.Shift & Keyboard.Modifiers) == ModifierKeys.Shift)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Width = 25;
                    elipse.Height = 25;
                    elipse.Stroke = Brushes.LightCoral;
                    elipse.StrokeThickness = 1;
                    cnvPissarra.Children.Add(elipse);

                    Canvas.SetTop(elipse, puntActual.Y - elipse.Height / 2);
                    elipse.SetValue(LeftProperty, puntActual.X - elipse.Width / 2);
                }
            }

            PuntAnterior = puntActual;
        
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            EnviaMissatge(tbMessage.Text);
        }
        private void EnviaMissatge(string missatge)
        {
            tbMessage.Text = "";
            infoSupplier manager = infoSupplier.Instance;
            manager.EnviaMissatge(missatge);
            ActualitzaChat(missatge);
        }
        public void ActualitzaChat(string chat)
        {
            tbChat.Text = chat;
        }
    }
}