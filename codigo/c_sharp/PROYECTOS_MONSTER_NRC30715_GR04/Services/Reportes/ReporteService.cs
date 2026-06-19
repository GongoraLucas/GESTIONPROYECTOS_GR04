using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes.Modelos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

public class ReporteService : IReporteService
{
    private readonly ProyectoDbContext _context;

    public ReporteService(ProyectoDbContext context)
    {
        _context = context;

        QuestPDF.Settings.License = LicenseType.Community;
    }

    // ═════════════════════════════════════════════════════════════════════
    //  USUARIOS
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteUsuarioDto>> ObtenerDatosUsuariosAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Estado)
            .Include(u => u.Empleado)
            .OrderBy(u => u.Login)
            .Select(u => new ReporteUsuarioDto
            {
                Id = u.Id,
                Login = u.Login,
                Email = u.Email,
                Estado = u.Estado != null ? u.Estado.Descripcion : "",
                Empleado = u.Empleado != null ? u.Empleado.Apellidos + " " + u.Empleado.Nombres : ""
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarUsuariosPdfAsync()
    {
        var datos = await ObtenerDatosUsuariosAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Usuarios"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(40);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(3);
                            cols.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(9))
                                 .Background("#4f46e5").Padding(6);

                            header.Cell().Element(CeldaHead).Text("Id");
                            header.Cell().Element(CeldaHead).Text("Login");
                            header.Cell().Element(CeldaHead).Text("Email");
                            header.Cell().Element(CeldaHead).Text("Empleado / Estado");
                        });

                        bool alterno = false;
                        foreach (var u in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(6);

                            table.Cell().Element(c => Celda(c, bg)).Text(u.Id.ToString());
                            table.Cell().Element(c => Celda(c, bg)).Text(u.Login);
                            table.Cell().Element(c => Celda(c, bg)).Text(u.Email);
                            table.Cell().Element(c => Celda(c, bg)).Text($"{u.Empleado} / {u.Estado}");
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarUsuariosExcelAsync()
    {
        var datos = await ObtenerDatosUsuariosAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Usuarios");

        ws.Cell(1, 1).Value = "REPORTE DE USUARIOS";
        AplicarEstiloTitulo(ws.Cell(1, 1));
        ws.Range(1, 1, 1, 4).Merge();

        ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(ws.Cell(2, 1));
        ws.Range(2, 1, 2, 4).Merge();

        AplicarEncabezados(ws, 4, new[] { "Id", "Login", "Email", "Empleado / Estado" });

        int fila = 5; bool alt = false;
        foreach (var u in datos)
        {
            alt = !alt;
            var bg = alt ? XLColor.FromHtml("#ECEFF1") : XLColor.White;
            ws.Cell(fila, 1).Value = u.Id;
            ws.Cell(fila, 2).Value = u.Login;
            ws.Cell(fila, 3).Value = u.Email;
            ws.Cell(fila, 4).Value = $"{u.Empleado} / {u.Estado}";
            var r = ws.Range(fila, 1, fila, 4);
            r.Style.Fill.BackgroundColor = bg;
            r.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            r.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        ws.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarUsuariosCsvAsync()
    {
        var datos = await ObtenerDatosUsuariosAsync();

        using var mem = new MemoryStream();
        using var wr  = new StreamWriter(mem, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(wr, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Id"); csv.WriteField("Login"); csv.WriteField("Email"); csv.WriteField("Empleado"); csv.WriteField("Estado");
        await csv.NextRecordAsync();

        foreach (var u in datos)
        {
            csv.WriteField(u.Id); csv.WriteField(u.Login); csv.WriteField(u.Email);
            csv.WriteField(u.Empleado); csv.WriteField(u.Estado);
            await csv.NextRecordAsync();
        }

        await wr.FlushAsync();
        return mem.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  EMPLEADOS
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteEmpleadoDto>> ObtenerDatosEmpleadosAsync()
    {
        return await _context.Empleados
            .Where(e => e.Estado == "A")
            .Include(e => e.Sexo)
            .Include(e => e.EstadoCivil)
            .Include(e => e.Cargo)
                .ThenInclude(c => c!.Departamento)
            .Include(e => e.Jefe)
            .OrderBy(e => e.Apellidos)
            .ThenBy(e => e.Nombres)
            .Select(e => new ReporteEmpleadoDto
            {
                Codigo          = e.Codigo,
                Cedula          = e.Cedula,
                Apellidos       = e.Apellidos,
                Nombres         = e.Nombres,
                Email           = e.Email,
                Telefono        = e.Telefono,
                Salario         = e.Salario,
                FechaNacimiento = e.FechaNacimiento,
                FechaSalida     = e.FechaSalida,

                Sexo        = e.Sexo != null ? e.Sexo.Descripcion : "",
                EstadoCivil = e.EstadoCivil != null ? e.EstadoCivil.Descripcion : "",
                Cargo       = e.Cargo != null ? e.Cargo.Descripcion : "",

                Departamento = e.Cargo != null && e.Cargo.Departamento != null
                    ? e.Cargo.Departamento.Descripcion
                    : "",

                JefeNombre = e.Jefe != null
                    ? e.Jefe.Apellidos + " " + e.Jefe.Nombres
                    : "Sin jefe"
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarEmpleadosPdfAsync()
    {
        var datos = await ObtenerDatosEmpleadosAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(9));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Empleados"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(45);
                            cols.ConstantColumn(70);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(1.5f);
                            cols.ConstantColumn(70);
                            cols.ConstantColumn(65);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(8.5f))
                                 .Background("#4f46e5")
                                 .Padding(6);

                            header.Cell().Element(CeldaHead).Text("Código");
                            header.Cell().Element(CeldaHead).Text("Cédula");
                            header.Cell().Element(CeldaHead).Text("Apellidos");
                            header.Cell().Element(CeldaHead).Text("Nombres");
                            header.Cell().Element(CeldaHead).Text("Departamento");
                            header.Cell().Element(CeldaHead).Text("Cargo");
                            header.Cell().Element(CeldaHead).Text("Teléfono");
                            header.Cell().Element(CeldaHead).AlignRight().Text("Salario");
                        });

                        bool alterno = false;
                        foreach (var emp in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(5);

                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Codigo);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Cedula);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Apellidos);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Nombres);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Departamento);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Cargo);
                            table.Cell().Element(c => Celda(c, bg)).Text(emp.Telefono);
                            table.Cell().Element(c => Celda(c, bg)).AlignRight()
                                 .Text($"${emp.Salario:N2}");
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarEmpleadosExcelAsync()
    {
        var datos = await ObtenerDatosEmpleadosAsync();

        using var workbook  = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Empleados");

        worksheet.Cell(1, 1).Value = "REPORTE DE EMPLEADOS";
        AplicarEstiloTitulo(worksheet.Cell(1, 1));
        worksheet.Range(1, 1, 1, 10).Merge();

        worksheet.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(worksheet.Cell(2, 1));
        worksheet.Range(2, 1, 2, 10).Merge();

        var encabezados = new[]
        {
            "Código", "Cédula", "Apellidos", "Nombres",
            "Departamento", "Cargo", "Sexo", "Estado Civil",
            "Teléfono", "Salario"
        };
        AplicarEncabezados(worksheet, 4, encabezados);

        int fila = 5;
        bool alterno = false;
        foreach (var emp in datos)
        {
            alterno = !alterno;
            var bgColor = alterno ? XLColor.FromHtml("#ECEFF1") : XLColor.White;

            worksheet.Cell(fila, 1).Value  = emp.Codigo;
            worksheet.Cell(fila, 2).Value  = emp.Cedula;
            worksheet.Cell(fila, 3).Value  = emp.Apellidos;
            worksheet.Cell(fila, 4).Value  = emp.Nombres;
            worksheet.Cell(fila, 5).Value  = emp.Departamento;
            worksheet.Cell(fila, 6).Value  = emp.Cargo;
            worksheet.Cell(fila, 7).Value  = emp.Sexo;
            worksheet.Cell(fila, 8).Value  = emp.EstadoCivil;
            worksheet.Cell(fila, 9).Value  = emp.Telefono;
            worksheet.Cell(fila, 10).Value = emp.Salario;
            worksheet.Cell(fila, 10).Style.NumberFormat.SetFormat("$#,##0.00");

            var rango = worksheet.Range(fila, 1, fila, 10);
            rango.Style.Fill.BackgroundColor = bgColor;
            rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.Cell(fila, 9).Value = "TOTAL:";
        worksheet.Cell(fila, 9).Style.Font.SetBold(true)
            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell(fila, 10).FormulaA1 = $"=SUM(J5:J{fila - 1})";
        worksheet.Cell(fila, 10).Style.Font.SetBold(true)
            .NumberFormat.SetFormat("$#,##0.00")
            .Fill.SetBackgroundColor(XLColor.FromHtml("#B0BEC5"));

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarEmpleadosCsvAsync()
    {
        var datos = await ObtenerDatosEmpleadosAsync();

        using var memStream = new MemoryStream();
        using var writer    = new StreamWriter(memStream, System.Text.Encoding.UTF8);
        using var csv       = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Codigo"); csv.WriteField("Cedula"); csv.WriteField("Apellidos");
        csv.WriteField("Nombres"); csv.WriteField("Email"); csv.WriteField("Telefono");
        csv.WriteField("Departamento"); csv.WriteField("Cargo"); csv.WriteField("Sexo");
        csv.WriteField("EstadoCivil"); csv.WriteField("FechaNacimiento");
        csv.WriteField("FechaSalida"); csv.WriteField("Salario"); csv.WriteField("JefeNombre");
        await csv.NextRecordAsync();

        foreach (var emp in datos)
        {
            csv.WriteField(emp.Codigo); csv.WriteField(emp.Cedula); csv.WriteField(emp.Apellidos);
            csv.WriteField(emp.Nombres); csv.WriteField(emp.Email); csv.WriteField(emp.Telefono);
            csv.WriteField(emp.Departamento); csv.WriteField(emp.Cargo); csv.WriteField(emp.Sexo);
            csv.WriteField(emp.EstadoCivil);
            csv.WriteField(emp.FechaNacimiento.ToString("yyyy-MM-dd"));
            csv.WriteField(emp.FechaSalida?.ToString("yyyy-MM-dd") ?? "");
            csv.WriteField(emp.Salario.ToString("F2", CultureInfo.InvariantCulture));
            csv.WriteField(emp.JefeNombre);
            await csv.NextRecordAsync();
        }

        await writer.FlushAsync();
        return memStream.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  DEPARTAMENTOS
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteDepartamentoDto>> ObtenerDatosDepartamentosAsync()
    {
        return await _context.Departamentos
            .OrderBy(d => d.Descripcion)
            .Select(d => new ReporteDepartamentoDto
            {
                Codigo      = d.Codigo,
                Descripcion = d.Descripcion,
                TotalCargos = d.Cargos.Count()
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarDepartamentosPdfAsync()
    {
        var datos = await ObtenerDatosDepartamentosAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Departamentos"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(60);
                            cols.RelativeColumn();
                            cols.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(9))
                                 .Background("#4f46e5").Padding(6);

                            header.Cell().Element(CeldaHead).Text("Código");
                            header.Cell().Element(CeldaHead).Text("Descripción");
                            header.Cell().Element(CeldaHead).AlignCenter().Text("Cargos");
                        });

                        bool alterno = false;
                        foreach (var dep in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(6);

                            table.Cell().Element(c => Celda(c, bg)).Text(dep.Codigo);
                            table.Cell().Element(c => Celda(c, bg)).Text(dep.Descripcion);
                            table.Cell().Element(c => Celda(c, bg)).AlignCenter().Text(dep.TotalCargos.ToString());
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarDepartamentosExcelAsync()
    {
        var datos = await ObtenerDatosDepartamentosAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Departamentos");

        ws.Cell(1, 1).Value = "REPORTE DE DEPARTAMENTOS";
        AplicarEstiloTitulo(ws.Cell(1, 1));
        ws.Range(1, 1, 1, 3).Merge();

        ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(ws.Cell(2, 1));
        ws.Range(2, 1, 2, 3).Merge();

        AplicarEncabezados(ws, 4, new[] { "Código", "Descripción", "Total Cargos" });

        int fila = 5; bool alt = false;
        foreach (var dep in datos)
        {
            alt = !alt;
            var bg = alt ? XLColor.FromHtml("#ECEFF1") : XLColor.White;
            ws.Cell(fila, 1).Value = dep.Codigo;
            ws.Cell(fila, 2).Value = dep.Descripcion;
            ws.Cell(fila, 3).Value = dep.TotalCargos;
            var r = ws.Range(fila, 1, fila, 3);
            r.Style.Fill.BackgroundColor = bg;
            r.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            r.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        ws.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarDepartamentosCsvAsync()
    {
        var datos = await ObtenerDatosDepartamentosAsync();

        using var mem = new MemoryStream();
        using var wr  = new StreamWriter(mem, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(wr, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Codigo"); csv.WriteField("Descripcion"); csv.WriteField("TotalCargos");
        await csv.NextRecordAsync();

        foreach (var dep in datos)
        {
            csv.WriteField(dep.Codigo); csv.WriteField(dep.Descripcion); csv.WriteField(dep.TotalCargos);
            await csv.NextRecordAsync();
        }

        await wr.FlushAsync();
        return mem.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  SEXO
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteSexoDto>> ObtenerDatosSexosAsync()
    {
        return await _context.Sexos
            .OrderBy(s => s.Descripcion)
            .Select(s => new ReporteSexoDto
            {
                Codigo         = s.Codigo,
                Descripcion    = s.Descripcion,
                TotalEmpleados = s.Empleados.Count()
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarSexosPdfAsync()
    {
        var datos = await ObtenerDatosSexosAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Sexos"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(60);
                            cols.RelativeColumn();
                            cols.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(9))
                                 .Background("#4f46e5").Padding(6);

                            header.Cell().Element(CeldaHead).Text("Código");
                            header.Cell().Element(CeldaHead).Text("Descripción");
                            header.Cell().Element(CeldaHead).AlignCenter().Text("Total Empleados");
                        });

                        bool alterno = false;
                        foreach (var s in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(6);

                            table.Cell().Element(c => Celda(c, bg)).Text(s.Codigo);
                            table.Cell().Element(c => Celda(c, bg)).Text(s.Descripcion);
                            table.Cell().Element(c => Celda(c, bg)).AlignCenter().Text(s.TotalEmpleados.ToString());
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarSexosExcelAsync()
    {
        var datos = await ObtenerDatosSexosAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Sexos");

        ws.Cell(1, 1).Value = "REPORTE DE SEXOS";
        AplicarEstiloTitulo(ws.Cell(1, 1));
        ws.Range(1, 1, 1, 3).Merge();

        ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(ws.Cell(2, 1));
        ws.Range(2, 1, 2, 3).Merge();

        AplicarEncabezados(ws, 4, new[] { "Código", "Descripción", "Total Empleados" });

        int fila = 5; bool alt = false;
        foreach (var s in datos)
        {
            alt = !alt;
            var bg = alt ? XLColor.FromHtml("#ECEFF1") : XLColor.White;
            ws.Cell(fila, 1).Value = s.Codigo;
            ws.Cell(fila, 2).Value = s.Descripcion;
            ws.Cell(fila, 3).Value = s.TotalEmpleados;
            var r = ws.Range(fila, 1, fila, 3);
            r.Style.Fill.BackgroundColor = bg;
            r.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            r.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        ws.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarSexosCsvAsync()
    {
        var datos = await ObtenerDatosSexosAsync();

        using var mem = new MemoryStream();
        using var wr  = new StreamWriter(mem, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(wr, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Codigo"); csv.WriteField("Descripcion"); csv.WriteField("TotalEmpleados");
        await csv.NextRecordAsync();

        foreach (var s in datos)
        {
            csv.WriteField(s.Codigo); csv.WriteField(s.Descripcion); csv.WriteField(s.TotalEmpleados);
            await csv.NextRecordAsync();
        }

        await wr.FlushAsync();
        return mem.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  ESTADO CIVIL
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteEstadoCivilDto>> ObtenerDatosEstadosCivilesAsync()
    {
        return await _context.EstadosCiviles
            .OrderBy(ec => ec.Descripcion)
            .Select(ec => new ReporteEstadoCivilDto
            {
                Codigo         = ec.Codigo,
                Descripcion    = ec.Descripcion,
                TotalEmpleados = ec.Empleados.Count()
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarEstadosCivilesPdfAsync()
    {
        var datos = await ObtenerDatosEstadosCivilesAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Estados Civiles"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(60);
                            cols.RelativeColumn();
                            cols.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(9))
                                 .Background("#4f46e5").Padding(6);

                            header.Cell().Element(CeldaHead).Text("Código");
                            header.Cell().Element(CeldaHead).Text("Descripción");
                            header.Cell().Element(CeldaHead).AlignCenter().Text("Total Empleados");
                        });

                        bool alterno = false;
                        foreach (var ec in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(6);

                            table.Cell().Element(c => Celda(c, bg)).Text(ec.Codigo);
                            table.Cell().Element(c => Celda(c, bg)).Text(ec.Descripcion);
                            table.Cell().Element(c => Celda(c, bg)).AlignCenter().Text(ec.TotalEmpleados.ToString());
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarEstadosCivilesExcelAsync()
    {
        var datos = await ObtenerDatosEstadosCivilesAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("EstadosCiviles");

        ws.Cell(1, 1).Value = "REPORTE DE ESTADOS CIVILES";
        AplicarEstiloTitulo(ws.Cell(1, 1));
        ws.Range(1, 1, 1, 3).Merge();

        ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(ws.Cell(2, 1));
        ws.Range(2, 1, 2, 3).Merge();

        AplicarEncabezados(ws, 4, new[] { "Código", "Descripción", "Total Empleados" });

        int fila = 5; bool alt = false;
        foreach (var ec in datos)
        {
            alt = !alt;
            var bg = alt ? XLColor.FromHtml("#ECEFF1") : XLColor.White;
            ws.Cell(fila, 1).Value = ec.Codigo;
            ws.Cell(fila, 2).Value = ec.Descripcion;
            ws.Cell(fila, 3).Value = ec.TotalEmpleados;
            var r = ws.Range(fila, 1, fila, 3);
            r.Style.Fill.BackgroundColor = bg;
            r.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            r.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        ws.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarEstadosCivilesCsvAsync()
    {
        var datos = await ObtenerDatosEstadosCivilesAsync();

        using var mem = new MemoryStream();
        using var wr  = new StreamWriter(mem, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(wr, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Codigo"); csv.WriteField("Descripcion"); csv.WriteField("TotalEmpleados");
        await csv.NextRecordAsync();

        foreach (var ec in datos)
        {
            csv.WriteField(ec.Codigo); csv.WriteField(ec.Descripcion); csv.WriteField(ec.TotalEmpleados);
            await csv.NextRecordAsync();
        }

        await wr.FlushAsync();
        return mem.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  CARGOS
    // ═════════════════════════════════════════════════════════════════════

    private async Task<List<ReporteCargoDto>> ObtenerDatosCargosAsync()
    {
        return await _context.Cargos
            .Include(c => c.Departamento)
            .OrderBy(c => c.Departamento!.Descripcion)
            .ThenBy(c => c.Descripcion)
            .Select(c => new ReporteCargoDto
            {
                Departamento   = c.Departamento != null ? c.Departamento.Descripcion : "",
                Codigo         = c.Codigo,
                Descripcion    = c.Descripcion,
                TotalEmpleados = c.Empleados.Count()
            })
            .ToListAsync();
    }

    public async Task<byte[]> GenerarCargosPdfAsync()
    {
        var datos = await ObtenerDatosCargosAsync();

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeaderGenerico("Reporte de Cargos"));

                page.Content().Element(content =>
                {
                    content.PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2);
                            cols.ConstantColumn(60);
                            cols.RelativeColumn(2);
                            cols.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            static IContainer CeldaHead(IContainer c) =>
                                c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White).FontSize(9))
                                 .Background("#4f46e5").Padding(6);

                            header.Cell().Element(CeldaHead).Text("Departamento");
                            header.Cell().Element(CeldaHead).Text("Código");
                            header.Cell().Element(CeldaHead).Text("Descripción");
                            header.Cell().Element(CeldaHead).AlignCenter().Text("Total Empleados");
                        });

                        bool alterno = false;
                        foreach (var c in datos)
                        {
                            alterno = !alterno;
                            var bg = alterno ? "#f8fafc" : "#ffffff";

                            static IContainer Celda(IContainer c, string bg) =>
                                c.Background(bg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten3).Padding(6);

                            table.Cell().Element(x => Celda(x, bg)).Text(c.Departamento);
                            table.Cell().Element(x => Celda(x, bg)).Text(c.Codigo);
                            table.Cell().Element(x => Celda(x, bg)).Text(c.Descripcion);
                            table.Cell().Element(x => Celda(x, bg)).AlignCenter().Text(c.TotalEmpleados.ToString());
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return documento.GeneratePdf();
    }

    public async Task<byte[]> GenerarCargosExcelAsync()
    {
        var datos = await ObtenerDatosCargosAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Cargos");

        ws.Cell(1, 1).Value = "REPORTE DE CARGOS";
        AplicarEstiloTitulo(ws.Cell(1, 1));
        ws.Range(1, 1, 1, 4).Merge();

        ws.Cell(2, 1).Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
        AplicarEstiloSubtitulo(ws.Cell(2, 1));
        ws.Range(2, 1, 2, 4).Merge();

        AplicarEncabezados(ws, 4, new[] { "Departamento", "Código", "Descripción", "Total Empleados" });

        int fila = 5; bool alt = false;
        foreach (var c in datos)
        {
            alt = !alt;
            var bg = alt ? XLColor.FromHtml("#ECEFF1") : XLColor.White;
            ws.Cell(fila, 1).Value = c.Departamento;
            ws.Cell(fila, 2).Value = c.Codigo;
            ws.Cell(fila, 3).Value = c.Descripcion;
            ws.Cell(fila, 4).Value = c.TotalEmpleados;
            var r = ws.Range(fila, 1, fila, 4);
            r.Style.Fill.BackgroundColor = bg;
            r.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            r.Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            fila++;
        }

        ws.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarCargosCsvAsync()
    {
        var datos = await ObtenerDatosCargosAsync();

        using var mem = new MemoryStream();
        using var wr  = new StreamWriter(mem, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(wr, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," });

        csv.WriteField("Departamento"); csv.WriteField("Codigo");
        csv.WriteField("Descripcion"); csv.WriteField("TotalEmpleados");
        await csv.NextRecordAsync();

        foreach (var c in datos)
        {
            csv.WriteField(c.Departamento); csv.WriteField(c.Codigo);
            csv.WriteField(c.Descripcion); csv.WriteField(c.TotalEmpleados);
            await csv.NextRecordAsync();
        }

        await wr.FlushAsync();
        return mem.ToArray();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  HELPERS PRIVADOS COMPARTIDOS
    // ═════════════════════════════════════════════════════════════════════

    private static Action<IContainer> ComposeHeaderGenerico(string titulo)
        => container => container.Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text("MONSTER PROJECTS")
                     .Bold().FontSize(10).FontColor("#4f46e5");
                    
                    c.Item().Text(titulo)
                     .Bold().FontSize(20).FontColor("#1e293b");

                    c.Item().Text($"Reporte del Sistema • Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                     .FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });

            col.Item().PaddingTop(8).PaddingBottom(8)
               .LineHorizontal(1.5f).LineColor("#4f46e5");
        });

    private static void AplicarEstiloTitulo(IXLCell celda)
    {
        celda.Style
            .Font.SetBold(true)
            .Font.SetFontSize(16)
            .Font.SetFontName("Segoe UI")
            .Font.SetFontColor(XLColor.White)
            .Fill.SetBackgroundColor(XLColor.FromHtml("#4F46E5"))
            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
            .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
    }

    private static void AplicarEstiloSubtitulo(IXLCell celda)
    {
        celda.Style
            .Font.SetItalic(true)
            .Font.SetFontSize(10)
            .Font.SetFontName("Segoe UI")
            .Font.SetFontColor(XLColor.FromHtml("#E0E7FF"))
            .Fill.SetBackgroundColor(XLColor.FromHtml("#4F46E5"))
            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
            .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
    }

    private static void AplicarEncabezados(IXLWorksheet ws, int fila, string[] encabezados)
    {
        for (int col = 0; col < encabezados.Length; col++)
        {
            var celda = ws.Cell(fila, col + 1);
            celda.Value = encabezados[col];
            celda.Style
                .Font.SetBold(true)
                .Font.SetFontName("Segoe UI")
                .Font.SetFontSize(11)
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(XLColor.FromHtml("#1E293B"))
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Border.SetOutsideBorderColor(XLColor.FromHtml("#475569"))
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        }
        ws.Row(fila).Height = 24;
    }
}

