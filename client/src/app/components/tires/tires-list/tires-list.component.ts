import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { TiresForList } from 'src/app/_models/tiresForList';
import { TiresService } from 'src/app/_services/tires.service';

@Component({
  selector: 'app-tires-list',
  templateUrl: './tires-list.component.html',
  styleUrls: ['./tires-list.component.scss']
})
export class TiresListComponent implements OnInit {
  tires: TiresForList[];
  displayedColumns: string[] = ['id','carRegistrationNumber','car', 'model','type', 'quantity', 
  'storagePlace',' '];

  dataSource : MatTableDataSource<TiresForList>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private tiresService: TiresService ) { }

  ngOnInit(): void {
    this.getTires()
  }

  getTires(){
    return this.tiresService.getTires().subscribe((tires: TiresForList[]) => {
      this.tires = tires;
      this.dataSource = new MatTableDataSource<TiresForList>(this.tires);
      this.dataSource.sort = this.sort
      this.dataSource.paginator = this.paginator;
      this.customizePaginator(this.dataSource.paginator);
    }, error => {
      console.log(error)
    })
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
