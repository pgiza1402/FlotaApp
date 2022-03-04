import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Insurance } from 'src/app/_models/insurance';
import { InsuranceForList } from 'src/app/_models/insuranceForList';
import { InsuranceService } from 'src/app/_services/insurance.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-insurance-edit',
  templateUrl: './insurance-edit.component.html',
  styleUrls: ['./insurance-edit.component.scss']
})
export class InsuranceEditComponent implements OnInit {
  editInsuranceForm: FormGroup;
  insurance: Insurance;
  insurers = ["PZU","UNIQUA", "Link4"]
  package = ["ASS+NNW","ASS (bez limitu)+NNW+SZYBA", "ASS+NNW+GAP"]
  //cars: CarForList[]

  constructor(private insuranceService: InsuranceService, private fb: FormBuilder, private toastr: ToastrService, private bcService: BreadcrumbService,  private route: ActivatedRoute) {
    this.bcService.set('@name', ' ');
   }

  ngOnInit(): void {
    this.initializeForm()
    this.loadInsurance()
  }

  initializeForm(){
    this.editInsuranceForm = this.fb.group({
      insurer: [null, Validators.required],
      expirationDate: [null, Validators.required],
      package: [null, Validators.required],
      car: [null],
      carRegistrationNumber : [null]
    })

  }

  editInsurance(insurance: InsuranceForList){
    this.editInsuranceForm.patchValue({
      insurer: insurance.insurer,
      expirationDate : insurance.expirationDate,
      package: insurance.package,
      car: insurance.car,
      carRegistrationNumber: insurance.carRegistrationNumber
    })
  }

  loadInsurance(){
    this.insuranceService.getInsuranceById(+this.route.snapshot.paramMap.get('id')).subscribe((insurance: InsuranceForList) =>{
      this.editInsurance(insurance)
      this.insurance = insurance
      this.bcService.set('@name', `Edycja polisy dla pojazdu ${insurance.car} - ${insurance.carRegistrationNumber}`)
    })
  }

  updateInsurance(){
    this.insuranceService.updateInsurance(this.editInsuranceForm.value, +this.route.snapshot.paramMap.get("id")).subscribe(() => {
      this.toastr.success("Polisa została zaktualizowana")
      this.editInsuranceForm.reset(this.editInsuranceForm.value)
    })
  }




}

// ngOnInit(): void {
//   this.initializeForm()
//   this.getCars()
//   this.loadTires()
// }

// initializeForm(){
//   this.editInsuranceForm = this.fb.group({
//     model: [null, Validators.required],
//     quantity: [null, [Validators.required,Validators.pattern('^[0-9]+$')]],
//     type: [null, Validators.required],
//     storagePlace: [null],
//     carId: [null, Validators.required]

//   })
// }

// editTires(tires: Tires){
//   this.editInsuranceForm.patchValue({
//     model: tires.model,
//     quantity: tires.quantity,
//     type: tires.type,
//     storagePlace: tires.storagePlace,
//     carId: tires.carId

//   })
// }

// updateTires(){
//   return this.tiresService.updateTires(this.editInsuranceForm.value, +this.route.snapshot.paramMap.get('id')).subscribe(() => {
//     this.toastr.success("Opony zostały zaktualizowane");
//     this.editInsuranceForm.reset(this.editInsuranceForm.value)

//   })
// }

// loadTires(){
//   this.tiresService.getTiresById(+this.route.snapshot.paramMap.get('id')).subscribe((tires: Tires) => {
//     this.editTires(tires);
//     this.tires = tires;
//     this.bcService.set('@model', `Edycja opon ${tires.model}`)
//   })
// }

// getCars(){
//   this.carService.getCars().subscribe((cars: CarForList[]) =>{
//     this.cars = cars.filter(x => x.isArchival == false);

//   })   

//   }

// }

