import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  editUserForm: FormGroup
  user: User;
  roles = ["Nieaktywowany","Operator","Administrator"]

  constructor(private fb: FormBuilder, private userService: UserService, private toastr: ToastrService, private route: ActivatedRoute, private bcService: BreadcrumbService) {
    this.bcService.set('@userName', ' ');
   } 
   
 

  ngOnInit(): void {
    this.initializeForm();
    this.loadUser();
  }

  initializeForm(){
    this.editUserForm = this.fb.group({
      userName: [null, Validators.required],
      email: [null, [Validators.required, this.isValidEmail()]],
      phoneNumber: [null, [Validators.required,Validators.pattern('^[0-9]+$'), Validators.minLength(9)]],
      role: ["", Validators.required],
      password: ["", this.isContains()],
      confirmPassword: ["",[this.isContains(), this.matchValues('password')]],

    })
    this.editUserForm.controls.password.valueChanges.subscribe(() =>
    this.editUserForm.controls.confirmPassword.updateValueAndValidity());
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

  isValidEmail(): ValidatorFn{
    return(control: AbstractControl) => {
      var reg = new RegExp('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')
      return reg.test(control?.value) ? null : {isValidEmail: true};
    }
  }

  editUser(user: User){
    this.editUserForm.patchValue({
    userName: user.userName,
    email: user.email,
    phoneNumber: user.phoneNumber,
    role: user.roles,

    })
  }

  updateUser(){
    this.userService.updateUser(this.editUserForm.value,+this.route.snapshot.paramMap.get('id')).subscribe(() => {
      this.toastr.success("Użytkownik został zaktualizowany");
      this.editUserForm.reset(this.editUserForm.value);

  })
}

  loadUser(){
    this.userService.getUserById(+this.route.snapshot.paramMap.get('id')).subscribe((user : User) => {
      this.editUser(user);
      this.user = user;
      this.bcService.set('@userName', `Edycja użytkownika ${user.displayName}` )
    })

  }

  validateUserNameNotTaken(): AsyncValidatorFn{
    return control => {
      return timer(500).pipe(
        switchMap(() =>{
          if(!control.value){
            return of(null);
          }
          if(control.value != this.user.userName)
          {
          return this.userService.checkUsernameExists(control.value).pipe(
            map(res => {
              return res ? {userNameExists: true} : null;

            })
          );
          }
        })
      );
    };
  }
}
