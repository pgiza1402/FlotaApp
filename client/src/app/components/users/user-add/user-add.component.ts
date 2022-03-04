import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.scss']
})
export class UserAddComponent implements OnInit {
  addUserForm: FormGroup
  user: User;
  roles = ["Nieaktywowany","Operator","Administrator"]
  

  constructor(private fb: FormBuilder, private userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm()
  }

  initializeForm(){
    this.addUserForm = this.fb.group({
      userName: [null, Validators.required, [this.validateUserNameNotTaken()]],
      email: [null, [Validators.required, this.isValidEmail()]],
      phoneNumber: [null, [Validators.required,Validators.pattern('^[0-9]+$'), Validators.minLength(9)]],
      role: ["Nieaktywowany", Validators.required],
      password: [null,[Validators.minLength(8),this.isContains()]],
      confirmPassword: [null, this.matchValues('password')],
      //const control = new FormControl('1', Validators.pattern('[a-zA-Z ]*')
    })
    this.addUserForm.controls.password.valueChanges.subscribe(() =>
    this.addUserForm.controls.confirmPassword.updateValueAndValidity());
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
      ? null : {isMatching: true}
    }
  }

  isContains(): ValidatorFn{
    return(control: AbstractControl) => {
      if(control?.value != null && control?.value != "")
      {
      return /^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9!_@./#&+-\\$\\]{2,16}$/.test(control?.value)  ? null: {isContains : true} ;
      }else{
        return null;
      }
    }
  }

  addUser(){
    this.userService.addUser(this.addUserForm.value).subscribe(() => {
      this.toastr.success("Użytkownik został dodany do bazy");
      this.addUserForm.reset();
    })

  }

  isValidEmail(): ValidatorFn{
    return(control: AbstractControl) => {

      var reg = new RegExp('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')
      return reg.test(control?.value) ? null : {isValidEmail: true};
    }
  }

  validateUserNameNotTaken(): AsyncValidatorFn{
    return control => {
      return timer(500).pipe(
        switchMap(() =>{
          if(!control.value){
            return of(null);
          }
          return this.userService.checkUsernameExists(control.value).pipe(
            map(res => {
              return res ? {userNameExists: true} : null;

            })
          );
        })
      );
    };
  }
}
