import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss']
})
export class DateInputComponent implements ControlValueAccessor {
  @Input() label :string;
  @Input() disabled: boolean = false
  date = new Date(Date.now());
  minDate = new Date(this.date.getFullYear(), this.date.getMonth(), this.date.getDate());


  constructor(@Self() @Optional() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
 
  }
  registerOnChange(fn: any): void {
  
  }
  registerOnTouched(fn: any): void {
   
  }
}

