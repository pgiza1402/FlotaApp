import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarForList } from 'src/app/_models/carForList';
import { Field } from 'src/app/_models/field';
import { Tires } from 'src/app/_models/tires';
import { CarService } from 'src/app/_services/car.service';
import { FieldService } from 'src/app/_services/field.service';
import { TiresService } from 'src/app/_services/tires.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-tires-edit',
  templateUrl: './tires-edit.component.html',
  styleUrls: ['./tires-edit.component.scss']
})
export class TiresEditComponent implements OnInit {
  editTiresForm: FormGroup;
  tires: Tires;
  types = ["Letnie","Zimowe", "Wielosezonowe"]
  fields: Field[];
  quantity: string[] = []
  storagePlace: string[] = []
  cars: CarForList[]

  constructor(private fb: FormBuilder, private tiresService: TiresService, private toastr: ToastrService, private bcService: BreadcrumbService, private carService: CarService, private route: ActivatedRoute,
    private fieldService: FieldService) {
    this.bcService.set('@model', ' ');
   }

  ngOnInit(): void {
    this.initializeForm()
    this.getCars()
    this.getFields()
    this.loadTires()
  }

  initializeForm(){
    this.editTiresForm = this.fb.group({
      model: [null, Validators.required],
      quantity: [null, [Validators.required,Validators.pattern('^[0-9]+$')]],
      type: [null, Validators.required],
      storagePlace: [null],
      carId: [null, Validators.required]

    })
  }

  editTires(tires: Tires){
    this.editTiresForm.patchValue({
      model: tires.model,
      quantity: tires.quantity.toString(),
      type: tires.type,
      storagePlace: tires.storagePlace,
      carId: tires.carId

    })
  }

  updateTires(){
    return this.tiresService.updateTires(this.editTiresForm.value, +this.route.snapshot.paramMap.get('id')).subscribe(() => {
      this.toastr.success("Opony zostały zaktualizowane");
      this.editTiresForm.reset(this.editTiresForm.value)

    })
  }

  loadTires(){
    this.tiresService.getTiresById(+this.route.snapshot.paramMap.get('id')).subscribe((tires: Tires) => {
      this.editTires(tires);
      this.tires = tires;
      this.bcService.set('@model', `Edycja opon ${tires.model}`)
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

