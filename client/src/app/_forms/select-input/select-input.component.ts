import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-select-input',
  templateUrl: './select-input.component.html',
  styleUrls: ['./select-input.component.scss']
})
export class SelectInputComponent implements ControlValueAccessor {
  @Input() options : string[]
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


