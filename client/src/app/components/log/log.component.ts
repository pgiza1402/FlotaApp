import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Log } from 'src/app/_models/log';
import { LogService } from 'src/app/_services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.scss']
})
export class LogComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date','user', 'position', 
  'field', 'oldValue', 'newValue'];

logs : Log[]
dataSource : MatTableDataSource<Log>;
@ViewChild(MatPaginator) paginator: MatPaginator;
@ViewChild(MatSort) sort: MatSort;

constructor(private logService: LogService ) { }

ngOnInit(): void {
this.loadLogs()

}

loadLogs(){
this.logService.getLogs().subscribe((logs: Log[]) => {
this.logs = logs
this.dataSource = new MatTableDataSource<Log>(this.logs);
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
