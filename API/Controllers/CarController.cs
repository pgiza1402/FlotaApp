using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace API.Controllers
{
    public class CarController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
       
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CarController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult> AddCar(CarDto carToAddDto)
        {
            var car = new Car();
            var appUser = await _userManager.Users.Where(x => x.UserName == carToAddDto.UserName).FirstOrDefaultAsync();
            _mapper.Map(carToAddDto, car);
            car.AppUser = appUser;

            await _unitOfWork.CarRepository.AddCarAsync(car);
            
            return Ok();

          
          

        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("excel")]
        public FileResult GetExcelFile(IReadOnlyList<CarForListDto> cars)
        {
            var stream = new MemoryStream();

            using (var xlPackage = new ExcelPackage(stream))
            {

                var worksheet = xlPackage.Workbook.Worksheets.Add("FLOTA");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);


                worksheet.Cells["A1:K1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:K1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:K1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:K1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:K1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                worksheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                worksheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SandyBrown);
                worksheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SeaGreen);
                worksheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumPurple);
                worksheet.Cells["J1:K1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);

                worksheet.Cells["A1"].Value = "L.p.";
                worksheet.Cells["B1"].Value = "Marka";
                worksheet.Cells["C1"].Value = "Rocznik";
                worksheet.Cells["D1"].Value = "Użytkownik";
                worksheet.Cells["E1"].Value = "VAT";
                worksheet.Cells["F1"].Value = "Nr rejestracyjny";
                worksheet.Cells["G1"].Value = "Stan licznika";
                worksheet.Cells["H1"].Value = "Ubezpieczenie";
                worksheet.Cells["I1"].Value = "Badanie techniczne";
                worksheet.Cells["J1"].Value = "Termin serwisu";
                worksheet.Cells["K1"].Value = "Nast. serwis";

                var idx = 1;
                int row = 2;

                foreach (var car in cars)
                {

                    for (int i = 1; i <= 11; i++)
                    {
                        worksheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    }
                    worksheet.Cells[row, 1].Value = $"{idx++}.";
                    worksheet.Cells[row, 2].Value = $"{car.Brand} {car.Model}";
                    worksheet.Cells[row, 3].Value = $"{car.Year}";
                    worksheet.Cells[row, 4].Value = car.UserName;
                    worksheet.Cells[row, 5].Value = car.VAT;
                    worksheet.Cells[row, 6].Value = car.RegistrationNumber;
                    worksheet.Cells[row, 7].Value = car.MeterStatus;
                    worksheet.Cells[row, 8].Value = car.carInsuranceExpirationDate;
                    worksheet.Cells[row, 8].Style.Numberformat.Format = "dd-MM-yyyy";
                    worksheet.Cells[row, 9].Value = car.TechnicalExaminationExpirationDate;
                    worksheet.Cells[row, 9].Style.Numberformat.Format = "dd-MM-yyyy";
                    worksheet.Cells[row, 10].Value = car.ServiceExpirationDate;
                    worksheet.Cells[row, 10].Style.Numberformat.Format = "dd-MM-yyyy";
                    worksheet.Cells[row, 11].Value = car.NextServiceMeterStatus;

                    row++;
                }
                worksheet.Cells.AutoFitColumns();
                xlPackage.Save();
            }

            stream.Position = 0;

            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, "Ubezpieczenia i przeglądy samochodów.xlsx");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("pdf")]
        public FileResult GetPdfFile(IReadOnlyList<CarForListDto> cars)
        {
            var idx = 1;
            var stream = new MemoryStream();

            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4.Rotate());
            Document document = new Document(pdf);

            FontProgram fontProgram = FontProgramFactory.CreateFont();
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, "Cp1250");
            document.SetFont(font);

            Paragraph header = new Paragraph("Lista pojazdów")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(header);

            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            document.Add(new Paragraph("\n"));

            Table table = new Table(11, false);



            Cell lp = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("L.p."));
            Cell brandAndModel = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Marka"));
            Cell year = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Rocznik"));
            Cell user = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Użytkownik"));
            Cell vat = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("VAT"));
            Cell registrationNumber = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Nr rejestracyjny"));
            Cell metetStatus = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Stan licznika"));
            Cell insurance = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Ubezpieczenie"));
            Cell technicalExamination = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Badanie techniczne"));
            Cell serviceExpirationDate = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Termin serwisu"));
            Cell nextServiceMeterStatus = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Nast. serwis"));

            table.AddCell(lp);
            table.AddCell(brandAndModel);
            table.AddCell(year);
            table.AddCell(user);
            table.AddCell(vat);
            table.AddCell(registrationNumber);
            table.AddCell(metetStatus);
            table.AddCell(insurance);
            table.AddCell(technicalExamination);
            table.AddCell(serviceExpirationDate);
            table.AddCell(nextServiceMeterStatus);

            foreach (var car in cars)
            {
                var userName = car.UserName == null ? "" : car.UserName;
                table.AddCell($"{idx++}.");
                table.AddCell($"{car.Brand} {car.Model}");
                table.AddCell($"{car.Year}");
                table.AddCell(userName);
                table.AddCell(car.VAT);
                table.AddCell(car.RegistrationNumber);
                table.AddCell($"{car.MeterStatus} km.");
                table.AddCell(car.carInsuranceExpirationDate.ToString("dd-MM-yyyy"));
                table.AddCell(car.TechnicalExaminationExpirationDate.ToString("dd-MM-yyyy"));
                table.AddCell(car.ServiceExpirationDate.ToString("dd-MM-yyyy"));
                table.AddCell($"{car.NextServiceMeterStatus} km.");
            }
            document.Add(table);
            document.Close();

            byte[] byteinfo = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(byteinfo, 0, byteinfo.Length);
            stream.Position = 0;

            return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf, "Ubezpieczenia i przeglądy samochodów.pdf");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCar(int id)
        {
            var car = await _unitOfWork.CarRepository.GetCarById(id);

            if(car == null)
            {
                return NotFound("Brak pojazdu o wybranym id");
            }
            _unitOfWork.CarRepository.Delete(car);

            if (await _unitOfWork.Complete())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Nie udało się usunąć pojazdu");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarForListDto>> UpdateCar([FromBody] CarDto carDto, int id)
        {
            var login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.Users.Where(x => x.UserName == login).FirstOrDefaultAsync();

            var car = await _unitOfWork.CarRepository.GetCarById(id);
            var appUser = await _userManager.Users.Where(x => x.UserName == carDto.UserName).FirstOrDefaultAsync();
            if (car == null)
            {
                return BadRequest("Brak pojazdu o wybranym id");
            }

            await _unitOfWork.LogRepository.AddCarLogAsync(user.DisplayName, car, carDto);

            _mapper.Map(carDto, car);
            car.AppUser = appUser;
            _unitOfWork.CarRepository.Update(car);

            if (await _unitOfWork.Complete())
            {
                return Ok(carDto);
            }
            else
            {
                return BadRequest("Nie udało się zaktualizować pojazdu");
            }
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CarForListDto>>> GetCarsAsync()
        {
            var cars = await _unitOfWork.CarRepository.GetCarsAsync();

            var carsToReturn = _mapper.Map<IReadOnlyList<Car>, IReadOnlyList<CarForListDto>>(cars);

            return Ok(carsToReturn);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GerCarById(int id)
        {
            var car = await _unitOfWork.CarRepository.GetCarById(id);

            if (car == null) return BadRequest("Brak pojazdu o wybranym id");
            var carToReturn = new CarDto();
            _mapper.Map(car, carToReturn);

            return Ok(carToReturn);


        }
    }
}