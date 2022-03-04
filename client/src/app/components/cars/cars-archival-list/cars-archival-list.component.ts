import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CarForList } from 'src/app/_models/carForList';
import { CarService } from 'src/app/_services/car.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-cars-archival-list',
  templateUrl: './cars-archival-list.component.html',
  styleUrls: ['./cars-archival-list.component.scss']
})
export class CarsArchivalListComponent implements OnInit {
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
    this.cars = cars.filter(x => x.isArchival == true);
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


    doFilter(value: string){
    this.dataSource.filter = value.trim().toLocaleLowerCase(); 
    }

    }







