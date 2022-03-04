import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {CarForList } from 'src/app/_models/carForList';
import { Field } from 'src/app/_models/field';
import { CarService } from 'src/app/_services/car.service';
import { FieldService } from 'src/app/_services/field.service';
import { TiresService } from 'src/app/_services/tires.service';

@Component({
  selector: 'app-tires-add',
  templateUrl: './tires-add.component.html',
  styleUrls: ['./tires-add.component.scss']
})
export class TiresAddComponent implements OnInit {
  addTiresForm: FormGroup
  types = ["Letnie","Zimowe", "Wielosezonowe"]
  fields: Field[];
  quantity: string[] = []
  storagePlace: string[] = []
  cars: CarForList[]

  constructor(private fb: FormBuilder, private tiresService: TiresService, private toastr: ToastrService, private carService: CarService, private fieldService: FieldService) { }

  ngOnInit(): void {
    this.getFields()
    this.getCars()
    this.initializeForm();
  }


initializeForm(){
  this.addTiresForm = this.fb.group({
    model: [null, Validators.required],
    quantity: [null, [Validators.required, Validators.pattern('^[0-9]+$')]],
    type: [null, Validators.required],
    storagePlace: [null],
    carId: [null, Validators.required]
  })
}

addTires(){
  this.tiresService.addTires(this.addTiresForm.value).subscribe(() => {
    this.toastr.success("Opony zostały dodane do bazy");
    this.addTiresForm.reset();
  })
}

getCars(){
  this.carService.getCars().subscribe((cars: CarForList[]) =>{
    this.cars = cars.filter(x => x.isArchival == false);

  })   

  }

  getFields(){
    this.fieldService.getFields().subscribe((fields: Field[]) => {
      this.fields = fields
      if(fields) {
        this.fields.forEach(element => {
          if(element.category == "Miejsce składowania"){  

            this.storagePlace.push(element.name)

          } else if(element.category == "Ilość opon"){

          this.quantity.push(element.name)

          }
        })
      }
    }
    )
}
}



