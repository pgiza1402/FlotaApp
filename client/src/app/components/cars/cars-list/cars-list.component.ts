import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CarService } from 'src/app/_services/car.service';
import {CarForList} from 'src/app/_models/carForList';
import { MatSort } from '@angular/material/sort';
import { saveAs } from 'file-saver';


@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.scss'],
})
export class CarsListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'brand', 'year' ,'userName', 'registrationNumber', 
                               'meterStatus', 'vat', 'carInsuranceExpirationDate', 
                               'technicalExaminationExpirationDate', 'serviceExpirationDate',
                                'nextServiceMeterStatus', ' '];

  cars : CarForList[]
  dataSource : MatTableDataSource<CarForList>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  

  constructor(private carService: CarService ) { }
 

  ngOnInit(): void {
    this.loadCars()

  }


loadCars(){
  this.carService.getCars().subscribe((cars: CarForList[]) => {
    this.cars = cars.filter(x => x.isArchival == false);
    this.dataSource = new MatTableDataSource<CarForList>(this.cars);
    this.dataSource.sort = this.sort
    this.dataSource.paginator = this.paginator;
    this.customizePaginator(this.dataSource.paginator);
  }, error => {
    console.log(error)
  })
} 

getExcel() {
  this.carService.getExcel(this.cars).subscribe(result => {
  
    saveAs(result, "Ubezpieczenia i przeglądy samochodów.xlsx")
  });
}
getPdf() {
  this.carService.getPdf(this.cars).subscribe(result => {
  
    saveAs(result, "Ubezpieczenia i przeglądy samochodów.pdf")
  });
}

customizePaginator(paginator : MatPaginator)
{
  paginator._intl.firstPageLabel = "Pierwsza strona"
  paginator._intl.itemsPerPageLabel = "Ilość elementów na stronie"
  paginator._intl.nextPageLabel ="Następna strona"
  paginator._intl.previousPageLabel="Poprzednia strona"
  paginator._intl.lastPageLabel ="Ostatnia strona"
 
}

meterStatusError(meterStatus: number, nextServiceMeterStatus: number):boolean{

  if(nextServiceMeterStatus - meterStatus <= 300) return true;
}
meterStatusWarning(meterStatus: number, nextServiceMeterStatus: number):boolean{

  if(nextServiceMeterStatus - meterStatus <= 1500 && nextServiceMeterStatus - meterStatus > 300) return true;
}

 
 statusError(date: Date):boolean {
  var todayDate = new Date();
  var newDate = new Date(date.toString().slice(0,10))
  
    if(newDate < todayDate) return true;
 }

 statusWarning(date: Date):boolean {
  var todayDate = new Date();
  var newDate = new Date(date.toString().slice(0,10))
  newDate.setDate(newDate.getDate() - 6)
 

  if(newDate < todayDate) return true
 }

 statusInformation(date: Date):boolean {
  var todayDate = new Date();
  var newDate = new Date(date.toString().slice(0,10))
  newDate.setDate(newDate.getDate() - 13)

  if(newDate < todayDate) return true
 }

 calculateDays(date: Date): number{
  var todayDate = new Date();
  var newDate = new Date(date.toString().slice(0,10))
  const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
  if(newDate.valueOf() - todayDate.valueOf() < 0) return 0
  else
  return Math.round(Math.abs((newDate.valueOf() - todayDate.valueOf()) / oneDay) + 1);
 }

 doFilter(value: string){
  this.dataSource.filter = value.trim().toLocaleLowerCase(); 
}

calculateKm(meterStatus: number, nextServiceMeterStatus: number):number {
  var result = nextServiceMeterStatus - meterStatus
  if(result < 0) return 0
  else
  return result

}

 

 }







