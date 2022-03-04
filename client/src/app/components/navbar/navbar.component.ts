import { Component, Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
@Injectable({
  providedIn: 'root'
})
export class NavbarComponent implements OnInit {
  carsMenu: boolean = false;
  tiresMenu: boolean = false;
  usersMenu: boolean = false;
  
  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

  hideCars(){
    {
      this.carsMenu = !this.carsMenu
    }
  }

  hideTires(){
    {
      this.tiresMenu = !this.tiresMenu
    }
  }
  hideUsers(){
    {
      this.usersMenu = !this.usersMenu
    }
  }



}
