import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Car } from 'src/app/_models/car';
import { CarForList } from 'src/app/_models/carForList';
import { Field } from 'src/app/_models/field';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CarService } from 'src/app/_services/car.service';
import { FieldService } from 'src/app/_services/field.service';
import { UserService } from 'src/app/_services/user.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrls: ['./car-edit.component.scss']
})
export class CarEditComponent implements OnInit {
  editCarForm: FormGroup;
  car: Car;
  users: User[];
  user: User;
  vat: string[] = ["50%", "100%"]
  fields: Field[];
  package: string[] = []
  insurers: string[] = []
  disabled: boolean;
  insuranceAndServicePage: boolean = false;
  currentDate : Date

  constructor(private fb: FormBuilder, private carService: CarService, private userService: UserService, private toastr: ToastrService,private route: ActivatedRoute, private bcService: BreadcrumbService, public accountService: AccountService,
    private fieldService: FieldService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user
    })
    this.bcService.set('@carName', ' ');
   }

  ngOnInit(): void {
    this.disabledField()
    this.getFields()
    this.getUsers()
    this.initializeForm()
    this.currentDate = new Date(Date.now())
    this.loadCar()
  }


  initializeForm(){
    this.editCarForm = this.fb.group({
      brand:[null,Validators.required],
      model: [null, Validators.required],
      year : [null, [Validators.required,Validators.pattern('^[0-9]*$'), this.correctYear()]],
      userName:[null],
      registrationNumber: [null, Validators.required],
      meterStatus:[null,[Validators.required, Validators.pattern('^[0-9]*$')]],
      vat: [null, Validators.required],
      technicalExaminationExpirationDate:[null,Validators.required],
      isArchival:[false],
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

  loadCar(){
    this.carService.getCarById(+this.route.snapshot.paramMap.get('id')).subscribe((car : Car) => {
      this.editCar(car);
      this.car = car;
      this.bcService.set('@carName', `Edycja pojazdu ${car.brand} ${car.model}` )
    })

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

  disabledField():boolean{

    if(this.user?.roles.some(r => r.includes("Operator")))
    {
      this.disabled = true;
    } 
    else{
      this.disabled = false
    } 
    return this.disabled
  }

  updateCar(){
    this.carService.updateCar(this.editCarForm.value ,+this.route.snapshot.paramMap.get('id')).subscribe(() => {
      this.toastr.success("Pojazd został zaktualizowany");
      this.editCarForm.reset(this.editCarForm.value);
      this.insuranceAndServicePage = false
    })
  }

  editCar(car: Car){
    this.editCarForm.patchValue({
    brand: car.brand,
    model: car.model,
    year: car.year,
    userName: car.userName,
    registrationNumber: car.registrationNumber,
    meterStatus: car.meterStatus,
    vat: car.vat,
    technicalExaminationExpirationDate: car.technicalExaminationExpirationDate,
    isArchival: car.isArchival,
    insurance: ({
      insurer: car.insurance.insurer,
      expirationDate: car.insurance.expirationDate,
      package: car.insurance.package
    }),
    service :({
      serviceExpirationDate: car.service.serviceExpirationDate,
      nextServiceMeterStatus: car.service.nextServiceMeterStatus
    })
    
    })
  }

  getUsers(){
    this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users.filter(x => x.userName != "dbadmin" && x.userName != "dbuser");
    })
  }

  togglePage(){
    this.insuranceAndServicePage = !this.insuranceAndServicePage;

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

}
