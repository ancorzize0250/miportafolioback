using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PortafolioApi.DTOs.HojaVida;

public class PdfService : IPdfService
{
    public byte[] GenerarPdfHojaVida(HojaVidaResponseDto data)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Content().Column(col =>
                {
                    col.Item().Text($"{data.DatosPersonales?.Nombre} {data.DatosPersonales?.Apellidos}")
                        .FontSize(20).Bold();

                    col.Item().Text(data.DatosPersonales?.Profesion ?? "");

                    col.Item().Text(" ");

                    col.Item().Text("Experiencia Laboral").Bold().FontSize(16);

                    foreach (var exp in data.Experiencias)
                    {
                        col.Item().Text($"{exp.Cargo} - {exp.Empresa}");
                        col.Item().Text(exp.Descripcion ?? "").FontSize(10);
                    }

                    col.Item().Text(" ");

                    col.Item().Text("Educación").Bold().FontSize(16);

                    foreach (var est in data.Estudios)
                    {
                        col.Item().Text($"{est.Titulo} - {est.Institucion}");
                    }

                    col.Item().Text(" ");

                    col.Item().Text("Tecnologías").Bold().FontSize(16);

                    col.Item().Text(string.Join(", ", data.Tecnologias.Select(t => t.Nombre)));
                });
            });
        });

        return document.GeneratePdf();
    }
}