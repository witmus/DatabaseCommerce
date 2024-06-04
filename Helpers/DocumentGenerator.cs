using DatabaseCommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;

namespace DatabaseCommerce.Helpers
{
    public static class DocumentGenerator
    {
        public static void GenerateInvoice(InvoiceDataDto invoiceData)
        {
            var receiver = invoiceData.ReceiverWithAddress;
            var streetWithNumber = receiver.ApartmentNumber is null
                ? receiver.Street + " " + receiver.BuildingNumber
                : receiver.Street + " " + receiver.BuildingNumber + "/" + receiver.ApartmentNumber;

            Document.Create(c =>
            {
                c.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page
                        .Content()
                        .Column(x =>
                        {
                            x.Spacing(10);

                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem()
                                        .Text("TechTopia\r\nWyspiańskiego 27, 50-370 Wrocław\r\nTel.: 111-11-11, NIP: 111-111-11-11\r\nNBP O/Okr. w Warszawie\r\n11 111111111 11111 1111 1111 1111")
                                        .SemiBold();
                                    r.ConstantItem(PageSizes.A4.Width / 4);
                                    r.RelativeItem()
                                        .AlignCenter()
                                        .Text($"Miejsce wystawienia:\r\nWrocław\r\nData sprzedaży:\r\n{invoiceData.OrderDate:yyyy-MM-dd}");
                                });

                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem(PageSizes.A4.Width / 2)
                                        .Text("Sprzedawca")
                                        .AlignCenter();

                                    r.RelativeItem(PageSizes.A4.Width / 2)
                                        .Text("Nabywca")
                                        .AlignCenter();
                                });

                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem(PageSizes.A4.Width / 2)
                                        .Text("TechTopia\r\nWyspiańskiego 27,\r\n50-370, Wrocław\r\nNIP: 111-111-11-11")
                                        .AlignLeft();

                                    r.ConstantItem(PageSizes.A4.Width / 6);

                                    r.RelativeItem(PageSizes.A4.Width / 2)
                                        .Text($"{receiver.ReceiverName}\r\n{streetWithNumber}\r\n{receiver.ZipCode}, {receiver.Town}")
                                        .AlignLeft();
                                });


                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem(PageSizes.A4.Width / 2)
                                        .Text("Faktura VAT do zamówienia " + invoiceData.InvoiceNumber)
                                        .AlignCenter();
                                });

                            x.Item()
                                .Table(table =>
                                {
                                    // step 1
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn(5);
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn(1.2F);
                                        columns.RelativeColumn(1.2F);
                                    });

                                    // step 2
                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Lp.");
                                        header.Cell().Element(CellStyle).Text("Nazwa");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("j.m.");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Ilość");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Cena netto");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Vat [%]");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Cena brutto");
                                        header.Cell().Element(CellStyle).AlignRight().Text("Kwota Vat");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Wartość netto");
                                        header.Cell().Element(CellStyle).AlignCenter().Text("Wartość brutto");

                                        static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                                        {
                                            return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                        }
                                    });

                                    var positions = invoiceData.OrderPositions.ToList();

                                    foreach (var position in positions)
                                    {
                                        table.Cell().Element(CellStyle).AlignCenter().Text((positions.IndexOf(position) + 1).ToString());
                                        table.Cell().Element(CellStyle).AlignCenter().Text(position.Name);
                                        table.Cell().Element(CellStyle).AlignCenter().Text("szt.");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.Amount}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.NetPrice:F2}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.VatRate:F0}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.GrossPrice:F2}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{(position.GrossTotal - position.NetTotal):F2}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.NetTotal:F2}");
                                        table.Cell().Element(CellStyle).AlignCenter().Text($"{position.GrossTotal:F2}");

                                        static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                                        {
                                            return container
                                                .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                                .PaddingVertical(5)
                                                .BorderVertical(1).BorderColor(Colors.Grey.Lighten2);
                                        }
                                    }
                                });

                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem()
                                        .Text("Razem do zapłaty: " + invoiceData.OrderPositions.Sum(x => x.GrossTotal).ToString("F2"))
                                        .AlignRight();
                                });

                            x.Item()
                                .Row(r =>
                                {
                                    r.RelativeItem(PageSizes.A4.Width / 3)
                                        .Text("Wystawił(a)\r\n\r\nBen Dover")
                                        .AlignCenter();

                                    r.ConstantItem(PageSizes.A4.Width / 3);

                                    r.RelativeItem(PageSizes.A4.Width / 3)
                                        .Text($"Odebrał(a)")
                                        .AlignLeft();
                                });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdfAndShow();
        }
    }
}
