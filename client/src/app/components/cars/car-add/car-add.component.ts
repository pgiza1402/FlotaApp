import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { CarForList } from 'src/app/_models/carForList';
import { Field } from 'src/app/_models/field';
import { User } from 'src/app/_models/user';
import { CarService } from 'src/app/_services/car.service';
import { FieldService } from 'src/app/_services/field.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  styleUrls: ['./car-add.component.scss']
})
export class CarAddComponent implements OnInit {
  addCarForm: FormGroup
  car: CarForList;
  users: User[];
  vat: string[] = ["50%", "100%"]
  fields: Field[];
  package: string[] = []
  insurers: string[] = []
  insuranceAndServicePage: boolean = false;
  currentDate : Date
  

  constructor(private fb: FormBuilder, private carService: CarService, private userService: UserService, private toastr: ToastrService, private fieldService: FieldService) { }

  ngOnInit(): void {
    this.getUsers();
    this.getFields();
    this.initializeForm();
    this.currentDate = new Date(Date.now())
  }

  initializeForm(){
    this.addCarForm = this.fb.group({
      brand:[null,Validators.required],
      model: [null, Validators.required],
      userName:[null],
      registrationNumber: [null, [Validators.required, this.onlyNumbersAndLetters()]],
      year: [null, [Validators.required,Validators.pattern('^[0-9]*$'), this.correctYear()]],
      meterStatus:[null,[Validators.required, Validators.pattern('^[0-9]*$')]],
      vat: [null, Validators.required],
      technicalExaminationExpirationDate:[null,Validators.required],
      insurance: this.fb.group({
        insurer: [null, Validators.required],
        expirationDate: [null, Validators.required],
        package: [null, Validators.required],
      }),
      service : this.fb.group({
        serviceExpirationDate: [null, Validators.required],
        nextServiceMeterStatus: [null, [Validators.required, Validators.pattern('^[0-9]*$')]],
       })

    })
  }

  onlyNumbersAndLetters(): ValidatorFn{
    return(control: AbstractControl) => {
      return /^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$/.test(control?.value)  ? null: {isContains : true} ;
    }
  }

  correctYear(): ValidatorFn{
    return(control: AbstractControl) => {  
      var reg = new RegExp('^[0-9]*$')
      if(reg.test(control?.value))
      {   
        return control?.value > 2007  && control?.value <= this.currentDate.getFullYear()  ? null : {correctYear : true} 
      }

      }
      }
  
       
      

  getUsers(){
    this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users.filter(x => x.userName != "dbadmin" && x.userName != "dbuser");
    })
  }

  getFields(){
    this.fieldService.getFields().subscribe((fields: Field[]) => {
      this.fields = fields
      if(fields) {
        this.fields.forEach(element => {
          if(element.category == "Ubezpieczyciel"){

            this.insurers.push(element.name)

          } else if(element.category == "Pakiet"){

          this.package.push(element.name)

          }
        })
      }
    }
    )
  }

  addCar(){
    this.carService.addCar(this.addCarForm.value).subscribe(() => {
      this.toastr.success("Pojazd został dodany do bazy");
      this.addCarForm.reset();
      this.insuranceAndServicePage = false
    })
  }


  togglePage(){
    this.insuranceAndServicePage = !this.insuranceAndServicePage;

  }

}
