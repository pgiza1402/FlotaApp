import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { InsuranceForList } from 'src/app/_models/insuranceForList';



import { InsuranceService } from 'src/app/_services/insurance.service';

@Component({
  selector: 'app-insurances-list',
  templateUrl: './insurances-list.component.html',
  styleUrls: ['./insurances-list.component.scss']
})
export class InsurancesListComponent implements OnInit {
  insurances: InsuranceForList[]
  displayedColumns: string[] = ['id','carRegistrationNumber','car', 'insurer','expirationDate', 'package', ' ']
  dataSource : MatTableDataSource<InsuranceForList>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private insuranceService: InsuranceService) { }

  ngOnInit(): void {
    this.getInsurances();
  }

  getInsurances(){
    return this.insuranceService.getInsurances().subscribe((insuranceToList: InsuranceForList[]) => {
      this.insurances = insuranceToList
      this.dataSource = new MatTableDataSource<InsuranceForList>(this.insurances);
      this.dataSource.sort = this.sort
      this.dataSource.paginator = this.paginator;
      this.customizePaginator(this.dataSource.paginator);
    }, error => {
      console.log(error)
    })
  }

    customizePaginator(paginator : MatPaginator){
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







