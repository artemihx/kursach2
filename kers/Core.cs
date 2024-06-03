using System.Diagnostics;
using System.Linq;
using kers.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas.Draw;


namespace kers;
using System.Security.Cryptography;
using System.Text;

public class Core
{
    public static string FONT = "C:/Users/artemih/Desktop/kursach/kursach2/kers/Assets/Roboto-Black.ttf";
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    public static bool CheckSeat(Trip selectTrip)
    {
        int countPassenger = Service.GetDbConnection().Tickets.Where(t => t.Fktripid == selectTrip.Id).Count();
        Auto selectedAuto = Service.GetDbConnection().Autos.FirstOrDefault(a => a.Id == selectTrip.Autoid);
        if (countPassenger < selectedAuto.Maxcountpassneger)
        {
            return true;
        }

        return false;
    }

    public static void PrintTicketPDF(Ticket tic)
    {
        string filePath = $"Ticket_{tic.Id}.pdf";
        using (var writer = new PdfWriter(filePath))
        {
            using (var pdf = new PdfDocument(writer))
            {
                var document = new Document(pdf);

                document.Add(new Paragraph("Билет")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(24)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.BLUE)
                );
                
                document.Add(new LineSeparator(new SolidLine()));
                
                document.Add(new Paragraph($"Номер билета: {tic.Id}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                    .SetMarginTop(20)
                );

                document.Add(
                    new Paragraph($"ФИО: {tic.Fkpassport.Fname} {tic.Fkpassport.Lname} {tic.Fkpassport.Mname ?? ""}")
                        .SetFont(PdfFontFactory.CreateFont(FONT))
                        .SetFontSize(12)
                );

                document.Add(new Paragraph($"Серия паспорта: {tic.Fkpassport.Serail}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                );

                document.Add(new Paragraph($"Номер паспорта: {tic.Fkpassport.Number}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                );

                document.Add(new Paragraph($"Направление: {tic.Fktrip.Route.Name}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                );

                document.Add(new Paragraph($"Время отправления: {tic.Fktrip.Timestart:yyyy-MM-dd HH:mm}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                );


                document.Add(new Paragraph($"т/с: {tic.Fktrip.Auto.Name} {tic.Fktrip.Auto.Carnumber}")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                );
                
                document.Add(new LineSeparator(new SolidLine()).SetMarginTop(20));
                document.Add(new Paragraph("Спасибо, что выбираете нас!")
                    .SetFont(PdfFontFactory.CreateFont(FONT))
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginTop(20)
                );
                
                document.Close();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };

                System.Diagnostics.Process.Start(startInfo);
            }
        }
    }
}