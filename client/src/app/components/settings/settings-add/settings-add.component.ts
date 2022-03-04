import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from 'src/app/_dialog/confirm-dialog/confirm-dialog.component';
import { Field } from 'src/app/_models/field';
import { FieldService } from 'src/app/_services/field.service';

@Component({
  selector: 'app-settings-add',
  templateUrl: './settings-add.component.html',
  styleUrls: ['./settings-add.component.scss']
})
export class SettingsAddComponent implements OnInit {
  addFieldForm: FormGroup
  fields: Field[]
  displayedColumns: string[] = ['id','name','category', ' ']
  category: string[] = ['Ilość opon', 'Miejsce składowania', 'Ubezpieczyciel', 'Pakiet']
  dataSource : MatTableDataSource<Field>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
 

  constructor(private fieldService: FieldService, private fb : FormBuilder, private toastr: ToastrService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getFields();
    this.initializeForm()
  }


  addField(){
    this.fieldService.addField(this.addFieldForm.value).subscribe(() => {
      this.toastr.success("Pole zostało dodane do bazy");
      this.getFields();
      this.addFieldForm.reset();
    })

  }

  deleteField(id: number){
    this.fieldService.deleteField(id).subscribe(() => {
      this.toastr.success("Pole zostało usunięte")
      this.getFields()
    }, error => {
      console.log(error)
    })
  }

  openDialog(name : string, category: string, id:number){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data:{
        label : name,
        category: category
      }
    }); 

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.deleteField(id);
      }
    });
  }


  initializeForm(){
    this.addFieldForm = this.fb.group({
      name: [null, [Validators.required, this.isDuplicate(), this.onlyNumbersWhenTires("category")]],
      category: [null, Validators.required],
    })
    this.addFieldForm.controls.category.valueChanges.subscribe(() => 
    this.addFieldForm.controls.name.updateValueAndValidity());
  }

  getFields(){
    return this.fieldService.getFields().subscribe((fields: Field[]) => {
      this.fields = fields;
      this.dataSource = new MatTableDataSource<Field>(this.fields);
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

  
  isDuplicate(): ValidatorFn {
    return (control: AbstractControl) => {
      for (let i = 0; i < this.fields?.length; i++) {

        if(control?.value != null &&  control?.value != "")
        {
          if(control?.value.toLowerCase() === this.fields[i].name.toLowerCase() ){

          return { isDuplicate: true }
        } 
      }
    }
      return null;
      };
    }

    onlyNumbersWhenTires(matchTo: string): ValidatorFn {
      return (control: AbstractControl) => {
          var reg = new RegExp('^[0-9]+$');
          if(control?.parent?.controls[matchTo].value === 'Ilość opon'){

            return reg.test(control?.value) ? null : {onlyNumbersWhenTires : true}
          }

          return null;
        };
      }

}

