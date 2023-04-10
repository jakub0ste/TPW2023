using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using TPW_PN_JS.Dane;

namespace TPW_PN_JS.Logika
{
    public interface IKulkaService
    {
        void AktualizujPozycje(Kulka kulka, Rectangle boundary);
    }

    public class KulkaService : IKulkaService
    {
        public void AktualizujPozycje(Kulka kulka, Rectangle boundary)
        {
            double leftBoundary = boundary.Margin.Left + 20;
            double topBoundary = boundary.Margin.Top + 20;
            double rightBoundary = leftBoundary + boundary.Width - 40;
            double bottomBoundary = topBoundary + boundary.Height - 40;

            double newX = kulka.X + kulka.VelocityX;
            double newY = kulka.Y + kulka.VelocityY;

            if (newX >= leftBoundary && newX <= rightBoundary)
            {
                kulka.X = newX;
            }
            else
            {
                kulka.VelocityX = -kulka.VelocityX;
            }

            if (newY >= topBoundary && newY <= bottomBoundary)
            {
                kulka.Y = newY;
            }
            else
            {
                kulka.VelocityY = -kulka.VelocityY;
            }
        }
    }

}
