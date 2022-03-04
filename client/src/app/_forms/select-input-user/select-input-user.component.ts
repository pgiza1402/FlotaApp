import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-select-input-user',
  templateUrl: './select-input-user.component.html',
  styleUrls: ['./select-input-user.component.scss']
})
export class SelectInputUserComponent implements ControlValueAccessor {
  @Input() users : string[]
  @Input() label : string;
  @Input() disabled: boolean = false


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
