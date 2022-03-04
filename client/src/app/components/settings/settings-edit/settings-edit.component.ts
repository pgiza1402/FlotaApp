import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Field } from 'src/app/_models/field';
import { FieldForList } from 'src/app/_models/fieldForList';
import { FieldService } from 'src/app/_services/field.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-settings-edit',
  templateUrl: './settings-edit.component.html',
  styleUrls: ['./settings-edit.component.scss']
})
export class SettingsEditComponent implements OnInit {
  editFieldForm: FormGroup
  fields: FieldForList[]
  field: Field
  displayedColumns: string[] = ['id','name','category', ' ']
  category: string[] = ['Ilość opon', 'Miejsce składowania', 'Ubezpieczyciel', 'Pakiet']
  dataSource : MatTableDataSource<FieldForList>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private fieldService: FieldService, private fb : FormBuilder, private toastr: ToastrService, private route: ActivatedRoute, private router: Router, private bcService: BreadcrumbService) {
    this.bcService.set('@name', ' ');
  }

  ngOnInit(): void {
    this.getFields();
    this.initializeForm()
    this.getField();
  }

  isDuplicate(): ValidatorFn {
    return (control: AbstractControl) => {
      for (let i = 0; i < this.fields?.length; i++) {

        if(control?.value.toLowerCase() === this.fields[i].name.toLowerCase() ){

          return { isDuplicate: true }
        } 
      }
      return null;
      };
    }

    onlyNumbersWhenTires(matchTo: string): ValidatorFn {
      return (control: AbstractControl) => {
          var reg = new RegExp('^[0-9]$');
          if(control?.parent?.controls[matchTo].value === 'Ilość opon'){

            return reg.test(control?.value) ? null : {onlyNumbersWhenTires : true}
          }

          return null;
        };
      }
  



  updateField(){
    this.fieldService.updateField(this.editFieldForm.value, +this.route.snapshot.paramMap.get('id')).subscribe(() => {
      this.toastr.success("Kategoria została zaktualizowana");
      this.editFieldForm.reset(this.editFieldForm.value);
      this.getFields();
      
    })
  }


  initializeForm(){
    this.editFieldForm = this.fb.group({
      name: [null, [Validators.required, this.isDuplicate(), this.onlyNumbersWhenTires("category")]],
      category: [null, Validators.required],
    })
    this.editFieldForm.controls.category.valueChanges.subscribe(() => 
    this.editFieldForm.controls.name.updateValueAndValidity());
  }

  editField(field : Field){
    this.editFieldForm.patchValue({
      name: field.name,
      category: field.category
    })

  }

getField(){
  return this.fieldService.getFieldById(+this.route.snapshot.paramMap.get("id")).subscribe((field: Field) => {
    this.field = field
    this.editField(field);
    this.bcService.set('@name', `Edycja kategorii ${field.name}` )
  })
}

  getFieldById(id: number){
    return this.fieldService.getFieldById(id).subscribe((field: Field) => {
      this.field = field
      this.editField(field);
      this.bcService.set('@name', `Edycja kategorii ${field.name}` )
    })

  }



  getFields(){
    return this.fieldService.getFields().subscribe((fields: FieldForList[]) => {
      this.fields = fields;
      this.dataSource = new MatTableDataSource<FieldForList>(this.fields);
      this.dataSource.sort = this.sort
      this.dataSource.paginator = this.paginator;
      this.customizePaginator(this.dataSource.paginator);
    }, error => {
      console.log(error)
    })
  }

  customizePaginator(paginator : MatPaginator){
    paginator._intl.firstPageLabel = "Pierwsza strona"
    paginator._intl.itemsPerPageLabel = "Ilość elementów na stronie"
    paginator._intl.nextPageLabel ="Następna strona"
    paginator._intl.previousPageLabel="Poprzednia strona"
    paginator._intl.lastPageLabel ="Ostatnia strona"
    }


  doFilter(value: string){
    this.dataSource.filter = value.trim().toLocaleLowerCase(); 
  }



}
