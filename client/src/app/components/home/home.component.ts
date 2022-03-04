import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(public accountService: AccountService, private  route : Router) { }

  ngOnInit(): void {
  }

  redirectToLoginPage(){
    this.route.navigateByUrl('/login')
  }

}
