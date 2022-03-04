import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../_dialog/confirm-dialog/confirm-dialog.component';




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-center-center',
      preventDuplicates: true
    }),
  ],
  exports: [
    ToastrModule,
  ]
 
})
export class SharedModule { }
