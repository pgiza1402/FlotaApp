import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {
  displayedColumns: string[] = ['id','userName', 'displayName', 
                               'email', 'phoneNumber', 'roles',' '];

  users : User[]
  dataSource : MatTableDataSource<User>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers()
  }

  getUsers(){
    return this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users;
      this.dataSource = new MatTableDataSource<User>(this.users);
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
