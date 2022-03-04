import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  @Output() cancelLogin = new EventEmitter();
  model: any = {};
  user: User;
  loginForm: FormGroup

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.fb.group({
      userName: [null, Validators.required],
      password: ["", [Validators.required]]
    })
  }

  login() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
      if (this.user?.roles.some(r => r.includes('Administrator'))) {
        this.router.navigateByUrl('/cars');
      }else if(this.user?.roles.some(r => r.includes('Operator'))) {
        this.user.carId == null ? this.router.navigateByUrl('/') : this.router.navigateByUrl('/car/' + this.user.carId);
      }else{
        this.router.navigateByUrl('/');
      }
      this.cancel();
    })

  }
  cancel() {
    this.cancelLogin.emit(false);

  }

}
